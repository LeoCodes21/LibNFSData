using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LibNFSData.Core.Utils;

namespace LibNFSData.Core
{
    /**
     * General options for a container.
     * This object is generally only accepted by a base file reader.
     */
    public class ContainerOptions
    {
        public long Start = -1;
        public long End = -1;
    }

    public class ContainerAction
    {
        public virtual void Run(ReaderContext context)
        {
            throw new Exception();
        }

        public virtual void Cleanup()
        {
            //
        }
    }

    /**
     * Containers are used to retrieve data in the form of models.
     */
    public abstract class Container<TModel>
    {
        protected Container(BinaryReader reader, long? containerSize)
        {
            if (reader.BaseStream.Length == 0)
            {
                throw new ArgumentException($"Empty file! ({nameof(reader)})");
            }

            Context = new ReaderContext(reader);
            PreRead = new Stack<ContainerAction>();

            if (containerSize != null)
            {
                ContainerSize = (long) containerSize;
            }
        }

        /**
         * Read data from the underlying reader and return a model.
         */
        public virtual TModel Get()
        {
            throw new NotImplementedException();
        }

        /**
         * Serialize a model to a file.
         */
        public virtual void Write(BinaryWriter writer, TModel model)
        {
            throw new NotImplementedException();
        }

        /**
         * Read data from the underlying data stream.
         */
        protected virtual void Read(long size)
        {
            // Run the action pipeline in case we have things to do
            RunPipeline();
            
            var runTo = Context.Reader.BaseStream.Position + size;

            while (Context.Reader.BaseStream.Position < runTo)
            {
                var id = Context.Reader.ReadUInt32();
                var chunkSize = Context.Reader.ReadUInt32();
                var chunkRunTo = Context.Reader.BaseStream.Position + chunkSize;

                var normalizedId = id & 0xffffffff;
                
                DebugUtil.PrintID(Context.Reader, id, normalizedId, chunkSize, GetType());
                
                HandleChunk(normalizedId, chunkSize);
                
                DebugUtil.ValidatePosition(Context.Reader, chunkRunTo, GetType());
                Context.Reader.BaseStream.Seek(chunkRunTo - Context.Reader.BaseStream.Position, SeekOrigin.Current);
            }

            RunPipelineCleanup();
        }

        private void RunPipeline()
        {
            if (!PreRead.Any())
                return;

            while (PreRead.Count > 0)
            {
                var action = PreRead.Pop();
                action.Run(Context);
                _executedPreActions.Push(action);
            }
        }

        private void RunPipelineCleanup()
        {
            if (!_executedPreActions.Any())
                return;

            while (_executedPreActions.Count > 0)
            {
                var action = _executedPreActions.Pop();

                action.Cleanup();
            }
        }

        protected ChunkID IDToEnum(uint id)
        {
            if (Enum.IsDefined(typeof(ChunkID), (long) id))
            {
                return (ChunkID) id;
            }

            return ChunkID.BCHUNK_UNKNOWN;
        }
        
        protected abstract void HandleChunk(uint id, uint size);

        protected readonly ReaderContext Context;
        protected long ContainerSize;

        protected readonly Stack<ContainerAction> PreRead;
        private readonly Stack<ContainerAction> _executedPreActions = new Stack<ContainerAction>();
    }
}
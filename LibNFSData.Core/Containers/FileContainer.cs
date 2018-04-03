using System;
using System.Collections.Generic;
using System.IO;
using LibNFSData.Core.Models;
using LibNFSData.Core.Utils;

namespace LibNFSData.Core.Containers
{
    public enum CompressionType
    {
        JDLZ,
        WorldBin,
        WorldContainer,
        Uncompressed
    }

    internal class DecompressFile : ContainerAction
    {
        private readonly Random _random = new Random();
        private string _fileName;
        private readonly CompressionType _type;

        public DecompressFile(CompressionType type)
        {
            _type = type;
        }

        public override void Run(ReaderContext context)
        {
            var fileName = _fileName = $"decompress_{_random.Next()}.bin";

            Console.WriteLine($"decompressing to {_fileName}");

            context.Reader.BaseStream.Position = 0;

            var fileData = new byte[context.Reader.BaseStream.Length];

            context.Reader.Read(fileData, 0, fileData.Length);

            byte[] decompressed;

            switch (_type)
            {
                case CompressionType.JDLZ:
                    decompressed = JDLZ.Decompress(fileData);
                    break;
                case CompressionType.WorldBin:
                    decompressed = fileData;
                    
                    for (var i = decompressed.Length - 1; i >= 1; --i)
                    {
                        decompressed[i] ^= decompressed[i - 1];
                    }
                    
                    break;
                default:
                    decompressed = new byte[0];
                    break;
            }

            using (var writer = new BinaryWriter(new FileStream(fileName, FileMode.Create)))
            {
                writer.Write(decompressed, 0, decompressed.Length);
            }

            context.Reader = new BinaryReader(File.OpenRead(fileName));
        }

        public override void Cleanup()
        {
            File.Delete(_fileName);
        }
    }

    /**
     * Reads chunks from a file.
     */
    public abstract class FileContainer : Container<List<Model>>
    {
        protected FileContainer(BinaryReader reader) : base(reader, 0)
        {
            ContainerSize = reader.BaseStream.Length;
        }

        public override List<Model> Get()
        {
            Read(ContainerSize);

            return Models;
        }

        protected override void Read(long size)
        {
            Console.WriteLine("here");
            var compType = GetCompressionType();
            Console.WriteLine(compType);
            
            if (compType != CompressionType.Uncompressed)
            {
                PreRead.Push(new DecompressFile(compType));
            }

            base.Read(size);
        }

        protected override void HandleChunk(uint id, uint size)
        {
            Models.Add(new NullModel(IDToEnum(id), size, Context.Reader.BaseStream.Position));
        }

        private CompressionType GetCompressionType()
        {
            var position = Context.Reader.BaseStream.Position;
            CompressionType type;

            // Check for compression
            {
                var flag = Context.Reader.ReadBytes(4);

                if (flag[0] == 'J' && flag[1] == 'D' && flag[2] == 'L' && flag[3] == 'Z')
                {
                    DebugUtil.EnsureCondition(Context.Reader.ReadInt16() == 0x1002, () => "Invalid JDLZ header!");
                    type = CompressionType.JDLZ;
                }
                else
                {
                    type = FindCompressionType();
                }
            }

            Context.Reader.BaseStream.Position = position;

            return type;
        }

        protected abstract CompressionType FindCompressionType();

        protected readonly List<Model> Models = new List<Model>();
    }
}
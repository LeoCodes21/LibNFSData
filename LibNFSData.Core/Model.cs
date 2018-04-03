using LibNFSData.Core.Utils;

namespace LibNFSData.Core
{
    public abstract class Model
    {
        public readonly ChunkID ID;

        public readonly long Size;

        public readonly long Position;

        protected Model(ChunkID id, long size, long position)
        {
            ID = id;
            Size = size;
            Position = position;
        }
    }
}
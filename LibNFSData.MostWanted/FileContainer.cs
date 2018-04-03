using System.IO;
using LibNFSData.Core.Containers;
using LibNFSData.Core.Utils;
using BaseContainer = LibNFSData.Core.Containers.FileContainer;
using LanguageContainer = LibNFSData.MostWanted.Containers.LanguageContainer;
using TexturePackContainer = LibNFSData.MostWanted.Containers.TexturePackContainer;

namespace LibNFSData.MostWanted
{
    public class FileContainer : BaseContainer
    {
        public FileContainer(BinaryReader reader) : base(reader)
        {
        }

        protected override void HandleChunk(uint id, uint size)
        {
            base.HandleChunk(id, size);

            if (id == (uint) ChunkID.BCHUNK_SPEED_TEXTURE_PACK_LIST_CHUNKS)
            {
                Models.Add(new TexturePackContainer(Context.Reader, size, false).Get());
            } else if (id == (uint) ChunkID.BCHUNK_LANGUAGE)
            {
                Models.Add(new LanguageContainer(Context.Reader, size).Get());
            }
        }

        protected override CompressionType FindCompressionType()
        {
            return CompressionType.Uncompressed;
        }
    }
}
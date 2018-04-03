using System.Collections.Generic;
using LibNFSData.Core.Utils;

namespace LibNFSData.Core.Models
{
    public class Texture
    {
        public int TextureHash { get; set; }

        public int TypeHash { get; set; }

        public uint DataOffset { get; set; }

        public uint DataSize { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int MipMap { get; set; }

        public string Name { get; set; }

        public uint CompressionType { get; set; }

        public byte[] Data { get; set; }
    }

    /**
     * A texture pack contains data about textures that
     * are used within the game. The majority are found
     * within track stream files, because of the nature of the game.
     */
    public class TexturePack : Model
    {
        public TexturePack(ChunkID id, long size, long position) : base(id, size, position)
        {
            DebugUtil.EnsureCondition(
                id == ChunkID.BCHUNK_SPEED_TEXTURE_PACK_LIST_CHUNKS,
                () => $"Expected BCHUNK_SPEED_TEXTURE_PACK_LIST_CHUNKS, got {id.ToString()}");
        }

        public string Name { get; set; }

        public string Path { get; set; }

        public int Hash { get; set; }

        public List<Texture> Textures { get; } = new List<Texture>();

        public List<uint> Hashes { get; } = new List<uint>();
    }
}
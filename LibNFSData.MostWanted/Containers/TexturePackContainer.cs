using System;
using System.IO;
using System.Runtime.InteropServices;
using LibNFSData.Core.Models;
using LibNFSData.Core.Utils;
using BaseContainer = LibNFSData.Core.Containers.TexturePackContainer;

namespace LibNFSData.MostWanted.Containers
{
    public class TexturePackContainer : BaseContainer
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct GamePixelFormat
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            private readonly byte[] pad;
            
            public readonly uint FourCC;

            public readonly uint Unknown1;
            
            public readonly uint Unknown2;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct TpkTextureHeader
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xC)]
            private readonly byte[] zero;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
            public readonly string Name;

            public readonly int TextureHash;

            public readonly int TypeHash;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            private readonly byte[] blankOne;

            public readonly uint DataOffset;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            private readonly byte[] blankTwo;

            public readonly uint DataSize;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            private readonly byte[] blankThree;

            public readonly short Width;

            public readonly short Height;

            private readonly short MipMapLow;

            public readonly short MipMap;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
            private readonly byte[] restOfData;
        }

        public TexturePackContainer(BinaryReader reader, long? containerSize, bool compressed) : base(reader,
            containerSize, compressed)
        {
        }

        public override void Write(BinaryWriter writer, TexturePack model)
        {
            //
        }

        #region Interface Implementation

        protected override void ReadTPKInfo()
        {
            var header = BinaryUtil.ReadStruct<TpkInfoHeader>(Context.Reader);
            Pack.Name = header.Name;
            Pack.Path = header.Path;
            Pack.Hash = header.Hash;
        }

        protected override void ReadTPKTextures(long size)
        {
            var textures = BinaryUtil.ReadList<TpkTextureHeader>(Context.Reader, size);

            foreach (var textureHeader in textures)
            {
                var texture = new Texture
                {
                    TextureHash = textureHeader.TextureHash,
                    TypeHash = textureHeader.TypeHash,
                    Name = textureHeader.Name,
                    Width = textureHeader.Width,
                    Height = textureHeader.Height,
                    MipMap = textureHeader.MipMap,
                    DataOffset = textureHeader.DataOffset,
                    DataSize = textureHeader.DataSize
                };

                Pack.Textures.Add(texture);
            }
        }

        protected override void ReadDxtHeaders(long size)
        {
            var pixelFormats = BinaryUtil.ReadList<GamePixelFormat>(Context.Reader, size);

            for (var i = 0; i < pixelFormats.Count; i++)
            {
                var format = pixelFormats[i];
                var texture = Pack.Textures[i];

                texture.CompressionType = format.FourCC;
            }
        }

        protected override void ReadTPKData()
        {
            Context.Reader.BaseStream.Seek(0x78, SeekOrigin.Current);

            var dataStart = Context.Reader.BaseStream.Position;
            
            foreach (var texture in Pack.Textures)
            {
                Context.Reader.BaseStream.Seek(dataStart + texture.DataOffset, SeekOrigin.Begin);
                texture.Data = new byte[texture.DataSize];

                Context.Reader.Read(texture.Data, 0, (int) texture.DataSize);

                if (texture.CompressionType == P8Compression)
                {
                    continue;
                }

                using (var writer = new BinaryWriter(File.OpenWrite($"{texture.Name}_0x{texture.TextureHash:x8}.dds")))
                {
                    OutputTexture(texture, BuildDDSHeader(texture), writer);
                }

                texture.Data = null;
            }
        }
        
        #endregion
    }
}
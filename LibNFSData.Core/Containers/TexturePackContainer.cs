using System;
using System.IO;
using System.Runtime.InteropServices;
using LibNFSData.Core.Models;
using LibNFSData.Core.Utils;

namespace LibNFSData.Core.Containers
{
    /**
     * Handles reading chunks of the type BCHUNK_SPEED_TEXTURE_PACK_LIST_CHUNKS.
     */
    public abstract class TexturePackContainer : Container<TexturePack>
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        protected struct TpkInfoHeader
        {
            private readonly uint Marker;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x1C)]
            public readonly string Name;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x40)]
            public readonly string Path;

            public readonly int Hash;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            private readonly byte[] empty;
        }
        
        protected const uint P8Compression = 0x00000029;

        private enum TPKChunks : long
        {
            TPKRoot = 0xb3310000,
            TPKInfo = 0x33310001,
            TPKTextureHashes = 0x33310002,
            TPKCompressedData = 0x33310003,
            TPKTextureHeaders = 0x33310004,
            TPKDXTHeaders = 0x33310005,
            TPKDataRoot = 0xb3320000,
            TPKData = 0x33320002
        }

        protected TexturePackContainer(BinaryReader reader, long? containerSize, bool compressed) : base(reader,
            containerSize)
        {
            if (ContainerSize == 0)
            {
                throw new ArgumentException("Empty TPK container!");
            }

            IsCompressed = compressed;
            Pack = new TexturePack(ChunkID.BCHUNK_SPEED_TEXTURE_PACK_LIST_CHUNKS, ContainerSize,
                Context.Reader.BaseStream.Position - 8);
        }

        public override TexturePack Get()
        {
            Read(ContainerSize);

            return Pack;
        }
        
        protected override void HandleChunk(uint id, uint size)
        {
            var normalizedId = (long) (id & 0xffffffff);

            switch (normalizedId)
            {
                case (long) TPKChunks.TPKRoot:
                case (long) TPKChunks.TPKDataRoot:
                {
                    // Read sub-chunks
                    Read(size);
                    break;
                }
                case (long) TPKChunks.TPKInfo:
                {
                    ReadTPKInfo();
                    break;
                }
                case (long) TPKChunks.TPKTextureHashes:
                {
                    ReadTPKHashes(size);
                    break;
                }
                case (long) TPKChunks.TPKTextureHeaders:
                {
                    ReadTPKTextures(size);
                    break;
                }
                case (long) TPKChunks.TPKCompressedData:
                {
                    ReadTPKTextures(size);
                    break;
                }
                case (long) TPKChunks.TPKDXTHeaders:
                {
                    ReadDxtHeaders(size);
                    break;
                }
                case (long) TPKChunks.TPKData:
                {
                    ReadTPKData();
                    break;
                }
                default:
                {
                    throw new Exception("Unknown chunk!");
                }
            }
        }

        protected abstract void ReadTPKInfo();

        protected virtual void OutputTexture(Texture texture, DDS.Header header, BinaryWriter writer)
        {
            writer.Write(0x20534444);
            BinaryUtil.WriteStruct(writer, header);
            writer.Write(texture.Data);
        }

        protected virtual DDS.Header BuildDDSHeader(Texture texture)
        {
            if (texture.CompressionType == P8Compression)
            {
                throw new ArgumentException("P8 is not supported!");
            }

            var header = new DDS.Header
            {
                Size = 124,
                Flags = 0x21007,
                Height = (uint) texture.Height,
                Width = (uint) texture.Width,
                MipMapCount = 0,
            };

            var pixelFormat = new DDS.PixelFormat
            {
                Size = 32
            };

            if (texture.CompressionType == 0x00000015)
            {
                pixelFormat.Flags = 0x41;
                pixelFormat.RGBBitCount = 0x20;
                pixelFormat.RBitMask = 0xFF0000;
                pixelFormat.GBitMask = 0xFF00;
                pixelFormat.BBitMask = 0xFF;
                pixelFormat.ABitMask = 0xFF000000;

                header.Caps = 0x40100A;
            }
            else
            {
                pixelFormat.Flags = 4;
                pixelFormat.FourCC = texture.CompressionType;
                header.Caps = 0x401008;
            }

            header.PixelFormat = pixelFormat;

            return header;
        }

        private void ReadTPKHashes(long size)
        {
            if (size % 8 != 0)
            {
                throw new ArgumentException($"Cannot read hash pairs from this chunk! {size} is not divisible by 8.");
            }

            for (var i = 0; i < size / 8; i++)
            {
                var hash = Context.Reader.ReadUInt32();
                Pack.Hashes.Add(hash);

                Context.Reader.BaseStream.Seek(4, SeekOrigin.Current);
            }
        }

        protected abstract void ReadTPKTextures(long size);

        protected abstract void ReadDxtHeaders(long size);

        protected abstract void ReadTPKData();

        protected readonly bool IsCompressed;
        protected readonly TexturePack Pack;
    }
}
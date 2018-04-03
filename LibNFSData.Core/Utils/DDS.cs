using System.Runtime.InteropServices;

namespace LibNFSData.Core.Utils
{
    public static class DDS
    {
        public const uint Magic = 0x20534444;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PixelFormat
        {
            public uint Size;

            public uint Flags;

            public uint FourCC;

            public uint RGBBitCount;

            public uint RBitMask;

            public uint GBitMask;

            public uint BBitMask;

            public uint ABitMask;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Header
        {
            public uint Size;
            public uint Flags;
            public uint Height;
            public uint Width;
            public uint PitchOrLinearSize;
            public uint Depth;
            public uint MipMapCount;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public uint[] Reserved;
            
            public PixelFormat PixelFormat;
            public uint Caps;
            public uint Caps2;
            public uint Caps3;
            public uint Caps4;
            public uint Reserved2;
        }
    }
}
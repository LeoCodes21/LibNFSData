using System;
using System.IO;
using System.Runtime.InteropServices;
using LibNFSData.Core.Models;
using LibNFSData.Core.Utils;
using BaseContainer = LibNFSData.Core.Containers.LanguageContainer;

namespace LibNFSData.MostWanted.Containers
{
    public class LanguageContainer : BaseContainer
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct LanguageHeader
        {
            private readonly uint Marker;

            public readonly uint NumStrings;

            public readonly uint HashTableOffset;

            public readonly uint StringTableOffset;
        }
        
        public LanguageContainer(BinaryReader reader, long? containerSize) : base(reader, containerSize)
        {
        }

        public override void Write(BinaryWriter writer, Language model)
        {
            
        }

        protected override void Read(long size)
        {
            var curPos = Context.Reader.BaseStream.Position;
            var runTo = curPos + size;
            var header = BinaryUtil.ReadStruct<LanguageHeader>(Context.Reader);

            DebugUtil.EnsureCondition(
                header.HashTableOffset % 2 == 0,
                () => "Hash table is not aligned evenly! This is very bad.");
            DebugUtil.EnsureCondition(
                header.StringTableOffset % 2 == 0,
                () => "String table is not aligned evenly! This is very bad.");
            
            Context.Reader.BaseStream.Position = curPos + header.HashTableOffset;
            
            DebugUtil.PrintPosition(Context.Reader, GetType());
            DebugUtil.EnsureCondition(
                Context.Reader.BaseStream.Position + header.NumStrings * 8 < runTo,
                () => "Cannot read further - hash table would overflow and/or leave no room for strings. This is very bad.");
            
            for (var i = 0; i < header.NumStrings; i++)
            {
                var entry = new LanguageEntry
                {
                    HashOne = Context.Reader.ReadUInt32(),
                    HashTwo = Context.Reader.ReadUInt32(),
                };

                LanguagePack.Entries.Add(entry);
            }
            
            Context.Reader.BaseStream.Position = curPos + header.StringTableOffset;
            DebugUtil.PrintPosition(Context.Reader, GetType());
            
            for (var i = 0; i < header.NumStrings; i++)
            {
                LanguagePack.Entries[i].Text = BinaryUtil.ReadNullTerminatedString(Context.Reader);
            }
        }

        protected override void HandleChunk(uint id, uint size)
        {
            throw new NotImplementedException();
        }
    }
}
using System.Collections.Generic;
using LibNFSData.Core.Utils;

namespace LibNFSData.Core.Models
{
    public class LanguageEntry
    {
        public uint HashOne { get; set; }
        public uint HashTwo { get; set; }
        public string Text { get; set; }
    }

    public class Language : Model
    {
        public Language(ChunkID id, long size, long position) : base(id, size, position)
        {
            DebugUtil.EnsureCondition(
                id == ChunkID.BCHUNK_LANGUAGE,
                () => $"Expected BCHUNK_LANGUAGE, got {id.ToString()}");
        }

        public uint NumStrings { get; set; }

        public string Name { get; set; } = "No name";

        public List<LanguageEntry> Entries { get; set; } = new List<LanguageEntry>();
    }
}
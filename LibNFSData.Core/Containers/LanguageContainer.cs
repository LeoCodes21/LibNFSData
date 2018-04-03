using System;
using System.IO;
using LibNFSData.Core.Models;
using LibNFSData.Core.Utils;

namespace LibNFSData.Core.Containers
{
    public abstract class LanguageContainer : Container<Language>
    {
        protected LanguageContainer(BinaryReader reader, long? containerSize) : base(reader, containerSize)
        {
            if (ContainerSize == 0)
            {
                throw new ArgumentException("Empty language container!");
            }
            
            LanguagePack = new Language(ChunkID.BCHUNK_LANGUAGE, ContainerSize, Context.Reader.BaseStream.Position - 8);
        }

        public override Language Get()
        {
            Read(ContainerSize);

            return LanguagePack;
        }

        protected readonly Language LanguagePack;
    }
}
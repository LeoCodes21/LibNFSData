using System.IO;

namespace LibNFSData.Core
{
    /**
     * Manages the context of a read. This allows us
     * to switch between files if we need to.
     */
    public class ReaderContext
    {
        public BinaryReader Reader { get; set; }

        public ReaderContext(BinaryReader reader)
        {
            Reader = reader;
        }
    }
}
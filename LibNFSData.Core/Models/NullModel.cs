using LibNFSData.Core.Utils;

namespace LibNFSData.Core.Models
{
    /**
     * A model with no data.
     */
    public class NullModel : Model
    {
        public NullModel(ChunkID id, long size, long position) : base(id, size, position)
        {
        }
    }
}
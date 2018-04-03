using System;

namespace LibNFSData.Core
{
    public class NFSException : Exception
    {
        public NFSException()
        {
        }

        public NFSException(string message) : base(message)
        {
        }
    }
}
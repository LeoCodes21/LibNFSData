using System;
using System.IO;
using System.Linq;
using MW = LibNFSData.MostWanted;

namespace LibNFSData.DataPlayground
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            using (var reader = new BinaryReader(File.OpenRead("MW/STREAML2RA.BUN")))
            {
                var container = new MW.FileContainer(reader);
                var result = container.Get();

                foreach (var modelGroup in result.GroupBy(m => m.GetType()))
                {
                    Console.WriteLine($"Group: {modelGroup.Key} | count: {modelGroup.Count()}");
                }
            }
        }
    }
}
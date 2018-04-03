using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace LibNFSData.Core.Utils
{
    /**
     * Debugging utilities, such as assertions.
     */
    public static class DebugUtil
    {
        public static void EnsureCondition(bool condition, Func<string> exceptionMessage, [CallerMemberName] string callerName = "")
        {
            if (!condition)
            {
                throw new NFSException($"[{callerName}]: {exceptionMessage()}");
            }
        }
        
        public static void EnsureCondition(Predicate<object> condition, Func<string> exceptionMessage, [CallerMemberName] string callerName = "")
        {
            EnsureCondition(condition.Invoke(new object()), exceptionMessage);
        }
        
        public static void PrintPosition(BinaryReader reader, Type classType)
        {
            Console.WriteLine(
                $"[{classType.Name}]: Current position: 0x{reader.BaseStream.Position:X16} ({reader.BaseStream.Position})");
        }

        public static void PrintID(BinaryReader reader, uint id, long normalizedId, uint size, Type classType,
            int level = 0, Type enumType = null)
        {
            var pad = "    ".Repeat(level);
            Console.Write(
                $"{pad}[{classType.Name}]: chunk: 0x{id:X8} [{size} bytes] @ 0x{reader.BaseStream.Position:X16}");

            enumType = enumType == null ? typeof(ChunkID) : enumType;

            if (Enum.IsDefined(enumType, normalizedId))
            {
                Console.Write(" | Type: {0}", enumType.GetEnumName(normalizedId));
            }

            Console.WriteLine();
        }
        
        public static void ValidatePosition(BinaryReader reader, long boundary, Type classType)
        {
            if (reader.BaseStream.Position > boundary)
            {
                throw new Exception(
                    $"[{classType.Name}]: Buffer overflow? Chunk runs to 0x{boundary:X16}, we're at 0x{reader.BaseStream.Position:X16} (diff: {(reader.BaseStream.Position - boundary):X16})");
            }
        }
    }
}
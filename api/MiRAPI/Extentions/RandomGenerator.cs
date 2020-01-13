using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.Extentions
{
    public static class RandomGenerator
    {
        public static int GenerateInt32 => BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
    }
}

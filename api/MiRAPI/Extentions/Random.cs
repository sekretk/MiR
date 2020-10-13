using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.Extentions
{
    public static class RandomName
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "zxcvnasdkjffghqwetryuiowtptopgkvd";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

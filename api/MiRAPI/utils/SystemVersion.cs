using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRAPI.utils
{
    public static class SystemVersion
    {
        public static string[] Changes()
        {
            string path = Path.Combine(PathExtentions.GetApplicationRoot, "changes.txt");

            string newLine = String.Empty;

            if (System.IO.File.Exists(path))
            {
                string[] versions = System.IO.File.ReadAllLines(path);

                return versions.Where(v => !v.Contains("(i) deployment")).ToArray();
            }

            return new[] { "" };
        }
    }
}

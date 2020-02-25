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
            var seedb = new StringBuilder();

            string path = Path.Combine(PathExtentions.GetApplicationRoot, "changes.txt");

            string newLine = String.Empty;

            if (System.IO.File.Exists(path))
            {
                string[] versions = System.IO.File.ReadAllLines(path, System.Text.Encoding.GetEncoding(1251));

                seedb = versions.Where(v => v != String.Empty).Aggregate(seedb, (sb, el) =>
                {
                    if (el.StartsWith("#rev"))
                    {
                        if (!String.IsNullOrEmpty(newLine) && !newLine.Contains("(i)"))
                        {
                            sb.Append(newLine);
                            sb.Append(Environment.NewLine);
                        }
                        newLine = String.Empty;
                        newLine += el.Substring(4);
                        newLine += " - ";
                    }
                    else
                    {
                        newLine += el;
                        newLine += "; ";
                    }

                    return sb;
                });
            }

            return seedb
                .ToString()
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Where(s => !String.IsNullOrEmpty(s))
                .ToArray();
        }
    }
}

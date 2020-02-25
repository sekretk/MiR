using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiRAPI.utils
{
    public static class PathExtentions
    {
        public static string GetApplicationRoot
        {
            get
            {
                var exePath = Path.GetDirectoryName(System.Reflection
                    .Assembly.GetExecutingAssembly().CodeBase);
                //Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*");
                var appRoot = appPathMatcher.Match(exePath).Value;
                return appRoot;
            }
        }
    }
}

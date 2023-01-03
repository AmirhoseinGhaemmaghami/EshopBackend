using Ganss.Xss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Core.Security
{
    public static class XssSanitizer
    {
        public static string Sanitize(this string s)
        {
            return new HtmlSanitizer().Sanitize(s);
        }
    }
}

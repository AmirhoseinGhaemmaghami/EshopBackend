using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Core.Security
{
    public interface IHashUtility
    {
        bool VerifyHash(string input, string hash);

        string GetHash(string input);
    }
}

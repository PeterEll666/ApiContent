using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiContent.Services
{
    interface ICryptoService
    {
        string CryptPassword(string password);
        bool CheckPassword(string suppliedPwd, string savedPwd);
    }
}

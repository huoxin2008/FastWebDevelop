using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastWebDevelop
{
    public class TokenManager
    {

        public static string GetUserToken(string name)
        {
            return (name + "_快速网站开发").Md5Byte().Base64().ToLower();
        }

    }
}

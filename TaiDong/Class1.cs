using FastWebDevelop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiDong
{
    [Safe]
    public class Class1
    {
        [UnSafe]
        public static string GetAbout()
        {
            return "操你妈,傻逼!";
        }


        [UnSafe]
        public static AjaxResult Login(string name, string pass)
        {
            if (name == "1" && pass == "2")
            {
                var tk = TokenManager.GetUserToken(name);
                Program.UserToken.Add(name, tk);
                return AjaxResult.Success(tk);
            }
            return AjaxResult.Error("登录失败！");
        }



    }
}

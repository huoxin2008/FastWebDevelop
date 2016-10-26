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
        static string DBName = "TaiDong";

        [UnSafe]
        public static string GetContent(string type)
        {
            return (Mongo.First<Content>(a => a.type == type, DBName) ?? new Content()).content;
        }

        public static AjaxResult SetContent(string token, string type, string content)
        {
            return MvcHelp.Try(() =>
            {
                var bd = Mongo.GetUpdateDef<Content>();

                var dd = bd.Set<string>(b => b.content, content);
                var cc = bd.Set<DateTime>(b => b.editTime, DateTime.Now);

                Mongo.Update<Content>(a => a.type == type, bd.Combine(cc, dd), DBName);
            });
        }


        [UnSafe]
        public static AjaxResult Login(string name, string pass)
        {
            Mongo.Insert<User>(new User { name = "admin", pass = "admin", level = "管理员" });
            try
            {
                var u = Mongo.First<User>(a => a.name == name && a.pass == pass, DBName);
                if (u == null)
                    return AjaxResult.Error("登录失败！");
                //var tk = TokenManager.GetUserToken(name);
                //Program.UserToken.Add(name, tk);
                Program.Token = TokenManager.GetGlobalToken();
                return AjaxResult.Success(Program.Token);
            }
            catch (Exception e)
            {
                return AjaxResult.Error(e.Message);
            }

        }



    }

    public class Content
    {
        public DateTime editTime;
        public string content;
        public string type;
    }

    public class User
    {
        public int id;
        public string name;
        public string pass;
        public string level;
    }

}

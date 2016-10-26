using FastWebDevelop;
using MongoDB.Bson.Serialization.Attributes;
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
        static Mongo Db = new Mongo("TaiDong");

        [UnSafe]
        public static bool CheckToken(string token)
        {
            return (Program.Token == token);
        }
        [UnSafe]
        public static string GetContent(string type)
        {
            return (Db.First<Content>(a => a.type == type) ?? new Content()).content;
        }


        public static AjaxResult ChangePass(string token, string pass)
        {
            return MvcHelp.Try(() =>
            {
                //var fo = Db.First<User>(a => a.name == "admin");
                Db.Update<User>(a => a.name == "admin", Mongo.GetUpdateDef<User>().Set(b => b.pass, pass));
            });
        }
        public static AjaxResult SetContent(string token, string type, string content)
        {
            return MvcHelp.Try(() =>
            {
                var bd = Mongo.GetUpdateDef<Content>();

                var dd = bd.Set<string>(b => b.content, content);
                var cc = bd.Set<DateTime>(b => b.editTime, DateTime.Now);
                var fo = Db.First<Content>(a => a.type == type);
                if (fo == null)
                    Db.Insert<Content>(new Content { type = type, content = content, editTime = DateTime.Now });
                else
                    Db.Update<Content>(a => a.type == type, bd.Combine(cc, dd));
            });
        }


        [UnSafe]
        public static AjaxResult Login(string name, string pass)
        {
            try
            {
                var u = Db.First<User>(a => a.name == name && a.pass == pass);
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
        [BsonId]
        public string type;
        public DateTime editTime;
        public string content;

    }

    public class User
    {
        [BsonId]
        public string name;
        public string pass;
        public string level;
    }

}

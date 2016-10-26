using Hprose.IO;
using Hprose.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using System.IO;
using System.Reflection;
using TaiDong;

namespace FastWebDevelop
{

    public class Program
    {
        static string dPath = Environment.CurrentDirectory + "\\dll\\";
        static List<string> SaveFunc = new List<string>();
        static List<string> UserSaveFunc = new List<string>();

        public static string Token = "";
        public static Dictionary<string, string> UserToken = new Dictionary<string, string>();

        //static Func<string, string, bool> LoginHandle;
        static HproseHttpListenerServer server;
        static string AppId = "BD50D47A-0C22-435B-911B-F6F3D8E3BCEA";
        static string AppSecret = "69AF905B-E460-47A1-990A-D1225994E934";

        static void Main(string[] args)
        {
#if !DEBUG
             server = new HproseHttpListenerServer("http://114.215.253.25:22012/");
#else
            server = new HproseHttpListenerServer("http://127.0.0.1:22012/");
#endif
            //Mongo Db = new Mongo("TaiDong");
            //Db.DeleteMany<User>(add=>true);
            // Db.Delete<Content>(a=>a.type=="about");
            //var add= Db.FindList<Content>(a => true);
            var AddDomian = new string[] { "TaiDong" };
            //Mongo.Insert<User>(new User { name = "admin", pass = "admin", level = "管理员" }, "TaiDong");

            var fs = Directory.GetFiles(dPath, "*.dll");
            foreach (var f in fs)
            {
                var fn = Path.GetFileName(f);
                try
                {
                    var aes = Assembly.LoadFile(f);
                    var ts = aes.GetTypes();
                    foreach (var t in ts)
                    {
                        if (t.IsNotPublic)
                            continue;
                        if (t.Name.ToLower().EndsWith("model"))
                        {
                            HproseClassManager.Register(t, t.Name);
                        }
                        else
                        {
                            AddClass(t);
                        }
                    }
                }
                catch { }
            }

            try
            {
                var aes = Assembly.GetExecutingAssembly();
                var ts = aes.GetTypes();
                foreach (var t in ts)
                {
                    if (t.IsNotPublic || AddDomian.All(a => !t.FullName.Contains(a)))
                        continue;
                    if (t.Name.ToLower().EndsWith("model"))
                    {
                        HproseClassManager.Register(t, t.Name);
                    }
                    else
                    {
                        AddClass(t);
                    }
                }
            }
            catch { }

            server.OnBeforeInvoke += (name, arg, byRef, context) =>
            {
                if (SaveFunc.Contains(name))
                {
                    if (arg.Length <= 0 || Token != arg[0] as string)
                    {
                        throw new Exception("验证失败!");
                    }
                }
                else if (UserSaveFunc.Contains(name))
                {
                    if (arg.Length <= 0 || !UserToken.ContainsValue(arg[0] as string))
                    {
                        throw new Exception("验证失败!");
                    }
                }

            };
            server.IsCrossDomainEnabled = true;
            //server.CrossDomainXmlFile = "crossdomain.xml";
            server.Start();
            Console.WriteLine("Server started.");
            Console.ReadLine();
            Console.WriteLine("Server stopped.");

        }

        static void AddClass(Type t)
        {
            server.Add(t);
            var att1 = t.GetCustomAttribute(typeof(SafeAttribute)) != null;
            var att2 = t.GetCustomAttribute(typeof(UnSafeAttribute)) != null;
            var att3 = t.GetCustomAttribute(typeof(LoginAttribute)) != null;

            var fs = t.GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (var f in fs)
            {
                var att4 = f.GetCustomAttribute(typeof(SafeAttribute)) != null;
                var att5 = f.GetCustomAttribute(typeof(UnSafeAttribute)) != null;
                var att6 = f.GetCustomAttribute(typeof(LoginAttribute)) != null;
                if (att4 || (att1 && !att5))
                    SaveFunc.Add(f.Name);
                if (att6 || (att3 && !att4 && !att5))
                    UserSaveFunc.Add(f.Name);
            }

        }


        public static bool CheckToken(string token)
        {
            return (Token == token);
        }

    }
}
//server.IsCrossDomainEnabled = true;
////server.CrossDomainXmlFile = "crossdomain.xml";

//FileSystemWatcher watcher = new FileSystemWatcher();

//watcher.Path = dPath;
//watcher.Filter = "*.dll";
//watcher.Changed += (ae, e) =>
//{
//    if (e.ChangeType == WatcherChangeTypes.Changed)
//    {
//        Init();
//    }
//};
//watcher.EnableRaisingEvents = true;
//watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess
//                       | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
//watcher.IncludeSubdirectories = true;

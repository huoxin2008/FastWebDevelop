using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace FastWebDevelop
{
    /// <summary>
    /// 封装了MONGODB 的 CURD 
    /// </summary>
    public class Mongo
    {
        static MongoClient ct;
        static Mongo()
        {
            string connectionStr = "mongodb://127.0.0.1";
            ct = new MongoClient(connectionStr);
        }

        string dbName = "";
        public Mongo(string dbname)
        {
            dbName = dbname;
        }

        #region 实例方法
        public T First<T>(Expression<Func<T, bool>> fiter)
        {
            try
            {
                var db = ct.GetDatabase(dbName);
                var col = db.GetCollection<T>(typeof(T).FullName);
                return col.Find<T>(fiter).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return default(T);
        }

        public long Count<T>(Expression<Func<T, bool>> fiter)
        {
            var db = ct.GetDatabase(dbName);
            var col = db.GetCollection<T>(typeof(T).FullName);
            return col.Count<T>(fiter);
        }
        public List<T> FindList<T>(Expression<Func<T, bool>> fiter)
        {
            var db = ct.GetDatabase(dbName);
            var col = db.GetCollection<T>(typeof(T).FullName);
            return col.Find<T>(fiter).ToList();
        }
        public IFindFluent<T, T> Find<T>(Expression<Func<T, bool>> fiter)
        {
            var db = ct.GetDatabase(dbName);
            var col = db.GetCollection<T>(typeof(T).FullName);
            return col.Find<T>(fiter);
        }

        public void Insert<T>(T data)
        {
            var db = ct.GetDatabase(dbName);
            var col = db.GetCollection<T>(typeof(T).FullName);
            col.InsertOne(data);
        }

        public void Insert<T>(IEnumerable<T> data)
        {
            var db = ct.GetDatabase(dbName);
            var col = db.GetCollection<T>(typeof(T).FullName);
            col.InsertMany(data);
        }

        public UpdateResult Update<T>(Expression<Func<T, bool>> fiter, UpdateDefinition<T> update)
        {
            var db = ct.GetDatabase(dbName);
            var col = db.GetCollection<T>(typeof(T).FullName);
            return col.UpdateOne(fiter, update);
        }

        public UpdateResult UpdateMany<T>(Expression<Func<T, bool>> fiter, UpdateDefinition<T> update)
        {
            var db = ct.GetDatabase(dbName);
            var col = db.GetCollection<T>(typeof(T).FullName);
            return col.UpdateMany(fiter, update);
        }

        public DeleteResult Delete<T>(Expression<Func<T, bool>> fiter)
        {
            var db = ct.GetDatabase(dbName);
            var col = db.GetCollection<T>(typeof(T).FullName);
            return col.DeleteOne(fiter);
        }

        public DeleteResult DeleteMany<T>(Expression<Func<T, bool>> fiter)
        {
            var db = ct.GetDatabase(dbName);
            var col = db.GetCollection<T>(typeof(T).FullName);
            return col.DeleteMany(fiter);
        }

        #endregion

        public static T First<T>(Expression<Func<T, bool>> fiter, string dbname = "default")
        {
            var db = ct.GetDatabase(dbname);
            var col = db.GetCollection<T>(typeof(T).FullName);
            return col.Find<T>(fiter).FirstOrDefault();
        }

        public static List<T> FindList<T>(Expression<Func<T, bool>> fiter, string dbname = "default")
        {
            var db = ct.GetDatabase(dbname);
            var col = db.GetCollection<T>(typeof(T).FullName);
            return col.Find<T>(fiter).ToList();
        }
        public static IFindFluent<T, T> Find<T>(Expression<Func<T, bool>> fiter, string dbname = "default")
        {
            var db = ct.GetDatabase(dbname);
            var col = db.GetCollection<T>(typeof(T).FullName);
            return col.Find<T>(fiter);
        }

        public static void Insert<T>(T data, string dbname = "default")
        {
            var db = ct.GetDatabase(dbname);
            var col = db.GetCollection<T>(typeof(T).FullName);
            col.InsertOne(data);
        }

        public static void Insert<T>(IEnumerable<T> data, string dbname = "default")
        {
            var db = ct.GetDatabase(dbname);
            var col = db.GetCollection<T>(typeof(T).FullName);
            col.InsertMany(data);
        }

        public static UpdateResult Update<T>(Expression<Func<T, bool>> fiter, UpdateDefinition<T> update, string dbname = "default")
        {
            var db = ct.GetDatabase(dbname);
            var col = db.GetCollection<T>(typeof(T).FullName);
            return col.UpdateOne(fiter, update);
        }

        public static UpdateResult UpdateMany<T>(Expression<Func<T, bool>> fiter, UpdateDefinition<T> update, string dbname = "default")
        {
            var db = ct.GetDatabase(dbname);
            var col = db.GetCollection<T>(typeof(T).FullName);
            return col.UpdateMany(fiter, update);
        }

        public static DeleteResult Delete<T>(Expression<Func<T, bool>> fiter, string dbname = "default")
        {
            var db = ct.GetDatabase(dbname);
            var col = db.GetCollection<T>(typeof(T).FullName);
            return col.DeleteOne(fiter);
        }

        public static DeleteResult DeleteMany<T>(Expression<Func<T, bool>> fiter, string dbname = "default")
        {
            var db = ct.GetDatabase(dbname);
            var col = db.GetCollection<T>(typeof(T).FullName);
            return col.DeleteMany(fiter);
        }

        public static UpdateDefinitionBuilder<T> GetUpdateDef<T>()
        {
            return new UpdateDefinitionBuilder<T>();
        }
    }
}

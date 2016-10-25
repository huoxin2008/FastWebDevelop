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
        private static readonly string _connectionString = "Server=127.0.0.1:27017";

        private static readonly string _dbName = "MyNorthwind";

        static MongoClient ct;
        static Mongo()
        {
            string connectionStr = "mongodb://127.0.0.1";
            ct = new MongoClient(connectionStr);
        }

        public static T First<T>(Expression<Func<T, bool>> fiter, string dbname = "default")
        {
            var db = ct.GetDatabase(dbname);
            var col = db.GetCollection<T>(typeof(T).FullName);
            return col.Find<T>(fiter, new FindOptions { BatchSize = 1 }).FirstOrDefault();
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


    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace FastWebDevelop
{
    public static partial class MvcHelp
    {
        public static string PicServer = "http://hzimg.guwanch.com";

        public static string YMD(this DateTime d)
        {
            return d.ToString("yyyy-MM-dd");
        }
        public static string YMDHMS(this DateTime d)
        {
            return d.ToString("yyyy-MM-dd HH:mm:ss");
        }




        public static string Str(this DateTime? t, string sp)
        {
            return t.HasValue ? t.Value.ToString(sp) : "";
        }

        public static double Min(this double t, double a)
        {
            return t < a ? a : t;
        }

        public static double Max(this double t, double a)
        {
            return t > a ? a : t;
        }

        public static void For(Action<int> func, int time)
        {
            for (int i = 0; i < time; i++)
                func(i);
        }



        #region 扩展方法


        public static string format(this string v, params object[] args)
        {
            return string.Format(v, args);
        }


        public static string byteToHexStr(this byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
        public static string Join<T>(this IEnumerable<T> b, char symbol = ',')
        {
            var a = "";
            foreach (var cc in b)
                a += cc + symbol.ToString();
            return a.Trim(symbol);
        }
        public static string Join<T, T2>(this IEnumerable<T> b, Func<T, T2> select, char symbol = ',')
        {
            return b.Select(select).ToList().Join(symbol);
        }
        public static Dictionary<TKey, List<T>> ToDic<T, TKey>(this IEnumerable<T> b, Func<T, TKey> select)
        {
            try
            {
                var v = new Dictionary<TKey, List<T>>();
                var s = b.GroupBy(select);
                foreach (var a in s)
                {
                    v.Add(a.Key, a.ToList());
                }
                return v;
            }
            catch { }
            return new Dictionary<TKey, List<T>>();
        }
        public static T Get<T>(this IList<T> list, int i)
        {
            if (list != null && list.Count > i)
            {
                return list[i];
            }
            return default(T);
        }

        public static T2 Get<T, T2>(this Dictionary<T, T2> b, T key)
        {
            if (b == null || !b.ContainsKey(key))
                return default(T2);
            return b[key];
        }

        public static string RandomName(this int len)
        {
            var pass = "";
            var r = new Random();
            for (int i = 0; i < len; i++)
            {
                if (r.Next(2) == 0)
                {
                    int rand = r.Next(26) + 97;
                    pass += ((char)rand).ToString();
                }
                else
                {
                    int rand = r.Next(10) + 48;
                    pass += ((char)rand).ToString();
                }
            }
            return pass;
        }

        public static string RandomWord(this int len)
        {
            var pass = "";
            var r = new Random();
            for (int i = 0; i < len; i++)
            {
                int rand = r.Next(26) + 97;
                pass += ((char)rand).ToString();
            }
            return pass;
        }

        static Random r = new Random(Environment.TickCount);
        public static T Random<T>(this IList<T> list)
        {
            if (list.Count == 0)
                return default(T);
            return list[r.Next(list.Count)];
        }
        public static T Random<T>(this IList<T> list, int count)
        {
            if (list.Count == 0)
                return default(T);
            return list[r.Next(list.Count)];
        }
        public static int Random(this int num)
        {
            return r.Next(num);
        }
        public static string RandomNum(this int len)
        {
            var pass = "";
            var r = new Random();
            for (int i = 0; i < len; i++)
            {
                int rand = r.Next(10) + 48;
                pass += ((char)rand).ToString();
            }
            return pass;
        }
        public static MatchCollection Matchs2(this string patern, string str)
        {
            return str.Nil() ? null : Regex.Matches(str, patern);
        }
        public static Match Match2(this string patern, string str)
        {
            return str.Nil() ? System.Text.RegularExpressions.Match.Empty : Regex.Match(str, patern);
        }
        public static bool IsMatch2(this string patern, string str)
        {
            return str.Nil() ? false : Regex.IsMatch(str, patern);
        }
        public static MatchCollection Matchs(this string str, string patern)
        {
            return str.Nil() ? null : Regex.Matches(str, patern);
        }
        public static Match Match(this string str, string patern)
        {
            return str.Nil() ? System.Text.RegularExpressions.Match.Empty : Regex.Match(str, patern);
        }
        public static bool IsMatch(this string str, string patern)
        {
            return str.Nil() ? false : Regex.IsMatch(str, patern);
        }

        public static TimeSpan Span(this DateTime a)
        {
            return DateTime.Now - a;
        }


        public static string Null(this string b, string c)
        {
            if (!string.IsNullOrEmpty(b))
                return b;
            return c;
        }


        public static byte[] Download(this string url)
        {
            try
            {
                return new WebClient().DownloadData(url);
                //return new HttpClient().GetByteArrayAsync(url).Result;
            }
            catch { }
            return null;
        }
        public static byte[] Sub(this byte[] ds, int index, int len)
        {
            var c = new byte[len];
            for (int i = 0; i < len; i++)
                c[i] = ds[i + index];
            return c;
        }
        public static bool Download(this string url, string savePath)
        {
            try
            {
                var bs = new HttpClient().GetByteArrayAsync(url).Result;
                if (bs.Length > 0)
                {
                    File.WriteAllBytes(savePath, bs);
                    return true;
                }
            }
            catch { }
            return false;
        }

        public static string ToStr<T>(this List<T> t, string split = ",")
        {
            var s = "";
            t.ForEach(a => s += a + split);
            return s.TrimEnd(split.ToCharArray());
        }



        public static DateTime ToTime(this int timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        public static DateTime ToTime(this string timeStamp)
        {
            try
            {
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long lTime = long.Parse(timeStamp + "0000000");
                TimeSpan toNow = new TimeSpan(lTime);
                return dtStart.Add(toNow);
            }
            catch { }
            return DateTime.Now;
        }
        public static DateTime ToTimeMS(this string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        /// <summary>  
        /// DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time"> DateTime时间格式</param>  
        /// <returns>Unix时间戳格式</returns>  
        public static int ToStamp(this System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        public static List<T> toList<T>(this IEnumerable<T> v)
        {
            if (v == null || v.Count() == 0)
                return new List<T>();
            return v.ToList();
        }
        public static string Md5Str(this string v)
        {
            byte[] result = Encoding.UTF8.GetBytes(v);
            MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            return md5.ComputeHash(result).byteToHexStr();
        }
        public static int Md5(this string v)
        {
            byte[] result = Encoding.UTF8.GetBytes(v);
            MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            return BitConverter.ToInt32(md5.ComputeHash(result), 0);
        }
        public static byte[] Md5Byte(this string v)
        {
            byte[] result = Encoding.UTF8.GetBytes(v);
            MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            return md5.ComputeHash(result);
        }
        public static string Base64(this byte[] v)
        {
            return Convert.ToBase64String(v);
        }

        public static bool Nil(this string v)
        {
            return string.IsNullOrEmpty(v);
        }

        public static bool Nil(this object v)
        {
            return v == null;
        }


        public static int Int(this string v)
        {
            var a = 0;
            int.TryParse(v, out a);
            return a;
        }

        public static string Sub(this string s, int len, string replace = "...")
        {
            return s.Nil() ? "" : s.Length > len - 3 ? s.Substring(0, len - 3) + replace : s;
        }



        public static AjaxResult Try(this Func<AjaxResult> f)
        {
            try
            {
                return f();
            }
            catch (Exception e)
            {
                return AjaxResult.Error(e.GetExceptionMsg());
            }
        }

        public static AjaxResult Try(this Action f)
        {
            try
            {
                f();
                return AjaxResult.Success();
            }
            catch (Exception e)
            {
                return AjaxResult.Error(e.GetExceptionMsg());
            }
        }

        public static T Try<T>(this Func<T> f)
        {
            try
            {
                return f();
            }
            catch
            {
            }
            return default(T);
        }


        public static string GetExceptionMsg(this Exception ex)
        {
            var s = "";
            var tex = ex;
            do
            {
                s += tex.Message + ":" + tex.StackTrace + Environment.NewLine;
                tex = tex.InnerException;
            } while (tex != null);
            return s;
        }

        #endregion


        #region Log日志
        public static string LogFile;
        public static void LogNoSafe(this string msg, string header = "")
        {
            lock (LogFile)
            {
                new Action(() =>
                {
                    try
                    {
                        var p = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\";
                        if (!Directory.Exists(p))
                            Directory.CreateDirectory(p);
                        File.AppendAllText(p + DateTime.Now.ToString("yyMMdd") + ".txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "- " + header + "\t" + msg + Environment.
                                                NewLine);
                    }
                    catch { }
                }).BeginInvoke(null, null);
            }
        }
        public static void Log(this string msg, string header = "")
        {
            lock (LogFile)
            {
                new Action(() =>
                {
                    File.AppendAllText(LogFile + DateTime.Now.ToString("yyMMdd") + ".txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "- " + header + "\t" + msg + Environment.
                        NewLine);
                }).BeginInvoke(null, null);
            }
        }

        public static void Log(this Exception ex, string header = "")
        {
            lock (LogFile)
            {
                new Action(() =>
                {
                    string msg = ex.GetExceptionMsg();
                    File.AppendAllText(LogFile + DateTime.Now.ToString("yyMMdd") + ".txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "- " + header + "\t" + msg + Environment.
                        NewLine);
                }).BeginInvoke(null, null);
            }
        }


        public static string ShortUrl(this string url)
        {
            //可以自定义生成MD5加密字符传前的混合KEY
            string key = "nico";
            //要使用生成URL的字符
            string[] chars = new string[]{
        "a" , "b" , "c" , "d" , "e" , "f" , "g" , "h",
        "i" , "j" , "k" , "l" , "m" , "n" , "o" , "p" ,
        "q" , "r" , "s" , "t" , "u" , "v" , "w" , "x" ,
        "y" , "z" , "0" , "1" , "2" , "3" , "4" , "5" ,
        "6", "7" , "8" , "9" , "A" , "B" , "C" , "D" ,
        "E" , "F" , "G" , "H" , "I" , "J" , "K" , "L" ,
        "M" , "N" , "O" , "P" , "Q" , "R" , "S" , "T" ,
        "U" , "V" , "W" , "X" , "Y" , "Z"
    };
            //对传入网址进行MD5加密
            string hex = (key + url).Md5Str();
            string[] resUrl = new string[4];
            for (int i = 0; i < 4; i++)
            {
                //把加密字符按照8位一组16进制与0x3FFFFFFF进行位与运算
                int hexint = 0x3FFFFFFF & Convert.ToInt32("0x" + hex.Substring(i * 8, 8), 16);
                string outChars = string.Empty;
                for (int j = 0; j < 6; j++)
                {
                    //把得到的值与0x0000003D进行位与运算，取得字符数组chars索引
                    int index = 0x0000003D & hexint;
                    //把取得的字符相加
                    outChars += chars[index];
                    //每次循环按位右移5位
                    hexint = hexint >> 5;
                }
                //把字符串存入对应索引的输出数组
                resUrl[i] = outChars;
            }
            return resUrl[0];
        }
        #endregion


    }
}

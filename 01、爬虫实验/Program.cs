using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Data.SqlClient;

namespace _01_爬虫实验
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //SqlConnection conn = new SqlConnection("server=localhost;database=test;Trusted_Connection=SSPI");
            SqlConnection conn = new SqlConnection(@"Data Source=np:\\.\pipe\LOCALDB#F2838E46\tsql\query;Initial Catalog=test;Integrated Security=True");
            try
            {
                conn.Open();
            }
            catch
            {
                Console.WriteLine("数据库链接失败");
            }
            int n = 0;
            for (int i = 1; i < 4; i++)
            {
                string url = "http://www.jokeji.cn/list_" + i + ".htm";
                WebRequest request = WebRequest.Create(url);
                Stream stream = WebRequest.Create(url).GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.Default);
                //获取到源码
                string str = reader.ReadToEnd();
                stream.Close();
                stream.Dispose();
                //声明HtmlDocument对象
                HtmlDocument doc = new HtmlDocument();
                HtmlDocument doc1 = new HtmlDocument();
                //加载html文档
                doc.LoadHtml(str);
                //通过xPath获取到html中指定元素
                HtmlNodeCollection collection = doc.DocumentNode.SelectNodes(@"/html/body/div[3]/div[1]/div[2]/ul/li/b/a");
                HtmlNodeCollection col;
                string b = string.Empty;
                string a = string.Empty;
                string c = string.Empty;
                foreach (var item in collection)
                {
                    //a = item.InnerHtml;
                    b = "http://www.jokeji.cn/" + item.Attributes["href"].Value;
                    c = item.InnerText;
                    request = WebRequest.Create(b);
                    stream = WebRequest.Create(b).GetResponse().GetResponseStream();
                    reader = new StreamReader(stream, Encoding.Default);
                    //获取到源码
                    str = reader.ReadToEnd();
                    stream.Close();
                    doc1.LoadHtml(str);
                    col = doc1.DocumentNode.SelectNodes(@"//*[@id='text110']");
                    a = col[0].InnerHtml;
                    //SqlCommand cmd = new SqlCommand("insert into article values(2,1,'" + c + "','" + a + "',0,0,getdate(),1)", conn);
                    SqlCommand cmd = new SqlCommand("insert into qwe values('" + c + "','" + a + "',getdate())", conn);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        n++;
                    }
                    Console.WriteLine("抓取了" + n + "条消息");
                }
            }
            conn.Close();//关闭数据库
            Console.ReadKey();
            #region 将源码写入指定路径
            //using (FileStream writer = new FileStream(@"F:\t12.txt", FileMode.OpenOrCreate, FileAccess.Write))
            //{
            //    byte[] buffer = Encoding.UTF8.GetBytes(str);
            //    writer.Write(buffer, 0, buffer.Length);
            //}
            //Console.WriteLine("OK");
            #endregion
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace picture
{
    class Program
    {
        static void Main(string[] args)
        {
            //SqlConnection conn = new SqlConnection("server=localhost;database=test;Trusted_Connection=SSPI");
            //try
            //{
            //    conn.Open();
            //}
            //catch
            //{
            //    Console.WriteLine("数据库链接失败");
            //}
            int n = 0;
            string url = "http://www.mahua.com/newjokes/index_2.htm";
            WebRequest request = WebRequest.Create(url);
            Stream stream = WebRequest.Create(url).GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
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
            HtmlNodeCollection collection = doc.DocumentNode.SelectNodes(@"/html/body/div[2]/div[1]/dl");
            string b = string.Empty;
            string a = string.Empty; //接受地址
            string fileName = string.Empty;
            string c = string.Empty; //图片种类
            SqlCommand cmd;
            int k;
            int m;
            foreach (var item in collection)
            {
                k = collection.IndexOf(item);
                b = item.SelectNodes(@"//span[@class='joke-title']/a")[k].InnerText;  // title 
                //图片地址//*[@id="\"joke_1641076\""]/dd[1]/img;
                a = item.SelectNodes(@"./dd[1]/img")[0].GetAttributeValue("src", "");
                //foreach (var item1 in hn)
                //{
                //    string q = item1.GetAttributeValue("mahuaimg", "");
                //    HtmlAttributeCollection hac;
                //    hac = item1.Attributes;
                //    IEnumerable<HtmlAttribute> ha = hac.AttributesWithName("src");

                //    Console.WriteLine("标题{0}---网址{1}", b, q);
                //}
                //a = item.SelectNodes(@"//dd[1]/img")[0].Attributes["mahuaImg"].Value;    //图片地址mahuaImg mahuaimg  mahuaImg
                Console.WriteLine("标题{0}---网址{1}", b, a);
                //if (!String.IsNullOrEmpty(a))
                //{//   [^\/]*$
                //    Regex reg = new Regex(@"[^\/]*$");
                //    fileName = reg.Match(a).ToString();  //获取URL 尾部设置为文件名
                //    WebClient mywebclient = new WebClient();
                //    string filepath = @"D:\img\" + fileName;
                //    try
                //    {
                //        mywebclient.DownloadFile(a, filepath);
                //        cmd = new SqlCommand("insert into article values("+c+",1,'" + b + "','" + fileName + "',0,0,getdate(),1)", conn);
                //        //执行SQL语句
                //        if (cmd.ExecuteNonQuery() > 0)
                //        {
                //            n++;
                //        }
                //        Console.WriteLine("抓取了" + n + "条消息");
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine(ex.ToString());
                //        Console.ReadKey();
                //    }
                //}
            }
                Console.WriteLine("完成");
            Console.ReadKey();
            }
            //conn.Close();//关闭数据库
        }
    }


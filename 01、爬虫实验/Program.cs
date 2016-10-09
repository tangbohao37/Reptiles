using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace _01_爬虫实验
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string url = "http://www.yidm.com/article/html/2/2061/index.html";
                //WebRequest request = WebRequest.Create(url);
                //WebResponse response = request.GetResponse();
                //Stream stream = response.GetResponseStream();

                Stream stream = WebRequest.Create(url).GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.Default);
                //获取到源码
                string str = reader.ReadToEnd();
                stream.Close();
                stream.Dispose();


                //声明HtmlDocument对象
                HtmlDocument doc = new HtmlDocument();
                //加载html文档
                doc.LoadHtml(str);
                //通过xPath获取到html中指定元素
                HtmlNodeCollection collection = doc.DocumentNode.SelectNodes(@"html/body/div[3]/div[1]/div/div/div/div[2]/a");
                StringBuilder sb = new StringBuilder();

                foreach (var item in collection)
                {
                    sb.Append(string.Format("{0}:{1}\r\n", item.InnerText, item.Attributes["href"].Value));
                }

            }
            catch
            {
                Console.WriteLine("异常，请结束并重试");
            }
            
            #region 将源码写入指定路径
            //using (FileStream writer = new FileStream(@"F:\t12.txt", FileMode.OpenOrCreate, FileAccess.Write))
            //{
            //    byte[] buffer = Encoding.UTF8.GetBytes(str);
            //    writer.Write(buffer, 0, buffer.Length);
            //}
            //Console.WriteLine("OK");
            #endregion

            Console.ReadKey();
        }
    }
}

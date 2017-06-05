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

namespace 电子书
{
    class Program
    {

        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection("server=localhost;database=test;Trusted_Connection=SSPI");
            try
            {
                conn.Open();
            }
            catch
            {
                Console.WriteLine("数据库链接失败");
            }
            int n = 0;
            string url = "http://www.ysx8.net/fenlei/49_1.html";
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
            HtmlNodeCollection collection = doc.DocumentNode.SelectNodes(@"/html/body/div[6]/div[1]/div[2]/ul/li/a"); ///html/body/div[7]/div[1]/div[2]
            //获取到 li 集合
            HtmlNodeCollection col;
            HtmlNodeCollection col1;
            string b = string.Empty;
            string a = string.Empty;
            string c = string.Empty;
            foreach (var item in collection)
            {
                //"/yousheng/down5113.html"
                b = "http://www.ysx8.net"+item.Attributes["href"].Value;  //获取内页 地址   
                a = item.InnerText;//文件夹名字
                //Regex reg = new Regex(@"[1-9]\d*");//获取地址 关键字  
                //c = reg.Match(b).ToString();  //重新组合 url
                ////http://www.ysx8.net/dd_17920_58_1_1.html
                request = WebRequest.Create(b);
                stream = WebRequest.Create(b).GetResponse().GetResponseStream();
                reader = new StreamReader(stream, Encoding.Default);
                //获取到内页源码
                str = reader.ReadToEnd();
                stream.Close();
                doc1.LoadHtml(str); 
                //内页 地址 /html/body/div[7]/div[1]/div/div/ul/li[1]/a[1]
                col = doc1.DocumentNode.SelectNodes(@" //*[@id='top']/ div[2] / div[3] / div[1] / div / div / ul / li /a");/////*[@id="\"top\""]/div[2]/div[3]/div[1]/div/div/ul/li[1]/a[1]
                foreach (var item3 in col)
                {
                    if (item3.InnerText.Trim()=="下载")
                    {
                        url= "http://www.ysx8.net/"+item3.Attributes["href"].Value;//跳转到下载链接
                        request = WebRequest.Create(url);
                        stream = WebRequest.Create(url).GetResponse().GetResponseStream();
                        reader = new StreamReader(stream, Encoding.Default);
                        //获取到内页源码
                        str = reader.ReadToEnd();
                        stream.Close();
                        doc1.LoadHtml(str);
                        col1 = doc1.DocumentNode.SelectNodes("//a"); ///html/body/div[1]/li[1]
                    }
                }
                SqlCommand cmd = new SqlCommand("insert into article values(2,1,'" + c + "','" + a + "',0,0,getdate(),1)", conn);
                //执行SQL语句
                if (cmd.ExecuteNonQuery() > 0)
                {
                    n++;
                }
                Console.WriteLine("抓取了" + n + "条消息");
            }
            conn.Close();//关闭数据库
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

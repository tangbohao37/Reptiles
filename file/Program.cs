using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace file
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = @"D:\BaiduYunDownload\dianzishu";
            DirectoryInfo[] dirInfo = new DirectoryInfo(dir).GetDirectories();
            string a = string.Empty;
            string c = string.Empty;
            string d = string.Empty;
            FileInfo[] b;
            SqlConnection conn = new SqlConnection("server=localhost;database=test;Trusted_Connection=SSPI");
            try
            {
                conn.Open();
            }
            catch
            {
                Console.WriteLine("数据库链接失败");
            }
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd2 = new SqlCommand();
            int k=0;
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                b= new DirectoryInfo(NextFolder.FullName).GetFiles();
                a = NextFolder.Name;
                Console.WriteLine(a);  //书名
                cmd.CommandText = "insert into wifi2_novel values('" + a + "','')";
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                int m = 0;
                foreach (FileInfo item in b)
                {
                    c = item.Name;     // mp3 文件名
                    d=item.FullName;  //MP3 路径
                    d=d.Replace(@"D:\BaiduYunDownload\dianzishu", "");
                    //{有声听书吧www.Ysts8.com}
                    c=c.Replace(@"{有声听书吧www.Ysts8.com}", "");
                    Console.WriteLine(c);
                    m++;
                    //执行SQL语句
                    cmd2.CommandText = "insert into wifi2_part values( (select id from wifi2_novel where name='"+a+"'),'"+c+"','"+m+"')";
                    cmd2.Connection = conn;
                    cmd2.ExecuteNonQuery();
                    if (m == 3)
                    {
                        m = 0;
                    }
                    //执行SQL语句
                }
            }
            conn.Close();
            Console.WriteLine("完成");
            Console.ReadKey();
        }
    }
}



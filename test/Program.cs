using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        //static void Main(string[] args)
        //{
            //WebClient mywebclient = new WebClient();
            //string url = "http://dxpsf-d.ysx8.net:8000/其他评书/王子封臣评书-历代政变演义第1部(先前政变演义)/[我听评书网www.5tps.com]先秦政变演义001.mp3?17102547038311x1476168165x17103009651065-a56895b4a0a2842b07fb51ba1102";
            //string newfilename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".mp3";
            //string filepath = @"D:\" + newfilename;
            //try
            //{
            //    mywebclient.DownloadFile(url, filepath);
            //    //filename = newfilename;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
        //}
        public class DirectoryAllFiles
        {
            static List<FileInformation> FileList = new List<FileInformation>();
            public static List<FileInformation> GetAllFiles(DirectoryInfo dir)
            {
                FileInfo[] allFile = dir.GetFiles();
                foreach (FileInfo fi in allFile)
                {
                    FileList.Add(new FileInformation { FileName = fi.Name, FilePath = fi.FullName });
                }
                DirectoryInfo[] allDir = dir.GetDirectories();
                foreach (DirectoryInfo d in allDir)
                {
                    GetAllFiles(d);
                }
                return FileList;
            }
        }
        public class FileInformation
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
        }
        static void Main(string[] args)
        {
            List<FileInformation> list = DirectoryAllFiles.GetAllFiles(new System.IO.DirectoryInfo(@"D:\BaiduYunDownload\01-javascript（8天）"));

            foreach (var item in list)
            {
                Console.WriteLine(string.Format("文件名：{0}---文件目录{1}", item.FileName, item.FilePath));
            }
            Console.ReadKey();
        }
    }
}

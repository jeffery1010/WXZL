using System;
using System.IO;
using System.Threading.Tasks;

namespace ERPWeb.Util
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public static class LogHelper
    {

        /// <summary>
        /// 写入日志到本地TXT文件 
        /// </summary>
        /// <param name="log">日志内容</param>
        public static void WriteLog(string content, string folder = @"c:\logs\")
        {
            try
            {
                if (string.IsNullOrEmpty(folder)) folder = AppDomain.CurrentDomain.BaseDirectory;
                WriteLogFile(content, folder);
            }
            catch { }
        }
        public static string CurrentTimeString
        {
            get { return " 【" + System.DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss fff") + "】"; }
        }
        public static void WriteLogFile(string content, string logFolder = @"c:\logs\", bool needtime = false)
        {
            if (needtime) content += "  " + CurrentTimeString;
            string filename = System.DateTime.Now.ToLocalTime().ToString("yyMMddHHmm") + ".txt";

            string directory = Path.GetDirectoryName(logFolder);
            string path = logFolder + filename;
            if (!Directory.Exists(directory))
            {
                try
                {
                    Directory.CreateDirectory(directory);
                }
                catch (Exception er)
                {
                    //盘符不存在情况
                    logFolder = "c:\\logs\\";
                    directory = Path.GetDirectoryName(logFolder);
                    Directory.CreateDirectory(directory);
                }

            }

            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                if (!File.Exists(path))
                    fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                else
                    fs = new FileStream(path, FileMode.Append, FileAccess.Write);


                sw = new StreamWriter(fs);
                //开始写入
                sw.WriteLine(content);
                //清空缓冲区
                sw.Flush();
            }
            catch (Exception er)
            {
            }
            finally
            {
                //关闭流
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }

            }
        }
        /// <summary>
        /// 写入日志到本地TXT文件
        /// 注：日志文件名为"A_log.txt",目录为根目录
        /// </summary>
        /// <param name="log">日志内容</param>
        public static void WriteLog_LocalTxt(string log)
        {
            Task.Run(() =>
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "A_log.txt");
                string logContent = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:{log}\r\n";
                File.AppendAllText(filePath, logContent);
            });
        }
    }
}

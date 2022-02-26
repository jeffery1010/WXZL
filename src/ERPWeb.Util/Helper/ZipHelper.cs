using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Drawing;
using System.IO.Compression;
using System.Data;

namespace ERPWeb.Util.Helper
{
    public class ZipHelper
    {
        
        /// <summary>
        /// 创建ZIP文件
        /// </summary>
        /// <param name="realFilePathList">文件真是地址</param>
        /// <param name="fileNameList">文件名</param>
        /// <param name="zipPath">打包之后ZIP的地址</param>
        public void CreateZipFile(List<string> realFilePathList, List<string>fileNameList, string zipPath) {
            if (File.Exists(zipPath)) File.Delete(zipPath);
            using (ZipArchive za = System.IO.Compression.ZipFile.Open(zipPath,ZipArchiveMode.Create))
            {
                for (int i = 0; i < realFilePathList.Count; i++)
                {
                    za.CreateEntryFromFile(realFilePathList[i], fileNameList[i]);
                }
            }
        }

        public string DownHttpImg(string imgUrl, string directory, string fileName) {
            string result = string.Empty;
            try
            {
                if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(imgUrl);
                req.ServicePoint.Expect100Continue = false;
                req.Method = "GET";
                req.KeepAlive = true;
                req.ContentType = "image/*";
                HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
                Stream stream = rsp.GetResponseStream();
                Image.FromStream(stream).Save(directory + fileName);
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
                result = directory + fileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}

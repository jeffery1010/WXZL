using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPWeb.Business.IMGX;
using ERPWeb.DAL.IMGX;
using ERPWeb.Entity.IMGX;
using ERPWeb.Util.Helper;
using ERPWeb.Web.Model;

namespace ERPWeb.Web.Areas.GlobalM.Controllers.IMGX
{
    public class SearchController : Controller
    {
        // GET: GlobalM/Search
        public ActionResult Where(string type="")
        {
            ViewData["type"] = type;
            return PartialView("~/Areas/GlobalM/Views/Search/Where.cshtml");
        }


        public ActionResult ForOP()
        {
            return View();
        }

        public ActionResult ForTime()
        {
            return View();
        }


        public ActionResult ByOPID()
        {
            return View();
        }
        public ActionResult BySerialNo()
        {
            return View();
        }
        public ActionResult CombinationOP()
        {
            return View();
        }
        public ActionResult CombinationAC()
        {
            return View();
        }



        [HttpPost]
        public ActionResult ForTime(
            string Model, string Line, string Mt, string Mac,
            string Location, string Judge,
            DateTime timeBegin, DateTime timeEnd, string Cameras, int page, int rows)
        {
            try
            {
                string type = string.Empty;
                try
                {
                    type = Request.ServerVariables.Get("Remote_Addr").ToString() + "卐" + Request.ServerVariables.Get("Remote_Host").ToString();
                }
                catch (Exception ex)
                {
                    type = ex.Message;
                }
                List<Search> recordList = SearchDAL.getRecord(Model, Line, Mt, Mac, Location, Judge, timeBegin, timeEnd,type, Cameras);
                List<Search> resultList = recordList.Skip((rows * page - rows)).Take(rows).ToList<Search>();
                return Json(new { total = recordList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult ForOP(string Model, string Mt, string OPID, int page, int rows)
        {
            try
            {
                string type = string.Empty;
                try
                {
                    type = Request.ServerVariables.Get("Remote_Addr").ToString() + "卐" + Request.ServerVariables.Get("Remote_Host").ToString();
                }
                catch (Exception ex)
                {
                    type = ex.Message;
                }
                List<Search> recordList = SearchDAL.getRecord(Model, Mt, OPID, type);
                List<Search> resultList = recordList.Skip((rows * page - rows)).Take(rows).ToList<Search>();
                return Json(new { total = recordList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }


        [HttpPost]
        public ActionResult DownloadZip(string model,string idStr, string pathStr)
        {

            RespEntity result = new RespEntity();
           
            string timeStr = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            string zipFileName = timeStr + ".zip";
          
            string imgDirectory = Server.MapPath("~/ImgBuffer/" + timeStr + "/");
         
            string excelReadPath = imgDirectory + timeStr + ".xls";
            string zipRealPath = Server.MapPath("~/ZipFile/") + zipFileName;
            if (!Directory.Exists(imgDirectory)) Directory.CreateDirectory(imgDirectory);
            if (!Directory.Exists(zipRealPath)) Directory.CreateDirectory(Server.MapPath("~/ZipFile/"));
            this.ClearBuffer();//清除今天之前的记录（记录保存一天）
            string fileName = string.Empty;
            string fileRealPath = string.Empty;
            try
            { 
                if (!Directory.Exists(imgDirectory)) Directory.CreateDirectory(imgDirectory);
                if (!Directory.Exists(Server.MapPath("~/ZipFile/"))) Directory.CreateDirectory(Server.MapPath("~/ZipFile/"));
                ZipHelper zip = new ZipHelper();
                List<string> realFilePathList = new List<string>();
                List<string> fileNameList = new List<string>();
                DataTable table = new DataTable();
                table.Columns.Add("Id");
                table.Columns.Add("FileName");
                table.Columns.Add("XPath");
                DataRow row = null;
                string[] idArr = idStr.Split(',');
                string[] picArr = pathStr.Split(',');
                for (int i = 0; i < picArr.Length; i++)
                {
                    if (string.IsNullOrEmpty(picArr[i])) continue;
                    fileName = picArr[i].Substring(picArr[i].LastIndexOf("/") + 1);
                    fileRealPath = zip.DownHttpImg(picArr[i], imgDirectory, fileName);
                    realFilePathList.Add(fileRealPath);
                    fileNameList.Add(fileName);

                    row = table.NewRow();
                    row["Id"] = idArr[i];
                    row["FileName"] = fileName;
                    row["XPath"] = picArr[i];
                    table.Rows.Add(row);
                }
                //制作Excel
                List<ExcelEntity> recordList = SearchDAL.getExcel(model, table);
                ExcelHelper.CreateExcel(recordList, excelReadPath,"info");
                realFilePathList.Add(excelReadPath);
                fileNameList.Add(timeStr + ".xls");
                //打包文件
                zip.CreateZipFile(realFilePathList, fileNameList, zipRealPath);
                result.Code = 200;
                result.Message = "?zipPath=" + zipRealPath + "&zipName=" + zipFileName;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            return Json(result);
        }

        public ActionResult ReturnZip(string zipPath,string zipName) {
            return File(zipPath, "application/x-zip-compressed", zipName);
        }

        private void ClearBuffer() {
            string dirctory= Server.MapPath("~/ImgBuffer/");
            
            this.ClearBufferDetail(dirctory);

            dirctory = Server.MapPath("~/ZipFile/");
            this.ClearBufferDetail(dirctory);
        }
        private void ClearBufferDetail(string directory) {
            DirectoryInfo dir = new DirectoryInfo(directory);
            DirectoryInfo[] dirSub = dir.GetDirectories();
            DateTime now = DateTime.Now;
            int year = now.Year, month = now.Month, day = now.Day;
            DateTime indexTime = new DateTime(year, month, day);
            for (int i = 0; i < dirSub.Length; i++)
            {
                if (dirSub[i].CreationTime<indexTime)
                {
                    Directory.Delete(dirSub[i].FullName, true);
                }
            }
        }


        [HttpPost]
        public ActionResult DownloadAllForTime(
            string Model, string Line, string Mt, string Mac,
            string Location, string Judge,
            DateTime timeBegin, DateTime timeEnd,
            string Cameras) {
            string type = string.Empty;
            try
            {
                type = Request.ServerVariables.Get("Remote_Addr").ToString() + "卐" + Request.ServerVariables.Get("Remote_Host").ToString();
            }
            catch (Exception ex)
            {
                type = ex.Message;
            }
            List<Search> searchList = SearchDAL.getRecord(Model, Line, Mt, Mac, Location, Judge, timeBegin, timeEnd, type, Cameras);
            RespEntity result = this.getResultZipBySearchList(searchList);
            return Json(result);
        }

        [HttpPost]
        public ActionResult DownloadAllForOP(string Model, string Mt, string OPID)
        {
            string type = string.Empty;
            try
            {
                type = Request.ServerVariables.Get("Remote_Addr").ToString() + "卐" + Request.ServerVariables.Get("Remote_Host").ToString();
            }
            catch (Exception ex)
            {
                type = ex.Message;
            }
            List<Search> searchList = SearchDAL.getRecord(Model, Mt, OPID, type);
            RespEntity result = this.getResultZipBySearchList(searchList);
            return Json(result);
        }

        private RespEntity getResultZipBySearchList(List<Search> searchList) {
            RespEntity result = new RespEntity();
            try
            {
                List<ExcelEntity> excelList = new List<ExcelEntity>();
                ExcelEntity excel = null;
                if (searchList.Count >= 1000)
                {
                    result.Code = 500;
                    result.Message = "范围太大，请精准查询条件(1000)";
                }
                else
                {
                    ZipHelper zip = new ZipHelper();
                    string timeStr = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
                    string zipFileName = timeStr + ".zip";
                    List<string> realFilePathList = new List<string>();
                    List<string> fileNameList = new List<string>();
                    string imgDirectory = Server.MapPath("~/ImgBuffer/" + timeStr + "/");
                    string excelReadPath = imgDirectory + timeStr + ".xls";
                    string zipRealPath = Server.MapPath("~/ZipFile/") + zipFileName;
                    string fileName = string.Empty;
                    string fileRealPath = string.Empty;
                    foreach (var item in searchList)
                    {
                        excel = new ExcelEntity();
                        excel.Id = item.id;
                        excel.OPID = item.opid;
                        excel.Judge = item.judge == 0 ? "NG" : "OK";
                        excel.Line = item.cellid.ToString();
                        excel.MtNo = item.mtid.ToString();
                        excel.MacName = item.macname;
                        excel.Model = item.model;
                        excel.WorkStation = item.wsname;
                        excel.Part = item.part;
                        excel.CreateTime = bitIntTimeToString(item.inputtime.ToString());
                        excel.Extend1 = "";
                        excel.Extend2 = "";
                        fileName = item.xpath.Substring(item.xpath.LastIndexOf("/") + 1);
                        excel.XPath = item.xpath;
                        excel.FileName = fileName;
                        fileRealPath = zip.DownHttpImg(item.xpathDownload, imgDirectory, fileName);
                        realFilePathList.Add(fileRealPath);
                        fileNameList.Add(fileName);
                        excelList.Add(excel);
                    }
                    ExcelHelper.CreateExcel(excelList, excelReadPath, "info");
                    realFilePathList.Add(excelReadPath);
                    fileNameList.Add(timeStr + ".xls");
                    zip.CreateZipFile(realFilePathList, fileNameList, zipRealPath);
                    result.Code = 200;
                    result.Message = "?zipPath=" + zipRealPath + "&zipName=" + zipFileName;
                }
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            return result;
        }
        private string bitIntTimeToString(string timeInt) {
            string result = string.Empty;
            if (string.IsNullOrEmpty(timeInt))
            {
                result = "2021-01-01 01:01:01.000";
            }
            else
            {
                result = "20" + timeInt.Substring(0, 2) + "-" +
                    timeInt.Substring(2, 2) + "-" +
                    timeInt.Substring(4, 2) + " " +
                    timeInt.Substring(6, 2) + ":" +
                    timeInt.Substring(8, 2) + ":" +
                    timeInt.Substring(10, 2) + "." +
                    timeInt.Substring(12);
            }
            return result;
        }
    }
}
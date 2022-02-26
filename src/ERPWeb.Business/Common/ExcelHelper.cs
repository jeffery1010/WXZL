using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPWeb.Entity.IMGX;
using NPOI.SS.UserModel;

namespace ERPWeb.Business.IMGX
{
    public class ExcelHelper
    {
        public static void CreateExcel(List<ExcelEntity> list, string excelRealPath,string sheetName) {
            new ToExcelTemplate(excelRealPath, sheetName).ExportTemplate(list);
        }
    }
}

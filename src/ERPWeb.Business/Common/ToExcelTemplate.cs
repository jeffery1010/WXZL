using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPWeb.Entity.IMGX;

namespace ERPWeb.Business.IMGX
{
    public class ToExcelTemplate:ExcelBaseNPOI
    {
        private ToExcelTemplate() { }
        public ToExcelTemplate(string filePath, string sheetName = "") : base(filePath, sheetName) { }

        public static string FileName
        {
            get { return "ToDoList模板"; }
        }

        #region 重写样式 字典
        protected override Dictionary<string, ICellStyle> createCellStyles(IWorkbook wb)
        {
            //return base.createCellStyles(wb);
            Dictionary<string, ICellStyle> styles = new Dictionary<string, ICellStyle>();
            IDataFormat df = wb.CreateDataFormat();
            NPOI.SS.UserModel.IFont MsFont_9_Black_Font = wb.CreateFont();
            MsFont_9_Black_Font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;
            SetFontHeight(MsFont_9_Black_Font, 9);
            MsFont_9_Black_Font.FontName = "微软雅黑";

            NPOI.SS.UserModel.IFont MsFont_9_Blue_Font = wb.CreateFont();
            MsFont_9_Blue_Font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;
            MsFont_9_Blue_Font.Color = NPOI.HSSF.Util.HSSFColor.Blue.Index;
            SetFontHeight(MsFont_9_Blue_Font, 9);
            MsFont_9_Blue_Font.FontName = "微软雅黑";

            NPOI.SS.UserModel.IFont MsFont_9_Red_Font = wb.CreateFont();
            MsFont_9_Red_Font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;
            MsFont_9_Red_Font.Color = NPOI.HSSF.Util.HSSFColor.Red.Index;
            SetFontHeight(MsFont_9_Red_Font, 9);
            MsFont_9_Red_Font.FontName = "微软雅黑";


            NPOI.SS.UserModel.ICellStyle cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(MsFont_9_Black_Font);
            cellStyle.WrapText = (true);
            cellStyle.IsLocked = false;
            styles.Add("微黑_9_V中H中_换行_全框细", cellStyle);

            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(MsFont_9_Black_Font);
            cellStyle.WrapText = (true);
            cellStyle.IsLocked = false;
            styles.Add("微黑_9_V中H右_换行_全框细", cellStyle);

            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(MsFont_9_Blue_Font);
            cellStyle.WrapText = (true);
            cellStyle.IsLocked = false;
            styles.Add("微黑_9_V中H左_换行_全框细_蓝字", cellStyle);


            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(MsFont_9_Black_Font);
            cellStyle.WrapText = (true);
            cellStyle.IsLocked = true;  //上锁
            styles.Add("微黑_9_V中H中_换行_全框细_上锁", cellStyle);

            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(MsFont_9_Red_Font);
            cellStyle.WrapText = (true);
            cellStyle.IsLocked = false;
            styles.Add("微黑_红9_V中H中_换行_全框细", cellStyle);

            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(MsFont_9_Black_Font);
            cellStyle.WrapText = (true);
            cellStyle.IsLocked = false;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.PaleBlue.Index;
            styles.Add("微黑_9_V中H中_换行_全框细_蓝背景", cellStyle);

            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(MsFont_9_Black_Font);
            cellStyle.WrapText = (true);
            cellStyle.IsLocked = true;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.PaleBlue.Index;
            styles.Add("微黑_9_V中H中_换行_全框细_蓝背景_上锁", cellStyle);

            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(MsFont_9_Blue_Font);
            cellStyle.WrapText = (true);
            cellStyle.IsLocked = true;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.PaleBlue.Index;
            styles.Add("微黑_9蓝_V中H中_换行_全框细_蓝背景_上锁", cellStyle);

            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(MsFont_9_Black_Font);
            cellStyle.WrapText = (true);
            cellStyle.IsLocked = false;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.BrightGreen.Index;
            styles.Add("微黑_9_V中H右_换行_全框细_绿背景", cellStyle);

            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(MsFont_9_Black_Font);
            cellStyle.WrapText = (true);
            cellStyle.IsLocked = false;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LemonChiffon.Index;
            styles.Add("微黑_9_V中H中_换行_全框细_柠檬黄背景", cellStyle);

            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(MsFont_9_Black_Font);
            cellStyle.WrapText = (true);
            cellStyle.IsLocked = false;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LemonChiffon.Index;
            styles.Add("微黑_9_V中H右_换行_全框细_柠檬黄背景", cellStyle);

            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(MsFont_9_Black_Font);
            cellStyle.WrapText = (true);
            cellStyle.IsLocked = true;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LemonChiffon.Index;
            styles.Add("微黑_9_V中H中_换行_全框细_柠檬黄背景_上锁", cellStyle);

            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(MsFont_9_Black_Font);
            cellStyle.WrapText = (false);
            cellStyle.IsLocked = false;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.DataFormat = (df.GetFormat("m-d h:mm"));
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LemonChiffon.Index;
            styles.Add("微黑_9_V中H中_不换行_全框细_柠檬黄背景_日期", cellStyle);

            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(MsFont_9_Black_Font);
            cellStyle.WrapText = (true);
            cellStyle.IsLocked = false;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightTurquoise.Index;
            styles.Add("微黑_9_V中H中_换行_全框细_淡蓝背景", cellStyle);


            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(MsFont_9_Black_Font);
            cellStyle.WrapText = (false);
            cellStyle.IsLocked = false;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightTurquoise.Index;
            cellStyle.DataFormat = (df.GetFormat("m-d h:mm"));
            styles.Add("微黑_9_V中H中_不换行_全框细_淡蓝背景_日期", cellStyle);


            NPOI.SS.UserModel.IFont Bold_13_Black_Font = wb.CreateFont();
            Bold_13_Black_Font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            Bold_13_Black_Font.FontHeight = 13 * 20;      //字体高度，XLSX下有字体大小问题
            //Bold_15_Black_Font.FontHeightInPoints = 10; //字体高度，XLS下有大小问题
            Bold_13_Black_Font.FontName = "黑体";

            NPOI.SS.UserModel.IFont Bold_15_Black_Font = wb.CreateFont();
            Bold_15_Black_Font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            Bold_15_Black_Font.FontHeight = 15 * 20;      //字体高度，XLSX下有字体大小问题
            //Bold_15_Black_Font.FontHeightInPoints = 10; //字体高度，XLS下有大小问题
            Bold_15_Black_Font.FontName = "黑体";

            NPOI.SS.UserModel.IFont MsFont_12_Black_Font = wb.CreateFont();
            MsFont_12_Black_Font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;
            SetFontHeight(MsFont_12_Black_Font, 12);
            MsFont_12_Black_Font.FontName = "微软雅黑";

            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(Bold_13_Black_Font);
            //cellStyle.WrapText = (false);//换行
            styles.Add("Bold_13_Black_Font", cellStyle);

            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(Bold_15_Black_Font);
            //cellStyle.WrapText = (false);
            styles.Add("Bold_15_Black_Font", cellStyle);


            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(MsFont_12_Black_Font);
            //cellStyle.WrapText = (true);//换行
            cellStyle.IsLocked = true;
            styles.Add("微黑_12_V中H中_全框细_上锁", cellStyle);

            return styles;
        }
        #endregion

        #region 下载模板
        public bool ExportTemplate(List<ExcelEntity> recordList)
        {
            #region Sheet基本设置
            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.CreateSheet(ExcelMainSheetName);
            Dictionary<String, ICellStyle> styles = createCellStyles(hssfworkbook);
            if (IsXlsx == false)
                sheet.TabColorIndex = NPOI.HSSF.Util.HSSFColor.CornflowerBlue.Index;
            NPOI.SS.UserModel.IPrintSetup printSetup = sheet.PrintSetup;
            sheet.DisplayGridlines = false;
            SetDefaultRowHeight(sheet, 28.0F);

            sheet.FitToPage = (true);
            sheet.HorizontallyCenter = (true);  //设置Sheet缩放
            sheet.SetZoom(82, 100);              // 100 percent magnification
            SetColumnWidth(sheet, 00, 25.00);
            SetColumnWidth(sheet, 01, 25.00);
            SetColumnWidth(sheet, 02, 25.00);
            SetColumnWidth(sheet, 03, 25.00);
            SetColumnWidth(sheet, 04, 25.00);
            SetColumnWidth(sheet, 05, 25.00);
            SetColumnWidth(sheet, 06, 25.00);
            SetColumnWidth(sheet, 07, 25.00);
            SetColumnWidth(sheet, 08, 25.00);
            SetColumnWidth(sheet, 09, 25.00);
            SetColumnWidth(sheet, 10, 25.00);

            sheet.SetColumnHidden(24, true);
            #endregion

            AddDropDownDataValidation(sheet, 1, 65535, 7, 7, new string[] { "量试", "量产", "实验" });

            IRow curRow = sheet.CreateRow(0);
            ICell curCel = null;
            SetRowHeight(curRow, 33);//第一行行高
            #region 第一行 列头
            curCel = curRow.CreateCell(0);
            SetColumnWidth(sheet, 00, 25.00);
            curCel.SetCellValue("OPID");
            curCel.CellStyle = styles["Bold_13_Black_Font"];

            curCel = curRow.CreateCell(1);
            SetColumnWidth(sheet, 01, 15.00);
            curCel.SetCellValue("判定结果");
            curCel.CellStyle = styles["Bold_13_Black_Font"];

            curCel = curRow.CreateCell(2);
            SetColumnWidth(sheet, 02, 15.00);
            curCel.SetCellValue("线别");
            curCel.CellStyle = styles["Bold_13_Black_Font"];

            curCel = curRow.CreateCell(3);
            SetColumnWidth(sheet, 03, 15.00);
            curCel.SetCellValue("机台");
            curCel.CellStyle = styles["Bold_13_Black_Font"];

            curCel = curRow.CreateCell(4);
            SetColumnWidth(sheet, 04, 15.00);
            curCel.SetCellValue("机种");
            curCel.CellStyle = styles["Bold_13_Black_Font"];

            curCel = curRow.CreateCell(5);
            SetColumnWidth(sheet, 05, 20.00);
            curCel.SetCellValue("工位");
            curCel.CellStyle = styles["Bold_13_Black_Font"];

            curCel = curRow.CreateCell(6);
            SetColumnWidth(sheet, 06, 15.00);
            curCel.SetCellValue("拍照部位");
            curCel.CellStyle = styles["Bold_13_Black_Font"];

            curCel = curRow.CreateCell(7);
            SetColumnWidth(sheet, 07, 25.00);
            curCel.SetCellValue("画像链接");
            curCel.CellStyle = styles["Bold_13_Black_Font"];

            curCel = curRow.CreateCell(8);
            SetColumnWidth(sheet, 08, 30.00);
            curCel.SetCellValue("图片保存时间");
            curCel.CellStyle = styles["Bold_13_Black_Font"];

            curCel = curRow.CreateCell(9);
            SetColumnWidth(sheet, 09, 10.00);
            curCel.SetCellValue("拓展1");
            curCel.CellStyle = styles["Bold_13_Black_Font"];

            curCel = curRow.CreateCell(10);
            SetColumnWidth(sheet, 10, 10.00);
            curCel.SetCellValue("拓展2");
            curCel.CellStyle = styles["Bold_13_Black_Font"];
            #endregion

            int row = 1;
            int col = 0;
            foreach (ExcelEntity item in recordList)
            {
                col = 0;//重新初始化
                curRow = sheet.CreateRow(row++);

                curCel = curRow.CreateCell(col++);
                curCel.SetCellValue(item.OPID);
                curCel.CellStyle = styles["微黑_12_V中H中_全框细_上锁"];

                curCel = curRow.CreateCell(col++);
                curCel.SetCellValue(item.Judge);
                curCel.CellStyle = styles["微黑_12_V中H中_全框细_上锁"];

                curCel = curRow.CreateCell(col++);
                curCel.SetCellValue(item.Line);
                curCel.CellStyle = styles["微黑_12_V中H中_全框细_上锁"];

                curCel = curRow.CreateCell(col++);
                curCel.SetCellValue(item.MtNo);
                curCel.CellStyle = styles["微黑_12_V中H中_全框细_上锁"];

                curCel = curRow.CreateCell(col++);
                curCel.SetCellValue(item.Model);
                curCel.CellStyle = styles["微黑_12_V中H中_全框细_上锁"];

                curCel = curRow.CreateCell(col++);
                curCel.SetCellValue(item.WorkStation);
                curCel.CellStyle = styles["微黑_12_V中H中_全框细_上锁"];

                curCel = curRow.CreateCell(col++);
                curCel.SetCellValue(item.Part);
                curCel.CellStyle = styles["微黑_12_V中H中_全框细_上锁"];

                curCel = curRow.CreateCell(col++);
                curCel.SetCellValue(item.FileName);
                curCel.CellStyle = styles["微黑_12_V中H中_全框细_上锁"];
                HSSFHyperlink link = new HSSFHyperlink(HyperlinkType.Url);
                link.Address = item.FileName;
                curCel.Hyperlink = link;

                curCel = curRow.CreateCell(col++);
                curCel.SetCellValue(item.CreateTime);
                curCel.CellStyle = styles["微黑_12_V中H中_全框细_上锁"];

                curCel = curRow.CreateCell(col++);
                curCel.SetCellValue(item.Extend1);
                curCel.CellStyle = styles["微黑_12_V中H中_全框细_上锁"];

                curCel = curRow.CreateCell(col++);
                curCel.SetCellValue(item.Extend2);
                curCel.CellStyle = styles["微黑_12_V中H中_全框细_上锁"];
            }

            return WriteToFile(this.ExcelFilePath);
        }
        #endregion
    }
}

using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Business.IMGX
{
    public class ExcelBaseNPOI
    {
        protected NPOI.SS.UserModel.IWorkbook hssfworkbook;
        protected string ExcelFilePath { get; set; }
        protected string ExcelMainSheetName { get; set; }
        protected bool IsXlsx { get; set; } //true：Excel2007 Xlsx格式，False：Excel2003,Xls格式
        public ExcelBaseNPOI() { }
        public ExcelBaseNPOI(string filePath, string sheetName = "")
        {
            this.ExcelFilePath = filePath;
            this.ExcelMainSheetName = sheetName;

            #region 版本工厂化hssfworkbook           
            if (filePath.IndexOf(".xlsx") > 0) // 2007版本
            {
                this.IsXlsx = true;
                InitializeWorkbook(this.IsXlsx);
            }
            else if (filePath.IndexOf(".xls") > 0) // 2003版本
            {
                this.IsXlsx = false;
                InitializeWorkbook(this.IsXlsx);
            }
            else
                throw new InvalidOperationException("操作错误！");
            #endregion
        }

        #region 示例创建样式
        protected virtual Dictionary<String, NPOI.SS.UserModel.ICellStyle> createCellStyles(NPOI.SS.UserModel.IWorkbook wb)
        {
            Dictionary<String, NPOI.SS.UserModel.ICellStyle> styles = new Dictionary<String, NPOI.SS.UserModel.ICellStyle>();

            IDataFormat df = wb.CreateDataFormat();

            //IQC带框正常字体的单元格样式---------------------------------------------------------------
            NPOI.SS.UserModel.IFont Bold_11_Black_Font = wb.CreateFont();
            Bold_11_Black_Font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            Bold_11_Black_Font.FontHeight = 11 * 20;      //字体高度，XLSX下有字体大小问题
            //Bold_11_Black_Font.FontHeightInPoints = 10; //字体高度，XLS下有大小问题
            Bold_11_Black_Font.FontName = "黑体";

            //Bold_11_Black_Font.IsBold = false;                          //True粗体
            //Bold_11_Black_Font.IsItalic = false;                        //是否斜体
            //Bold_11_Black_Font.Underline = FontUnderlineType.Single;//下划线样式，无下划线None,单下划线Single,双下划线Double会计用单下划线SingleAccounting,会计用双下划线Doubleaccounting
            //Bold_11_Black_Font.TypeOffset = FontSuperScript.Sub;//字体上标下标，默认None,上标Sub,下标Super
            //Bold_11_Black_Font.IsStrikeout = true;  //True删除线

            NPOI.SS.UserModel.ICellStyle cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(Bold_11_Black_Font);
            cellStyle.WrapText = (false);
            styles.Add("黑体_粗11_V中H中_全框细", cellStyle);


            cellStyle = wb.CreateCellStyle();
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CenterSelection;        //跨列居中
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(Bold_11_Black_Font);
            cellStyle.WrapText = (false);
            styles.Add("黑体_粗11_不换行_V中H中_无框", cellStyle);


            NPOI.SS.UserModel.IFont Normal_11_Black_Font = wb.CreateFont();
            Normal_11_Black_Font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;
            Normal_11_Black_Font.FontHeight = 11 * 20;
            Normal_11_Black_Font.FontName = "黑体";

            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(Normal_11_Black_Font);
            cellStyle.WrapText = (false);
            cellStyle.DataFormat = (df.GetFormat("yyyy-MM-dd HH:mm:ss"));
            styles.Add("黑体_11_V中H中_全框细", cellStyle);



            bool isXlsx = wb is NPOI.XSSF.UserModel.XSSFWorkbook;
            NPOI.SS.UserModel.IFont Normal_11_Blue_Font = wb.CreateFont();
            Normal_11_Blue_Font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;
            Normal_11_Blue_Font.FontHeight = 11 * 20;
            Normal_11_Blue_Font.FontName = "黑体";
            Normal_11_Blue_Font.IsItalic = true;
            if (!isXlsx)
                Normal_11_Blue_Font.Color = NPOI.HSSF.Util.HSSFColor.DarkBlue.Index;

            cellStyle = CreateBorderedStyle(wb, BorderStyle.Thin);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            //cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.DarkBlue.Index;
            // cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(Normal_11_Blue_Font);
            cellStyle.WrapText = (false);
            styles.Add("黑体_11_V中H中_全框细_链接", cellStyle);

            NPOI.SS.UserModel.IFont font_Normal_9_HeiTi = wb.CreateFont();
            font_Normal_9_HeiTi.Boldweight = (short)FontBoldWeight.Normal;
            font_Normal_9_HeiTi.FontHeight = 9 * 20;
            font_Normal_9_HeiTi.FontName = "黑体";

            cellStyle = wb.CreateCellStyle();
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CenterSelection;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(font_Normal_9_HeiTi);
            cellStyle.BorderLeft = BorderStyle.Medium;
            cellStyle.LeftBorderColor = (IndexedColors.Black.Index);
            cellStyle.BorderTop = BorderStyle.Medium;
            cellStyle.TopBorderColor = (IndexedColors.Black.Index);
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BottomBorderColor = (IndexedColors.Black.Index);
            styles.Add("黑体_9_不换行_V中H中_LmTmR无Bt黑框", cellStyle);

            //IQC带框正常字体的单元格样式---------------------------------------------------------------
            NPOI.SS.UserModel.IFont normalCellFont = wb.CreateFont();
            normalCellFont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;
            normalCellFont.FontHeight = 13 * 20;
            normalCellFont.FontName = "宋体";

            NPOI.SS.UserModel.ICellStyle vCellStyle = CreateVBorderStyle(wb);
            NPOI.SS.UserModel.IFont vCellFont = wb.CreateFont();
            vCellFont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            vCellFont.FontHeight = 10 * 20;
            vCellFont.FontName = "楷体";
            vCellFont.Color = (short)HSSFColor.Red.Index;
            vCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            vCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            vCellStyle.SetFont(vCellFont);
            vCellStyle.WrapText = (false);
            IDataFormat dataformat = wb.CreateDataFormat();
            vCellStyle.DataFormat = dataformat.GetFormat("yyyy-MM-dd");
            styles.Add("IQC垂直框居中不换行正常10红字粗体", vCellStyle);


            cellStyle = CreateBorderedStyle(wb);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(normalCellFont);
            cellStyle.WrapText = (false);
            cellStyle.FillPattern = NPOI.SS.UserModel.FillPattern.LessDots; //图案样式 FineDots细点，SolidForeground立体前景，isAddFillPattern=True时存在
            //背景颜色
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
            cellStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
            styles.Add("IQC带框居中不换行正常10网格", cellStyle);

            IFont boldCellFont = wb.CreateFont();
            boldCellFont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            boldCellFont.FontHeight = 36 * 20;
            boldCellFont.FontName = "黑体";

            cellStyle = CreateBorderedStyle(wb);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(boldCellFont);
            cellStyle.WrapText = (true);
            cellStyle.FillPattern = NPOI.SS.UserModel.FillPattern.LessDots;
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
            cellStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
            styles.Add("IQC带框居中换行粗体11_网格", cellStyle);


            cellStyle = CreateBorderedStyle(wb);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(normalCellFont);
            cellStyle.WrapText = (false);
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            styles.Add("IQC带框居中不换行正常10灰25", cellStyle);


            cellStyle = CreateBorderedStyle(wb);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(boldCellFont);    //LIGHT_GREEN
            cellStyle.FillForegroundColor = (NPOI.SS.UserModel.IndexedColors.LightGreen.Index);
            cellStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;
            cellStyle.WrapText = (true);
            styles.Add("绿色粗体边框", cellStyle);

            cellStyle = CreateBorderedStyle(wb);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(boldCellFont);    //LIGHT_GREEN
            cellStyle.FillForegroundColor = (NPOI.SS.UserModel.IndexedColors.LightGreen.Index);
            cellStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;
            cellStyle.WrapText = (true);
            styles.Add("绿色粗体居中边框", cellStyle);

            //黑细边框 粗体 居中 淡蓝背景 时间
            cellStyle = CreateBorderedStyle(wb);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.FillForegroundColor = (IndexedColors.LightCornflowerBlue.Index);
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.SetFont(boldCellFont);
            cellStyle.DataFormat = (df.GetFormat("yyyy-MM-dd HH:mm:ss"));
            styles.Add("BoldCenterLightBlueDateTime", cellStyle);

            cellStyle = CreateBorderedStyle(wb);
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.SetFont(boldCellFont);
            cellStyle.FillForegroundColor = (IndexedColors.Grey25Percent.Index);
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.DataFormat = (df.GetFormat("yyyy-MM-dd"));
            styles.Add("BoldCenterGrayDateTime", cellStyle);


            //粗体 居中 48号字体 蓝色字体
            NPOI.SS.UserModel.IFont titleFont = wb.CreateFont();
            titleFont.FontHeightInPoints = ((short)48);
            titleFont.Color = (IndexedColors.DarkBlue.Index);
            cellStyle = wb.CreateCellStyle();
            cellStyle.Alignment = (NPOI.SS.UserModel.HorizontalAlignment.Center);
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellStyle.SetFont(titleFont);
            styles.Add("BoldCenterBlueFont48", cellStyle);

            //粗体 居中 12字体 白色字体 深蓝背景
            NPOI.SS.UserModel.IFont monthFont = wb.CreateFont();
            monthFont.FontHeightInPoints = ((short)12);
            monthFont.Color = (IndexedColors.White.Index);
            monthFont.Boldweight = (short)(FontBoldWeight.Bold);
            cellStyle = wb.CreateCellStyle();
            cellStyle.Alignment = (NPOI.SS.UserModel.HorizontalAlignment.Center);
            cellStyle.VerticalAlignment = (NPOI.SS.UserModel.VerticalAlignment.Center);
            cellStyle.FillForegroundColor = (IndexedColors.DarkBlue.Index);
            cellStyle.FillPattern = (FillPattern.SolidForeground);
            cellStyle.SetFont(monthFont);
            styles.Add("BoldCenterWhiteFont12DarkBlue", cellStyle);

            short borderColor = IndexedColors.Grey50Percent.Index;
            //粗体 左顶 14字体 薇蓝背景 细边框
            NPOI.SS.UserModel.IFont dayFont = wb.CreateFont();
            dayFont.FontHeightInPoints = ((short)14);
            dayFont.Boldweight = (short)(FontBoldWeight.Bold);
            cellStyle = wb.CreateCellStyle();
            cellStyle.Alignment = (NPOI.SS.UserModel.HorizontalAlignment.Left);
            cellStyle.VerticalAlignment = (NPOI.SS.UserModel.VerticalAlignment.Top);
            cellStyle.FillForegroundColor = (IndexedColors.LightCornflowerBlue.Index);
            cellStyle.FillPattern = (FillPattern.SolidForeground);
            cellStyle.BorderLeft = (BorderStyle.Thin);
            cellStyle.LeftBorderColor = (borderColor);
            cellStyle.BorderBottom = (BorderStyle.Thin);
            cellStyle.BottomBorderColor = (borderColor);
            cellStyle.SetFont(dayFont);
            styles.Add("BoldLetTopFont14LightCornflowerBlue", cellStyle);



            return styles;
        }
        protected NPOI.SS.UserModel.ICellStyle CreateBorderedStyle(NPOI.SS.UserModel.IWorkbook wb,
            NPOI.SS.UserModel.BorderStyle defaltBorderStyle = NPOI.SS.UserModel.BorderStyle.Thin)
        {
            NPOI.SS.UserModel.ICellStyle style = wb.CreateCellStyle();
            style.BorderRight = defaltBorderStyle;
            style.BorderBottom = defaltBorderStyle;
            style.BorderLeft = defaltBorderStyle;
            style.BorderTop = defaltBorderStyle;
            style.RightBorderColor = (NPOI.SS.UserModel.IndexedColors.Black.Index);
            style.BottomBorderColor = (NPOI.SS.UserModel.IndexedColors.Black.Index);
            style.LeftBorderColor = (NPOI.SS.UserModel.IndexedColors.Black.Index);
            style.TopBorderColor = (NPOI.SS.UserModel.IndexedColors.Black.Index);
            return style;
        }
        //仅创建垂直方向有线条的单元格样式
        private NPOI.SS.UserModel.ICellStyle CreateVBorderStyle(NPOI.SS.UserModel.IWorkbook wb)
        {
            NPOI.SS.UserModel.ICellStyle style = wb.CreateCellStyle();
            style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style.RightBorderColor = (NPOI.SS.UserModel.IndexedColors.Black.Index);
            style.LeftBorderColor = (NPOI.SS.UserModel.IndexedColors.Black.Index);
            return style;
        }
        #endregion

        #region 单元格换行
        protected static void ReSizeColumnWidth(ISheet sheet, ICell cell)
        {
            int cellLength = (Encoding.Default.GetBytes(cell.ToString()).Length + 2) * 256;
            const int maxLength = 60 * 256; //255 * 256;
            if (cellLength > maxLength) //当单元格内容超过30个中文字符（英语60个字符）宽度，则强制换行
            {
                cellLength = maxLength;
                cell.CellStyle.WrapText = true;
            }
            int colWidth = sheet.GetColumnWidth(cell.ColumnIndex);
            if (colWidth < cellLength)
            {
                sheet.SetColumnWidth(cell.ColumnIndex, cellLength);
            }
        }
        #endregion

        #region 示例Excel写入导出
        private bool Export(DataTable dtMainTable)
        {
            #region Sheet基本设置
            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.CreateSheet(ExcelMainSheetName);
            Dictionary<String, ICellStyle> styles = createCellStyles(hssfworkbook);
            sheet.TabColorIndex = NPOI.HSSF.Util.HSSFColor.CornflowerBlue.Index;
            NPOI.SS.UserModel.IPrintSetup printSetup = sheet.PrintSetup;
            sheet.DisplayGridlines = false;
            sheet.DefaultRowHeight = sheet.DefaultRowHeight = 18 * 20;
            sheet.FitToPage = (true);
            sheet.HorizontallyCenter = (true);  //设置Sheet缩放
            sheet.SetZoom(10, 10);              // 100 percent magnification
            sheet.SetColumnWidth(00, (int)((12.63 + 0.72) * 256));
            sheet.SetColumnWidth(01, (int)((09.5 + 0.72) * 256));
            #endregion


            IRow row = sheet.CreateRow(1);
            row.Height = (int)(15.75 * 20); //行高        XLSX下会有问题
            //row.HeightInPoints = 50;        //行高        XLS会有问题
            ICell cell = row.CreateCell(1);

            #region 第一行内容
            cell.SetCellValue("工程名称");
            cell.CellStyle = styles["黑体_粗11_V中H中_全框细"];

            cell = row.CreateCell(2);
            cell.SetCellValue("线束");
            cell.CellStyle = styles["黑体_粗11_V中H中_全框细"];
            cell = row.CreateCell(3);
            cell.CellStyle = styles["黑体_粗11_V中H中_全框细"];
            cell = row.CreateCell(4);
            cell.CellStyle = styles["黑体_粗11_V中H中_全框细"];
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(row.RowNum, row.RowNum, 2, 4));
            #endregion

            var pariarch = sheet.CreateDrawingPatriarch();  //HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch(); //HSSFTextbox 翻遍了源码没有发现 Padding 之类的属性
            #region NPOI备注
            string comment = "时间必填";
            NPOI.SS.UserModel.IComment cellComment = pariarch.CreateCellComment(this.IsXlsx == true
                        ? (IClientAnchor)new NPOI.XSSF.UserModel.XSSFClientAnchor(0, 0, 0, 0, cell.ColumnIndex, cell.RowIndex, cell.ColumnIndex + 1, cell.RowIndex + 1)
                        : (IClientAnchor)new NPOI.HSSF.UserModel.HSSFClientAnchor(0, 0, 0, 0, cell.ColumnIndex, cell.RowIndex, cell.ColumnIndex + 1, cell.RowIndex + 1));
            cellComment.String = this.IsXlsx == true                                //dx1dy1dx2dy2  col1  row1 col2 row2
                ? (IRichTextString)new NPOI.XSSF.UserModel.XSSFRichTextString(comment)
                : (IRichTextString)new NPOI.HSSF.UserModel.HSSFRichTextString(comment);
            cellComment.Author = "LTPLUS";
            cellComment.Visible = false;
            cell.CellComment = cellComment;
            #endregion

            #region 链接
            ICell efDataTitleDataCell = sheet.GetRow(1).CreateCell(5);
            efDataTitleDataCell.SetCellValue("返回");
            efDataTitleDataCell.CellStyle = styles["主Sheet_顶部_主标题_Link"];
            NPOI.SS.UserModel.IHyperlink hssfHyperlink2 = this.IsXlsx == true
                ? (NPOI.SS.UserModel.IHyperlink)new NPOI.XSSF.UserModel.XSSFHyperlink(HyperlinkType.Document)
                {
                    Address = (string.Format("'{0}'!A1", sheet.SheetName))
                }
                : (NPOI.SS.UserModel.IHyperlink)new NPOI.HSSF.UserModel.HSSFHyperlink(HyperlinkType.Document)
                {
                    Address = (string.Format("'{0}'!A1", sheet.SheetName))
                };


            efDataTitleDataCell.Hyperlink = hssfHyperlink2;

            efDataTitleDataCell = sheet.GetRow(2).CreateCell(5);
            efDataTitleDataCell.CellStyle = styles["主Sheet_顶部_主标题_Link"];

            efDataTitleDataCell = sheet.GetRow(3).CreateCell(5);
            efDataTitleDataCell.CellStyle = styles["主Sheet_顶部_主标题_Link"];

            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 3, 5, 5));
            #endregion

            #region 表达式
            //cellsubUnitBomSheet = rowsubUnitBomSheet.CreateCell(3);
            //cellsubUnitBomSheet.CellStyle = styles["无_无_无_无_黄绿虚线框"];
            //cellsubUnitBomSheet.SetCellFormula("VLOOKUP(B1,机种信息!A2:$B$1538,2,0)");

            //NPOI.SS.UserModel.IRow rowTar = sheet.GetRow(CopyExamRow16.RowNum + ScrapItemTable.Rows.Count + emptyRow);
            //cell = rowTar.GetCell(GetExcelColIndex("I"));
            //cell.SetCellFormula(string.Format("SUM(I{0}:I{1})", CopyExamRow16.RowNum + 1, CopyExamRow16.RowNum + ScrapItemTable.Rows.Count + emptyRow));
            #endregion

            #region 文本框
            ////HSSFTextbox 翻遍了源码没有发现 Padding 之类的属性
            //HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
            //HSSFTextbox textbox1 = (HSSFTextbox)patriarch.CreateTextbox(
            //        new HSSFClientAnchor(0, 0, 400, 0, (short)0, bottomRow2.RowNum, (short)2, bottomRow2.RowNum + 6));
            //textbox1.String.ApplyFont(normalCellFont);
            //textbox1.String = new HSSFRichTextString(" 卖方 & 发货人签名盖章");
            //textbox1.HorizontalAlignment = NPOI.HSSF.Record.HorizontalTextAlignment.Left;
            //textbox1.VerticalAlignment = NPOI.HSSF.Record.VerticalTextAlignment.Bottom;



            //HSSFTextbox textbox2 = (HSSFTextbox)patriarch.CreateTextbox(
            //        new HSSFClientAnchor(500, 0, 400, 0, (short)2, bottomRow2.RowNum, (short)4, bottomRow2.RowNum + 6));
            //textbox2.String.ApplyFont(normalCellFont);
            //textbox2.String = new HSSFRichTextString(" 司机签名\n 及运输公司盖章");
            //textbox2.HorizontalAlignment = NPOI.HSSF.Record.HorizontalTextAlignment.Left;
            //textbox2.VerticalAlignment = NPOI.HSSF.Record.VerticalTextAlignment.Bottom;

            //HSSFTextbox textbox3 = (HSSFTextbox)patriarch.CreateTextbox(
            //        new HSSFClientAnchor(0, 0, 900, 0, (short)5, bottomRow2.RowNum, (short)7, bottomRow2.RowNum + 6));
            //textbox3.String.ApplyFont(normalCellFont);
            //textbox3.String = new HSSFRichTextString(" 收货人签名盖章");
            //textbox3.HorizontalAlignment = NPOI.HSSF.Record.HorizontalTextAlignment.Left;
            //textbox3.VerticalAlignment = NPOI.HSSF.Record.VerticalTextAlignment.Bottom;




            //HSSFTextbox textbox4 = (HSSFTextbox)patriarch.CreateTextbox(
            //       new HSSFClientAnchor(330, 0, 500, 100, (short)2, bottomRow2.RowNum + 7, (short)4, bottomRow2.RowNum + 8));
            //textbox4.String = new HSSFRichTextString("SPECIFIED FOR SONY USE ONLY");
            //textbox4.HorizontalAlignment = NPOI.HSSF.Record.HorizontalTextAlignment.Center;
            //textbox4.VerticalAlignment = NPOI.HSSF.Record.VerticalTextAlignment.Center;
            #endregion

            #region 二维码和图片
            ////托运单导出Excxel 二维码
            //Gma.QrCodeNet.Encoding.QrEncoder qrEncoder = new Gma.QrCodeNet.Encoding.QrEncoder(Gma.QrCodeNet.Encoding.ErrorCorrectionLevel.H);
            //Gma.QrCodeNet.Encoding.QrCode qrCode = new Gma.QrCodeNet.Encoding.QrCode();
            //Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules QuietZones = Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules.Two;
            //qrEncoder.TryEncode("你的二维码", out qrCode);
            //var render = new Gma.QrCodeNet.Encoding.Windows.Render.GraphicsRenderer(new Gma.QrCodeNet.Encoding.Windows.Render.FixedModuleSize(5, QuietZones));
            //var ms = new MemoryStream();
            //render.WriteToStream(qrCode.Matrix, System.Drawing.Imaging.ImageFormat.Png, ms);
            ////footerRow = sheet.CreateRow(rowIndex);
            ////footerRow.Height = 200 * 20;
            ////第四步：设置锚点 （在起始单元格的X坐标0-1023，Y的坐标0-255，在终止单元格的X坐标0-1023，Y的坐标0-255，起始单元格列数,行数，终止单元格列数,行数）
            //IClientAnchor anchor = pariarch.CreateAnchor(0, 50, 0, 40, 7, 0, 8, 3);
            //int pictureIdx2 = hssfworkbook.AddPicture(ms.ToArray(), PictureType.PNG);
            //NPOI.SS.UserModel.IPicture pict = pariarch.CreatePicture(anchor, pictureIdx2);
            //ms.Dispose();
            //ms.Close();


            ////托运单Excel 索尼密函     //图片
            //ms = new MemoryStream();
            ////WPFApp.Properties.Resources.Sony_SecretLogo.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //anchor = pariarch.CreateAnchor(0, 37, 270, 57, 0, 0, 1, 1);
            //pictureIdx2 = hssfworkbook.AddPicture(ms.ToArray(), PictureType.PNG);
            //pict = pariarch.CreatePicture(anchor, pictureIdx2);
            //ms.Dispose();
            //ms.Close();
            #endregion

            #region 图片2
            //NPOI.SS.UserModel.IDrawing patriarch = sheet.CreateDrawingPatriarch();
            //IClientAnchor anchor = null;
            //NPOI.SS.UserModel.IPicture pict;
            ////做成人图片
            //if (!string.IsNullOrEmpty(execlIQCpre.SubmitUser))
            //{
            //    Bitmap sonyEmployeeStamp2 = WPFApp.ComContros.SonyDeptStamp.CreatSeal(execlIQCpre.Submit_Dept, execlIQCpre.SubmitUser, "    ", true);
            //    byte[] bytes2 = WPFApp.ComContros.SonyDeptStamp.Bitmap2Byte(sonyEmployeeStamp2, System.Drawing.Imaging.ImageFormat.Png);
            //    // Create the drawing patriarch.  This is the top level container for all shapes. 
            //    int pictureIdx2 = hssfworkbook.AddPicture(bytes2, PictureType.PNG);
            //    //第四步：设置锚点 （在起始单元格的X坐标0-1023，Y的坐标0-255，在终止单元格的X坐标0-1023，Y的坐标0-255，起始单元格列数,行数，终止单元格列数,行数）
            //    anchor = patriarch.CreateAnchor(226, 226, 820, 40, 6, 2, 7, 6);
            //    //第五步：创建图片
            //    pict = patriarch.CreatePicture(anchor, pictureIdx2);
            //}
            #endregion

            #region 列宽
            //适应列宽
            //sheet.SetColumnWidth(1, 12 * 256);
            //int col2WorldLength = categoryType.Rows[i]["Update_Date"].ToString().Length > 4
            //        ? categoryType.Rows[i]["Update_Date"].ToString().Length
            //        : 12;
            //sheet.SetColumnWidth(2, col2WorldLength * 256);
            //int col4WorldLength = "yyyy-MM-dd HH:mm:ss".Length > categoryType.Rows[i]["Remark"].ToString().Length
            //         ? "yyyy-MM-dd HH:mm:ss".Length
            //         : categoryType.Rows[i]["Remark"].ToString().Length;
            //sheet.SetColumnWidth(3, 12 * 256);
            //sheet.SetColumnWidth(4, col4WorldLength * 256);
            #endregion

            #region 行Group
            sheet.GroupRow(10, 15);
            #endregion

            #region 绘图
            //HSSFPatriarch patriarch = (HSSFPatriarch)sheetJoc1.CreateDrawingPatriarch();
            //HSSFClientAnchor a1 = new HSSFClientAnchor(0, 0, 0, 0, 4, 4, 6, 4);
            //HSSFSimpleShape line1 = patriarch.CreateSimpleShape(a1);
            //line1.ShapeType = HSSFSimpleShape.OBJECT_TYPE_LINE;
            //line1.LineStyle = HSSFShape.LINESTYLE_SOLID;
            ////在NPOI中线的宽度12700表示1pt,所以这里是0.5pt粗的线条。
            //line1.LineWidth = 6350;

            //a1 = new HSSFClientAnchor(0, 0, 0, 0, 4, 5, 6, 5);
            //line1 = patriarch.CreateSimpleShape(a1);
            //line1.ShapeType = HSSFSimpleShape.OBJECT_TYPE_LINE;
            //line1.LineStyle = HSSFShape.LINESTYLE_SOLID;
            ////在NPOI中线的宽度12700表示1pt,所以这里是0.5pt粗的线条。
            //line1.LineWidth = 6350;


            //a1 = new HSSFClientAnchor(800, 0, 400, 0, 9, 4, 12, 4);
            //line1 = patriarch.CreateSimpleShape(a1);
            //line1.ShapeType = HSSFSimpleShape.OBJECT_TYPE_LINE;
            //line1.LineStyle = HSSFShape.LINESTYLE_SOLID;
            ////在NPOI中线的宽度12700表示1pt,所以这里是0.5pt粗的线条。
            //line1.LineWidth = 6350;

            //a1 = new HSSFClientAnchor(800, 0, 400, 0, 9, 5, 12, 5);
            //line1 = patriarch.CreateSimpleShape(a1);
            //line1.ShapeType = HSSFSimpleShape.OBJECT_TYPE_LINE;
            //line1.LineStyle = HSSFShape.LINESTYLE_SOLID;
            ////在NPOI中线的宽度12700表示1pt,所以这里是0.5pt粗的线条。
            //line1.LineWidth = 6350;
            #endregion
            return WriteToFile(ExcelFilePath);
        }
        #endregion

        #region 垂直合并单元格
        //垂直合并单元格
        protected virtual void MergeCellVertical(NPOI.SS.UserModel.ISheet targetSheet, int startRowIndex, int endRowIndex, int colIndex)
        {
            string previousCellValue = "";
            int previousRowIndex = startRowIndex;
            for (int i = startRowIndex; i <= endRowIndex; i++)
            {
                IRow irow = targetSheet.GetRow(i);
                ICell icell = irow != null ? irow.GetCell(colIndex) : null;
                if (i == startRowIndex)
                {
                    previousCellValue = icell != null ? icell.StringCellValue : "";
                    previousRowIndex = startRowIndex;
                    continue;
                }
                string currentCellValue = icell != null ? icell.StringCellValue : "";
                if (previousCellValue.Equals(currentCellValue, StringComparison.OrdinalIgnoreCase))
                {
                    if (i == endRowIndex && i > (previousRowIndex + 1))//遍历到最后一行发现前面的Excel行存在多行
                    {
                        targetSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(previousRowIndex, i, colIndex, colIndex));
                    }
                }
                else if (i > (previousRowIndex + 1))//说明之间间隔  //i >  previousRowIndex - 1
                {
                    targetSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(previousRowIndex, i - 1, colIndex, colIndex));
                    previousCellValue = currentCellValue;
                    previousRowIndex = i;
                }
                else
                {
                    previousCellValue = currentCellValue;
                    previousRowIndex = i;
                }
            }
        }
        #endregion

        #region 初始化WorkBook
        protected void InitializeWorkbook(bool isXlsx, string companyName = "SNIX", string subject = "Excel Export 2020")
        {
            string createrUser = "ISaints";    //作者
            if (isXlsx)
            {
                hssfworkbook = new NPOI.XSSF.UserModel.XSSFWorkbook();
                NPOI.POIXMLProperties props = ((NPOI.XSSF.UserModel.XSSFWorkbook)hssfworkbook).GetProperties();
                props.CoreProperties.Creator = createrUser;
                props.CoreProperties.Created = DateTime.Now;
                props.CoreProperties.Subject = subject;
                if (!props.CustomProperties.Contains("NPOI Team"))
                    props.CustomProperties.AddProperty("NPOI Team", "Hello World!");
            }
            else
            {
                hssfworkbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
                //2.创建Excel文件的摘要信息(DocumentSummaryInformation)            
                NPOI.HPSF.DocumentSummaryInformation dsi = NPOI.HPSF.PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = companyName;//单位名       
                ((NPOI.HSSF.UserModel.HSSFWorkbook)hssfworkbook).DocumentSummaryInformation = dsi;

                //3.创建SummaryInformation
                NPOI.HPSF.SummaryInformation si = NPOI.HPSF.PropertySetFactory.CreateSummaryInformation();
                si.Subject = subject;           //主题
                si.Author = createrUser;
                si.CreateDateTime = DateTime.Now;    //创建时间
                ((NPOI.HSSF.UserModel.HSSFWorkbook)hssfworkbook).SummaryInformation = si;
            }
        }
        #endregion

        #region 写入到文件
        protected virtual bool WriteToFile(string fileName)
        {
            System.IO.FileStream file = null;
            try
            {
                file = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
                hssfworkbook.Write(file);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                    file.Dispose();
                }
            }
        }
        #endregion





        #region 设置ExcelRow高度
        protected void SetRowHeight(IRow targetRow, float rowHeight)
        {
            targetRow.Height = Convert.ToInt16(rowHeight * 20);
            //if (IsXlsx == true)
            //{
            //    targetRow.HeightInPoints = rowHeight;
            //}
            //else
            //{
            //    targetRow.Height = Convert.ToInt16(rowHeight * 20);
            //}
        }
        #endregion

        #region 设置字体高度
        protected void SetFontHeight(IFont targetFont, double fontHeight)
        {
            if (IsXlsx == true)
            {
                targetFont.FontHeightInPoints = Convert.ToInt16(fontHeight);
            }
            else
            {
                targetFont.FontHeight = fontHeight * 20;
            }
        }
        #endregion

        #region 单元格添加下拉控件
        protected void AddDropDownDataValidation(ISheet targetSheet, int firstRow, int lastRow, int firstCol, int lastCol,
            params string[] ValidateValues)
        {
            NPOI.SS.Util.CellRangeAddressList regions = new NPOI.SS.Util.CellRangeAddressList(firstRow, lastRow, firstCol, lastCol);
            if (IsXlsx == false)
            {
                NPOI.HSSF.UserModel.DVConstraint constraint = NPOI.HSSF.UserModel.DVConstraint.CreateExplicitListConstraint(ValidateValues);
                NPOI.HSSF.UserModel.HSSFDataValidation dataValidate = new NPOI.HSSF.UserModel.HSSFDataValidation(regions, constraint);
                targetSheet.AddValidationData(dataValidate);
            }
            else
            {
                NPOI.XSSF.UserModel.XSSFDataValidationHelper dvHelper = new NPOI.XSSF.UserModel.XSSFDataValidationHelper((NPOI.XSSF.UserModel.XSSFSheet)targetSheet);
                NPOI.XSSF.UserModel.XSSFDataValidationConstraint dvConstraint = (NPOI.XSSF.UserModel.XSSFDataValidationConstraint)dvHelper.CreateExplicitListConstraint(ValidateValues);
                NPOI.XSSF.UserModel.XSSFDataValidation validation = (NPOI.XSSF.UserModel.XSSFDataValidation)dvHelper.CreateValidation(dvConstraint, regions);
                targetSheet.AddValidationData(validation);
            }
        }
        #endregion

        #region 设置获得列宽
        protected void SetColumnWidth(ISheet sheet, int columnIndex, double width)
        {
            sheet.SetColumnWidth(columnIndex, (int)((width + 0.72) * 256));
            //if(IsXlsx)
            //{
            //    sheet.SetColumnWidth(columnIndex, Convert.ToInt32(width));
            //}
            //else
            //{
            //    sheet.SetColumnWidth(columnIndex, (int)((width + 0.72) * 256));
            //}
        }
        #endregion

        #region 设置默认的行高
        protected void SetDefaultRowHeight(ISheet sheet, float rowHeight)
        {
            sheet.DefaultRowHeight = Convert.ToInt16(rowHeight * 20);
            //if(IsXlsx ==false)
            //{
            //    sheet.DefaultRowHeight = Convert.ToInt16(rowHeight * 20);
            //}
            //else
            //{
            //    sheet.DefaultRowHeightInPoints = rowHeight;
            //}
        }
        #endregion
    }
}

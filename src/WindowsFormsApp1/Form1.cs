using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Export("");
            metafile1 = VectorPhoto.LoadVectorPhoto(emf);
            metafileDelegate = new Graphics.EnumerateMetafileProc(MetafileCallback);
        }
        private Metafile metafile1;
        private Graphics.EnumerateMetafileProc metafileDelegate;
       

        private bool MetafileCallback(EmfPlusRecordType recordType, int flags, int dataSize, IntPtr data, PlayRecordCallback callbackData)
        {
            byte[] dataArray = null;
            if (data != IntPtr.Zero)
            {

                dataArray = new byte[dataSize];
                Marshal.Copy(data, dataArray, 0, dataSize);
                //GdipComment
                metafile1.PlayRecord(recordType, flags, dataSize, dataArray);
                switch (recordType) //记录类型
                {

                    case EmfPlusRecordType.Object: //对象
                    case EmfPlusRecordType.DrawLines: //线
                    case EmfPlusRecordType.SetPageTransform: //设置页变换
                        break;
                }
            }
            Console.WriteLine(recordType.ToString());
            return true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            PointF destPoint = new PointF(e.ClipRectangle.X, e.ClipRectangle.Y);
           e.Graphics.EnumerateMetafile(metafile1, destPoint, metafileDelegate);

        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            metafile1.Dispose();
        }
        string emf { get; set; }
        /// <summary>
        /// 导出为 Emf 或 Wmf 文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否成功</returns>
        private bool Export(string filePath)
        {
           
            filePath = @"C:\Users\Administrator\Desktop\Debug\1.jpg";
            FileInfo fi = new FileInfo(filePath);
            emf = Path.GetDirectoryName(filePath) + "\\1.emf";
            fi.CopyTo(emf,true);
            try
            {
                Bitmap bmp = new Bitmap(2220, 2220);
                Graphics gs = Graphics.FromImage(bmp);
                Metafile mf = new Metafile(emf, gs.GetHdc());

                Graphics g = Graphics.FromImage(mf);
                //string sf = System.IO.Path.GetDirectoryName(filePath) + "1.wmf";
                //mf.Save(sf, ImageFormat.Wmf);
                Draw(g);
                g.Save();
                
                g.Dispose();
                mf.Dispose();

                return true;
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
                return false;
            }
        }
        /// <summary>
        /// 绘制图形
        /// </summary>
        /// <param name="g">用于绘图的Graphics对象</param>
        private void Draw(Graphics g)
        {
            HatchBrush hb = new HatchBrush(HatchStyle.LightUpwardDiagonal, Color.Black, Color.White);

            g.FillEllipse(Brushes.Gray, 10f, 10f, 200, 200);
            g.DrawEllipse(new Pen(Color.Black, 1f), 10f, 10f, 200, 200);

            g.FillEllipse(hb, 30f, 95f, 30, 30);
            g.DrawEllipse(new Pen(Color.Black, 1f), 30f, 95f, 30, 30);

            g.FillEllipse(hb, 160f, 95f, 30, 30);
            g.DrawEllipse(new Pen(Color.Black, 1f), 160f, 95f, 30, 30);

            g.FillEllipse(hb, 95f, 30f, 30, 30);
            g.DrawEllipse(new Pen(Color.Black, 1f), 95f, 30f, 30, 30);

            g.FillEllipse(hb, 95f, 160f, 30, 30);
            g.DrawEllipse(new Pen(Color.Black, 1f), 95f, 160f, 30, 30);

            g.FillEllipse(Brushes.Blue, 60f, 60f, 100, 100);
            g.DrawEllipse(new Pen(Color.Black, 1f), 60f, 60f, 100, 100);

            g.FillEllipse(Brushes.BlanchedAlmond, 95f, 95f, 30, 30);
            g.DrawEllipse(new Pen(Color.Black, 1f), 95f, 95f, 30, 30);

            g.DrawRectangle(new Pen(System.Drawing.Brushes.Blue, 0.1f), 6, 6, 208, 208);

            g.DrawLine(new Pen(Color.Black, 0.1f), 110f, 110f, 220f, 25f);
            g.DrawString("剖面图", new Font("宋体", 9f), Brushes.Green, 220f, 20f);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Export(txtFilePath.Text);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class VectorPhoto
    {
        public Metafile GetMetafile(int metafileWidth, int metafileHeight)
        {
            Metafile metafile;
            using (Graphics offScreenGraphics = Graphics.FromHwndInternal(IntPtr.Zero))
            {
                IntPtr hDC = offScreenGraphics.GetHdc();
                metafile = new Metafile(hDC,
                                        new Rectangle(0, 0, metafileWidth, metafileHeight),
                                        MetafileFrameUnit.Pixel,
                                        EmfType.EmfPlusOnly);
                offScreenGraphics.ReleaseHdc();
            }

            return metafile;
        }


        //2、GDI Graphics 创建
        public void GDIGrapicsFromMetafile(Metafile metafile)
        {
            var graphics = Graphics.FromImage(metafile); //也可以使用句柄

            //有时大小可能设置的不好使 需进行特殊处理
            var width = 0;
            var height = 0;
            graphics.SetClip(new RectangleF(0, 0, width, height));
        }


        //3、非托管调用代码

        /// <summary>
        /// 获取矢量图的byte
        /// </summary>
        /// <param name="hemf"></param>
        /// <param name="cbBuffer"></param>
        /// <param name="lpbBuffer"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern uint GetEnhMetaFileBits(IntPtr hemf, uint cbBuffer, byte[] lpbBuffer);
        /// <summary>
        /// byte转换矢量图
        /// </summary>
        /// <param name="cbBuffer"></param>
        /// <param name="lpBuffer"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern IntPtr SetEnhMetaFileBits(uint cbBuffer, byte[] lpBuffer);
        /// <summary>
        /// 删除矢量图
        /// </summary>
        /// <param name="hemf"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern bool DeleteEnhMetaFile(IntPtr hemf);

        /// <summary>
        /// Copy EMF to file
        /// </summary>
        /// <param name="hemfSrc">Handle to EMF</param>
        /// <param name="lpszFile">File</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        private static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, string lpszFile);

        //4、矢量图 转换 byte[]

        public static byte[] ConvertMetaFileToByteArray(Image image)
        {
            byte[] dataArray = null;

            Metafile mf = (Metafile)image;

            IntPtr enhMetafileHandle = mf.GetHenhmetafile();

            uint bufferSize = GetEnhMetaFileBits(enhMetafileHandle, 0, null);


            if (enhMetafileHandle != IntPtr.Zero)
            {
                dataArray = new byte[bufferSize];

                GetEnhMetaFileBits(enhMetafileHandle, bufferSize, dataArray);
            }

            DeleteEnhMetaFile(enhMetafileHandle);

            return dataArray;
        }

        //5、byte[] 转换 矢量图
        public static Image ConvertByteArrayToMetafile(byte[] data)
        {
            Metafile mf = null;
            IntPtr hemf = SetEnhMetaFileBits((uint)data.Length, data);
            mf = new Metafile(hemf, true);
            //DeleteEnhMetaFile(hemf); //如若后续对图像进行操作不能进行删除句柄
            return (Image)mf;
        }


        //6、矢量图保存

        public static void SaveMetafile(Metafile file, string emfName)
        {
            //MetafileHeader metafileHeader = file.GetMetafileHeader(); //这句话可要可不要
            IntPtr iptrMetafileHandle = file.GetHenhmetafile();
            CopyEnhMetaFile(iptrMetafileHandle, emfName);
            DeleteEnhMetaFile(iptrMetafileHandle);
        }

        //7、转换base64 字符
        public static Metafile LoadVectorPhoto(string imgpath = @"C:\Users\Administrator\Desktop\Debug\1.jpg")
        {
            //var m = (Metafile)Metafile.FromFile(@"EMF.emf"); //加载矢量图
            var m = (Metafile)Metafile.FromFile(imgpath); //加载矢量图

            var by = ConvertMetaFileToByteArray(m); //转换数组
            var ls = Convert.ToBase64String(by); //转字符串

            //8、base64转 byte[]

            var bt = Convert.FromBase64String(ls);

            //9、图像的通过字符生长之后、可保存操作、不能进行绘制操作、这时需要进行 创建一个新的对象接受这个生成对象、然后操作
            var imz = (Metafile)ConvertByteArrayToMetafile(bt);
            var jbhtr = imz.GetHenhmetafile();
            Metafile mf = new Metafile(jbhtr, true); //new 新对象接收
            return mf;
        }

        ///10、矢量图的结构播放主要使用：EnumerateMetafileProc 代码如下

    }
}

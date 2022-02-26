﻿using ERPWeb.Entity.BasicInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Business.EntityExtend
{
   public class GoodsExtend:Goods
    {
        static string _Default64Img = "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UHMAAAAAUGdQQwAAAABMZWZ0VW50RiNSbHQAAAAAAAAAAAAAAABUb3AgVW50RiNSbHQAAAAAAAAAAAAAAABTY2wgVW50RiNQcmNAWQAAAAAAAAAAABBjcm9wV2hlblByaW50aW5nYm9vbAAAAAAOY3JvcFJlY3RCb3R0b21sb25nAAAAAAAAAAxjcm9wUmVjdExlZnRsb25nAAAAAAAAAA1jcm9wUmVjdFJpZ2h0bG9uZwAAAAAAAAALY3JvcFJlY3RUb3Bsb25nAAAAAAA4QklNA+0AAAAAABAASAAAAAEAAgBIAAAAAQACOEJJTQQmAAAAAAAOAAAAAAAAAAAAAD+AAAA4QklNBA0AAAAAAAQAAAB4OEJJTQQZAAAAAAAEAAAAHjhCSU0D8wAAAAAACQAAAAAAAAAAAQA4QklNJxAAAAAAAAoAAQAAAAAAAAACOEJJTQP1AAAAAABIAC9mZgABAGxmZgAGAAAAAAABAC9mZgABAKGZmgAGAAAAAAABADIAAAABAFoAAAAGAAAAAAABADUAAAABAC0AAAAGAAAAAAABOEJJTQP4AAAAAABwAAD/////////////////////////////A+gAAAAA/////////////////////////////wPoAAAAAP////////////////////////////8D6AAAAAD/////////////////////////////A+gAADhCSU0EAAAAAAAAAgABOEJJTQQCAAAAAAAEAAAAADhCSU0EMAAAAAAAAgEBOEJJTQQtAAAAAAAGAAEAAAAMOEJJTQQIAAAAAAAQAAAAAQAAAkAAAAJAAAAAADhCSU0EHgAAAAAABAAAAAA4QklNBBoAAAAAAz8AAAAGAAAAAAAAAAAAAAC3AAAAygAAAAVnKmgHmJgALQAxAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAADKAAAAtwAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAAAAABAAAAABAAAAAAAAbnVsbAAAAAIAAAAGYm91bmRzT2JqYwAAAAEAAAAAAABSY3QxAAAABAAAAABUb3AgbG9uZwAAAAAAAAAATGVmdGxvbmcAAAAAAAAAAEJ0b21sb25nAAAAtwAAAABSZ2h0bG9uZwAAAMoAAAAGc2xpY2VzVmxM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NDU2NjEsIDIwMTIvMDIvMDYtMTQ6NTY6MjcgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0RXZ0PSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VFdmVudCMiIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6cGhvdG9zaG9wPSJodHRwOi8vbnMuYWRvYmUuY29tL3Bob3Rvc2hvcC8xLjAvIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDUzYgKFdpbmRvd3MpIiB4bXA6Q3JlYXRlRGF0ZT0iMjAyMi0wMi0yNVQxNzowOTozOCswODowMCIgeG1wOk1ldGFkYXRhRGF0ZT0iMjAyMi0wMi0yNVQxNzowOTozOCswODowMCIgeG1wOk1vZGlmeURhdGU9IjIwMjItMDItMjVUMTc6MDk6MzgrMDg6MDAiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6MEZEMTZDM0ExNzk2RUMxMUExOTdGQkUyQ0VCNTZCODciIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6MEVEMTZDM0ExNzk2RUMxMUExOTdGQkUyQ0VCNTZCODciIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDowRUQxNkMzQTE3OTZFQzExQTE5N0ZCRTJDRUI1NkI4NyIgZGM6Zm9ybWF0PSJpbWFnZS9qcGVnIiBwaG90b3Nob3A6Q29sb3JNb2RlPSIzIiBwaG90b3Nob3A6SUNDUHJvZmlsZT0ic1JHQiBJRUM2MTk2Ni0yLjEiPiA8eG1wTU06SGlzdG9yeT4gPHJkZjpTZXE+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJjcmVhdGVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOjBFRDE2QzNBMTc5NkVDMTFBMTk3RkJFMkNFQjU2Qjg3IiBzdEV2dDp3aGVuPSIyMDIyLTAyLTI1VDE3OjA5OjM4KzA4OjAwIiBzdEV2dDpzb2Z0d2FyZUFnZW50PSJBZG9iZSBQaG90b3Nob3AgQ1M2IChXaW5kb3dzKSIvPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0ic2F2ZWQiIHN0RXZ0Omluc3RhbmNlSUQ9InhtcC5paWQ6MEZEMTZDM0ExNzk2RUMxMUExOTdGQkUyQ0VCNTZCODciIHN0RXZ0OndoZW49IjIwMjItMDItMjVUMTc6MDk6MzgrMDg6MDAiIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9IkFkb2JlIFBob3Rvc2hvcCBDUzYgKFdpbmRvd3MpIiBzdEV2dDpjaGFuZ2VkPSIvIi8+IDwvcmRmOlNlcT4gPC94bXBNTTpIaXN0b3J5PiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8P3hwYWNrZXQgZW5kPSJ3Ij8+/+IMWElDQ19QUk9GSUxFAAEBAAAMSExpbm8CEAAAbW50clJHQiBYWVogB84AAgAJAAYAMQAAYWNzcE1TRlQAAAAASUVDIHNSR0IAAAAAAAAAAAAAAAAAAPbWAAEAAAAA0y1IUCAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAARY3BydAAAAVAAAAAzZGVzYwAAAYQAAABsd3RwdAAAAfAAAAAUYmtwdAAAAgQAAAAUclhZWgAAAhgAAAAUZ1hZWgAA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";
        private static GoodsExtend AutoCopy(Goods parent) { GoodsExtend child = new GoodsExtend(); var ParentType = typeof(Goods); var Properties = ParentType.GetProperties(); foreach (var Propertie in Properties) { if (Propertie.CanRead && Propertie.CanWrite) { Propertie.SetValue(child, Propertie.GetValue(parent, null), null); } } return child; }
        public  string GoodImgBase64 { get; set; }
        public static List<GoodsExtend> GetGoodsExtendList(IQueryable<Goods> list,string defaultImgPath= @"C:\Works\ERPWeb\src\ERPWeb.Web\Images\non.jpg") {
            List<GoodsExtend> rlist = new List<GoodsExtend>();
            byte[] imgbytes = GetImgBytes(defaultImgPath);
            
            foreach (Goods g in list) {
                GoodsExtend ge = AutoCopy(g);
                //ge.GoodImgBase64 = g.Image==null?"":Convert.ToBase64String(g.Image);
                if (ge.Image == null) { ge.Image = imgbytes;ge.GoodImgBase64 = _Default64Img; }
                else{ ge.Image = g.Image; ge.GoodImgBase64 = Convert.ToBase64String(g.Image);  }
                rlist.Add(ge);
            }
            return rlist;
        }
        static byte[] GetImgBytes(string path=""){
            byte[] imgbytes = null;
            if (path!=""&& File.Exists(path))
                imgbytes = ERPWeb.Util.ImgHelper.GetImageBytes(path);
            else
                imgbytes = Encoding.Default.GetBytes(_Default64Img);return imgbytes;
        }
        public static GoodsExtend GetGoodsExtend(Goods goods) {
            byte[] imgbytes = GetImgBytes();
            GoodsExtend ge = AutoCopy(goods); 
            if (ge.Image == null) { ge.Image = imgbytes; ge.GoodImgBase64 = _Default64Img; }
            else { ge.Image = goods.Image; ge.GoodImgBase64 = Convert.ToBase64String(goods.Image); }
            return ge;
        }
    }
}
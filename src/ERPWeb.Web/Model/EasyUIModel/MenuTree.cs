using ERPWeb.Entity.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPWeb.Web.Model.EasyUIModel
{
    public class MenuTree
    {
        public string code { get; set; }
        public string text { get; set; }
        public string url { get; set; }
        public string parentCode { get; set; }
        public int isvisible { get; set; }
        public string icon { get; set; }

        public List<MenuTree> children = new List<MenuTree>();


        public List<MenuTree> ChangeMenuToTree(List<MenuInfo> MenuInfoList)
        {
            List<MenuTree> list = new List<MenuTree>();
            MenuTree mt = null;
            foreach (var item in MenuInfoList)
            {
                mt = new MenuTree()
                {
                    code = item.Code,
                    text = item.Name,
                    url = item.XPath,
                    parentCode = item.ParentCode,
                    isvisible = item.IsVisible,
                    icon = item.Icon,
                };
                list.Add(mt);
            }
            list = this.GetJsonList(list);
            return list;
        }
        private List<MenuTree> GetJsonList(List<MenuTree> allList)
        {
            List<MenuTree> resultList = new List<MenuTree>();
            var code = from x in allList select x.code;
            var parentCode = from x in allList select x.parentCode;
            List<string> rootParentCodeList = new List<string>();
            //将根父节点都找出来
            foreach (var item in parentCode)
            {
                if (!code.Contains(item)) rootParentCodeList.Add(item);
            }
            //将第一层节点先加入要返回的结果集
            resultList.AddRange(allList.Where(x => rootParentCodeList.Contains(x.parentCode)));
            resultList = this.ReadChildList(allList, resultList);
            return resultList;
        }
        private List<MenuTree> ReadChildList(List<MenuTree> allList, List<MenuTree> list)
        {
            foreach (var item in list)
            {
                item.children.AddRange(allList.Where(x => x.parentCode.Equals(item.code)));
                if (item.children.Count > 0)
                {
                    item.children = this.ReadChildList(allList, item.children);
                }
            }
            return list;
        }
    }
}
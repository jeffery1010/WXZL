using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Util.Helper
{
    public class DataHelper
    {
        /// <summary>
        /// 将DataTable转换为List
        /// </summary>
        /// <typeparam name="T">转换之后的类型</typeparam>
        /// <param name="dt">要转换的DataTable</param>
        /// <returns></returns>
        public static List<T> ToDataList<T>(DataTable dt)
        {
            var list = new List<T>();
            var plist = new List<System.Reflection.PropertyInfo>(typeof(T).GetProperties());
            foreach (DataRow item in dt.Rows)
            {
                T s = Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    //当属性名和列名一致时
                    System.Reflection.PropertyInfo info = plist.Find(p => p.Name.ToLower().Equals(dt.Columns[i].ColumnName.ToLower()));
                    if (info != null)
                    {
                        try
                        {
                            //当值不为空
                            if (!Convert.IsDBNull(item[i]))
                            {
                                object v = null;
                                if (info.PropertyType.ToString().Contains("System.Nullable"))
                                {
                                    v = Convert.ChangeType(item[i], Nullable.GetUnderlyingType(info.PropertyType));
                                }
                                else
                                {
                                    v = Convert.ChangeType(item[i], info.PropertyType);
                                }
                                info.SetValue(s, v, null);
                            }
                            else
                            {
                                //当数据类型为System.String，并且为Null。将值设置为""
                                if (info.PropertyType.ToString().Contains("System.String"))
                                {
                                    info.SetValue(s, "", null);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("字段[" + info.Name + "]转换出错," + ex.Message);
                        }
                    }
                }
                list.Add(s);
            }
            return list;
        }
    }
}

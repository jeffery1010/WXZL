using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Util.Helper
{
  public class ClassConvert
     {
        //private static GoodsExtend AutoCopy(Goods parent) { GoodsExtend child = new GoodsExtend(); var ParentType = typeof(Goods); var Properties = ParentType.GetProperties(); foreach (var Propertie in Properties) { if (Propertie.CanRead && Propertie.CanWrite) { Propertie.SetValue(child, Propertie.GetValue(parent, null), null); } } return child; }
        public static T CreateInstance<T>() where T : new()
        {
            return default(T) ;
        }
        public static TargetType Copy<SrcType, TargetType>(SrcType srcInst) where TargetType : new()
        {
            TargetType target = new TargetType();
            var srcType = typeof(SrcType);
            var srcProperties = srcType.GetProperties();
            var targetType = typeof(TargetType);
            var targetProperties = targetType.GetProperties();
            foreach (var sp in srcProperties)
            {
                foreach (var tp in targetProperties)
                {
                    if(sp.Name==tp.Name)
                    if (tp.CanRead && tp.CanWrite)
                    { tp.SetValue(target, sp.GetValue(srcInst, null), null); }
                }
            }
            return target;
        }
        
    }
}

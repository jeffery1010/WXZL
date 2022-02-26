using ERPWeb.Business.GlobalM;
using ERPWeb.Util;
using System.Linq;

namespace ERPWeb.Business.Cache
{
    public class Base_UserModelCache : BaseCache<Base_UserModel>
    {
        public Base_UserModelCache()
            : base("Base_UserModel", UserId =>
            {
                if (UserId.IsNullOrEmpty())
                    return null;
                return new UserBusiness().GetDataList("UserId", UserId, new Pagination()).FirstOrDefault();
            })
        {

        }
    }
}

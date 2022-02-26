using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPWeb.Entity.Power;
using System.Data.Entity;
using ERPWeb.Entity.GlobalM;
using ERPWeb.Entity.BasicInfo;

namespace ERPWeb.Web.Model
{
  
    public class UserManageContext : DbContext
    {
        static string _ConnectionString { get { return System.Configuration.ConfigurationManager.ConnectionStrings["WXZL"].ToString(); } }
        //无参构造方法  
        public UserManageContext()
            : base(_ConnectionString)
        {
            //关闭初始化策略(Code First连接现有数据库)
            try { System.Data.Entity.Database.SetInitializer<Model.UserManageContext>(null); } catch (Exception er) { throw new Exception("数据库连接失败："+_ConnectionString+","+er.Message); }
            
        }

        public DbSet<OrgInfo> OrgInfo { get; set; }//组织表
        public DbSet<RoleInfo> RoleInfo { get; set; }//角色表
        public DbSet<PowerInfo> PowerInfo { get; set; }//权限表
        public DbSet<UserInfo> UserInfo { get; set; }//人员表
        public DbSet<MenuInfo> MenuInfo { get; set; }//菜单表表
        public DbSet<UserInRole> UserInRole { get; set; }//人员角色表
        public DbSet<PowerInRole> PowerInRole { get; set; }//角色权限表
        public DbSet<PowerInRes> PowerInRes { get; set; }//权限菜单表
        public DbSet<DatabaseLink> _DatabaseLink { get; set; }
        public DbSet<Goods> _Goods { get; set; }
        public DbSet<WebUser> WebUser { get; set; }
        public DbSet<GoodSort> _GoodSort { get; set; }
        public DbSet<CostComputeStyle> _CostComputeStyle { get; set; }
        public DbSet<GoodProperty> _GoodProperty { get; set; }
    }
}
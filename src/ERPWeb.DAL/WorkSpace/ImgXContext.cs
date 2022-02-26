using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPWeb.Entity.IMGX;

namespace ERPWeb.DAL.IMGX
{
    public class ImgXContext : DbContext
    {
        public ImgXContext()
            : base(@"Data Source=172.22.9.171\spcm001,1433;database=IMGX;uid=developer;pwd=p@d201908;")
        {
            //关闭初始化策略(Code First连接现有数据库)
            System.Data.Entity.Database.SetInitializer<ImgXContext>(null);
        }

        public DbSet<Cell> Cells { get; set; }
        public DbSet<Consoles> Consoles { get; set; }
        public DbSet<Mac> Mac { get; set; }
        public DbSet<ReadSetting> ReadSetting { get; set; }
        public DbSet<WorkStation> WorkStation { get; set; }
        public DbSet<WorkStationCell> WorkStationCell { get; set; }
        public DbSet<XPath> XPath { get; set; }
    }
}

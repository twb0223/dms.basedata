using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using Base.Model;

namespace BaseData.DataAccess
{
    public class MyDataContext : DbContext
    {
        private static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BaseDataCS"].ToString();

        public DbSet<Equement> Equements { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Station> Stations { get; set; }

        public MyDataContext()
            : base(ConnectionString)
        {
            //延迟加载
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;  //自动监测变化，默认值为 true  
            this.Configuration.ProxyCreationEnabled = false;

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}



using MvvmLight.Lx.DbAccess.Entitys;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using EntityFramework.DynamicFilters;
namespace MvvmLight.Lx.DbAccess
{
    public class MvvmLightContext : DbContext
    {
        public static string connectionString  = ConfigurationManager.ConnectionStrings["MySql"].ConnectionString;
        public DbSet<UserInfo> Users { get; set; }
        public DbSet<HeaderInfo> Headers { get; set; }
        public MvvmLightContext() : base("MySql")
        {
            
          //  Database.SetInitializer(new CreateDatabaseIfNotExists<MvvmLightContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Filter("IsDeleted", d=>d.IsDeleted,false);
            modelBuilder.Filter("IsDeleted", (IDeletionWare d) => d.IsDeleted,false);          
            modelBuilder.Entity<UserInfo>();//.ToTable("headerinfo");
            modelBuilder.Entity<HeaderInfo>().ToTable("headerinfo");
            base.OnModelCreating(modelBuilder);
        
        }
        public override int SaveChanges()
        {
            ChangeTracker.Entries<IDeletionWare>().ToList().ForEach(entry => SetFilterDelete(entry));
            return base.SaveChanges();
        }

        private void SetFilterDelete(DbEntityEntry<IDeletionWare> entry)
        {
            switch (entry.State)
            {
                //防止模型字段被污染
                case EntityState.Added:
                    entry.Property(model=>model.IsDeleted).CurrentValue=false;
                    entry.Property(model => model.DeleteTime).CurrentValue = null;
                    break;
                case EntityState.Modified:
                entry.Property(model => model.IsDeleted).CurrentValue = false;
                entry.Property(model => model.DeleteTime).CurrentValue = null;
                    break;
                case EntityState.Deleted:

                    //阻止默认删除操作
                    entry.State=EntityState.Unchanged;
                entry.Property(model => model.IsDeleted).CurrentValue = false;
                entry.Property(model => model.DeleteTime).CurrentValue = null;
                    break;
                default:
                    break;
            }
        }
    }
}



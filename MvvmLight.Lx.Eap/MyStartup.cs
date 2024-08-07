using MvvmLight.Lx.Core.Handlers;
using MvvmLight.Lx.DbAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Lifetime;
using Unity;
using System.Data.Entity.Core.Metadata.Edm;
using Microsoft.Practices.Unity.Configuration;
using System.IO;
using System.Collections;
using MvvmLight.Lx.DbAccess.Modules;
using MvvmLight.Lx.DbAccess.Entitys;

namespace MvvmLight.Lx.Eap
{
    public class MyStartup
    {
        public static void AddMvvmLightContext()
        {
          // var users=SqlQueryModule.Queryable<UserInfo>();
          //  var headers = SqlQueryModule.Queryable<HeaderInfo>();
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"unity.config")
            };
            
            var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap,ConfigurationUserLevel.None);
            var manSection =config.GetSection("Man").CurrentConfiguration.AppSettings;
          //  Add1();
           // var s1=configuration.GetSection("unity");
          //  UnityConfigurationSection section = configuration.GetSection(UnityConfigurationSection.SectionName) as UnityConfigurationSection;
           // unityContainer = new UnityContainer();
          //  section.Configure(unityContainer, "IQueryContainer");
        }

        public static void Add1()
        {
         
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"unity.config")
            };
         
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
           
            UnityConfigurationSection section = configuration.GetSection(UnityConfigurationSection.SectionName) as UnityConfigurationSection;
            var unityContainer = new UnityContainer();
            section.Configure(unityContainer, "IQueryContainer");
        }
    }

    public static class MyStartupExtension
    {
        public static void AddMvvmLightContext(this IUnityContainer container)
        {
            if (DataBaseOrTableHandler.IsCreated)
            {
                using (MvvmLightContext db = new MvvmLightContext())
                {
                    db.Database.Initialize(true);
                    db.SaveChanges();
                }
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MySql"].ConnectionString;
            container.AddExtension(new Diagnostic());
            //   container.RegisterType<DbContext, MvvmLightContext>(new HierarchicalLifetimeManager(), new InjectionConstructor(connectionString));
            container.RegisterType(typeof(DbContext), typeof(MvvmLightContext), new HierarchicalLifetimeManager());
        }
    }
}

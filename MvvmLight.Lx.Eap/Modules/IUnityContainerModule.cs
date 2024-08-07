using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MvvmLight.Lx.Admin.IServices;
using MvvmLight.Lx.Admin.Services;
using Unity;

namespace MvvmLight.Lx.Eap.Modules
{
    public interface IUnityContainerModule
    {
        void ConfigureServices(IUnityContainer container);
    }

  

    public static class UnityContainerModuleExtension
    {
        public static IUnityContainer RegisterModules([NotNull] this IUnityContainer container, params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length == 0)
            {
                assemblies= AppDomain.CurrentDomain.GetAssemblies();
               // assemblies = ReflectHelper.GetAssemblies();
            }
            var types=assemblies.SelectMany(ass => ass.GetTypes()).Where(t =>
                        t.IsClass && !t.IsAbstract && typeof(IUnityContainerModule).IsAssignableFrom(t));

            foreach (var type in types)
            {
                try
                {
                    if (Activator.CreateInstance(type) is IUnityContainerModule module)
                    {
                        module.ConfigureServices(container);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return container;
        }
    }
}

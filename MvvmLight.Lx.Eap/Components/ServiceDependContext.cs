using MvvmLight.Lx.Eap.ViewModels;
using MvvmLight.Lx.Eap.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace MvvmLight.Lx.Eap.Components
{
    public class ServiceDependContext
    {
        public static void Usage()
        {
            Type type = typeof(MainWindow);


            var con1 = type.GetConstructor(Type.EmptyTypes);
            var constructors=type.GetConstructors();

            var defaultConstructor=constructors.First();
            var parameter=defaultConstructor.GetParameters().FirstOrDefault();
            var s1=  parameter.DefaultValue;
            var s2 = parameter.HasDefaultValue;
            Type s3=parameter.ParameterType;

            
        }

        public static void Usage2(IUnityContainer unityContainer)
        {
            Type type = typeof(MainWindowViewModel);


            var con1 = type.GetConstructor(new[] { typeof(IUnityContainer)});
            if (con1 != null) 
            {
                var instance=con1.Invoke(new object[] { unityContainer }) as MainWindowViewModel;
                var name=instance.Name;
            }
            var constructors = type.GetConstructors();
            object instance2 = Activator.CreateInstance(type, unityContainer);
        }
    }
}

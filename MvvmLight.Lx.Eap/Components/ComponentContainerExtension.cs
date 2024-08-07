using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace MvvmLight.Lx.Eap.Components
{
    public static class ComponentContainerExtension
    {
        public static IUnityContainer RegisterComponent<TComponent>(this IUnityContainer container, object options = default)
            where TComponent : class, IContainerComponent, new()
        {
            return container.RegisterComponent<TComponent, object>(options);
        }


        public static IUnityContainer RegisterComponent<TComponent, TComponentOptions>(this IUnityContainer container, TComponentOptions options = default)
            where TComponent : class, IContainerComponent, new()
        {
            return container.RegisterComponent(typeof(TComponent), options);
        }

        public static IUnityContainer RegisterComponent(this IUnityContainer container, Type componentType, object options = default)
        {
            // 创建组件依赖链
          /*  var componentContextLinkList = ManualDependProvider.CreateDependLinkList(componentType, options);

            // 逐条创建组件实例并调用
            foreach (var context in componentContextLinkList)
            {
                // 创建组件实例
                var component = Activator.CreateInstance(context.ComponentType) as IContainerComponent;
                // 调用
                component.Load(container);
            }
        */
           var s=  Activator.CreateInstance(typeof(LocalServerComponent)) as IContainerComponent;
            s.Load(container);
            return container;
        }
    }

    internal class ManualDependProvider
    {
        internal static List<ComponentContext> CreateDependLinkList(Type componentType, object options = default)
        {
            // 根组件上下文
            var rootComponentContext = new ComponentContext
            {
                ComponentType = componentType,
                IsRoot = true
            };
            rootComponentContext.SetProperty(componentType, options);

            // 初始化组件依赖链
            var dependLinkList = new List<Type> { componentType };
            var componentContextLinkList = new List<ComponentContext> { rootComponentContext };

            // 创建组件依赖链
            //  CreateDependLinkList(componentType, ref dependLinkList, ref componentContextLinkList);

            return componentContextLinkList;
        }
    }


    public sealed class ComponentContext
    {
        /// <summary>
        /// 组件类型
        /// </summary>
        public Type ComponentType { get; internal set; }

        /// <summary>
        /// 上级组件上下文
        /// </summary>
        public ComponentContext CalledContext { get; internal set; }

        /// <summary>
        /// 根组件上下文
        /// </summary>
        public ComponentContext RootContext { get; internal set; }

        /// <summary>
        /// 依赖组件列表
        /// </summary>
        public Type[] DependComponents { get; internal set; }

        /// <summary>
        /// 链接组件列表
        /// </summary>
        public Type[] LinkComponents { get; internal set; }

        /// <summary>
        /// 上下文数据
        /// </summary>
        private Dictionary<string, object> Properties { get; set; } =new Dictionary<string, object>();

        /// <summary>
        /// 是否是根组件
        /// </summary>
        internal bool IsRoot { get; set; } = false;

        /// <summary>
        /// 设置组件属性参数
        /// </summary>
        public Dictionary<string, object> SetProperty<TComponent>(object value) where TComponent : class, new()
        {
            return SetProperty(typeof(TComponent), value);
        }

        /// <summary>
        /// 设置组件属性参数
        /// </summary>
        public Dictionary<string, object> SetProperty(Type componentType, object value)
        {
            return SetProperty(componentType.FullName, value);
        }

        /// <summary>
        /// 设置组件属性参数
        /// </summary>
        public Dictionary<string, object> SetProperty(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            var properties = RootContext == null ? Properties : RootContext.Properties;

            if (!properties.ContainsKey(key))
            {
                properties.Add(key, value);
            }
            else properties[key] = value;

            return properties;
        }

        /// <summary>
        /// 获取组件属性参数
        /// </summary>
        /// <returns></returns>
        public TComponentOptions GetProperty<TComponent, TComponentOptions>() where TComponent : class, new()
        {
            return GetProperty<TComponentOptions>(typeof(TComponent));
        }

        /// <summary>
        /// 获取组件属性参数
        /// </summary>
        /// <returns></returns>
        public TComponentOptions GetProperty<TComponentOptions>(Type componentType)
        {
            return GetProperty<TComponentOptions>(componentType.FullName);
        }

        /// <summary>
        /// 获取组件属性参数
        /// </summary>
        /// <returns></returns>
        public TComponentOptions GetProperty<TComponentOptions>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            var properties = RootContext == null ? Properties : RootContext.Properties;

            return !properties.ContainsKey(key)
                ? default
                : (TComponentOptions)properties[key];
        }

        /// <summary>
        /// 获取组件所有依赖参数
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetProperties()
        {
            return RootContext == null ? Properties : RootContext.Properties;
        }
    }
}

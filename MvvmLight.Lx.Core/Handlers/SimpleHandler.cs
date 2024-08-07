using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Interception.PolicyInjection.Pipeline;
using Unity.Interception.PolicyInjection.Policies;

namespace MvvmLight.Lx.Core.Handlers
{
    public class SimpleHandler : ICallHandler
    {
        public int Order {  get; set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            return getNext()(input, getNext);
        }
    }

    public class SimpleHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new SimpleHandler() { Order=this.Order};
        }
    }
}

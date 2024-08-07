using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Core.Messengers
{
    public class LoginMessanger
    {
        public static void SubscribeMessage()
        {
            WeakReferenceMessenger.Default.Send(new LoginArgs("张三"));
        }

        public void PublishMessage()
        {
            WeakReferenceMessenger.Default.Register<LoginArgs>(this,HandleLoginMessage);
        }

        private void HandleLoginMessage(object recipient, LoginArgs message)
        {

        }
    }

    public class LoginArgs
    {
        public string Name { get; set; }
        public LoginArgs(string name) 
        {
            Name=name;
        }
    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Core.Extensions
{
    public class EventAggregator
    {
        private readonly Dictionary<string, Action> _eventHandlers = new Dictionary<string, Action>();

        public void Subscribe(string eventName, Action handler)
        {
            if (!_eventHandlers.ContainsKey(eventName))
            {
                _eventHandlers[eventName] = handler;
            }
        }

        public void Unsubscribe(string eventName)
        {
            if (_eventHandlers.ContainsKey(eventName))
            {
                _eventHandlers.Remove(eventName);
            }
        }

        public void Publish(string eventName)
        {
            
            if (_eventHandlers.ContainsKey(eventName))
            {
                _eventHandlers[eventName]();
            }
        }
    }


    public static class EventNames
    {
        public const string OpenRfidDebugerWindow = "OpenRfidDebugerWindow";
        public const string OpenPlcDebugerWindow = "OpenPlcDebugerWindow";
    }


    public class MainViewModel222
    {
        private readonly EventAggregator _eventAggregator;

        public MainViewModel222(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(EventNames.OpenRfidDebugerWindow, OpenRfidDebugerWindow);
            _eventAggregator.Subscribe(EventNames.OpenPlcDebugerWindow, OpenPlcDebugerWindow);
        }

        private void OpenRfidDebugerWindow()
        {
           /* var window = new RfidDebugerWindow();
            window.Show();*/
        }

        private void OpenPlcDebugerWindow()
        {
            /*var window = new PlcDebugerWindow();
            window.Show();*/
        }

        public void ExecuteOpenTest(string navigatPath)
        {
            switch (navigatPath)
            {
                case "RfidDebugerWindow":
                    _eventAggregator.Publish(EventNames.OpenRfidDebugerWindow);
                    break;
                case "PlcDebugerWindow":
                    _eventAggregator.Publish(EventNames.OpenPlcDebugerWindow);
                    break;
                default:
                    // Handle unknown window
                    break;
            }
        }
    }
}

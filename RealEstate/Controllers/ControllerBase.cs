using System.Diagnostics;
using System.Web.Mvc;
using RealEstate.Messaging;

namespace RealEstate.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly MessageBus MessageBus;

        public ControllerBase(MessageBus messageBus)
        {
            MessageBus = messageBus;
            AddListeners(messageBus);
        }

        private void AddListeners(MessageBus messageBus)
        {
            messageBus.Subscribe(MessageType.A, (m) => Debug.WriteLine(m.Message));
            messageBus.Subscribe(MessageType.B, (m) => Debug.WriteLine(m.Message));
            messageBus.Subscribe(MessageType.C, (m) => Debug.WriteLine(m.Message));
        }
    }
}
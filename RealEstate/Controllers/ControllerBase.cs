using System.Diagnostics;
using System.Web.Mvc;
using RealEstate.Messaging;

namespace RealEstate.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly MessageBus MessageBus;
        private readonly MessageLogger _logger;

        public ControllerBase(MessageBus messageBus, MessageLogger logger)
        {
            MessageBus = messageBus;
            _logger = logger;
            AddListeners(messageBus);
        }

        private void AddListeners(MessageBus messageBus)
        {
            messageBus.Subscribe(MessageType.A, (m) => Debug.WriteLine(m.Message));
            messageBus.Subscribe(MessageType.B, (m) => Debug.WriteLine(m.Message));
            messageBus.Subscribe(MessageType.C, (m) => Debug.WriteLine(m.Message));
            messageBus.Subscribe(MessageType.B, (m) => { _logger.LogAsync(m); });
            messageBus.Subscribe(MessageType.A, (m) => { _logger.LogAsync(m); });


        }
    }
}
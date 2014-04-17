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
            messageBus.Subscribe<TestMessage>((m) => { _logger.LogAsync(m); });
            messageBus.Subscribe<MessageA>((m) => { _logger.LogAsync(m); });
            messageBus.Subscribe<MessageB>((m) => { _logger.LogAsync(m); });
        }
    }
}
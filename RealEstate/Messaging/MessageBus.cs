using System;
using System.Collections.Generic;

namespace RealEstate.Messaging
{
    public class MessageBus
    {
        private readonly Dictionary<MessageType, List<Action<IMessage>>> _subscribers = new Dictionary<MessageType, List<Action<IMessage>>>();

        public void Publish(IMessage message)
        {
            if(message == null) throw new ArgumentException("null message");

            List<Action<IMessage>> activeSubscribers;

            if(!_subscribers.TryGetValue(message.MessageType, out activeSubscribers)) return;

            foreach (var subscriber in activeSubscribers)
            {
                subscriber(message);
            }
        }

        public void Subscribe(MessageType messageType, Action<IMessage> callback)
        {
            AddSubscriber(messageType, callback);
        }

        public void Subscribe(IEnumerable<MessageType> messageTypes, Action<IMessage> callback)
        {
            if(messageTypes == null) return;

            foreach (var messageType in messageTypes)
            {
                AddSubscriber(messageType, callback);
            }
        }

        private void AddSubscriber(MessageType messageType, Action<IMessage> callback)
        {
            List<Action<IMessage>> queue;
            if (!_subscribers.TryGetValue(messageType, out queue))
            {
                _subscribers[messageType] = new List<Action<IMessage>> { callback };
                return;
            }

            queue.Add(callback);            
        }
    }
}
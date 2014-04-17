using System;
using System.Collections.Generic;
using System.Reflection;

namespace RealEstate.Messaging
{
    public class MessageBus
    {
        private readonly Dictionary<Type, List<Action<IMessage>>> _subscribers = new Dictionary<Type, List<Action<IMessage>>>();

        public void Publish(IMessage message)
        {
            if(message == null) throw new ArgumentException("null message");

            List<Action<IMessage>> activeSubscribers;

            if(!_subscribers.TryGetValue(message.GetType(), out activeSubscribers)) return;

            foreach (var subscriber in activeSubscribers)
            {
                subscriber(message);
            }
        }

        public void Subscribe<TMessage>(Action<IMessage> callback) where TMessage : IMessage
        {
            AddSubscriber<TMessage>(callback);
        }

        private void AddSubscriber<TMessage>(Action<IMessage> callback) where TMessage : IMessage
        {
            var messageType = typeof (TMessage);

            List<Action<IMessage>> queue;

            if (!_subscribers.TryGetValue(messageType, out queue))
            {
                _subscribers[messageType] = new List<Action<IMessage>> { callback };
                return;
            }

            queue.Add(callback);            
        }

        private static Type GetMessageTypeFrom(Action<IMessage> callback)
        {
            return callback.GetMethodInfo().GetParameters()[0].ParameterType;
        }
    }
}
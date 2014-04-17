namespace RealEstate.Messaging
{
    public class TestMessage : IMessage
    {
        public TestMessage(object sender, MessageType messageType, string message)
        {
            MessageType = messageType;
            Sender = sender;
            Message = message;
        }

        public MessageType MessageType { get; private set; }

        public object Sender { get; private set; }

        public string Message { get; private set; }
    }
}
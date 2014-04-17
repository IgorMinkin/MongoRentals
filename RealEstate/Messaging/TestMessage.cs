namespace RealEstate.Messaging
{
    public class TestMessage : IMessage
    {
        public TestMessage(object sender, string message)
        {
            Sender = sender;
            Message = message;
        }

        public object Sender { get; private set; }

        public string Message { get; private set; }
    }
}
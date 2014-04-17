namespace RealEstate.Messaging
{
    public class MessageA : IMessage
    {
        public MessageA(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}
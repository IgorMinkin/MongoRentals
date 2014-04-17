namespace RealEstate.Messaging
{
    public class MessageB : IMessage
    {
        public MessageB(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}
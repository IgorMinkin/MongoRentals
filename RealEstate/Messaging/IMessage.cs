namespace RealEstate.Messaging
{
    public interface IMessage
    {
        MessageType MessageType { get; }
        object Sender { get; }

        string Message { get; }
    }
}
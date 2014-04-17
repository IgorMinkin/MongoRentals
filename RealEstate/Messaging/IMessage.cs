namespace RealEstate.Messaging
{
    public interface IMessage
    {
        object Sender { get; }

        string Message { get; }
    }
}
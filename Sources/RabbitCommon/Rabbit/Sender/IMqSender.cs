namespace RabbitCommand.Rabbit.Sender
{
    public interface IMqSender<T>
    {
        public void Send(T command);
    }
}
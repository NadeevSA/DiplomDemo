namespace RabbitCommand.Rabbit.Sender
{
    public interface IMqSender
    {
        public void Send(BaseCommand command);
    }
}
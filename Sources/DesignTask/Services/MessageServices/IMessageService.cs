using RabbitCommand.Rabbit;

namespace DesignTask.Services.MessageServices
{
    public interface IMessageService
    {
        public void Serve(BaseCommand command);
    }
}
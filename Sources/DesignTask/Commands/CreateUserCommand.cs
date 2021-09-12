using Common.Models;
using MediatR;

namespace DesignTask.Commands
{
    public class CreateUserCommand: IRequest<User>
    {
        public User User { get; set; }
    }
}
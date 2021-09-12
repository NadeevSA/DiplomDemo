using Common.Models;
using MediatR;

namespace DesignTask.Commands
{
    public class UpdateUserCommand: IRequest<User>
    {
        public User User { get; set; }
    }
}
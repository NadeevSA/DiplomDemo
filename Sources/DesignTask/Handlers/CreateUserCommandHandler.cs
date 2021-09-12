using System.Threading;
using System.Threading.Tasks;
using Common.Models;
using DesignTask.Commands;
using MediatR;

namespace DesignTask.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        
        public CreateUserCommandHandler()
        {
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return new User
            {
                Name = "Tom"
            };
        }
    }
}
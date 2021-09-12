using System.Threading;
using System.Threading.Tasks;
using Common.Models;
using DesignTask.Commands;
using MediatR;

namespace DesignTask.Handlers
{
    public class UpdateUserHandler: IRequestHandler<UpdateUserCommand, User>
    {
        
        public UpdateUserHandler()
        {
        }

        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return new User
            {
                Name = "Updated"
            };
        }
    }
}
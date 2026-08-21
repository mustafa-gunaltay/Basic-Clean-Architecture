using Application.Interfaces;
using MediatR;

namespace Application.Features.TodoUser.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        // FK cascade tanımlı: user silinince ona ait TaskItem'lar da silinir
        var isDeleted = _userRepository.Delete(request.Id);

        if (isDeleted)
        {
            return new DeleteUserResponse(true, $"User with Id {request.Id} deleted successfully.");
        }

        return new DeleteUserResponse(false, $"User with Id {request.Id} not found.");
    }
}

using MediatR;

namespace Application.Features.TodoUser.Commands.UpdateUser;

public record UpdateUserCommand(
    int Id,
    string Username,
    string Password,
    string Email
) : IRequest<UpdateUserResponse>;

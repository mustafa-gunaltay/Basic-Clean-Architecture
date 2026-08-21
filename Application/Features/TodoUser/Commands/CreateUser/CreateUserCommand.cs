using MediatR;

namespace Application.Features.TodoUser.Commands.CreateUser;

public record CreateUserCommand(
    string Username,
    string Password,
    string Email
) : IRequest<CreateUserResponse>;

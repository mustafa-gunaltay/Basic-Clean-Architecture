using MediatR;

namespace Application.Features.TodoUser.Commands.DeleteUser;

public record DeleteUserCommand(int Id) : IRequest<DeleteUserResponse>;

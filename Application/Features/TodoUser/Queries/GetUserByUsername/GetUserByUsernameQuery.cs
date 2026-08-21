using MediatR;

namespace Application.Features.TodoUser.Queries.GetUserByUsername;

public record GetUserByUsernameQuery(string Username) : IRequest<GetUserByUsernameResponse?>;

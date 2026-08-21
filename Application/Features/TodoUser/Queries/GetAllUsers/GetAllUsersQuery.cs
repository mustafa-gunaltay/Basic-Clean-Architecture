using MediatR;

namespace Application.Features.TodoUser.Queries.GetAllUsers;

public record GetAllUsersQuery() : IRequest<IEnumerable<GetAllUsersResponse>>;

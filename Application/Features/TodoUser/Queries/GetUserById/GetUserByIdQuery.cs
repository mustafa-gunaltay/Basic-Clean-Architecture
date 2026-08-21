using MediatR;

namespace Application.Features.TodoUser.Queries.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<GetUserByIdResponse?>;

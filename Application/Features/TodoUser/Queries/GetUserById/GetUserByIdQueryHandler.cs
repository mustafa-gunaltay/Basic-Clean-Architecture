using Application.Interfaces;
using MediatR;

namespace Application.Features.TodoUser.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserByIdResponse?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetById(request.Id);

        if (user == null)
            return null;

        return new GetUserByIdResponse(
            user.Id,
            user.Username,
            user.Email ?? string.Empty,
            user.TaskItems.Count
        );
    }
}

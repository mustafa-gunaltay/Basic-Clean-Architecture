using Application.Interfaces;
using MediatR;

namespace Application.Features.TodoUser.Queries.GetUserByUsername;

public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, GetUserByUsernameResponse?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByUsernameQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserByUsernameResponse?> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetByUsername(request.Username);

        if (user == null)
            return null;

        return new GetUserByUsernameResponse(
            user.Id,
            user.Username,
            user.Email ?? string.Empty,
            user.TaskItems.Count
        );
    }
}

using Application.Interfaces;
using MediatR;

namespace Application.Features.TodoUser.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<GetAllUsersResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<GetAllUsersResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _userRepository.GetAll();

        // Password bilinçli olarak response'a taşınmıyor
        return users.Select(user => new GetAllUsersResponse(
            user.Id,
            user.Username,
            user.Email ?? string.Empty,
            user.TaskItems.Count
        ));
    }
}

using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.TodoUser.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Username veritabanında unique, aynı isimle ikinci bir kayıt açılamaz
        var existingUser = _userRepository.GetByUsername(request.Username);
        if (existingUser != null)
        {
            throw new InvalidOperationException($"Username '{request.Username}' is already taken. Please choose another one.");
        }

        var user = new User
        {
            Username = request.Username,
            Password = request.Password,
            Email = request.Email
        };

        var createdUser = _userRepository.Create(user);

        return new CreateUserResponse(
            createdUser.Id,
            createdUser.Username,
            createdUser.Email ?? string.Empty,
            createdUser.TaskItems.Count
        );
    }
}

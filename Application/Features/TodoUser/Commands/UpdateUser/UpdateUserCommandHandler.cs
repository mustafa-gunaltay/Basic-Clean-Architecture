using Application.Interfaces;
using MediatR;

namespace Application.Features.TodoUser.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Önce mevcut user'ı bul
        var existingUser = _userRepository.GetById(request.Id);

        if (existingUser == null)
        {
            throw new KeyNotFoundException($"User with Id {request.Id} not found.");
        }

        // Username değiştiriliyorsa, başka bir kullanıcı tarafından alınmış olmamalı
        if (existingUser.Username != request.Username)
        {
            var userWithSameUsername = _userRepository.GetByUsername(request.Username);
            if (userWithSameUsername != null)
            {
                throw new InvalidOperationException($"Username '{request.Username}' is already taken. Please choose another one.");
            }
        }

        // Güncelle
        existingUser.Username = request.Username;
        existingUser.Password = request.Password;
        existingUser.Email = request.Email;

        var updatedUser = _userRepository.Update(existingUser);

        return new UpdateUserResponse(
            updatedUser.Id,
            updatedUser.Username,
            updatedUser.Email ?? string.Empty,
            updatedUser.TaskItems.Count
        );
    }
}

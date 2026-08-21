using Application.Features.TodoUser.Commands.CreateUser;
using Application.Features.TodoUser.Commands.DeleteUser;
using Application.Features.TodoUser.Commands.UpdateUser;
using Application.Features.TodoUser.Queries.GetAllUsers;
using Application.Features.TodoUser.Queries.GetUserById;
using Application.Features.TodoUser.Queries.GetUserByUsername;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> CreateUser([FromBody] CreateUserCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetUserById), new { id = response.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserByIdResponse>> GetUserById(int id)
        {
            var query = new GetUserByIdQuery(id);
            var response = await _mediator.Send(query);

            if (response == null)
                return NotFound($"User with id {id} not found.");

            return Ok(response);
        }

        [HttpGet("by-username/{username}")]
        public async Task<ActionResult<GetUserByUsernameResponse>> GetUserByUsername(string username)
        {
            var query = new GetUserByUsernameQuery(username);
            var response = await _mediator.Send(query);

            if (response == null)
                return NotFound($"User with username '{username}' not found.");

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllUsersResponse>>> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateUserResponse>> UpdateUser(int id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id in URL does not match Id in request body.");

            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteUserResponse>> DeleteUser(int id)
        {
            var command = new DeleteUserCommand(id);
            var response = await _mediator.Send(command);

            if (!response.IsSuccess)
                return NotFound(response.Message);

            return Ok(response);
        }
    }
}

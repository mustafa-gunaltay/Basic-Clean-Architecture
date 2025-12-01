using Application.Features.TodoTaskItem.Commands.CreateTaskItem;
using Application.Features.TodoTaskItem.Commands.DeleteTaskItem;
using Application.Features.TodoTaskItem.Commands.UpdateTaskItem;
using Application.Features.TodoTaskItem.Queries.GetAllTaskItems;
using Application.Features.TodoTaskItem.Queries.GetTaskItemById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateTaskItemResponse>> CreateTaskItem([FromBody] CreateTaskItemCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetTaskItemById), new { id = response.Id }, response);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetTaskItemByIdResponse>> GetTaskItemById(int id)
        {
            var query = new GetTaskItemByIdQuery(id);
            var response = await _mediator.Send(query);
            
            if (response == null)
                return NotFound($"TaskItem with id {id} not found.");

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllTaskItemsResponse>>> GetAllTaskItems()
        {
            var query = new GetAllTaskItemsQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateTaskItemResponse>> UpdateTaskItem(int id, [FromBody] UpdateTaskItemCommand command)
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
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteTaskItemResponse>> DeleteTaskItem(int id)
        {
            var command = new DeleteTaskItemCommand(id);
            var response = await _mediator.Send(command);
            
            if (!response.IsSuccess)
                return NotFound(response.Message);

            return Ok(response);
        }
    }
}

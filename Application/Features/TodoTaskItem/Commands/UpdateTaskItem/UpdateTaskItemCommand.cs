using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.TodoTaskItem.Commands.UpdateTaskItem;

public record UpdateTaskItemCommand(
    int Id,
    string Title,
    string Description,
    bool IsCompleted,
    int UserId
) : IRequest<UpdateTaskItemResponse>;

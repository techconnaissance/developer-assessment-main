using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoList.Api.Commands;
using TodoList.Api.Models;

namespace TodoList.Api.Handlers
{
    public class CreateToDoItemHandler : IRequestHandler<CreateToDoItemCommand, TodoItem>
    {
        private readonly TodoContext _context;
        public CreateToDoItemHandler(TodoContext context)
        {
            _context = context;
        }
        public async Task<TodoItem> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
        {
            if (TodoItemDescriptionExists(request.TodoItemToCreate.Description))
            {
                request.TodoItemToCreate.Message = "Description already exists" ;
            }
            else
            {

                _context.TodoItems.Add(request.TodoItemToCreate);
                var result = await _context.SaveChangesAsync();

            }
            return request.TodoItemToCreate;
        }

        private bool TodoItemDescriptionExists(string description)
        {
            return _context.TodoItems
                   .Any(x => x.Description.ToLowerInvariant() == description.ToLowerInvariant() && !x.IsCompleted);
        }
    }
}

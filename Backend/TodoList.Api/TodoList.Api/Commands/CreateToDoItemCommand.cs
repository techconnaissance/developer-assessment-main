using MediatR;
using TodoList.Api.Models;

namespace TodoList.Api.Commands
{
    public class CreateToDoItemCommand : IRequest<TodoItem>
    {
        public TodoItem TodoItemToCreate { get; set; }

        public CreateToDoItemCommand(TodoItem todoItemToCreate)
        {
            TodoItemToCreate = todoItemToCreate;
        }
    }
}

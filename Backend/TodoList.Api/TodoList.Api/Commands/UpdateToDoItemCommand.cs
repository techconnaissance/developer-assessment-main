using MediatR;
using System;
using TodoList.Api.Models;

namespace TodoList.Api.Commands
{
    public class UpdateToDoItemCommand : IRequest<bool>
    {
        public TodoItem TodoItemToUpdate { get; set; }
        public Guid Id { get; set; }
        public UpdateToDoItemCommand(Guid id, TodoItem todoItemToUpdate)
        {
            TodoItemToUpdate = todoItemToUpdate;
            Id = id;
        }
    }
}

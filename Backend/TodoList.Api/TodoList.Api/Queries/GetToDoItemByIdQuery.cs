using MediatR;
using System;
using System.Collections.Generic;
using TodoList.Api.Models;

namespace TodoList.Api.Queries
{
    public class GetToDoItemByIdQuery : IRequest<TodoItem>
    {
        public Guid Id { get; set; }
        public GetToDoItemByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

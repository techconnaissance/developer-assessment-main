using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoList.Api.Models;
using TodoList.Api.Queries;

namespace TodoList.Api.Handlers
{
    public class GetToDoItemByIdHandler : IRequestHandler<GetToDoItemByIdQuery, TodoItem>
    {
        private readonly TodoContext _context;
        public GetToDoItemByIdHandler(TodoContext context)
        {
            _context = context;
        }

        public Task<TodoItem> Handle(GetToDoItemByIdQuery request, CancellationToken cancellationToken)
        {
            return _context.TodoItems.Where(p => p.Id == request.Id).FirstOrDefaultAsync();
        }
    }
}

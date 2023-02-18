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
    public class GetToDoItemsHandler : IRequestHandler<GetToDoItemsQuery, List<TodoItem>>
    {
        private readonly TodoContext _context;
        public GetToDoItemsHandler(TodoContext context)
        {
            _context = context;
        }
        public Task<List<TodoItem>> Handle(GetToDoItemsQuery request, CancellationToken cancellationToken)
        {
            return _context.TodoItems.Where(x => x.IsCompleted == request.IsCompletedFlag).ToListAsync();
        }
    }
}

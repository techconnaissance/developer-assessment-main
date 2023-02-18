using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TodoList.Api.Commands;
using TodoList.Api.Models;
using TodoList.Api.Queries;
using System.Linq;
using Microsoft.Extensions.Logging;
using TodoList.Api.Controllers;

namespace TodoList.Api.Handlers
{
    public class UpdateToDoItemHandler : IRequestHandler<UpdateToDoItemCommand, bool>
    {
        private readonly TodoContext _context;
        public UpdateToDoItemHandler(TodoContext context)
        {
            _context= context;
        }
        public async Task<bool> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
        {
            _context.Entry(request.TodoItemToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemIdExists(request.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        private bool TodoItemIdExists(Guid id)
        {
            return _context.TodoItems.Any(x => x.Id == id);
        }
    }
}

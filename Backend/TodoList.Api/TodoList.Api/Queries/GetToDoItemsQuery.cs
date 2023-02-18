using MediatR;
using System.Collections.Generic;
using TodoList.Api.Models;

namespace TodoList.Api.Queries
{
    public class GetToDoItemsQuery : IRequest<List<TodoItem>>
    {
        public bool IsCompletedFlag { get; set; }
        public GetToDoItemsQuery(bool isCompletedFlag) { 
            this.IsCompletedFlag = isCompletedFlag;
        }
    }
}

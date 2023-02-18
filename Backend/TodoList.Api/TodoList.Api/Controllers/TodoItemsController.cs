using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Api.Models;
using MediatR;
using TodoList.Api.Queries;
using TodoList.Api.Commands;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> _logger;
        private readonly IMediator _mediator;

        public TodoItemsController( ILogger<TodoItemsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            try
            {
                GetToDoItemsQuery query = new GetToDoItemsQuery(false);
                var results = await _mediator.Send(query);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
            return BadRequest();
        }

        // GET: api/TodoItems/...
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(Guid id)
        {
            try
            {
                GetToDoItemByIdQuery query = new GetToDoItemByIdQuery(id);
                var result = await _mediator.Send(query);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
            return BadRequest();
        }

        // PUT: api/TodoItems/... 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoItem todoItem)
        {
            try
            {
                if (id != todoItem.Id)
                {
                    return BadRequest();
                }

                UpdateToDoItemCommand query = new UpdateToDoItemCommand(id, todoItem);
                var result = await _mediator.Send(query);

                return result == true ? NoContent() : NotFound();
            }
            catch (Exception ex) 
            { 
                _logger.LogError(ex.Message); 
                _logger.LogError(ex.StackTrace); 
            }
            return BadRequest();
        } 

        // POST: api/TodoItems 
        [HttpPost]
        public async Task<IActionResult> PostTodoItem(TodoItem todoItem)
        {
            try
            {
                if (TryValidateModel(todoItem))
                {
                    CreateToDoItemCommand query = new CreateToDoItemCommand(todoItem);
                    var result = await _mediator.Send(query);

                    //if (!string.IsNullOrEmpty(result.Message))
                    //{
                    //    return BadRequest(result.Message);
                    //}

                    return CreatedAtAction(nameof(GetTodoItem), new { id = result.Id }, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
            
            return BadRequest();
        } 
    }
}

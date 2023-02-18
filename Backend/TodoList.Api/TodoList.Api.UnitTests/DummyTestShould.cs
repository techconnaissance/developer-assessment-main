using Azure;
using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;
using TodoList.Api.Commands;
using TodoList.Api.Handlers;
using TodoList.Api.Models;
using TodoList.Api.Queries;


namespace TodoList.Api.UnitTests
{
    [TestFixture]
    public class DummyTestShould
    {
        private TodoContext _dbContext;
        GetToDoItemsHandler _GetAllToDoListhandler;
        GetToDoItemByIdHandler _GetToDoItemByIdHandler;

        UpdateToDoItemHandler _UpdateToDoItemHandler;
        CreateToDoItemHandler _CreateToDoItemHandler;

        [SetUp]
        public void Setup()
        {
            List<TodoItem> data = new List<TodoItem>
            {
                new TodoItem { Id = new Guid("6f8d739c-3f11-4ca0-bc7e-93bfa6e5dccc"), Description = "Test1", IsCompleted = false},
                new TodoItem { Id = new Guid("4eccfff6-7796-4020-acc9-245ed373e46e"), Description = "Test2", IsCompleted = true},
                new TodoItem { Id = new Guid("2d44db2a-49f6-44e0-9472-4c9850e507c7"), Description = "Test3", IsCompleted = false},
            };

            var mockContext = new Mock<TodoContext>();
            
            mockContext.Setup(m => m.TodoItems).ReturnsDbSet(data);
            _dbContext = mockContext.Object;
            
            var mediator = new Mock<IMediator>();
            _GetAllToDoListhandler = new GetToDoItemsHandler(mockContext.Object);
            _GetToDoItemByIdHandler = new GetToDoItemByIdHandler(mockContext.Object);

            _UpdateToDoItemHandler = new UpdateToDoItemHandler(mockContext.Object);
            _CreateToDoItemHandler = new CreateToDoItemHandler(mockContext.Object);
        }

        [TestCase]
        public void Test_Should_Return_InComplete_ToDoList()
        {
            GetToDoItemsQuery query1 = new GetToDoItemsQuery(false);
            var cancellationToken = new CancellationToken();

            var result = _GetAllToDoListhandler.Handle(query1, cancellationToken).Result;

            var expectedData = new List<TodoItem>
            {
                new TodoItem { Id = new Guid("6f8d739c-3f11-4ca0-bc7e-93bfa6e5dccc"), Description = "Test1", IsCompleted = false},
                new TodoItem { Id = new Guid("2d44db2a-49f6-44e0-9472-4c9850e507c7"), Description = "Test3", IsCompleted = false},
            };

            Assert.AreEqual(expectedData[0].Description, result[0].Description);
            Assert.AreEqual(expectedData[1].Description, result[1].Description);
        }

        [TestCase]
        public void Test_Should_Return_SingleToDoList_With_ID()
        {
            GetToDoItemByIdQuery query1 = new GetToDoItemByIdQuery(new Guid("2d44db2a-49f6-44e0-9472-4c9850e507c7"));
            var cancellationToken = new CancellationToken();

            var result = _GetToDoItemByIdHandler.Handle(query1, cancellationToken).Result;

            var expectedData = new TodoItem { Id = new Guid("2d44db2a-49f6-44e0-9472-4c9850e507c7"), Description = "Test3", IsCompleted = false };
            

            Assert.AreEqual(expectedData.Description, result.Description);
            Assert.AreEqual(expectedData.Description, result.Description);
        }

        [TestCase]
        public void Test_Should_Set_IsCompleted_Flag_To_True()
        {
            var inputData = new TodoItem { Id = new Guid("2d44db2a-49f6-44e0-9472-4c9850e507c7"), Description = "Test3", IsCompleted = true };

            UpdateToDoItemCommand command1 = new UpdateToDoItemCommand(new Guid("2d44db2a-49f6-44e0-9472-4c9850e507c7"), inputData);
            var cancellationToken = new CancellationToken();

            var result = _UpdateToDoItemHandler.Handle(command1, cancellationToken).Result;
            var updatedValue = _dbContext.TodoItems.Where(p => p.Id == inputData.Id).FirstOrDefault().IsCompleted;
            Assert.AreEqual(result, true);
            Assert.AreEqual(updatedValue, true);
        }

        //[TestCase]
        //public void Test_Should_Create_New_TodoList()
        //{
        //    var inputData = new TodoItem { Description = "Test4", IsCompleted = false };

        //    CreateToDoItemCommand command1 = new CreateToDoItemCommand(inputData);
        //    var cancellationToken = new CancellationToken();

        //    var result = _CreateToDoItemHandler.Handle(command1, cancellationToken).Result;

        //    var updatedValue = _dbContext.TodoItems.Where(p => p.Description == inputData.Description).FirstOrDefault();
            
        //    Assert.AreEqual(result, updatedValue);
        //}
    }
}

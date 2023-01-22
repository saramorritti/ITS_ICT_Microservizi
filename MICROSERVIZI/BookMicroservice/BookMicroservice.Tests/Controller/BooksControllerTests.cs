using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using BookMicroservice.Controllers;
using BookMicroservice.Data;
using BookMicroservice.Services;
using BookMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BookMicroservice.Services.Interfaces;
using BookMicroservice.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Xunit.Assert;

namespace BookMicroservice.Tests.Controller
{
    public class BooksControllerTests
    {
        private readonly IBookServices _BookServices;
        private readonly IMapper _mapper;
        private readonly BooksController _booksController;

        public BooksControllerTests()
        {
            _BookServices = A.Fake<IBookServices>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task BooksController_GetAll_ReturnOK()
        {
            //Arrange
            var books = A.Fake<ICollection<Book>>();
            var bookList = A.Fake<List<Book>>();
            A.CallTo(() => _mapper.Map<List<Book>>(books)).Returns(bookList);
            var controller = new BooksController(_BookServices, _mapper);

            //Act
            var result = await controller.GetAll();

            //Assert
            result.Should().NotBeNull();
            Assert.IsType<ActionResult<IEnumerable<Books>>>(result);
        }


        [Fact]
        public async Task BooksController_GetOne_ReturnOk()
        {
            var books = A.Fake<ICollection<Book>>();
            var bookList = A.Fake<List<Book>>();
            A.CallTo(() => _mapper.Map<List<Book>>(books)).Returns(bookList);
            var controller = new BooksController(_BookServices, _mapper);
            int id = 3;

            var result = await controller.GetOne(id);

            result.Should().NotBeNull();
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [MemberData(nameof(TestData.GetPostTestData), MemberType = typeof(TestData))]
        public async Task<ActionResult> BooksController_Post_ReturnOk(BooksBaseModel value)
        {
            var books = A.Fake<ICollection<BooksBaseModel>>();
            var bookList = A.Fake<List<BooksBaseModel>>();
            A.CallTo(() => _mapper.Map<List<BooksBaseModel>>(books)).Returns(bookList);
            var _bookServices = A.Fake<IBookServices>();
            var controller = new BooksController(_bookServices, _mapper);


            A.CallTo(() => _bookServices.Create(value)).Returns(Task.FromResult(1));
            var result = await controller.Post(value);

            Assert.IsType<CreatedAtActionResult>(result);
            A.CallTo(() => _bookServices.Create(value)).MustHaveHappened();
            return result; 
        }

        [Theory]
        [MemberData(nameof(TestData.GetPutTestData), MemberType = typeof(TestData))]
        public async Task<ActionResult> BooksController_Put_ReturnNoContent(int id, BooksBaseModel value)
        {
            var books = A.Fake<ICollection<BooksBaseModel>>();
            var bookList = A.Fake<List<BooksBaseModel>>();
            A.CallTo(() => _mapper.Map<List<BooksBaseModel>>(books)).Returns(bookList);
            var _bookServices = A.Fake<IBookServices>();
            var controller = new BooksController(_bookServices, _mapper);


            A.CallTo(() => _bookServices.Update(1, value)).Returns(Task.FromResult(1));
            var result = await controller.Put(1, value);

            Assert.IsType<NoContentResult>(result);
            A.CallTo(() => _bookServices.Update(1, value)).MustHaveHappened();
            return result;
        }

        
        [Theory]
        [InlineData(1)]
        public ActionResult BooksController_Delete_ReturnOk(int value)
        {
            var books = A.Fake<ICollection<BooksBaseModel>>();
            var bookList = A.Fake<List<BooksBaseModel>>();
            A.CallTo(() => _mapper.Map<List<BooksBaseModel>>(books)).Returns(bookList);
            var _bookServices = A.Fake<IBookServices>();
            var controller = new BooksController(_bookServices, _mapper);

            A.CallTo(() => _bookServices.Delete(value)).Invokes(x => _bookServices.Delete(value));
            var result = controller.Delete(value);

            Assert.IsType<OkObjectResult>(result);
            A.CallTo(() => _bookServices.Delete(value)).MustHaveHappenedOnceExactly();
            return result;
        }

    }
    
}

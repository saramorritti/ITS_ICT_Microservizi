using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;
using BorrowingMicroservice.Services.Interfaces;
using BorrowingMicroservice.Controllers;
using BorrowingMicroservice.Data.Entities;
using BorrowingMicroservice.Models;

namespace BorrowingMicroservice.Test.Controller
{
    public class BorrowingsControllerTests
    {
        private readonly IBorrowingServices _BorrowingServices;
        private readonly IMapper _mapper;
        private readonly BorrowingsController _borrowingsController;

        public BorrowingsControllerTests()
        {
            _BorrowingServices = A.Fake<IBorrowingServices>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task BorrowingsController_GetAll_ReturnOK()
        {
            //Arrange
            var borrowings = A.Fake<ICollection<Borrowing>>();
            var borrowingList = A.Fake<List<Borrowing>>();
            A.CallTo(() => _mapper.Map<List<Borrowing>>(borrowings)).Returns(borrowingList);
            var controller = new BorrowingsController(_BorrowingServices, _mapper);

            //Act
            var result = await controller.GetAll();

            //Assert
            result.Should().NotBeNull();
            Assert.IsType<ActionResult<IEnumerable<Borrowings>>>(result);
        }


        [Fact]
        public async Task BorrowignsController_GetOne_ReturnOk()
        {
            var borrowings = A.Fake<ICollection<Borrowing>>();
            var borrowingList = A.Fake<List<Borrowing>>();
            A.CallTo(() => _mapper.Map<List<Borrowing>>(borrowings)).Returns(borrowingList);
            var controller = new BorrowingsController(_BorrowingServices, _mapper);
            int id = 3;

            var result = await controller.GetOne(id);

            result.Should().NotBeNull();
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [MemberData(nameof(TestData.GetPostTestData), MemberType = typeof(TestData))]
        public async Task<ActionResult> BorrowingsController_Post_ReturnOk(BorrowingsBaseModel value)
        {
            var borrowings = A.Fake<ICollection<BorrowingsBaseModel>>();
            var borrowingList = A.Fake<List<BorrowingsBaseModel>>();
            A.CallTo(() => _mapper.Map<List<BorrowingsBaseModel>>(borrowings)).Returns(borrowingList);
            var _borrowingServices = A.Fake<IBorrowingServices>();
            var controller = new BorrowingsController(_borrowingServices, _mapper);


            A.CallTo(() => _borrowingServices.Create(value)).Returns(Task.FromResult(1));
            var result = await controller.Post(value);

            Assert.IsType<CreatedAtActionResult>(result);
            A.CallTo(() => _borrowingServices.Create(value)).MustHaveHappened();
            return result;
        }

        [Theory]
        [MemberData(nameof(TestData.GetPutTestData), MemberType = typeof(TestData))]
        public async Task<ActionResult> BorrowingsController_Put_ReturnNoContent(int id, BorrowingsBaseModel value)
        {
            var borrowings = A.Fake<ICollection<BorrowingsBaseModel>>();
            var borrowingList = A.Fake<List<BorrowingsBaseModel>>();
            A.CallTo(() => _mapper.Map<List<BorrowingsBaseModel>>(borrowings)).Returns(borrowingList);
            var _borrowingServices = A.Fake<IBorrowingServices>();
            var controller = new BorrowingsController(_borrowingServices, _mapper);


            A.CallTo(() => _borrowingServices.Update(1, value)).Returns(Task.FromResult(1));
            var result = await controller.Put(1, value);

            Assert.IsType<NoContentResult>(result);
            A.CallTo(() => _borrowingServices.Update(1, value)).MustHaveHappened();
            return result;
        }




        [Theory]
        [InlineData(1)]
        public ActionResult BorrowingsController_Delete_ReturnOk(int value)
        {
            var borrowings = A.Fake<ICollection<BorrowingsBaseModel>>();
            var borrowingList = A.Fake<List<BorrowingsBaseModel>>();
            A.CallTo(() => _mapper.Map<List<BorrowingsBaseModel>>(borrowings)).Returns(borrowingList);
            var _borrowingServices = A.Fake<IBorrowingServices>();
            var controller = new BorrowingsController(_borrowingServices, _mapper);

            A.CallTo(() => _borrowingServices.Delete(value)).Invokes(x => _borrowingServices.Delete(value));
            var result = controller.Delete(value);

            Assert.IsType<OkObjectResult>(result);
            A.CallTo(() => _borrowingServices.Delete(value)).MustHaveHappenedOnceExactly();
            return result;
        }
    }
}

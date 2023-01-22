using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using CustomerMicroservice.Controllers;
using CustomerMicroservice.Data;
using CustomerMicroservice.Services;
using CustomerMicroservice.Models;
using System.Threading.Tasks;
using Xunit;
using CustomerMicroservice.Services.Interfaces;
using CustomerMicroservice.Data.Entities;
using Assert = Xunit.Assert;

namespace CustomerMicroservice.Tests.Controller
{
    public class CustomersControllerTests
    {
        private readonly ICustomerServices _CustomerServices;
        private readonly IMapper _mapper;
        private readonly CustomersController _customersController;

        public CustomersControllerTests()
        {
            _CustomerServices = A.Fake<ICustomerServices>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task CustomersController_GetAll_ReturnOK()
        {
            //Arrange
            var customers = A.Fake<ICollection<Customer>>();
            var customerList = A.Fake<List<Customer>>();
            A.CallTo(() => _mapper.Map<List<Customer>>(customers)).Returns(customerList);
            var controller = new CustomersController(_CustomerServices, _mapper);

            //Act
            var result = await controller.GetAll();

            //Assert
            result.Should().NotBeNull();
            Assert.IsType<ActionResult<IEnumerable<Customers>>>(result);
        }


        [Fact]
        public async Task CustomersController_GetOne_ReturnOk()
        {
            var customers = A.Fake<ICollection<Customer>>();
            var customerList = A.Fake<List<Customer>>();
            A.CallTo(() => _mapper.Map<List<Customer>>(customers)).Returns(customerList);
            var controller = new CustomersController(_CustomerServices, _mapper);
            int id = 3;

            var result = await controller.GetOne(id);

            result.Should().NotBeNull();
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [MemberData(nameof(TestData.GetPostTestData), MemberType = typeof(TestData))]
        public async Task<ActionResult> CustomersController_Post_ReturnOk(CustomersBaseModel value)
        {
            var customers = A.Fake<ICollection<CustomersBaseModel>>();
            var customerList = A.Fake<List<CustomersBaseModel>>();
            A.CallTo(() => _mapper.Map<List<CustomersBaseModel>>(customers)).Returns(customerList);
            var _customerServices = A.Fake<ICustomerServices>();
            var controller = new CustomersController(_customerServices, _mapper);


            A.CallTo(() => _customerServices.Create(value)).Returns(Task.FromResult(1));
            var result = await controller.Post(value);

            Assert.IsType<CreatedAtActionResult>(result);
            A.CallTo(() => _customerServices.Create(value)).MustHaveHappened();
            return result;
        }

        [Theory]
        [MemberData(nameof(TestData.GetPutTestData), MemberType = typeof(TestData))]
        public async Task<ActionResult> CustomersController_Put_ReturnNoContent(int id, CustomersBaseModel value)
        {
            var customers = A.Fake<ICollection<CustomersBaseModel>>();
            var customerList = A.Fake<List<CustomersBaseModel>>();
            A.CallTo(() => _mapper.Map<List<CustomersBaseModel>>(customers)).Returns(customerList);
            var _customerServices = A.Fake<ICustomerServices>();
            var controller = new CustomersController(_customerServices, _mapper);


            A.CallTo(() => _customerServices.Update(1, value)).Returns(Task.FromResult(1));
            var result = await controller.Put(1, value);

            Assert.IsType<NoContentResult>(result);
            A.CallTo(() => _customerServices.Update(1, value)).MustHaveHappened();
            return result;
        }

        


        [Theory]
        [InlineData(1)]
        public ActionResult CustomersController_Delete_ReturnOk(int value)
        {
            var customers = A.Fake<ICollection<CustomersBaseModel>>();
            var customerList = A.Fake<List<CustomersBaseModel>>();
            A.CallTo(() => _mapper.Map<List<CustomersBaseModel>>(customers)).Returns(customerList);
            var _customerServices = A.Fake<ICustomerServices>();
            var controller = new CustomersController(_customerServices, _mapper);

            A.CallTo(() => _customerServices.Delete(value)).Invokes(x => _customerServices.Delete(value));
            var result = controller.Delete(value);

            Assert.IsType<OkObjectResult>(result);
            A.CallTo(() => _customerServices.Delete(value)).MustHaveHappenedOnceExactly();
            return result;
        }
    }

    
}

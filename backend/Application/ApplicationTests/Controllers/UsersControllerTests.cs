using Application.Entities;
using Application.Services;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;

namespace Application.Controllers.Tests
{
    [TestClass()]
    public class UsersControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUserService> _serviceMoq;
        private readonly Mock<IMapper> _autoMapperMoq;
        private readonly UsersController _usersController;

        public UsersControllerTests()
        {
            _fixture = new Fixture();
            _serviceMoq = _fixture.Freeze<Mock<IUserService>>();
            _autoMapperMoq = _fixture.Freeze<Mock<IMapper>>();
            _usersController = new UsersController(_serviceMoq.Object, _autoMapperMoq.Object);
        }

        [TestMethod()]
        [Fact]
        public void GetUsers_ShouldReturnOkResponse_WhenDataFound()
        {
            // Arrange
            var usersMock = _fixture.Create<IEnumerable<User>>();
            _serviceMoq.Setup(x => x.GetAll()).Returns(usersMock);

            // Act
            var result = _usersController.GetUsers();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull();
            _serviceMoq.Verify(x => x.GetAll(), Times.Once());
        }

        [TestMethod()]
        [Fact]
        public void GetUsers_ShouldReturnNotFound_WhenDataNotFound()
        {
            List<User>? users = null;
            _serviceMoq.Setup(x => x.GetAll()).Returns(users);

            var result = _usersController.GetUsers();

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _serviceMoq.Verify(x => x.GetAll(), Times.Once());
        }
    }
}
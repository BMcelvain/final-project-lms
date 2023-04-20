using FluentAssertions;
using Lms.APIErrorHandling;
using Lms.Controllers;
using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.UnitTests
{
#nullable disable warnings
    [TestClass]
    public class TeacherControllerTests
    {
        private Mock<ITeacherDao> _mockTeacherDao;
        private  IMemoryCache _cache;
        private TeacherController _sut;
        private Guid _teacherGuid;
        private Guid _invalidTeacherGuid;
        private JsonPatchDocument<TeacherModel> _teacherJsonDocument;
        private List<TeacherModel> _teachers;
       

        [TestInitialize]
        public void Initialize()
        {
            _mockTeacherDao = new Mock<ITeacherDao>();
            _cache = new MemoryCache(new MemoryCacheOptions());
            _sut = new TeacherController(_mockTeacherDao.Object, _cache);
            _teacherGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A6");
            _invalidTeacherGuid = new Guid("00000000-0000-0000-0000-000000000000");
            _teacherJsonDocument = new JsonPatchDocument<TeacherModel>();

            _teachers = new List<TeacherModel>()
            {
                new TeacherModel()
                {
                    TeacherId = _teacherGuid,
                    TeacherFirstName = "Test",
                    TeacherLastName = "Teach",
                    TeacherPhone = "999-999-9999",
                    TeacherEmail = "teacher@vu.com",
                    TeacherStatus = "Active"
                },
                new TeacherModel()
                {
                    TeacherId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A7"),
                    TeacherFirstName = "Tester",
                    TeacherLastName = "Substitute",
                    TeacherPhone = "999-999-9998",
                    TeacherEmail = "subTeacher@vu.com",
                    TeacherStatus = "Active"
                }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockTeacherDao = null;
            _sut = null;
            _teacherGuid = new Guid();
            _teacherJsonDocument = null;
            _teachers = null;
        }

        [TestMethod]
        public async Task CreateTeacher_ReturnsOkResponse_WhenModelIsValid()
        {
            // Act
            var result = await _sut.CreateTeacher(_teachers.First());

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            var teacherInApiOkResponse = apiOkResponseInOkResult.Result;

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            teacherInApiOkResponse.Should().NotBeNull();
            teacherInApiOkResponse.Should().BeEquivalentTo(_teachers.First());
        }


        [TestMethod]
        public async Task GetTeacher_ReturnsOkResponse_WhenTeacherIdFoundInCache()
        {
            // Arrange
            _cache.Set($"teacherKey{_teacherGuid}", _teachers);

            // Act
            var result = await _sut.GetTeacher(_teacherGuid, null, null, null);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponse = okResult.Value as ApiOkResponse;
            var teacherResult = apiOkResponse.Result as List<TeacherModel>;

            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult, typeof(OkObjectResult));
            Assert.IsNotNull(apiOkResponse);
            Assert.AreEqual(_teachers, apiOkResponse.Result);
            Assert.IsNotNull(teacherResult);
            CollectionAssert.AreEqual(_teachers, teacherResult);
        }

        [TestMethod]
        public async Task GetTeacher_ReturnsOkResponse_WhenTeacherIdFoundInDatabase()
        {
            // Arrange
            _mockTeacherDao.Setup(x => x.GetTeacher(_teacherGuid, null, null, null))
                .ReturnsAsync(_teachers);

            // Act
            var result = await _sut.GetTeacher(_teacherGuid, null, null, null);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponse = okResult.Value as ApiOkResponse;
            var teacherResult = apiOkResponse.Result as List<TeacherModel>;

            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult, typeof(OkObjectResult));
            Assert.IsNotNull(apiOkResponse);
            Assert.AreEqual(_teachers, apiOkResponse.Result);
            Assert.IsNotNull(teacherResult);
            CollectionAssert.AreEqual(_teachers, teacherResult);

            _mockTeacherDao.Verify(x => x.GetTeacher(_teacherGuid, null, null, null), Times.Once);
        }

        [TestMethod]
        public async Task GetTeacher_ReturnsNotFoundResponse_WhenTeacherGuidIsInvalid()
        {
            // Arrange
            _mockTeacherDao.Setup(x => x.GetTeacher(_invalidTeacherGuid, null, null, null))
                .ReturnsAsync((IEnumerable<TeacherModel>)null);

            // Act
            var result = await _sut.GetTeacher(_invalidTeacherGuid, null, null, null);
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponse = notFoundResult.Value as ApiResponse;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(404, apiResponse.StatusCode);
            Assert.AreEqual("Teacher not found.", apiResponse.Message);
        }

        [TestMethod]
        public async Task PartiallyUpdateTeacherById_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            _mockTeacherDao.Setup(x => x.GetTeacher(_teacherGuid, null, null, null))
                .ReturnsAsync(_teachers);
            _mockTeacherDao.Setup(x => x.PartiallyUpdateTeacherById(It.IsAny<TeacherModel>()))
                .Returns(Task.CompletedTask);

            _teacherJsonDocument.Replace(x => x.TeacherFirstName, "Testss");

            // Act
            var result = await _sut.PartiallyUpdateTeacherById(_teacherGuid, _teacherJsonDocument);


            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            var apiOkResponseInOkResult = okResult.Value as ApiOkResponse;
            Assert.IsNotNull(okResult);
            apiOkResponseInOkResult.StatusCode.Should().Be(200);
            apiOkResponseInOkResult.Message.Should().BeEquivalentTo("Results were a success.");
            
        }

        [TestMethod]
        public async Task PartiallyUpdateTeacherById_TeacherNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            _mockTeacherDao.Setup(x => x.GetTeacher(_teacherGuid, null, null, null))
                .ReturnsAsync((List<TeacherModel>)null);

            _teacherJsonDocument.Replace(x => x.TeacherFirstName, "Invalid");

            // Act
            var result = await _sut.PartiallyUpdateTeacherById(_teacherGuid, _teacherJsonDocument);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInNotFoundResult = notFoundResult.Value as ApiResponse;
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.IsNotNull(notFoundResult);
            apiResponseInNotFoundResult.StatusCode.Should().Be(404);
            apiResponseInNotFoundResult.Message.Should().BeEquivalentTo("Teacher with that id not found.");

        }

        [TestMethod]
        public async Task DeleteTeacherById_ValidId_ReturnsOkObjectResult()
        {
            // Arrange
            _mockTeacherDao.Setup(x => x.GetTeacher(_teacherGuid, null, null, null))
                .ReturnsAsync(_teachers);
            _mockTeacherDao.Setup(x => x.DeleteTeacherById(_teacherGuid))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _sut.DeleteTeacherById(_teacherGuid);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(StatusCodes.Status200OK, okObjectResult.StatusCode);
        }

        [TestMethod]
        public async Task DeleteTeacherById_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            _mockTeacherDao.Setup(x => x.GetTeacher(_invalidTeacherGuid, null, null, null))
                .ReturnsAsync((List<TeacherModel>)null);

            // Act
            var result = await _sut.DeleteTeacherById(_invalidTeacherGuid);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponseInNotFoundResult = notFoundResult.Value as ApiResponse;
            Assert.IsNotNull(notFoundResult);
            apiResponseInNotFoundResult.StatusCode.Should().Be(404);
            apiResponseInNotFoundResult.Message.Should().BeEquivalentTo("Teacher with that id not found.");
        }

    }
}

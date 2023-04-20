using FluentAssertions;
using Lms.APIErrorHandling;
using Lms.Controllers;
using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;


namespace LMS.UnitTests
{
    #nullable disable warnings
    [TestClass]
    public class StudentEnrollmentControllerTests
    {
        private Mock<IStudentEnrollmentDao> _mockStudentEnrollmentDao;
        private StudentEnrollmentController _sut;
        private Guid _studentGuid;
        private Guid _invalidStudentGuid;
        private List<StudentEnrollmentModel> _studentEnrollment;
        private IMemoryCache _cache;

        [TestInitialize]
        public void Initialize()
        {
            _mockStudentEnrollmentDao = new Mock<IStudentEnrollmentDao>();
            _cache = new MemoryCache(new MemoryCacheOptions());
            _sut = new StudentEnrollmentController(_mockStudentEnrollmentDao.Object, _cache);
            _studentGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A9");
            _invalidStudentGuid = new Guid("00000000-0000-0000-0000-000000000000");

            _studentEnrollment = new List<StudentEnrollmentModel>()
            {
                new StudentEnrollmentModel()
                {
                    CourseId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167B2"),
                    CourseName = "Unit Testing",
                    StartDate = "2023-03-16",
                    EndDate = "2023-5-16",
                    Cancelled = false,
                    CancellationReason = "Test Cancelled",
                    HasPassed = false
                }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockStudentEnrollmentDao = null;
            _sut = null;
            _studentGuid = new Guid();
            _studentEnrollment = null;
        }

        [TestMethod]
        public async Task GetStudentEnrollment_ReturnsOkResponse_WhenStudentIdFoundInDatabase()
        {
            // Arrange
            _mockStudentEnrollmentDao.Setup(x => x.GetStudentEnrollmentHistory(_studentGuid, null, null, null,null))
                .ReturnsAsync(_studentEnrollment);

            // Act
            var result = await _sut.GetStudentEnrollmentHistory(_studentGuid, null, null, null,null);

            // Assert
            var okResult = result as OkObjectResult;
            var apiOkResponse = okResult.Value as ApiOkResponse;
            var studentResult = apiOkResponse.Result as List<StudentEnrollmentModel>;

            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult, typeof(OkObjectResult));
            Assert.IsNotNull(apiOkResponse);
            Assert.AreEqual(_studentEnrollment, apiOkResponse.Result);
            Assert.IsNotNull(studentResult);
            CollectionAssert.AreEqual(_studentEnrollment, studentResult);

           _mockStudentEnrollmentDao.Verify(x => x.GetStudentEnrollmentHistory(_studentGuid, null, null, null, null), Times.Once);
        }

        [TestMethod]
        public async Task GetStudentEnrollment_ReturnsNotFoundResponse_WhenStudentGuidIsInvalid()
        {
            // Arrange
            _mockStudentEnrollmentDao.Setup(x => x.GetStudentEnrollmentHistory(_invalidStudentGuid, null, null, null, null))
                .ReturnsAsync((IEnumerable<StudentEnrollmentModel>)null);

            // Act
            var result = await _sut.GetStudentEnrollmentHistory(_invalidStudentGuid, null, null, null, null);
            var notFoundResult = result as NotFoundObjectResult;
            var apiResponse = notFoundResult.Value as ApiResponse;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(404, apiResponse.StatusCode);
            Assert.AreEqual("Student enrollment not found. Please check your entries and try again.", apiResponse.Message);
        }

    }
}

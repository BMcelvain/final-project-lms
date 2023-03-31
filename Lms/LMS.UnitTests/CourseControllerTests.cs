using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lms.Controllers;
using Moq;
using Lms.Daos;
using System.Threading.Tasks;
using Lms.Models;
using LMS.UnitTests.Mocks;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.JsonPatch;
using FluentAssertions;

namespace LMS.UnitTests
{
    #nullable disable warnings
    [TestClass]
    public class CourseControllerTests
    {
        Mock<ICourseDao> mockCourseDao;
        CourseController sut;
        CourseModel testCourse;
        
        [TestInitialize]
        public void Initialize()
        {
            mockCourseDao = MockICourseDao.MockDao();
            sut = new CourseController(mockCourseDao.Object);
            testCourse = new CourseModel()
            {
                CourseId = new Guid(),
                TeacherId = new Guid(),
                CourseName = "Test",
                StartDate = "01/01/2023",
                EndDate = "03/01/2023",
                CourseStatus = "Active"
            };
        }

        [TestMethod]
        public async Task CreateCourse_ReturnsOkStatusCode_WhenModelIsValid()
        {
            var result = await sut.CreateCourse(testCourse);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>("Because it ran successfully!");
        }

        [TestMethod]
        public async Task GetCoursesById_ReturnsOkStatusCode_WhenGuidIsValid()
        {
            var guid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A6");

            var result = await sut.GetCourseById(guid);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [TestMethod]
        [DataRow("0AE43554-0BB1-42B1-94C7-04420A2167A5")]
        [DataRow("0AE435540BB142B194C704420A2167A5")]
        [DataRow("(0AE43554-0BB1-42B1-94C7-04420A2167A5)")]
        [DataRow("{0AE43554-0BB1-42B1-94C7-04420A2167A5}")]
        public async Task GetCourseById_ThrowsException_WhenGuidIsInvalid(string data)
        {
            var guid = new Guid(data);

            var result = await sut.GetCourseById(guid);

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
        }


        //[TestMethod]
        //public async Task GetCourseByStatus_Returns_TheCorrectNumber_OfCourses()
        //{
        //    // Arrange
        //    Mock<ICourseDao> mockCourseDao = new(); //
        //    CourseController sut = new CourseController(mockCourseDao.Object);

        //    // Act
        //    var result = await sut.GetCourseByStatus("Active");

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        //}

        [TestMethod]
        public async Task GetCourseByStatus_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.GetCourseByStatus("Active");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetCourseByStatus_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);
            var testException = new Exception("Test Exception");

 

            // Act
            var result = await sut.GetCourseByStatus("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult)); 
        }

        [TestMethod]
        public async Task PartiallyUpdateCourseById_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            mockCourseDao
                .Setup(x => x.GetCourseById<CourseModel>(new Guid()))
                .ReturnsAsync(
                new CourseModel()
                {
                    CourseId = new Guid(),
                    TeacherId = new Guid(),
                    CourseName = "Test",
                    StartDate = "11/11/2022",
                    EndDate = "12/12/2022",
                    CourseStatus = "Test"
                });

            JsonPatchDocument<CourseModel> testDocument = new JsonPatchDocument<CourseModel>();    
            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.PartiallyUpdateCourseById(new Guid(), testDocument);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task PartiallyUpdateCourseById_ReturnsNotFound_WhenGetCourseIdReturnsNull()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);
            JsonPatchDocument<CourseModel> testDocument = new JsonPatchDocument<CourseModel>();

            // Act
            var result = await sut.PartiallyUpdateCourseById(new Guid(), testDocument);

            // Arrange
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task PartiallyUpdateCourseById_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);
            JsonPatchDocument<CourseModel> testDocument = new JsonPatchDocument<CourseModel>();
            var testException = new Exception("Test Exception");

            mockCourseDao
                .Setup(x => x.GetCourseById<CourseModel>(new Guid()))
                .Throws(testException);

            // Act
            var result = await sut.PartiallyUpdateCourseById(new Guid(), testDocument);

            // Arrange
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task DeleteCourseById_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            mockCourseDao
                .Setup(x => x.GetCourseById<CourseModel>(new Guid()))
                .ReturnsAsync(
                new CourseModel()
                {
                    CourseId = new Guid(),
                    TeacherId = new Guid(),
                    CourseName = "Test",
                    StartDate = "11/11/2022",
                    EndDate = "12/12/2022",
                    CourseStatus = "Test"
                });
            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.DeleteCourseById(new Guid());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task DeleteCourseById_ReturnsNotFound_WhenGetCourseByIdReturnsNull()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object); 

            // Act
            var result = await sut.DeleteCourseById(new Guid());

            // Assert 
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task DeleteCourseById_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);
            var testException = new Exception("Test Exception");

            mockCourseDao
                .Setup(x => x.GetCourseById<CourseModel>(new Guid()))
                .Throws(testException);

            // Act
            var result = await sut.DeleteCourseById(new Guid());

            // Assert 
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        //---------------Add Student To Course Section-------------- 

        [TestMethod]
        public async Task StudentInCourse_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);
            var course = new StudentInCourseModel();

            // Act
            var result = await sut.StudentInCourse(course);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task StudentInCourse_ThrowsException_OnError()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            var testException = new Exception("Test Exception");
            var testCourse = new StudentInCourseModel();

            mockCourseDao
                .Setup(x => x.StudentInCourse(It.IsAny<StudentInCourseModel>()))
                .Throws(testException);
            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.StudentInCourse(testCourse);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task PartiallyUpdateStudentInCourseById_ReturnsOKStatusCode()
        {
            //Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            mockCourseDao
                .Setup(x => x.GetCourseById<StudentInCourseModel>(new Guid()))
                .ReturnsAsync(
                new StudentInCourseModel()
                {
                    CourseId = new Guid(),
                    StudentId = new Guid(),
                    Cancelled = false,
                    CancellationReason = "test",
                    HasPassed = false
                });

            JsonPatchDocument<StudentInCourseModel> testDocument = new JsonPatchDocument<StudentInCourseModel>();
            CourseController sut = new CourseController(mockCourseDao.Object);

            //Act
            var result = await sut.PartiallyUpdateStudentInCourseByCourseStudentId(new Guid(), new Guid(), testDocument);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task PartiallyUpdateStudentInCourseByCourseStudentId_ReturnsNotFound_WhenGetCourseIdReturnsNull()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);
            JsonPatchDocument<StudentInCourseModel> testDocument = new JsonPatchDocument<StudentInCourseModel>();

            // Act
            var result = await sut.PartiallyUpdateStudentInCourseByCourseStudentId(new Guid(), new Guid(), testDocument);

            // Arrange
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task PartiallyUpdateStudentInCourseByCourseStudentId_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);
            JsonPatchDocument<StudentInCourseModel> testDocument = new JsonPatchDocument<StudentInCourseModel>();
            var testException = new Exception("Test Exception");

            mockCourseDao
                .Setup(x => x.GetCourseById<StudentInCourseModel>(new Guid()))
                .Throws(testException);

            // Act
            var result = await sut.PartiallyUpdateStudentInCourseByCourseStudentId(new Guid(), new Guid(), testDocument);

            // Arrange
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        [TestMethod]
        public async Task DeleteStudentInCourseByStudentCourseId_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            mockCourseDao
                .Setup(x => x.GetCourseById<StudentInCourseModel>(new Guid()))
                .ReturnsAsync(
                new StudentInCourseModel()
                {
                    CourseId = new Guid(),
                    StudentId = new Guid(),
                    Cancelled = false,
                    CancellationReason = "test",
                    HasPassed = false
                });

            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.DeleteStudentInCourseByStudentCourseId(new Guid(), new Guid());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        }

        [TestMethod]
        public async Task DeleteStudentInCourseByIdIdReturnsNull()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.DeleteStudentInCourseByStudentCourseId(new Guid(), new Guid());

            // Assert 
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task DeleteStudentInCourseById_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);
            var testException = new Exception("Test Exception");

            mockCourseDao
                .Setup(x => x.GetCourseById<CourseModel>(new Guid()))
                .Throws(testException);

            // Act
            var result = await sut.DeleteStudentInCourseByStudentCourseId(new Guid(), new Guid());

            // Assert 
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }
    }
}
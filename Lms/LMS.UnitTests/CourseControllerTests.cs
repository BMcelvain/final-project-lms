using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lms.Controllers;
using Moq;
using Lms.Daos;
using System.Threading.Tasks;
using Lms.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.JsonPatch;

namespace LMS.UnitTests
{
    [TestClass]
    public class CourseControllerTests
    {
        [TestMethod]
        public async Task CreateCourse_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);
            var course = new CourseModel();

            // Act
            var result = await sut.CreateCourse(course);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task CreateCourse_ThrowsException_OnError()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            var testException = new Exception("Test Exception");
            var testCourse = new CourseModel();

            mockCourseDao
                .Setup(x => x.CreateCourse(It.IsAny<CourseModel>()))
                .Throws(testException);
            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.CreateCourse(testCourse);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }


        [TestMethod]
        public async Task GetCoursesById_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();

            mockCourseDao
                .Setup(x => x.GetCourseById<CourseModel>(0))
                .ReturnsAsync(
                new CourseModel()
                {
                    CourseId = 0,
                    TeacherId = 0,
                    CourseName = "Test",
                    SemesterId = 0,
                    StartDate = "11/11/2022",
                    EndDate = "12/12/2022",
                    CourseStatus = "Test"
                });

            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.GetCourseById(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetCourseById_ThrowsExceptionOnError()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);
            var testException = new Exception("Test Exception");

            mockCourseDao
                .Setup(x => x.GetCourseById<CourseModel>(0))
                .Throws(testException);

            // Act
            var result = await sut.GetCourseById(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));    
        }

        [TestMethod]
        public async Task GetCourseByStatus_ReturnsOKStatusCode()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.GetCourseByStatus("Test");

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

            mockCourseDao
                .Setup(x => x.GetCourseByStatus("Test"))
                .Throws(testException);

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
                .Setup(x => x.GetCourseById<CourseModel>(0))
                .ReturnsAsync(
                new CourseModel()
                {
                    CourseId = 0,
                    TeacherId = 0,
                    CourseName = "Test",
                    SemesterId = 0,
                    StartDate = "11/11/2022",
                    EndDate = "12/12/2022",
                    CourseStatus = "Test"
                });

            JsonPatchDocument<CourseModel> testDocument = new JsonPatchDocument<CourseModel>();    
            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.PartiallyUpdateCourseById(0, testDocument);

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
            var result = await sut.PartiallyUpdateCourseById(0, testDocument);

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
                .Setup(x => x.GetCourseById<CourseModel>(0))
                .Throws(testException);

            // Act
            var result = await sut.PartiallyUpdateCourseById(0, testDocument);

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
                .Setup(x => x.GetCourseById<CourseModel>(0))
                .ReturnsAsync(
                new CourseModel()
                {
                    CourseId = 0,
                    TeacherId = 0,
                    CourseName = "Test",
                    SemesterId = 0,
                    StartDate = "11/11/2022",
                    EndDate = "12/12/2022",
                    CourseStatus = "Test"
                });
            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.DeleteCourseById(0);

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
            var result = await sut.DeleteCourseById(0);

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
                .Setup(x => x.GetCourseById<CourseModel>(0))
                .Throws(testException);

            // Act
            var result = await sut.DeleteCourseById(0);

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


        //[TestMethod]
        //public async Task PartiallyUpdateStudentInCourseById_ReturnsOKStatusCode()
        //{
        //    //Arrange
        //    Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
        //    mockCourseDao
        //        .Setup(x => x.GetCourseById(0))
        //        .ReturnsAsync(
        //        new StudentInCourseModel()
        //        {
        //            CourseId = 0,
        //            StudentId = 0,
        //            EnrollmentDate = "11/11/2022",
        //            Cancelled = false,
        //            CancellationReason = "test",
        //            HasPassed = false
        //        });

        //    JsonPatchDocument<StudentInCourseModel> testDocument = new JsonPatchDocument<StudentInCourseModel>();
        //    CourseController sut = new CourseController(mockCourseDao.Object);

        //    //Act
        //    var result = await sut.PartiallyUpdateStudentInCourseByCourseStudentId(0, 0, testDocument);

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        //}

        [TestMethod]
        public async Task PartiallyUpdateStudentInCourseByCourseStudentId_ReturnsNotFound_WhenGetCourseIdReturnsNull()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);
            JsonPatchDocument<StudentInCourseModel> testDocument = new JsonPatchDocument<StudentInCourseModel>();

            // Act
            var result = await sut.PartiallyUpdateStudentInCourseByCourseStudentId(0, 0, testDocument);

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
                .Setup(x => x.GetCourseById<StudentInCourseModel>(0))
                .Throws(testException);

            // Act
            var result = await sut.PartiallyUpdateStudentInCourseByCourseStudentId(0, 0, testDocument);

            // Arrange
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }

        //[TestMethod]
        //public async Task DeleteStudentInCourseByStudentCourseId_ReturnsOKStatusCode()
        //{
        //    // Arrange
        //    Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
        //    mockCourseDao
        //        .Setup(x => x.GetCourseById(0))
        //        .ReturnsAsync(
        //        new StudentInCourseModel()
        //        {
        //            CourseId = 0,
        //            StudentId = 0,
        //            EnrollmentDate = "11/11/2022",
        //            Cancelled = false,
        //            CancellationReason = "test",
        //            HasPassed = false
        //        });

        //    CourseController sut = new CourseController(mockCourseDao.Object);

        //    // Act
        //    var result = await sut.DeleteStudentInCourseByStudentCourseId(0, 0);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        //}

        [TestMethod]
        public async Task DeleteStudentInCourseByIdIdReturnsNull()
        {
            // Arrange
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>();
            CourseController sut = new CourseController(mockCourseDao.Object);

            // Act
            var result = await sut.DeleteStudentInCourseByStudentCourseId(0, 0);

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
                .Setup(x => x.GetCourseById<CourseModel>(0))
                .Throws(testException);

            // Act
            var result = await sut.DeleteStudentInCourseByStudentCourseId(0, 0);

            // Assert 
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
        }
    }
}
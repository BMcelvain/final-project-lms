using Lms.Controllers;
using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using FluentAssertions;
using System.Threading.Tasks;

namespace LMS.UnitTests
{
    [TestClass]
    public class StudentControllerTests
    {
        private Mock<IStudentDao> mockStudentDao;
        private StudentController sut;
        private Guid guid;


        [TestInitialize]
        public void Initialize()
        {
            mockStudentDao = new Mock<IStudentDao>();
            sut = new StudentController(mockStudentDao.Object);
            guid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A9");
        }

        //by using initialize and cleanup, we avoid repeat setup and teardown of the code - cleaner
        [TestCleanup]
        public void Cleanup()
        {
            mockStudentDao = null;
            sut = null;
        }

        private static Mock<StudentModel> GetMockStudent()
        {
            var mockStudent = new Mock<StudentModel>();
            mockStudent.Object.StudentId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A9");
            mockStudent.Object.StudentFirstName = "Fred";
            mockStudent.Object.StudentLastName = "Testing";
            mockStudent.Object.StudentPhone = "999-999-9999";
            mockStudent.Object.StudentEmail = "Test@test.com";
            mockStudent.Object.StudentStatus = "Test Status";
            mockStudent.Object.TotalPassCourses = 3;
            return mockStudent;
        }

        //private Guid guid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A9");

        [TestMethod]
        public async Task CreateStudent_ValidStudent_ReturnsOk()
        {
            // Arrange
            var mockStudent = GetMockStudent().Object;
            mockStudentDao
                .Setup(x => x.CreateStudent(It.IsAny<StudentModel>()))
                .Returns(Task.FromResult(mockStudent));

            // Act
            var result = await sut.CreateStudent(mockStudent);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public async Task GetStudentById_ValidId_ReturnsOk()
        {
            // Arrange
            //var guid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A9");
            var mockStudent = GetMockStudent();
            mockStudentDao
                .Setup(x => x.GetStudentById(guid))
                .ReturnsAsync(mockStudent.Object);

            // Act
            var result = await sut.GetStudentById(guid);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [TestMethod]
        public async Task GetStudentById_InvalidId_ReturnsNotFound()
        {
            // Arrange
            //var invalidId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A4");
            var mockStudent = GetMockStudent();
            mockStudentDao
                .Setup(x => x.GetStudentById(guid))
                .ReturnsAsync(mockStudent.Object);

            // Act
            var result = await sut.GetStudentById(new Guid());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();

        }

        [TestMethod]
        public async Task PartiallyUpdateStudentById_WithValidStudentIdAndUpdates_ReturnsOkObjectResult()
        {
            // Arrange
            //var studentId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A9");
            mockStudentDao
                 .Setup(x => x.PartiallyUpdateStudentById(It.IsAny<StudentModel>()))
                 .Callback(() => { return; });
            var student = new StudentModel { StudentId = guid, StudentFirstName = "Harry" };
            var patchDoc = new JsonPatchDocument<StudentModel>();
            patchDoc.Replace(s => s.StudentFirstName, "Joey");
            mockStudentDao.Setup(x => x.GetStudentById(guid)).ReturnsAsync(student);
            mockStudentDao.Setup(x => x.PartiallyUpdateStudentById(student)).Returns(Task.CompletedTask);

            // Act
            var result = await sut.PartiallyUpdateStudentById(guid, patchDoc);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task PartiallyUpdateStudentById_WithInvalidStudentId_ReturnsNotFound()
        {
            // Arrange
            //var studentId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A9");
            var invalidId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A3");
            mockStudentDao
                 .Setup(x => x.PartiallyUpdateStudentById(It.IsAny<StudentModel>()))
                 .Callback(() => { return; });
            var student = new StudentModel { StudentId = invalidId, StudentFirstName = "Harry" };
            var patchDoc = new JsonPatchDocument<StudentModel>();
            patchDoc.Replace(s => s.StudentFirstName, "Joey");
            mockStudentDao.Setup(x => x.GetStudentById(guid)).ReturnsAsync(student);
            mockStudentDao.Setup(x => x.PartiallyUpdateStudentById(student)).Returns(Task.CompletedTask);


            // Act
            var result = await sut.PartiallyUpdateStudentById(new Guid(), patchDoc);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteStudentByValidId_ReturnsOKStatusCode()
        {
            // Arrange
            var mockStudent = GetMockStudent();
            mockStudentDao
                .Setup(x => x.GetStudentById(new Guid()))
                .ReturnsAsync(mockStudent.Object);

            // Act
            var result = await sut.DeleteStudentById(new Guid());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task DeleteStudentById_ReturnsNotFound_WhenGetStudentByIdReturnsNull()
        {
            // Arrange
            mockStudentDao
                 .Setup(x => x.DeleteStudentById(It.IsAny<Guid>()))
                 .Callback(() => { return; });

            // Act
            var result = await sut.DeleteStudentById(new Guid());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task DeleteStudentById_ThrowExceptionError()
        {
            // Arrange
            var studentId = new Guid();
            mockStudentDao
                .Setup(x => x.DeleteStudentById(studentId))
                .Throws<Exception>();

            // Act
            var result = await sut.DeleteStudentById(new Guid());

            // Act & Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectResult));

        }

    }
}





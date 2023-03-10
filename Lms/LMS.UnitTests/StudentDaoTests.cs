using Lms.Wrappers;
using Lms.Daos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Lms.Models;
using Dapper;

namespace LMS.UnitTests
{
    [TestClass]
    public class StudentDaoTests
    {
        private readonly Mock<ISqlWrapper> mockSqlWrapper;

        public StudentDaoTests()
        {
            mockSqlWrapper = new Mock<ISqlWrapper>();
        }

        [TestMethod]
        public void CreateStudentInSql_UsesProperSqlQuery_OneTime()
        {
            //Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            StudentDao sut = new StudentDao(mockSqlWrapper.Object);
            var mockStudent = new StudentModel();

            //Act
            _ = sut.CreateStudent(mockStudent);

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "INSERT Student (StudentFirstName, StudentLastName,StudentPhone, StudentEmail, StudentStatus, TotalPassCourses)" + $"VALUES(@StudentFirstName, @StudentLastName, @StudentPhone, @StudentEmail, @StudentStatus, @TotalPassCourses)"), It.IsAny<DynamicParameters>()), Times.Once);
        }


        [TestMethod]
        public void GetStudentsById_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            StudentDao sut = new StudentDao(mockSqlWrapper.Object);

            // Act
            _ = sut.GetStudentById(2);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryFirstOrDefaultAsync<StudentModel>(It.Is<string>(sql => sql == "SELECT * FROM Student WHERE StudentId = 2")), Times.Once);
        }

        [TestMethod]
        public void PartiallyUpdateStudentById_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            StudentDao sut = new StudentDao(mockSqlWrapper.Object);
            var mockStudent = new StudentModel();

            // Act
            _ = sut.PartiallyUpdateStudentById(mockStudent);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "UPDATE Student SET StudentFirstName=@StudentFirstName, StudentLastName=@StudentLastName, " + $"StudentPhone=@StudentPhone, StudentEmail=@StudentEmail, StudentStatus=@StudentStatus, TotalPassCourses=@TotalPassCourses" + $" WHERE StudentId=@StudentId"), It.IsAny<DynamicParameters>()));
        }

        [TestMethod]
        public void DeleteStudentById_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            StudentDao sut = new StudentDao(mockSqlWrapper.Object);

            // Act
            _ = sut.DeleteStudentById(1);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "DELETE FROM Student WHERE StudentId = 1")), Times.Once); ;
        }
    }
}

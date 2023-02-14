using Lms.Wrappers;
using Lms.Daos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Lms.Models;
using Dapper;

namespace LMS.UnitTests
{
    [TestClass] // Every class must have this.
    public class CourseDaoTests
    {
        [TestMethod]
        public void CreateCourse_UsesProperSqlQuery_OneTime()
        {
            //Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            CourseDao sut = new CourseDao(mockSqlWrapper.Object);
            var mockCourse = new CourseModel();

            //Act
            _ = sut.CreateCourse(mockCourse);

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsyncWithParameters(It.Is<string>(sql => sql == "INSERT Course (TeacherId, CourseName, SemesterId, StartDate, EndDate, CourseStatus)VALUES(@TeacherId, @CourseName, @SemesterId, @StartDate, @EndDate, @CourseStatus)"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void GetAllCourses_UsesProperSqlQuery_OneTime()
        {
            //Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();  
            CourseDao sut = new CourseDao(mockSqlWrapper.Object);

            //Act
            _ = sut.GetCourses();

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<CourseModel>(It.Is<string>(sql => sql == "SELECT * FROM Course")), Times.Once);
        }

        [TestMethod]
        public void GetCoursesById_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            CourseDao sut = new CourseDao(mockSqlWrapper.Object);

            // Act
            _ = sut.GetCourseById(1);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryFirstOrDefaultAsync<CourseModel>(It.Is<string>(sql => sql == "SELECT * FROM Course WHERE CourseId = 1")));
        }

        [TestMethod]
        public void GetCoursesByStatus_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            CourseDao sut = new CourseDao(mockSqlWrapper.Object);

            // Act
            _ = sut.GetCourseByStatus("Active");

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<CourseModel>(It.Is<string>(sql => sql == "SELECT * FROM Course WHERE CourseStatus = 'Active'")));
        }

        [TestMethod]
        public void PartiallyUpdateCourseById_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            CourseDao sut = new CourseDao(mockSqlWrapper.Object);
            var mockCourse = new CourseModel();

            // Act
            _ = sut.PartiallyUpdateCourseById(mockCourse);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsyncWithParameters(It.Is<string>(sql => sql == "UPDATE Course SET TeacherId=@TeacherId, CourseName=@CourseName, SemesterId=@SemesterId, StartDate=@StartDate, EndDate=@EndDate, CourseStatus=@CourseStatus WHERE CourseId=@CourseId"), It.IsAny<DynamicParameters>()));
        }

        [TestMethod]
        public void DeleteCourseById_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            CourseDao sut = new CourseDao(mockSqlWrapper.Object);

            // Act
            _ = sut.DeleteCourseById(1);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "DELETE FROM Course WHERE CourseId = 1")));
        }
    }
}

using Lms.Wrappers;
using Lms.Daos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Lms.Models;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace LMS.UnitTests
{
    [TestClass] // Every class must have this.
    public class CourseDaoTests
    {

        [TestMethod]
        public void GetAllCoursesInSql()
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
        public void CreateCourseInSql()
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
    }
}

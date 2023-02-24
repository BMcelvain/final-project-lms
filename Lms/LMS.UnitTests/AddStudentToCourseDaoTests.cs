using Lms.Wrappers;
using Lms.Daos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Lms.Models;
using Dapper;

namespace LMS.UnitTests
{
    [TestClass]
    public class AddStudentToCourseDaoTests
    {
        [TestMethod]
        public void AddStudentToCourse_UsesProperSqlQuery_OneTime()
        {
            //Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new();
            AddStudentToCourseDao sut = new(mockSqlWrapper.Object);
            var mockCourse = new AddStudentToCourseModel();

            //Act
            _ = sut.AddStudentToCourse(mockCourse);

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "INSERT StudentEnrollmentLog (CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed)" +
              $"VALUES (@CourseId, @SemesterId, @StudentId, @EnrollmentDate, @Cancelled, @CancellationReason, @HasPassed)"), It.IsAny<DynamicParameters>()), Times.Once);
        }


        [TestMethod]
        public void GetCourseByCourseId_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new();
            AddStudentToCourseDao sut = new(mockSqlWrapper.Object);

            // Act
            _ = sut.GetCourseByCourseId(1);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryFirstOrDefaultAsync<AddStudentToCourseModel>(It.Is<string>(sql => sql == "SELECT * FROM StudentEnrollmentLog WHERE CourseId = 1")));
        }


        //[TestMethod]
        //public void PartiallyUpdateStudentInCourseByCourseStudentId_UsesProperSqlQuery_OneTime()
        //{
        //    // Arrange
        //    Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
        //    AddStudentToCourseDao sut = new AddStudentToCourseDao(mockSqlWrapper.Object);

        //    // Act
        //    _ = sut.PartiallyUpdateStudentInCourseByCourseStudentId(1, 1);

        //    // Assert
        //    mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "UPDATE StudentEnrollmentLog SET CourseId=@CourseId, SemesterId=@SemesterId, StudentId=@StudentId,EnrollmentDate=@EnrollmentDate, Cancelled=@Cancelled, CancellationReason=@CancellationReason, HasPassed=@HasPassed WHERE StudentId=@StudentId AND CourseId=@CourseId"), It.IsAny<DynamicParameters>()));
        //}

        [TestMethod]
        public void DeleteStudentInCourseByStudentCourseId_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new();
            AddStudentToCourseDao sut = new(mockSqlWrapper.Object);

            // Act
            _ = sut.DeleteStudentInCourseByStudentCourseId(1, 1);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == $"DELETE FROM StudentEnrollmentLog WHERE StudentId = 1 AND CourseId = 1")));
        }
    }
}

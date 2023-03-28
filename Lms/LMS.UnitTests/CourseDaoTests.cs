using Lms.Wrappers;
using Lms.Daos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Lms.Models;
using Dapper;
using System;

namespace LMS.UnitTests
{
    [TestClass]
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
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "INSERT Course (TeacherId, CourseName, SemesterId, StartDate, EndDate, CourseStatus)VALUES(@TeacherId, @CourseName, @SemesterId, @StartDate, @EndDate, @CourseStatus)"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void GetCoursesById_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            CourseDao sut = new CourseDao(mockSqlWrapper.Object);

            // Act
            _ = sut.GetCourseById<CourseModel>(new Guid());

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryFirstOrDefaultAsync<CourseModel>(It.Is<string>(sql => sql == "SELECT * FROM Course WHERE CourseId = 1")));
        }


        [TestMethod]
        public void DeleteCourseById_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            CourseDao sut = new CourseDao(mockSqlWrapper.Object);

            // Act
            _ = sut.DeleteCourseById(new Guid());

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "DELETE FROM Course WHERE CourseId = 1")));
        }

        //------Add Student In Course Section -----------

        [TestMethod]
        public void AddStudentInCourse_UsesProperSqlQuery_OneTime()
        {
            //Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new();
            CourseDao sut = new(mockSqlWrapper.Object);
            var mockCourse = new StudentInCourseModel();

            //Act
            _ = sut.StudentInCourse(mockCourse);

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "INSERT StudentEnrollmentLog (CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed)" +
              $"VALUES (@CourseId, @StudentId, @EnrollmentDate, @Cancelled, @CancellationReason, @HasPassed)"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void PartiallyUpdateStudentInCourseByCourseStudentId_UsesProperSqlQuery_OneTime()
        {
            {
                // Arrange	
                Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
                CourseDao sut = new CourseDao(mockSqlWrapper.Object);
                var mockModel = new StudentInCourseModel();

                // Act
                _ = sut.PartiallyUpdateStudentInCourseByCourseStudentId(mockModel);

                // Assert
                mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "UPDATE StudentEnrollmentLog SET CourseId=@CourseId, StudentId=@StudentId, " +
                        $"EnrollmentDate=@EnrollmentDate, Cancelled=@Cancelled, CancellationReason=@CancellationReason, HasPassed=@HasPassed WHERE StudentId=@StudentId AND CourseId=@CourseId"), It.IsAny<DynamicParameters>()));
            }
        }

        [TestMethod]
        public void DeleteStudentInCourseByStudentCourseId_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new();
            CourseDao sut = new(mockSqlWrapper.Object);

            // Act
            _ = sut.DeleteStudentInCourseByStudentCourseId(new StudentInCourseModel());

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == $"DELETE FROM StudentEnrollmentLog WHERE StudentId = 1 AND CourseId = 1")));
        }
    }
}

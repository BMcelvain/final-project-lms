using Dapper;
using Lms.Daos;
using Lms.Models;
using Lms.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace LMS.UnitTests
{
    [TestClass]
    public class StudentEnrollmentDaoTests
    {
        [TestMethod]
        public void GetStudentEnrollmentHistoryById_UsesProperSqlQuery_OneTime()
        {   
            // Arrange
            var mockSqlWrapper = new Mock<ISqlWrapper>();
            var sut = new StudentEnrollmentDao(mockSqlWrapper.Object);

            // Act
            _ = sut.GetStudentEnrollmentHistoryById(0);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<StudentEnrollmentModel>(It.Is<string>(sql => sql == $"SELECT" +
            $" [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseName]" +
            $", [LearningManagementSystem].[dbo].[Course].[StartDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[EndDate]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[Cancelled]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CancellationReason]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[HasPassed]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherEmail]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentPhone]" +
            $", [LearningManagementSystem].[dbo].[Student].[TotalPassCourses]" +
            $" FROM [LearningManagementSystem].[dbo].[StudentEnrollmentLog]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Course] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CourseId] = [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Teacher] ON [LearningManagementSystem].[dbo].[Course].[TeacherId] = [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Student] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $" WHERE [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = 0"+
            $" ORDER BY HasPassed ASC,CourseName")), Times.Once);
        }

        [TestMethod]
        public void GetStudentsInCourseByCourseId_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new();
            StudentEnrollmentDao sut = new(mockSqlWrapper.Object);

            // Act
            _ = sut.GetStudentsInCourseByCourseId(1);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<StudentModel>(It.Is<string>(sql => sql == $"SELECT * " +
            $"FROM [LearningManagementSystem].[dbo].[StudentEnrollmentLog]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Student] ON [LearningManagementSystem].[dbo].[Student].[StudentId] = [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId]" +
            $"WHERE CourseId = 1"+
            $"ORDER BY StudentLastName")), Times.Once);
        }
    }
}

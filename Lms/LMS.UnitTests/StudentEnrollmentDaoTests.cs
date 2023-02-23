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
        public async Task GetStudentEnrollmentHistoryById_UsesProperSqlQuery_OneTime()
        {   
            // Arrange
            var mockSqlWrapper = new Mock<ISqlWrapper>();
            var sut = new StudentEnrollmentDao(mockSqlWrapper.Object);

            // Act
            _ = sut.GetStudentEnrollmentHistoryById(0);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<StudentEnrollmentModel>(It.Is<string>(sql => sql == "SELECT [Id]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseName]" +
            $", [LearningManagementSystem].[dbo].[Course].[StartDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[EndDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseStatus]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[SemesterId]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[EnrollmentDate]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[Cancelled]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CancellationReason]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[HasPassed]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherFirstName]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherLastName]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherPhone]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherEmail]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherStatus]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentFirstName]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentLastName]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentPhone]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentEmail]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentStatus]" +
            $", [LearningManagementSystem].[dbo].[Student].[TotalPassCourses]" +
            $" FROM[LearningManagementSystem].[dbo].[StudentEnrollmentLog]" +
            $" INNER JOIN[LearningManagementSystem].[dbo].[Course] ON[LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CourseId] = [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $" INNER JOIN[LearningManagementSystem].[dbo].[Teacher] ON[LearningManagementSystem].[dbo].[Course].[TeacherId] = [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $" INNER JOIN[LearningManagementSystem].[dbo].[Student] ON[LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $" WHERE [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = 0")), Times.Once);
        }

        [TestMethod]
        public async Task GetStudentEnrollmentHistoryByStudentLastName_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            var mockSqlWrapper = new Mock<ISqlWrapper>();
            var sut = new StudentEnrollmentDao(mockSqlWrapper.Object);

            // Act
            _ = sut.GetStudentEnrollmentHistoryByStudentLastName("test");

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<StudentEnrollmentModel>(It.Is<string>(sql => sql == "SELECT [Id]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseName]" +
            $", [LearningManagementSystem].[dbo].[Course].[StartDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[EndDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseStatus]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[SemesterId]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[EnrollmentDate]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[Cancelled]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CancellationReason]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[HasPassed]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherFirstName]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherLastName]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherPhone]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherEmail]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherStatus]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentFirstName]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentLastName]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentPhone]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentEmail]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentStatus]" +
            $", [LearningManagementSystem].[dbo].[Student].[TotalPassCourses]" +
            $"  FROM[LearningManagementSystem].[dbo].[Student]" +
            $"  INNER JOIN [LearningManagementSystem].[dbo].[StudentEnrollmentLog]  ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = [LearningManagementSystem]. [dbo].[Student].[StudentId]" +
            $"  INNER JOIN [LearningManagementSystem].[dbo].[Course] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CourseId] = [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $"  INNER JOIN [LearningManagementSystem].[dbo].[Teacher] ON [LearningManagementSystem].[dbo].[Course].[TeacherId] = [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $"  WHERE [LearningManagementSystem].[dbo].[Student].[StudentLastName] = 'test'")), Times.Once);
        }
    }
}

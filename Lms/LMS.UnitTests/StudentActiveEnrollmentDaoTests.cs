using Lms.Daos;
using Lms.Models;
using Lms.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LMS.UnitTests
{
    public class StudentActiveEnrollmentDaoTests
    {
        [TestMethod]
        public void GetActiveStudents_UsesProperSqlQuery_OneTime()
        {
            //Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            StudentActiveEnrollmentDao sut = new StudentActiveEnrollmentDao(mockSqlWrapper.Object);          

            //Act
            _ = sut.GetActiveStudentEnrollmentByStudentLastName("test");

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<StudentActiveEnrollmentModel>(It.Is<string>(sql => sql == $"SELECT [LearningManagementSystem].[dbo].[Student].[StudentLastName]" +
            $",  [LearningManagementSystem].[dbo].[Student].[StudentFirstName]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseName]" +
            $", [LearningManagementSystem].[dbo].[Course].[StartDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[EndDate]" +
            $", [LearningManagementSystem].[dbo].[Semester].[Semester]" +
            $" FROM [LearningManagementSystem].[dbo].[Student]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[StudentEnrollmentLog] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Course] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CourseId] = [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Teacher] ON [LearningManagementSystem].[dbo].[Course].[TeacherId] = [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
             $" INNER JOIN [LearningManagementSystem].[dbo].[Semester] ON [LearningManagementSystem].[dbo].[Course].[SemesterId] = [LearningManagementSystem].[dbo].[Semester].[SemesterId]"+
            $" WHERE [LearningManagementSystem].[dbo].[Student].[StudentLastName] = 'Test' AND [LearningManagementSystem].[dbo].[Course].[CourseStatus] = 'Active'")), Times.Once);
        }
    }
}

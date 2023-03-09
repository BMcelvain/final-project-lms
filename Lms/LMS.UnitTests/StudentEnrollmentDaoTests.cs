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
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<StudentEnrollmentModel>(It.Is<string>(sql => sql == "SELECT" +
            $" [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseName]" +
            $", [LearningManagementSystem].[dbo].[Course].[StartDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[EndDate]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[Cancelled]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CancellationReason]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[HasPassed]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherEmail]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentEmail]" +
            $", [LearningManagementSystem].[dbo].[Student].[TotalPassCourses]" +
            $" FROM[LearningManagementSystem].[dbo].[StudentEnrollmentLog]" +
            $" INNER JOIN[LearningManagementSystem].[dbo].[Course] ON[LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CourseId] = [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $" INNER JOIN[LearningManagementSystem].[dbo].[Teacher] ON[LearningManagementSystem].[dbo].[Course].[TeacherId] = [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $" INNER JOIN[LearningManagementSystem].[dbo].[Student] ON[LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $" WHERE [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = 0")), Times.Once);
        }

        [TestMethod]
        public void GetStudentEnrollmentHistoryByStudentLastName_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            var mockSqlWrapper = new Mock<ISqlWrapper>();
            var sut = new StudentEnrollmentDao(mockSqlWrapper.Object);

            // Act
            _ = sut.GetStudentEnrollmentHistoryByStudentLastName("test");

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<StudentEnrollmentModel>(It.Is<string>(sql => sql == "SELECT" +
            $" [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseName]" +
            $", [LearningManagementSystem].[dbo].[Course].[StartDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[EndDate]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[Cancelled]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CancellationReason]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[HasPassed]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherEmail]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentEmail]" +
            $", [LearningManagementSystem].[dbo].[Student].[TotalPassCourses]" +
            $" FROM[LearningManagementSystem].[dbo].[StudentEnrollmentLog]" +
            $" INNER JOIN[LearningManagementSystem].[dbo].[Course] ON[LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CourseId] = [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $" INNER JOIN[LearningManagementSystem].[dbo].[Teacher] ON[LearningManagementSystem].[dbo].[Course].[TeacherId] = [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $" INNER JOIN[LearningManagementSystem].[dbo].[Student] ON[LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $"  WHERE [LearningManagementSystem].[dbo].[Student].[StudentLastName] = 'test'")), Times.Once);
        }

        [TestMethod]
        public void GetActiveStudents_UsesProperSqlQuery_OneTime()
        {
            //Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            StudentEnrollmentDao sut = new StudentEnrollmentDao(mockSqlWrapper.Object);

            //Act
            _ = sut.GetActiveStudentEnrollmentByStudentPhone("test");

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<StudentEnrollmentModel>(It.Is<string>(sql => sql == $"SELECT" +
           $" [LearningManagementSystem].[dbo].[Course].[CourseId]" +
           $", [LearningManagementSystem].[dbo].[Course].[CourseName]" +
           $", [LearningManagementSystem].[dbo].[Course].[StartDate]" +
           $", [LearningManagementSystem].[dbo].[Course].[EndDate]" +
           $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[Cancelled]" +
           $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CancellationReason]" +
           $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[HasPassed]" +
           $", [LearningManagementSystem].[dbo].[Teacher].[TeacherEmail]" +
           $", [LearningManagementSystem].[dbo].[Student].[StudentEmail]" +
           $", [LearningManagementSystem].[dbo].[Student].[TotalPassCourses]" +
           $" FROM [LearningManagementSystem].[dbo].[Student]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[StudentEnrollmentLog] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Course] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CourseId] = [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Teacher] ON [LearningManagementSystem].[dbo].[Course].[TeacherId] = [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Semester] ON [LearningManagementSystem].[dbo].[Course].[SemesterId] = [LearningManagementSystem].[dbo].[Semester].[SemesterId]" +
            $" WHERE [LearningManagementSystem].[dbo].[Student].[StudentPhone] = 'Test' AND [LearningManagementSystem].[dbo].[Course].[CourseStatus] = 'Active'")), Times.Once);
        }

        [TestMethod]
        public void AddStudentToCourse_UsesProperSqlQuery_OneTime()
        {
            //Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new();
            StudentEnrollmentDao sut = new(mockSqlWrapper.Object);
            var mockCourse = new StudentEnrollmentModel();

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
            StudentEnrollmentDao sut = new(mockSqlWrapper.Object);

            // Act
            _ = sut.GetCourseByCourseId(1);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryFirstOrDefaultAsync<StudentEnrollmentModel>(It.Is<string>(sql => sql == "SELECT * FROM StudentEnrollmentLog WHERE CourseId = 1")));
        }


        [TestMethod]
        public void PartiallyUpdateStudentInCourseByCourseStudentId_UsesProperSqlQuery_OneTime()
        {
            {
                // Arrange	
                Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
                StudentEnrollmentDao sut = new StudentEnrollmentDao(mockSqlWrapper.Object);
                var mockModel = new StudentEnrollmentModel();


                // Act
                _ = sut.PartiallyUpdateStudentInCourseByCourseStudentId(mockModel);


                // Assert
                mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "UPDATE StudentEnrollmentLog SET CourseId=@CourseId, SemesterId=@SemesterId, StudentId=@StudentId, " +
                  $"EnrollmentDate=@EnrollmentDate, Cancelled=@Cancelled, CancellationReason=@CancellationReason, HasPassed=@HasPassed WHERE StudentId=@StudentId AND CourseId=@CourseId"),It.IsAny<DynamicParameters>()));
            }
        }

        [TestMethod]
        public void DeleteStudentInCourseByStudentCourseId_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new();
            StudentEnrollmentDao sut = new(mockSqlWrapper.Object);

            // Act
            _ = sut.DeleteStudentInCourseByStudentCourseId(1, 1);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == $"DELETE FROM StudentEnrollmentLog WHERE StudentId = 1 AND CourseId = 1")));
        }
    }
}

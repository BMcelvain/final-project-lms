using Dapper;
using Lms.Daos;
using Lms.Models;
using Lms.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.UnitTests
{
    #nullable disable
    [TestClass]
    public class StudentEnrollmentDaoTests
    {

        Mock<ISqlWrapper> mockSqlWrapper;
        StudentEnrollmentDao sut;
        Guid courseGuid;
        Guid studentGuid;
        List<StudentEnrollmentModel> studentEnrollment;

        [TestInitialize]
        public void Initialize()
        {
            mockSqlWrapper = new Mock<ISqlWrapper>();
            sut = new StudentEnrollmentDao(mockSqlWrapper.Object);
            courseGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167B0");
            studentGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A7");
            studentEnrollment = new List<StudentEnrollmentModel>()
            {
                new StudentEnrollmentModel()
                {
                    CourseId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167B0"),
                    CourseName = "Test",
                    StartDate = "3/1/2023",
                    EndDate = "5/30/2023",
                    Cancelled = false,
                    CancellationReason = "test cancel",
                    HasPassed = true
                }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            mockSqlWrapper = null;
            sut = null;
            courseGuid = new Guid();
            studentGuid = new Guid();
            studentEnrollment = null;
        }
        [TestMethod]
        public void GetStudentEnrollmentHistoryByStudentId_UsesProperSqlQuery_OneTime()
        {   

            // Act
            _ = sut.GetStudentEnrollmentHistoryByStudentId(studentGuid);

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
            $" WHERE [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = @StudentId" +
            $" ORDER BY HasPassed ASC,CourseName"), It.IsAny<DynamicParameters>()), Times.Once);
        }


        [TestMethod]
        public void GetStudentEnrollmentHistoryByStudentLastName_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.GetStudentEnrollmentHistoryByStudentLastName("test");

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
            $"  WHERE [LearningManagementSystem].[dbo].[Student].[StudentLastName] = @studentLastName" +
            $" ORDER BY HasPassed ASC,CourseName"), It.IsAny<object>()), Times.Once);
        }


        //Table has to be updated Has Pass
        [TestMethod]
        public void GetActiveStudentEnrollmentByStudentPhone_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.GetActiveStudentEnrollmentByStudentPhone("888-888-8888");

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
            $", [LearningManagementSystem].[dbo].[Student].[StudentEmail]" +
            $", [LearningManagementSystem].[dbo].[Student].[TotalPassCourses]" +
            $" FROM [LearningManagementSystem].[dbo].[Student]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[StudentEnrollmentLog] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Course] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CourseId] = [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Teacher] ON [LearningManagementSystem].[dbo].[Course].[TeacherId] = [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $" WHERE [LearningManagementSystem].[dbo].[Student].[StudentPhone] = @studentPhone AND [LearningManagementSystem].[dbo].[Course].[CourseStatus] = 'Active' AND [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[Cancelled] = 0 AND [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[HasPassed] IS NULL ORDER BY StartDate ASC,CourseName"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void GetStudentsInCourseByCourseId_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.GetStudentsInCourseByCourseId(courseGuid);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<StudentModel>(It.Is<string>(sql => sql ==
           $"SELECT * " +
            $"FROM [LearningManagementSystem].[dbo].[StudentEnrollmentLog] " +
            $"INNER JOIN [LearningManagementSystem].[dbo].[Student] ON [LearningManagementSystem].[dbo].[Student].[StudentId] = [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] " +
            $"WHERE CourseId = @CourseId " +
            $"ORDER BY StudentLastName"), It.IsAny<DynamicParameters>()), Times.Once);
        }

    }
}

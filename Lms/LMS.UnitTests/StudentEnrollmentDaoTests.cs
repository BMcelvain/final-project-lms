using Dapper;
using Lms.Daos;
using Lms.Models;
using Lms.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

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
            $" [Course].[CourseId]" +
            $", [Course].[CourseName]" +
            $", [Course].[StartDate]" +
            $", [Course].[EndDate]" +
            $", [StudentEnrollmentLog].[Cancelled]" +
            $", [StudentEnrollmentLog].[CancellationReason]" +
            $", [StudentEnrollmentLog].[HasPassed]" +
            $", [Teacher].[TeacherEmail]" +
            $", [Student].[StudentPhone]" +
            $" FROM [StudentEnrollmentLog]" +
            $" INNER JOIN [Course] ON [StudentEnrollmentLog].[CourseId] = [Course].[CourseId]" +
            $" INNER JOIN [Teacher] ON [Course].[TeacherId] = [Teacher].[TeacherId]" +
            $" INNER JOIN [Student] ON [StudentEnrollmentLog].[StudentId] = [Student].[StudentId]" +
            $" WHERE [StudentEnrollmentLog].[StudentId] = @StudentId" +
            $" ORDER BY HasPassed ASC,CourseName"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void GetStudentEnrollmentHistoryByStudentLastName_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.GetStudentEnrollmentHistoryByStudentLastName("test");

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<StudentEnrollmentModel>(It.Is<string>(sql => sql == $"SELECT" +
            $" [Course].[CourseId]" +
            $", [Course].[CourseName]" +
            $", [Course].[StartDate]" +
            $", [Course].[EndDate]" +
            $", [StudentEnrollmentLog].[Cancelled]" +
            $", [StudentEnrollmentLog].[CancellationReason]" +
            $", [StudentEnrollmentLog].[HasPassed]" +
            $", [Teacher].[TeacherEmail]" +
            $", [Student].[StudentPhone]" +
            $" FROM [StudentEnrollmentLog]" +
            $" INNER JOIN [Course] ON [StudentEnrollmentLog].[CourseId] = [Course].[CourseId]" +
            $" INNER JOIN [Teacher] ON [Course].[TeacherId] = [Teacher].[TeacherId]" +
            $" INNER JOIN [Student] ON [StudentEnrollmentLog].[StudentId] = [Student].[StudentId]" +
            $"  WHERE [Student].[StudentLastName] = @studentLastName" +
            $" ORDER BY HasPassed ASC,CourseName"), It.IsAny<object>()), Times.Once);
        }

        [TestMethod]
        public void GetActiveStudentEnrollmentByStudentPhone_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.GetActiveStudentEnrollmentByStudentPhone("888-888-8888");

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<StudentEnrollmentModel>(It.Is<string>(sql => sql == $"SELECT" +
            $" [Course].[CourseId]" +
            $", [Course].[CourseName]" +
            $", [Course].[StartDate]" +
            $", [Course].[EndDate]" +
            $", [StudentEnrollmentLog].[Cancelled]" +
            $", [StudentEnrollmentLog].[CancellationReason]" +
            $", [StudentEnrollmentLog].[HasPassed]" +
            $", [Teacher].[TeacherEmail]" +
            $", [Student].[StudentEmail]" +
            $" FROM [Student]" +
            $" INNER JOIN [StudentEnrollmentLog] ON [StudentEnrollmentLog].[StudentId] = [Student].[StudentId]" +
            $" INNER JOIN [Course] ON [StudentEnrollmentLog].[CourseId] = [Course].[CourseId]" +
            $" INNER JOIN [Teacher] ON [Course].[TeacherId] = [Teacher].[TeacherId]" +
            $" WHERE [Student].[StudentPhone] = @studentPhone AND [Course].[CourseStatus] = 'Active' AND [StudentEnrollmentLog].[Cancelled] = 0 AND [StudentEnrollmentLog].[HasPassed] IS NULL ORDER BY StartDate ASC,CourseName"), It.IsAny<Object>()), Times.Once);
        }

        [TestMethod]
        public void GetStudentsInCourseByCourseId_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.GetStudentsInCourseByCourseId(courseGuid);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<StudentModel>(It.Is<string>(sql => sql ==
           $"SELECT * " +
            $"FROM [StudentEnrollmentLog] " +
            $"INNER JOIN [Student] ON [Student].[StudentId] = [StudentEnrollmentLog].[StudentId] " +
            $"WHERE CourseId = @CourseId " +
            $"ORDER BY StudentLastName"), It.IsAny<DynamicParameters>()), Times.Once);
        }
    }
}

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

        private Mock<ISqlWrapper> _mockSqlWrapper;
        private StudentEnrollmentDao _sut;
        private Guid _courseGuid;
        private Guid _studentGuid;
        private List<StudentEnrollmentModel> _studentEnrollment;

        [TestInitialize]
        public void Initialize()
        {
            _mockSqlWrapper = new Mock<ISqlWrapper>();
            _sut = new StudentEnrollmentDao(_mockSqlWrapper.Object);
            _courseGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167B0");
            _studentGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A7");
            _studentEnrollment = new List<StudentEnrollmentModel>()
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
            _mockSqlWrapper = null;
            _sut = null;
            _courseGuid = new Guid();
            _studentGuid = new Guid();
            _studentEnrollment = null;
        }

        [TestMethod]
        public void GetStudentEnrollmentHistoryByStudentId_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = _sut.GetStudentEnrollmentHistory(_studentGuid, null, null, null, null);

            // Assert
            _mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<StudentEnrollmentModel>(It.Is<string>(sql => sql == $"SELECT" +
            $"  C.[CourseId]" +
            $", C.[CourseName]" +
            $", C.[CourseStatus]" +
            $", C.[StartDate]" +
            $", C.[EndDate]" +
            $", SE.[Cancelled]" +
            $", SE.[CancellationReason]" +
            $", SE.[HasPassed]" +
            $", T.[TeacherEmail]" +
            $", S.[StudentEmail]" +
            $" FROM [StudentEnrollmentLog] as SE" +
            $" INNER JOIN [Course] as C ON SE.[CourseId] = C.[CourseId]" +
            $" INNER JOIN [Teacher] as T ON C.[TeacherId] = T.[TeacherId]" +
            $" INNER JOIN [Student] as S ON SE.[StudentId] = S.[StudentId]" +
            $" WHERE 1=1 AND S.StudentId = @StudentId"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void GetStudentsInCourseByCourseId_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = _sut.GetStudentsInCourseByCourseId(_courseGuid);

            // Assert
            _mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<StudentModel>(It.Is<string>(sql => sql ==
           $"SELECT * " +
            $"FROM [StudentEnrollmentLog] " +
            $"INNER JOIN [Student] ON [Student].[StudentId] = [StudentEnrollmentLog].[StudentId] " +
            $"WHERE CourseId = @CourseId " +
            $"ORDER BY StudentLastName"), It.IsAny<DynamicParameters>()), Times.Once);
        }
    }
}

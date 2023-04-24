using Dapper;
using Lms.Daos;
using Lms.Models;
using Lms.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LMS.UnitTests
{
    #nullable disable warnings
    [TestClass]
    public class CourseDaoTests
    {
        Mock<ISqlWrapper> mockSqlWrapper;
        CourseDao sut;
        Guid courseGuid;
        List<CourseModel> courses;

        [TestInitialize]
        public void Initialize()
        {
            mockSqlWrapper = new Mock<ISqlWrapper>();
            sut = new CourseDao(mockSqlWrapper.Object);
            courseGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A6");
            courses = new List<CourseModel>()
            {
                new CourseModel()
                {
                    CourseId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A6"),
                    TeacherId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A7"),
                    CourseName = "Test",
                    StartDate = "01/01/2023",
                    EndDate = "03/01/2023",
                    CourseStatus = "Active"
                }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            mockSqlWrapper = null;
            sut = null;
            courseGuid = new Guid();
            courses = null;
        }

        [TestMethod]
        public void CreateCourse_UsesProperSqlQuery_OneTime()
        {
            //Act
            _ = sut.CreateCourse(courses.First());

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "INSERT Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus)VALUES(@CourseId, @TeacherId, @CourseName, @StartDate, @EndDate, @CourseStatus)"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void GetCoursesById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.GetCourseById<CourseModel>(courseGuid);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryFirstOrDefaultAsync<CourseModel>(It.Is<string>(sql => sql == "SELECT * FROM Course WHERE CourseId = @Id"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void GetCoursesByStatus_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.GetCourseByStatus("Active");

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<CourseModel>(It.Is<string>(sql => sql == "SELECT * FROM Course WHERE CourseStatus = @courseStatus ORDER BY StartDate ASC"), It.IsAny<object>()), Times.Once);
        }

        [TestMethod]
        public void PartiallyUpdateCourseById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.PartiallyUpdateCourseById(courses.First());

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "UPDATE Course SET TeacherId=@TeacherId, CourseName=@CourseName, StartDate=@StartDate, EndDate=@EndDate, CourseStatus=@CourseStatus WHERE CourseId=@CourseId"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void DeleteCourseById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.DeleteCourseById(courseGuid);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "DELETE FROM Course WHERE CourseId = @CourseId"), It.IsAny<DynamicParameters>()), Times.Once);
        }
    }
}
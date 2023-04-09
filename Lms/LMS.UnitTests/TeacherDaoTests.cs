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
    #nullable disable
    [TestClass]
    public class TeacherDaoTests
    {
        Mock<ISqlWrapper> mockSqlWrapper;
        TeacherDao sut;
        Guid teacherGuid;
        List<TeacherModel> teachers;

        [TestInitialize]
        public void Initialize()
        {
            mockSqlWrapper = new Mock<ISqlWrapper>();
            sut = new TeacherDao(mockSqlWrapper.Object);
            teacherGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A6");
            teachers = new List<TeacherModel>()
            {
                new TeacherModel()
                {
                    TeacherId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A6"),
                    TeacherFirstName = "Test",
                    TeacherLastName = "Teacher",
                    TeacherPhone = "999-999-9999",
                    TeacherEmail = "teacher@vu.com",
                    TeacherStatus = "Active"
                }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            mockSqlWrapper = null;
            sut = null;
            teacherGuid = new Guid();
            teachers = null;
        }

        [TestMethod]
        public void CreateTeacher_UsesProperSqlQuery_OneTime()
        {
            //Act
            _ = sut.CreateTeacher(teachers.First());

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "INSERT Teacher(TeacherId, TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail,TeacherStatus)VALUES(@TeacherId, @TeacherFirstName, @TeacherLastName, @TeacherPhone, @TeacherEmail, @TeacherStatus)"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void GetTeachersById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.GetTeacherById<TeacherModel>(teacherGuid);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryFirstOrDefaultAsync<TeacherModel>(It.Is<string>(sql => sql == "SELECT * FROM Teacher WHERE TeacherId = @TeacherId"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void GetTeachersByStatus_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.GetTeacherByStatus("Test");

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<TeacherModel>(It.Is<string>(sql => sql == "SELECT * FROM Teacher WHERE TeacherStatus = @teacherStatus ORDER BY TeacherLastName ASC"), It.IsAny<object>()), Times.Once);
        }

        [TestMethod]
        public void PartiallyUpdateTeacherById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.PartiallyUpdateTeacherById(teachers.First());

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "UPDATE Teacher SET TeacherFirstName=@TeacherFirstName, TeacherLastName=@TeacherLastName, " +
              $"TeacherPhone=@TeacherPhone, TeacherEmail=@TeacherEmail, TeacherStatus=@TeacherStatus WHERE TeacherId=@TeacherId"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void DeleteTeacherById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.DeleteTeacherById(teacherGuid);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "DELETE FROM Teacher WHERE TeacherId = @TeacherId"), It.IsAny<DynamicParameters>()), Times.Once);
        }
    }
}

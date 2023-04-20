using Dapper;
using Lms.Daos;
using Lms.Models;
using Lms.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data;

namespace LMS.UnitTests
{
    #nullable disable
    [TestClass]
    public class TeacherDaoTests
    {
        private Mock<ISqlWrapper> _mockSqlWrapper;
        private TeacherDao _sut;
        private Guid _teacherGuid;
        private List<TeacherModel> _teachers;

        [TestInitialize]
        public void Initialize()
        {
            _mockSqlWrapper = new Mock<ISqlWrapper>();
            _sut = new TeacherDao(_mockSqlWrapper.Object);
            _teacherGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A6");
            _teachers = new List<TeacherModel>()
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
            _mockSqlWrapper = null;
            _sut = null;
            _teacherGuid = new Guid();
            _teachers = null;
        }

        [TestMethod]
        public void CreateTeacher_UsesProperSqlQuery_OneTime()
        {
            //Act
            _ = _sut.CreateTeacher(_teachers.First());

            //Assert
            _mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "INSERT Teacher(TeacherId, TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail,TeacherStatus)VALUES(@TeacherId, @TeacherFirstName, @TeacherLastName, @TeacherPhone, @TeacherEmail, @TeacherStatus)"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void GetTeacher_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = _sut.GetTeacher(Guid.Empty, null, null, "Active");
            // Assert
            _mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<TeacherModel>(It.Is<string>(sql => sql == $"SELECT * FROM Teacher WHERE 1=1 AND TeacherStatus = @TeacherStatus"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void PartiallyUpdateTeacherById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = _sut.PartiallyUpdateTeacherById(_teachers.First());

            // Assert
            _mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "UPDATE Teacher SET TeacherFirstName=@TeacherFirstName, TeacherLastName=@TeacherLastName, " +
              $"TeacherPhone=@TeacherPhone, TeacherEmail=@TeacherEmail, TeacherStatus=@TeacherStatus WHERE TeacherId=@TeacherId"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void DeleteTeacherById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = _sut.DeleteTeacherById(_teacherGuid);

            // Assert
            _mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "DELETE FROM Teacher WHERE TeacherId = @TeacherId"), It.IsAny<DynamicParameters>()), Times.Once);
        }
    }
}

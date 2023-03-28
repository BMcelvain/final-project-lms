﻿using Lms.Wrappers;
using Lms.Daos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dapper;
using Moq;
using Lms.Models;
using System;

namespace LMS.UnitTests
{
    [TestClass]
    public class TeacherDaoTests
    {
        [TestMethod]
        public void CreateTeacher_UsesProperSqlQuery_OneTime()
        {
            //Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            TeacherDao sut = new TeacherDao(mockSqlWrapper.Object);
            var mockTeacher = new TeacherModel();

            //Act
            _ = sut.CreateTeacher(mockTeacher);

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "INSERT Teacher(TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail,TeacherStatus)VALUES(@TeacherFirstName, @TeacherLastName, @TeacherPhone, @TeacherEmail, @TeacherStatus)"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void GetTeachersById_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            TeacherDao sut = new TeacherDao(mockSqlWrapper.Object);

            // Act
            _ = sut.GetTeacherById(new Guid());

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryFirstOrDefaultAsync<TeacherModel>(It.Is<string>(sql => sql == "SELECT * FROM Teacher WHERE TeacherId = 1")));
        }

        [TestMethod]
        public void PartiallyUpdateTeacherById_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            TeacherDao sut = new TeacherDao(mockSqlWrapper.Object);
            var mockTeacher = new TeacherModel();

            // Act
            _ = sut.PartiallyUpdateTeacherById(mockTeacher);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "UPDATE Teacher SET TeacherFirstName=@TeacherFirstName, TeacherLastName=@TeacherLastName, " +
              $"TeacherPhone=@TeacherPhone, TeacherEmail=@TeacherEmail, TeacherStatus=@TeacherStatus WHERE TeacherId=@TeacherId"), It.IsAny<DynamicParameters>()));
        }

        [TestMethod]
        public void DeleteTeacherById_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            TeacherDao sut = new TeacherDao(mockSqlWrapper.Object);

            // Act
            _ = sut.DeleteTeacherById(new Guid());

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "DELETE FROM Teacher WHERE TeacherId = 1")));
        }
    }
}

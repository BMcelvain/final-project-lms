﻿using Lms.Wrappers;
using Lms.Daos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Lms.Models;


namespace LMS.UnitTests
{
    [TestClass] // Every class must have this.
    public class StudentDaoTests
    {

        [TestMethod]
        public void CallSqlWithString()
        {
            //Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            StudentDao sut = new StudentDao(mockSqlWrapper.Object);

            //Act
            sut.GetStudents();

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.Query < StudentModel > (It.Is<string>(sql => sql == "SELECT * FROM [DBO.[LearningManagementSystem]")), Times.Once);
        }

        [TestMethod]
        public void DoNotCallSqlWithString()
        {
            //Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            StudentDao sut = new StudentDao(mockSqlWrapper.Object);

            //Act
            _ = sut.GetStudents(false);

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.Query<StudentModel>(It.Is<string>(sql => sql == "SELECT * FROM [DBO.[LearningManagementSystem]")), Times.Never);
        }
    }
}
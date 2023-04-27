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
    [TestClass]
    public class StudentDaoTests
    {
#nullable disable
        private Mock<ISqlWrapper> _mockSqlWrapper;
        private StudentDao _sut;
        private Guid _studentGuid;
        private List<StudentModel> _students;

        [TestInitialize]
        public void Initialize()
        {
            _mockSqlWrapper = new Mock<ISqlWrapper>();
            _sut = new StudentDao(_mockSqlWrapper.Object);
            _studentGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167B0");
            _students = new List<StudentModel>()
            {
                new StudentModel()
                {
                    StudentId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167B0"),
                    StudentFirstName = "Fred",
                    StudentLastName = "Testing",
                    StudentPhone = "999-999-9999",
                    StudentEmail = "Test@test.com",
                    StudentStatus = "Active"
                }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockSqlWrapper = null;
            _sut = null;
            _studentGuid = new Guid();
            _students = null;
        }

        [TestMethod]
        public void CreateStudentInSql_UsesProperSqlQuery_OneTime()
        {
            //Act
            _ = _sut.CreateStudent(_students.First());

            //Assert
            _mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "INSERT Student (StudentId, StudentFirstName, StudentLastName,StudentPhone, StudentEmail, StudentStatus)" +
                         $"VALUES(@StudentId, @StudentFirstName, @StudentLastName, @StudentPhone, @StudentEmail, @StudentStatus)"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void GetStudentById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = _sut.GetStudentById<StudentModel>(_studentGuid);

            // Assert
            _mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryFirstOrDefaultAsync<StudentModel>(It.Is<string>(sql => sql == $"SELECT * FROM Student WHERE StudentId = @StudentId"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void PartiallyUpdateStudentById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = _sut.PartiallyUpdateStudentById(_students.First());

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "UPDATE Student SET StudentFirstName=@StudentFirstName, StudentLastName=@StudentLastName, " +
                        $"StudentPhone=@StudentPhone, StudentEmail=@StudentEmail, StudentStatus=@StudentStatus " +
                        $"WHERE StudentId=@StudentId"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void DeleteStudentById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = _sut.DeleteStudentById(_studentGuid);

            // Assert
            _mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == $"DELETE FROM Student WHERE StudentId = @StudentId"), It.IsAny<DynamicParameters>()), Times.Once);
        }
    }
}

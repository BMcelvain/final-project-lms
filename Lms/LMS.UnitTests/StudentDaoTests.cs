using Dapper;
using Lms.Daos;
using Lms.Models;
using Lms.Wrappers;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System;


namespace LMS.UnitTests
{
    
    [TestClass]
    public class StudentDaoTests
    {
        #nullable disable
        Mock<ISqlWrapper> mockSqlWrapper;
        StudentDao sut;
        Guid studentGuid;
        List<StudentModel> students;

        [TestInitialize]
        public void Initialize()
        {
            mockSqlWrapper = new Mock<ISqlWrapper>();
            sut = new StudentDao(mockSqlWrapper.Object);
            studentGuid = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167B0");
            students = new List<StudentModel>()
            {
                new StudentModel()
                {
                    StudentId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167B0"),
                    StudentFirstName = "Fred",
                    StudentLastName = "Testing",
                    StudentPhone = "999-999-9999",
                    StudentEmail = "Test@test.com",
                    StudentStatus = "Active",
                    TotalPassCourses = 3
                }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            mockSqlWrapper = null;
            sut = null;
            studentGuid = new Guid();
            students = null;
        }

        [TestMethod]
        public void CreateStudentInSql_UsesProperSqlQuery_OneTime()
        {

            //Act
            _ = sut.CreateStudent(students.First());

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "INSERT Student (StudentId, StudentFirstName, StudentLastName,StudentPhone, StudentEmail, StudentStatus, TotalPassCourses)" +
              $"VALUES(@StudentId, @StudentFirstName, @StudentLastName, @StudentPhone, @StudentEmail, @StudentStatus, @TotalPassCourses)"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void GetStudentById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.GetStudentById(studentGuid);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryFirstOrDefaultAsync<StudentModel>(It.Is<string>(sql => sql == $"SELECT * FROM Student WHERE StudentId = @StudentId"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        //[TestMethod]
        //public async Task GetStudentById_ReturnStudentModel_NotNull_StudentId_Equals_MockStudent()
        //{
        //    //Arrange
        //    var studentId = Guid.NewGuid();
        //    var mockStudent = new StudentModel()
        //    {
        //        StudentId = studentId,
        //        StudentFirstName = "Fred",
        //        StudentLastName = "Testing",
        //        StudentPhone = "999-999-9999",
        //        StudentEmail = "Test@test.com",
        //        StudentStatus = "Test Status",
        //        TotalPassCourses = 3
        //    };
            
        //    var query = "SELECT * FROM Student WHERE StudentId = @StudentId";
        //    var parameters = new DynamicParameters();
        //    parameters.Add("StudentId", studentId, DbType.Guid);
            
        //    mockSqlWrapper.Setup(x => x.QueryFirstOrDefaultAsync<StudentModel>(query, parameters))
        //        .ReturnsAsync(mockStudent);

        //    //Act
        //    var result = await sut.GetStudentById(studentId);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(studentId, mockStudent.StudentId);  
        //}

        [TestMethod]
        public void PartiallyUpdateStudentById_UsesProperSqlQuery_OneTime()
        {
            // Act
            _ = sut.PartiallyUpdateStudentById(students.First());

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "UPDATE Student SET StudentFirstName=@StudentFirstName, StudentLastName=@StudentLastName, " + $"StudentPhone=@StudentPhone, StudentEmail=@StudentEmail, StudentStatus=@StudentStatus, TotalPassCourses=@TotalPassCourses" + $" WHERE StudentId=@StudentId"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void DeleteStudentById_UsesProperSqlQuery_OneTime()
        {

            // Act
            _ = sut.DeleteStudentById(studentGuid);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == $"DELETE FROM Student WHERE StudentId = @StudentId"), It.IsAny<DynamicParameters>()), Times.Once);
        }
    }
}

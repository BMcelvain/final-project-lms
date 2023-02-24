using Dapper;
using Lms.Daos;
using Lms.Models;
using Lms.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LMS.UnitTests
{
    [TestClass]
    public class SemesterDaoTests
    {
        [TestMethod]
        public void CreateSemester_UsesProperSqlQuery_OneTime()
        {
            //Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            SemesterDao sut = new SemesterDao(mockSqlWrapper.Object);
            var mockSemester = new SemesterModel();

            //Act
            _ = sut.CreateSemester(mockSemester);

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "INSERT Semester (Semester, Year) VALUES(@Semester, @Year)"), It.IsAny<DynamicParameters>()), Times.Once);
        }

        [TestMethod]
        public void GetAllSemesters_UsesProperSqlQuery_OneTime()
        {
            //Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            SemesterDao sut = new SemesterDao(mockSqlWrapper.Object);

            //Act
            _ = sut.GetSemesters();

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync<SemesterModel>(It.Is<string>(sql => sql == "SELECT * FROM Semester")), Times.Once);
        }

        [TestMethod]
        public void GetSemestersById_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            SemesterDao sut = new SemesterDao(mockSqlWrapper.Object);

            // Act
            _ = sut.GetSemesterById(1);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryFirstOrDefaultAsync<SemesterModel>(It.Is<string>(sql => sql == "SELECT * FROM Semester WHERE SemesterId = 1")));
        }

        [TestMethod]
        public void DeleteSemesterById_UsesProperSqlQuery_OneTime()
        {
            // Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            SemesterDao sut = new SemesterDao(mockSqlWrapper.Object);

            // Act
            _ = sut.DeleteSemesterById(1);

            // Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.ExecuteAsync(It.Is<string>(sql => sql == "DELETE FROM Semester WHERE SemesterId = 1")));
        }
    }
}

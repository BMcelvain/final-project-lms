using Lms.Wrappers;
using Lms.Daos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Lms.Models;


namespace LMS.UnitTests
{
    [TestClass] 
    public class StudentDaoTests
    {
        [TestMethod]
        public void GetAllStudentsInSql()
        {
            //Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            StudentDao sut = new StudentDao(mockSqlWrapper.Object);

            //Act
            _ = sut.GetStudents();

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.QueryAsync< StudentModel > (It.Is<string>(sql => sql == "SELECT * FROM Student")), Times.Once);
        }
    }
}

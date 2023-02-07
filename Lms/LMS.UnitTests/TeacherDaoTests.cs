using Lms.Wrappers;
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
    public class TeacherDaoTests
    {
        [TestMethod]
        public void CallSqlWithString()
        {
            //Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            TeacherDao sut = new TeacherDao(mockSqlWrapper.Object);

            //Act
            Task<IEnumerable<TeacherModel>> task = sut.GetTeachers();

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.Query<TeacherModel>(It.Is<string>(sql => sql == "SELECT * FROM Teacher")), Times.Once);
        }
    }
}

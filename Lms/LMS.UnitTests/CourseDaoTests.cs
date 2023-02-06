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
    public class CourseDaoTests
    {

        [TestMethod]
        public void GetAllCoursesInSql()
        {
            //Arrange
            Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper>();
            CourseDao sut = new CourseDao(mockSqlWrapper.Object);

            //Act
            sut.GetCourses();

            //Assert
            mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.Query < CourseModel > (It.Is<string>(sql => sql == "SELECT * FROM Course")), Times.Once);
        }

    }
}

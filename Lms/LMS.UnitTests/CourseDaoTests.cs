using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.UnitTests
{
    [TestClass] // Every class must have this.
    public class CourseDaoTests
    {

        //[TestMethod]
        //public void CallSqlWithString()
        //{
        //    Mock<ISqlWrapper> mockSqlWrapper = new Mock<ISqlWrapper> ();
        //    CourseDao sut = new CourseDao(mockSqlWrapper.Object);

        //    sut.GetCourse();

        //    mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.Query<CourseModel>>(It.Is<string>(sql => sql == "SELECT * FROM [DBO.[LMS]")),Times.Once);  
        //}

        //[TestMethod]
        //public void DoNotCallSqlWithString()
        //{
        //    Mock<ISqlWrapper> mockSqlWrapper = new Mock<IServiceProvider> ();
        //    CourseDao sut = new CourseDao(mockSqlWrapper.Object);

        //    sut.GetCourse(false);

        //    mockSqlWrapper.Verify(sqlWrapper => sqlWrapper.Query<CourseModel>(It.Is<string>(sql => sql == "SELECT * FROM [DBO.[LMS]")), Times.Never);
        //}
    }
}

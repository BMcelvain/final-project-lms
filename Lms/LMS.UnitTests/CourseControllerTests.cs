using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lms.Controllers;
using Moq;
using Lms.Daos;

namespace LMS.UnitTests
{
    [TestClass] // Every class must have this.
    public class CourseControllerTests
    {
        [TestMethod] // Every method must have this. 
        public void CallDao()
        {
            Mock<ICourseDao> mockCourseDao = new Mock<ICourseDao>(); //will work when you're actually in code

            CourseController sut = new CourseController(mockCourseDao.Object);

            sut.CallDao();  //in CallDao throw exception 

            mockCourseDao.Verify(courseDao => courseDao.GetCourse(), Times.Once()); //this is used as a temp object
        }
    }
}
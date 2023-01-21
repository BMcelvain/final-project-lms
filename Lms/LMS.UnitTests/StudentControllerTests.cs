using Lms.Controllers;
using Lms.Daos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LMS.UnitTests
{
    [TestClass] // Every class must have this.
    public class StudentControllerTests
    {
        [TestMethod] // Every method must have this. 
        public void CallDao()
        {
            Mock<IStudentDao> mockStudentDao = new Mock<IStudentDao>(); //will work when you're actually in code

            StudentController sut = new StudentController(mockStudentDao.Object);

            sut.CallDao();  //in CallDao throw exception 

            mockStudentDao.Verify(studentDao => studentDao.GetStudent(), Times.Once()); //this is used as a temp object
        }
    }
}


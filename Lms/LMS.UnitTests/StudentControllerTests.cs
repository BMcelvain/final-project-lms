using Lms.Controllers;
using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.Mvc;
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
        public void CallStudentDao()
        {
            Mock<IStudentDao> mockStudentDao = new Mock<IStudentDao>(); //will work when you're actually in code

            StudentController sut = new StudentController(mockStudentDao.Object);

            sut.GetStudents();  //in CallDao throw exception 

            mockStudentDao.Verify(studentDao => studentDao.GetStudents(), Times.Once()); //this is used as a temp object
        }

        [TestMethod]
        public async Task CreateClass_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<IStudentDao> mockStudentDao = new Mock<IStudentDao>();
            StudentController sut = new StudentController(mockStudentDao.Object);
            var course = new StudentModel();

            // Act
            var response = await sut.CreateStudent(course);

            // Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }
    }
}


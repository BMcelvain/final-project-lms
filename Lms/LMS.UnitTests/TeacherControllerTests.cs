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
    public class TeacherControllerTests
    {
        [TestMethod] // Every method must have this. 
        public void CallDao()
        {
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>(); //will work when you're actually in code

            TeacherController sut = new TeacherController(mockTeacherDao.Object);

            sut.GetTeachers();  //in CallDao throw exception 

            mockTeacherDao.Verify(teacherDao => teacherDao.GetTeachers(), Times.Once()); //this is used as a temp object
        }

        [TestMethod]
        public async Task CreateClass_ReturnsOkStatusCode()
        {
            // Arrange
            Mock<ITeacherDao> mockTeacherDao = new Mock<ITeacherDao>();
            TeacherController sut = new TeacherController(mockTeacherDao.Object);
            var course = new TeacherModel();

            // Act
            var response = await sut.CreateTeacher(course);

            // Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }
    }
}

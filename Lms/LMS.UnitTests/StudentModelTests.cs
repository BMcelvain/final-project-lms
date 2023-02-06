using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lms.Models;


namespace LMS.UnitTests
{
    [TestClass] // Every class must have this.
    public class StudentModelTests
    {
        [TestMethod] // Every method must have this. 
        public void AddStudent()
        {
            StudentModel sut = new StudentModel();
            StudentModel expectedStudent = new StudentModel();

            sut.AddStudent(expectedStudent);

            Assert.IsInstanceOfType(expectedStudent, typeof(StudentModel));
        }
    }
}

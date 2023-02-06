using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lms.Models;


namespace LMS.UnitTests
{
    [TestClass] // Every class must have this.
    public class TeacherModelTests
    {
        //[TestMethod] // Every method must have this. 
        public void AddTeacher()
        {
            TeacherModel sut = new TeacherModel();
            TeacherModel expectedTeacher = new TeacherModel();

            sut.AddTeacher(expectedTeacher);

            Assert.IsInstanceOfType(expectedTeacher, typeof(TeacherModel));
        }
    }
}

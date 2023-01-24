using Lms.Daos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {

        private IStudentDao studentDao;

        public StudentController(IStudentDao studentDao)
        {
            this.studentDao = studentDao;
        }

        [NonAction]
        public void CallDao()
        {
            studentDao.GetStudent();
        }


        //[HttpPost]
        //[Route("Student")]
        //public async Task<IActionResult> AddCourse(int StudentId, string StudentFirstName, string StudentLastName, string StudentPhone, string StudentEmail, string StudentStatus, int TotalPassCourses)
        //{
        //    try
        //    {
        //        await courseDao.AddCourse(StudentId,StudentFirstName,StudentLastName,StudentPhone,StudentEmail,StudentStatus,TotalPassCourses);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}
    }
}

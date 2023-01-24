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
    public class TeacherController : ControllerBase
    {

        private ITeacherDao teacherDao;

        public TeacherController(ITeacherDao teacherDao)
        {
            this.teacherDao = teacherDao;
        }

        [NonAction]
        public void CallDao()
        {
            teacherDao.GetTeacher();
        }

        //[HttpPost]
        //[Route("Teacher")]
        //public async Task<IActionResult> AddTeacher(int TeacherId, string TeacherFirstName, string TeacherLastName, string TeacherPhone, string TeacherEmail, string TeacherStatus)
        //{
        //    try
        //    {
        //        await courseDao.AddCourse(TeacherId, TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail, TeacherStatus);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

    }
}

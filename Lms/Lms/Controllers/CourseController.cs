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
    public class CourseController : ControllerBase
    {
        private ICourseDao courseDao;

        public CourseController(ICourseDao courseDao)
        {
            this.courseDao = courseDao;
        }

        public void CallDao()
        {
            courseDao.GetCourse();
        }

        //[HttpPost]
        //[Route("Course")]
        //public async Task<IActionResult> AddCourse(int CourseId, int TeacherId, string CourseName, int SemesterId, string StartDate, string EndDate, string CourseStatus)
        //{
        //    try
        //    {
        //        await courseDao.AddCourse(CourseId,TeacherId,CourseName,SemesterId,StartDate,EndDate,CourseStatus);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

    }
}

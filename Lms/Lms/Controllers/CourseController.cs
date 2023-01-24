using Lms.Daos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lms.Models;
using System.Drawing.Printing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.JsonPatch;

namespace Lms.Controllers
{
    [ApiController]
    public class CourseController : ControllerBase
    {
        private ICourseDao courseDao;
        private readonly CourseDao _courseDao;

        public CourseController(ICourseDao courseDao)
        {
            this.courseDao = courseDao;
        }

        // [ActivatorUtilitiesConstructor] Needed to distinguish between contructors
        // (ie: our testing constructor - CourseController(ICourseDao courseDao) &
        // CourseController(CourseDao courseDao)
        // https://stackoverflow.com/questions/32931716/asp-net-core-dependency-injection-with-multiple-constructors
        [ActivatorUtilitiesConstructor]
        public CourseController(CourseDao courseDao)
        {
            _courseDao = courseDao;
        }

        // [NonAction] Needed to show the program that we don't invoke this method. 
        // https://www.tutorialspoint.com/what-is-the-significance-of-nonactionattribute-in-asp-net-mvc-chash
        [NonAction]
        public void CallDao()
        {
            courseDao.GetCourses();
        }

        [HttpPost]
        [Route("course")]
        public async Task<IActionResult> CreateCourse(CourseModel newCourse)
        {
            try
            {
                await _courseDao.CreateCourse(newCourse);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("course")]
        public async Task<IActionResult> GetCourses()
        {
            try
            {
                var courses = await _courseDao.GetCourses();
                return Ok(courses);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("course/{id}")]
        public async Task<IActionResult> GetCourseById([FromRoute] int id)
        {
            try
            {
                var course = await _courseDao.GetCourseById(id);
                if (course == null)
                {
                    return StatusCode(404);
                }

                return Ok(course);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("course/{id}")]
        public async Task<IActionResult> PartiallyUpdateCourseById([FromRoute]int id, JsonPatchDocument<CourseModel> courseUpdates)
        {
            try
            {
                var course = await _courseDao.GetCourseById(id);

                if (course == null)
                {
                    return NotFound();
                }

                courseUpdates.ApplyTo(course);
                await _courseDao.PartiallyUpdateCourseById(course);

                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("course/{id}")]
        public async Task<IActionResult> DeleteCourseById([FromRoute] int id)
        {
            try
            {
                var course = await _courseDao.GetCourseById(id);
                if (course == null)
                {
                    return StatusCode(404);
                }

                await _courseDao.DeleteCourseById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
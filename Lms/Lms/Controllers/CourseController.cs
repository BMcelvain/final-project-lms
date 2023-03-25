using Lms.Daos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;
using Lms.APIErrorHandling;

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

        [HttpPost]
        [Route("courses")]
        public async Task<IActionResult> CreateCourse(CourseModel newCourse)
        {
            try
            {
                await courseDao.CreateCourse(newCourse);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("courses/byId/{id}")]
        public async Task<IActionResult> GetCourseById([FromRoute] int id)
        {
            try
            {
                var course = await courseDao.GetCourseById<CourseModel>(id);

                if (course == null)
                {
                    return NotFound(new ApiResponse(404, $"Course with id {id} not found."));
                }

                return Ok(new ApiOkResponse(course));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("courses/byStatus/{status}")]
        public async Task<IActionResult> GetCourseByStatus([FromRoute] string status)
        {
            try
            {
                if(status != "Inactive" && status != "Active")
                {
                    return BadRequest(new ApiResponse(400,"Please enter Active or Inactive status."));
                }

                var courses = await courseDao.GetCourseByStatus(status);

                if (courses == null)
                {
                    return NotFound(new ApiResponse(404, $"Course with status {status} not found."));
                }

                return Ok(new ApiOkResponse(courses));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("courses/{id}")]
        public async Task<IActionResult> PartiallyUpdateCourseById([FromRoute]int id, JsonPatchDocument<CourseModel> courseUpdates)
        {
            try
            {
                var course = await courseDao.GetCourseById<CourseModel>(id);

                if (course == null)
                {
                    return NotFound(new ApiResponse(404, $"Course with id {id} not valid."));
                }

                courseUpdates.ApplyTo(course);
                await courseDao.PartiallyUpdateCourseById(course);

                return Ok(new ApiOkResponse(course));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("courses/{id}")]
        public async Task<IActionResult> DeleteCourseById([FromRoute] int id)
        {
            try
            {
                var course = await courseDao.GetCourseById<CourseModel>(id);

                if (course == null)
                {
                    return NotFound(new ApiResponse(404, $"Student not found with id {id}"));
                }

                await courseDao.DeleteCourseById(id);
                return Ok(new ApiOkResponse(course));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("StudentInCourse")]
        public async Task<IActionResult> StudentInCourse(StudentInCourseModel addStudentInCourse)
        {
            try
            {
                await courseDao.StudentInCourse(addStudentInCourse);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("StudentInCourse/byStudentCourseId/{studentId},{courseId}")]
        public async Task<IActionResult> PartiallyUpdateStudentInCourseByCourseStudentId([FromRoute] int studentId, int courseId, JsonPatchDocument<StudentInCourseModel> addStudentCourseUpdates)
        {
            try
            {
                var addStudentInCourse = await courseDao.GetCourseById<StudentInCourseModel>(courseId);

                if (addStudentInCourse == null)
                {
                    return NotFound(new ApiResponse(404, $"Course with id {courseId} not found."));
                }

                addStudentCourseUpdates.ApplyTo(addStudentInCourse);
                await courseDao.PartiallyUpdateStudentInCourseByCourseStudentId(addStudentInCourse);

                return Ok(new ApiOkResponse(addStudentInCourse));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("StudentInCourse/byStudentCourseId/{studentId},{courseId}")]
        public async Task<IActionResult> DeleteStudentInCourseByStudentCourseId([FromRoute] int studentId, int courseId)
        {
            try
            {
                var addStudentInCourse = await courseDao.GetCourseById<StudentInCourseModel>(courseId);

                if (addStudentInCourse == null)
                {
                    return NotFound(new ApiResponse(404, $"Course with id {courseId} not found."));
                }

                await courseDao.DeleteStudentInCourseByStudentCourseId(studentId, courseId);
                return Ok(new ApiOkResponse(addStudentInCourse));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
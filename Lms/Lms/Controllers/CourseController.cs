using FluentAssertions.Equivalency.Tracing;
using Lms.APIErrorHandling;
using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;


namespace Lms.Controllers
{
    [ApiController]
    public class CourseController : ControllerBase
    {

        //private ICourseDao courseDao;
        private IMemoryCache cache;
        private readonly ICourseDao courseDao;


        public CourseController(ICourseDao courseDao, IMemoryCache cache)
        {
            this.courseDao = courseDao;
            this.cache = cache;
        }

        /// <summary>
        /// Create Course
        /// </summary>
        /// <param name="newCourse"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("courses")]
        public async Task<IActionResult> CreateCourse(CourseModel newCourse)
        {
            try
            {
                await courseDao.CreateCourse(newCourse);
                
                cache.Remove($"coursesKey{newCourse.CourseStatus}");

                return Ok(new ApiOkResponse(newCourse));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get Course by Guid Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("courses/{id}")]
        public async Task<IActionResult> GetCourseById([FromRoute] Guid id)
        {
            try
            {
                if (cache.TryGetValue($"courseKey{id}", out CourseModel course))
                {
                    Log.Information($"Course with that id found in cache");
                } 
                else
                {
                    Log.Information($"Course with that id not found in cache. Checking database.");

                    course = await courseDao.GetCourseById<CourseModel>(id);
                    if (course == null)
                    {
                        return NotFound(new ApiResponse(404, $"Course with that id not found."));
                    }

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(180))
                        .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                        .SetSize(1024);

                    cache.Set($"courseKey{id}", course, cacheEntryOptions);
                    return NotFound(new ApiResponse(404, $"Course with that id not found."));

                }

               return Ok(new ApiOkResponse(course));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get Course by Active or Inactive Status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("courses/{status}")]
        public async Task<IActionResult> GetCourseByStatus([FromRoute] string status)
        {
            try
            {
                if(status.ToLower() != "inactive" && status.ToLower() != "active")
                {
                    return BadRequest(new ApiResponse(400,"Please enter Active or Inactive status."));
                }

                if (cache.TryGetValue($"coursesKey{status}", out IEnumerable<CourseModel> courses))
                {
                    Log.Information($"Courses with status '{status}' fournd in cache");
                } 
                else
                {
                    Log.Information($"Courses with status '{status}' not found in cache. Checking database.");

                    courses = await courseDao.GetCourseByStatus(status);
                    if(courses == null)
                    {
                        return NotFound(new ApiResponse(404, $"Course with status {status} not found."));
                    }

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(180))
                        .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                        .SetSize(1024);

                    cache.Set($"coursesKey{status}", courses, cacheEntryOptions);
                }

                return Ok(new ApiOkResponse(courses));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Update CourseName,CourseStatus, TeacherId, StartDate, or EndDate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseUpdates"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("course/{id}")]
        public async Task<IActionResult> PartiallyUpdateCourseById(Guid id, [FromBody] JsonPatchDocument<CourseModel> courseUpdates)
        {
            if (courseUpdates == null)
            {
                return NotFound(new ApiResponse(404, $"Course with that id not found."));
            }

            var allowedOperations = new[] { "replace" };

            foreach (Operation<CourseModel> operation in courseUpdates.Operations)
            {
                if (!allowedOperations.Contains(operation.op.ToLower()))
                {
                    return BadRequest(new ApiResponse(400, "Only 'replace' operation is allowed."));
                }

                switch (operation.path.ToLower())
                {
                    case "/coursename":
                        string CourseName = operation.value?.ToString();
                        if (!Regex.IsMatch(CourseName, @"^[A-Z][A-Za-z]+}$"))
                        {
                            return BadRequest(new ApiResponse(400, "Please enter Course name starting with capital letter, lowercase for the remaining letters."));
                        }
                        break;
                    case "/startdate":
                        string StartDate = operation.value?.ToString();
                        if (!Regex.IsMatch(StartDate, @"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$"))
                        {
                            return BadRequest(new ApiResponse(400, "Please enter a date in a valid format: yyyy-mm-dd."));
                        }
                        break;
                    case "/enddate":
                        string EndDate = operation.value?.ToString();
                        if (!Regex.IsMatch(EndDate, @"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$"))
                        {
                            return BadRequest(new ApiResponse(400, "Please enter a date in a valid format: yyyy-mm-dd."));
                        }
                        break;
                    case "/coursestatus":
                        string StudentStatus = operation.value?.ToString();
                        if (StudentStatus != "Inactive" && StudentStatus != "Active")
                        {
                            return BadRequest(new ApiResponse(400, "Please enter Active or Inactive status."));
                        }
                        break;
                    default:
                        return BadRequest(new ApiResponse(500, "The JSON patch document is missing."));
                }

            }
            var course = await courseDao.GetCourseById<CourseModel>(id);
            if (course == null)
            {
                return NotFound(new ApiResponse(404, $"Course with that id not found."));
            }

            courseUpdates.ApplyTo(course);
            await courseDao.PartiallyUpdateCourseById(course);
            cache.Remove($"courseKey{course.CourseId}");
            cache.Remove($"coursesKey{course.CourseStatus}");
            return Ok(new ApiOkResponse(course));
        }

        /// <summary>
        /// Delete Course by Guid Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("courses/{id}")]
        public async Task<IActionResult> DeleteCourseById([FromRoute] Guid id)
        {
            try
            {
                var course = await courseDao.GetCourseById<CourseModel>(id);

                if (course == null)
                {
                    return NotFound(new ApiResponse(404, $"Course with that id not found."));
                }

                await courseDao.DeleteCourseById(id);

                cache.Remove($"courseKey{course.CourseId}");
                cache.Remove($"coursesKey{course.CourseStatus}");

                return Ok(new ApiOkResponse(course));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Add a Student to a Course
        /// </summary>
        /// <param name="addStudentInCourse"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("StudentInCourse")]
        public async Task<IActionResult> StudentInCourse(StudentInCourseModel addStudentInCourse)
        {
            try
            {
                await courseDao.StudentInCourse(addStudentInCourse);
                return Ok(new ApiOkResponse(addStudentInCourse));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Update a Student in a Course - e.g. passed the course
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="addStudentCourseUpdates"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("StudentInCourse")]
        public async Task<IActionResult> PartiallyUpdateStudentInCourseByCourseStudentId([Required][FromQuery] Guid studentId, [Required][FromQuery] Guid courseId, JsonPatchDocument<StudentInCourseModel> addStudentCourseUpdates)
        {
            try
            {
                var addStudentInCourse = await courseDao.GetCourseById<StudentInCourseModel>(courseId);

                if (addStudentInCourse == null)
                {
                    return NotFound(new ApiResponse(404, $"Course with that id not found."));
                }

                addStudentCourseUpdates.ApplyTo(addStudentInCourse);
                addStudentInCourse.StudentId = studentId;
                await courseDao.PartiallyUpdateStudentInCourseByCourseStudentId(addStudentInCourse);

                return Ok(new ApiOkResponse(addStudentInCourse));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Delete Student in a Course
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("StudentInCourse")]
        public async Task<IActionResult> DeleteStudentInCourseByStudentCourseId([Required][FromQuery] Guid studentId, [Required][FromQuery] Guid courseId)
        {
            try
            {
                var deleteStudentInCourse = await courseDao.GetCourseById<StudentInCourseModel>(courseId);

                if (deleteStudentInCourse == null)
                {
                    return NotFound(new ApiResponse(404, $"Course with that id not found."));
                }

                deleteStudentInCourse.StudentId = studentId;
                await courseDao.DeleteStudentInCourseByStudentCourseId(deleteStudentInCourse);
                return Ok(new ApiOkResponse(deleteStudentInCourse));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
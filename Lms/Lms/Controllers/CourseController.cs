using Lms.Daos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;
using Lms.APIErrorHandling;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using System.Collections.Generic;

namespace Lms.Controllers
{
    [ApiController]
    public class CourseController : ControllerBase
    {
        private ICourseDao courseDao;
        private IMemoryCache cache;

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
        [Route("courses/byId/{id}")]
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
                        return NotFound(new ApiResponse(404, $"Course with id {id} not found."));
                    }

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(180))
                        .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                        .SetSize(1024);

                    cache.Set($"courseKey{id}", course, cacheEntryOptions);
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
        [Route("courses/byStatus/{status}")]
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
        /// Update CourseName, CourseStatus, TeacherId, StartDate, or EndDate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseUpdates"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("courses/{id}")]
        public async Task<IActionResult> PartiallyUpdateCourseById([FromRoute] Guid id, JsonPatchDocument<CourseModel> courseUpdates)
        {
            try
            {
                var course = await courseDao.GetCourseById<CourseModel>(id);

                if (course == null)
                {
                    return NotFound(new ApiResponse(404, $"Course with id {id} not found."));
                }

                courseUpdates.ApplyTo(course);
                await courseDao.PartiallyUpdateCourseById(course);

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
                    return NotFound(new ApiResponse(404, $"Course with id {id} not found."));
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
        [Route("StudentInCourse/byStudentCourseId/{studentId},{courseId}")]
        public async Task<IActionResult> PartiallyUpdateStudentInCourseByCourseStudentId([FromRoute] Guid studentId, Guid courseId, JsonPatchDocument<StudentInCourseModel> addStudentCourseUpdates)
        {
            try
            {
                var addStudentInCourse = await courseDao.GetCourseById<StudentInCourseModel>(courseId);

                if (addStudentInCourse == null)
                {
                    return NotFound(new ApiResponse(404, $"Course with id {courseId} not found."));
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
        [Route("StudentInCourse/byStudentCourseId/{studentId},{courseId}")]
        public async Task<IActionResult> DeleteStudentInCourseByStudentCourseId([FromRoute] Guid studentId, Guid courseId)
        {
            try
            {
                var deleteStudentInCourse = await courseDao.GetCourseById<StudentInCourseModel>(courseId);

                if (deleteStudentInCourse == null)
                {
                    return NotFound(new ApiResponse(404, $"Course with id {courseId} not found."));
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
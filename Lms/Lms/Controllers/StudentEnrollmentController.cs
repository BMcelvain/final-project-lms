using Lms.APIErrorHandling;
using Lms.Daos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Caching.Memory;
using Lms.Models;
using Serilog;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Lms.Controllers
{
    [ApiController]
    public class StudentEnrollmentController : ControllerBase
    {
        private IStudentEnrollmentDao studentEnrollmentDao;
        private IMemoryCache cache;

        public StudentEnrollmentController(IStudentEnrollmentDao studentEnrollmentDao, IMemoryCache cache)
        {
            this.studentEnrollmentDao = studentEnrollmentDao;
            this.cache = cache;
        }

        /// <summary>
        /// Get Student Enrollment History By Filters
        /// </summary>
        /// <param name="StudentId"></param>
        /// <param name="StudentLastName"></param>
        /// <param name="StudentPhone"></param>
        /// <param name="StudentStatus"></param>
        /// /// <param name="CourseStatus"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("studentEnrollment")]
        public async Task<IActionResult> GetStudentEnrollmentHistory([FromQuery] Guid StudentId, string StudentLastName, string StudentPhone, string StudentStatus, string CourseStatus)
        {
            try
            {
                if (cache.TryGetValue($"enrollmentKey{StudentId}", out IEnumerable<StudentEnrollmentModel> studentEnrollments))
                {
                    Log.Information($"Student enrollment for student with that id found in cache");
                }
                else
                {
                    Log.Information($"Student enrollment for student with that id not found in cache. Checking database.");

                    studentEnrollments = await studentEnrollmentDao.GetStudentEnrollmentHistory(StudentId, StudentLastName, StudentPhone, StudentStatus, CourseStatus);

                    if (studentEnrollments.IsNullOrEmpty())
                    {
                        return NotFound(new ApiResponse(404, $"Student enrollment not found. Please check your entries and try again."));
                    }

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(180))
                        .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                        .SetSize(1024);

                    cache.Set($"courseKey{StudentId}", studentEnrollments, cacheEntryOptions);
                }

                return Ok(new ApiOkResponse(studentEnrollments));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        /// <summary>
        /// Get All Student Enrollment by CourseId
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("studentsInCourse")]
        public async Task<IActionResult> GetStudentsInCourseByCourseId([Required][FromQuery] Guid courseId)
        {
            try
            {
                if (cache.TryGetValue($"enrollmentKey{courseId}", out IEnumerable<StudentModel> studentsInCourse))
                {
                    Log.Information($"Student Enrollment for student with that id found in cache");
                }
                else
                {
                    Log.Information($"Student Enrollment for student with that id not found in cache. Checking database.");

                    studentsInCourse = await studentEnrollmentDao.GetStudentsInCourseByCourseId(courseId);
                    if (studentsInCourse.IsNullOrEmpty())
                    {
                        return NotFound(new ApiResponse(404, "No students found in a course with that id. The course may have not students."));
                    }

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(180))
                        .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                        .SetSize(1024);

                    cache.Set($"courseKey{courseId}", studentsInCourse, cacheEntryOptions);
                }
                
                return Ok(new ApiOkResponse(studentsInCourse));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
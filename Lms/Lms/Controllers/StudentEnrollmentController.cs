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
        /// Get Student Enrollment History by Student Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("studentEnrollment/byStudentId")]
        public async Task<IActionResult> GetStudentEnrollmentHistoryByStudentId([Required][FromQuery] Guid id)
        {
            try
            {
                if (cache.TryGetValue($"enrollmentKey{id}", out IEnumerable<StudentEnrollmentModel> studentEnrollments))
                {
                    Log.Information($"Student enrollment for student with that id found in cache");
                }
                else
                {
                    Log.Information($"Student enrollment for student with that id not found in cache. Checking database.");

                    studentEnrollments = await studentEnrollmentDao.GetStudentEnrollmentHistoryByStudentId(id);
                    if (studentEnrollments.IsNullOrEmpty())
                    {
                        return NotFound(new ApiResponse(404, $"Student enrollment with that Student Id not found."));
                    }

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(180))
                        .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                        .SetSize(1024);

                    cache.Set($"courseKey{id}", studentEnrollments, cacheEntryOptions);
                }

                return Ok(new ApiOkResponse(studentEnrollments));       
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get Student Enrollment History by Last Name
        /// </summary>
        /// <param name="studentLastName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("studentEnrollment/byStudentLastName")]
        public async Task<IActionResult> GetStudentEnrollmentHistoryByStudentLastName([Required][FromQuery] string studentLastName)
        {
            try
            {
                if (cache.TryGetValue($"enrollmentKey{studentLastName}", out IEnumerable<StudentEnrollmentModel> studentEnrollments))
                {
                    Log.Information($"Student enrollment for student with that last name found in cache");
                }
                else
                {
                    Log.Information($"Student enrollment for student with that last name not found in cache. Checking database.");

                    studentEnrollments = await studentEnrollmentDao.GetStudentEnrollmentHistoryByStudentLastName(studentLastName);
                    if (studentEnrollments.IsNullOrEmpty())
                    {
                        return NotFound(new ApiResponse(404, $"Student enrollment for student with that last name not found."));
                    }

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(180))
                        .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                        .SetSize(1024);

                    cache.Set($"courseKey{studentLastName}", studentEnrollments, cacheEntryOptions);
                }

                return Ok(new ApiOkResponse(studentEnrollments));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get Current Active Student Enrollment
        /// </summary>
        /// <param name="studentPhone"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("studentActiveEnrollment")]
        public async Task<IActionResult> GetActiveStudentEnrollmentByStudentPhone([Required][FromQuery] string studentPhone)
        {
            try
            {
                if (cache.TryGetValue($"enrollmentKey{studentPhone}", out IEnumerable<StudentEnrollmentModel> studentEnrollments))
                {
                    Log.Information($"Student enrollment for student with that phone number found in cache");
                }
                else
                {
                    Log.Information($"Student enrollment for student with that phone number not found in cache. Checking database.");

                    studentEnrollments = await studentEnrollmentDao.GetActiveStudentEnrollmentByStudentPhone(studentPhone);
                    if (studentEnrollments.IsNullOrEmpty())
                    {
                        return NotFound(new ApiResponse(404, "Student enrollment for student with that phone number not found."));
                    }

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(180))
                        .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                        .SetSize(1024);

                    cache.Set($"courseKey{studentPhone}", studentEnrollments, cacheEntryOptions);
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
﻿using Lms.APIErrorHandling;
using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using Lms.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;


namespace Lms.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IMemoryCache cache;
        private IStudentDao studentDao;

        public StudentController(IStudentDao studentDao, IMemoryCache cache)
        {
            this.studentDao = studentDao;
            this.cache = cache;
        }

        /// <summary>
        /// Create New Student
        /// </summary>
        /// <param name="newStudent"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("student")]
        public async Task<IActionResult> CreateStudent(StudentModel newStudent)
        {
            try
            {
                await studentDao.CreateStudent(newStudent);

                cache.Remove($"studentKey{newStudent.StudentStatus}");

                return Ok(new ApiOkResponse(newStudent));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get Student by Using Guid Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("student/bystudentid")]
        public async Task<IActionResult> GetStudentById([Required][FromQuery] Guid id)
        {

            try
            {
                if (cache.TryGetValue($"studentKey{id}", out StudentModel student))
                {
                    Log.Information($"Student with that id found in cache");
                }
                else
                {
                    Log.Information($"Student with that id not found in cache. Checking database.");

                    student = await studentDao.GetStudentById<StudentModel>(id);
                    if (student == null)
                    {
                        return NotFound(new ApiResponse(404, $"Student with that id not found."));
                    }

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(180))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                    .SetSize(1024);

                    cache.Set($"studentKey{id}", student, cacheEntryOptions);
                }

                return Ok(new ApiOkResponse(student));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get Student Enrollment Audit History By Parameters
        /// </summary>
        /// <param name="StudentId"></param>
        /// <param name="StudentPhone"></param>
        /// <param name="Cancelled"></param>
        /// <param name="HasPassed"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("student/studentenrollment/byparameters")]
        public async Task<IActionResult> GetStudentEnrollmentHistory([FromQuery] Guid StudentId, string StudentPhone, string Cancelled, string HasPassed)
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

                    studentEnrollments = await studentDao.GetStudentEnrollmentHistory(StudentId, StudentPhone, Cancelled, HasPassed);

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
        /// Get Student Enrollment Audit History by CourseId
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("students/bycourseid")]
        public async Task<IActionResult> GetStudentsInCourseByCourseId([Required][FromQuery] Guid courseId)
        {
            try
            {
                if (cache.TryGetValue($"enrollmentKey{courseId}", out IEnumerable<StudentEnrollmentModel> studentsInCourse))
                {
                    Log.Information($"Student Enrollment for student with that id found in cache");
                }
                else
                {
                    Log.Information($"Student Enrollment for student with that id not found in cache. Checking database.");

                    studentsInCourse = (IEnumerable<StudentEnrollmentModel>)await studentDao.GetStudentsInCourseByCourseId(courseId);
                    //await studentDao.GetStudentsInCourseByCourseId(courseId);
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

        /// <summary>
        /// Update StudentFirstName, StudentLastName, StudentPhone, StudentEmail,or Student Status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateRequest"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("student/{id}")]
        public async Task<IActionResult> PartiallyUpdateStudentById(Guid id, [FromBody] JsonPatchDocument<StudentModel> updateRequest)
        {
            try
            {
                if (updateRequest == null)
                {
                    return NotFound(new ApiResponse(404, $"Student with that id not found."));
                }

                var allowedOperations = new[] { "replace" };

                foreach (Operation<StudentModel> operation in updateRequest.Operations)
                {
                    if (!allowedOperations.Contains(operation.op.ToLower()))
                    {
                        return BadRequest(new ApiResponse(400, "Only 'replace' operation is allowed."));
                    }

                    switch (operation.path.ToLower())
                    {
                        case "/studentfirstname":
                            string StudentFirstName = operation.value?.ToString();
                            if (!Regex.IsMatch(StudentFirstName, @"^[A-Z][A-Za-z]+$"))
                            {
                                return BadRequest(new ApiResponse(400, "Please enter first name starting with capital letter, lowercase for the remaining letters."));
                            }
                            break;
                        case "/studentlastname":
                            string StudentLastName = operation.value?.ToString();
                            if (!Regex.IsMatch(StudentLastName, @"^[A-Z][A-Za-z-]+$"))
                            {
                                return BadRequest(new ApiResponse(400, "Please enter last name starting with capital letter, lowercase for the remaining letters. Hyphenated last names are acceptable."));
                            }
                            break;
                        case "/studentphone":
                            string StudentPhone = operation.value?.ToString();
                            if (!Regex.IsMatch(StudentPhone, @"^\d{3}-\d{3}-\d{4}$"))
                            {
                                return BadRequest(new ApiResponse(400, "Please enter phone number in a valid format: XXX-XXX-XXXX."));
                            }
                            break;
                        case "/studentemail":
                            string StudentEmail = operation.value?.ToString();
                            if (!Regex.IsMatch(StudentEmail, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
                            {
                                return BadRequest(new ApiResponse(400, "Please enter an E-Mail in a valid format: example@vu.com."));
                            }
                            break;
                        case "/studentstatus":
                            string StudentStatus = operation.value?.ToString();
                            if (StudentStatus != "Inactive" && StudentStatus != "Active")
                            {
                                return BadRequest(new ApiResponse(400, "Please enter Active or Inactive status."));
                            }
                            break;
                        default:
                            return BadRequest(new ApiResponse(400, "The JSON patch document is missing."));
                    }
                }

                var student = await studentDao.GetStudentById<StudentModel>(id);
                if (student == null)
                {
                    return NotFound(new ApiResponse(404, $"Student with that id not found."));
                }

                updateRequest.ApplyTo(student);
                await studentDao.PartiallyUpdateStudentById(student);

                cache.Remove($"studentKey{student.StudentId}");
                cache.Remove($"studentKey{student.StudentStatus}");

                return Ok(new ApiOkResponse(student));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Delete Student by Using Guid Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("student")]
        public async Task<IActionResult> DeleteStudentById([Required][FromQuery] Guid id)
        {
            try
            {
                var student = await studentDao.GetStudentById<StudentModel>(id);

                if (student == null)
                {
                    return NotFound(new ApiResponse(404, $"Student with that id not found."));
                }

                await studentDao.DeleteStudentById(id);

                cache.Remove($"studentKey{student.StudentId}");
                cache.Remove($"studentKey{student.StudentStatus}");

                return Ok(new ApiOkResponse(student));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
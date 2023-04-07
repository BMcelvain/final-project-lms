using Azure.Core;
using Lms.APIErrorHandling;
using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System;


namespace Lms.Controllers
{
    [ApiController]
    public class StudentEnrollmentController : ControllerBase
    {
        private IStudentEnrollmentDao studentEnrollmentDao;

        public StudentEnrollmentController(IStudentEnrollmentDao studentEnrollmentDao)
        {
            this.studentEnrollmentDao = studentEnrollmentDao;
        }


        /// <summary>
        /// Get Student Enrollment History by Student Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("studentEnrollment/{id}")]
        public async Task<IActionResult> GetStudentEnrollmentHistoryByStudentId([FromRoute] Guid id)
        {
            try
            {
                var studentEnrollments = await studentEnrollmentDao.GetStudentEnrollmentHistoryByStudentId(id);

                if (studentEnrollments.Count() == 0)
                {
                    return NotFound(new ApiResponse(404, $"Student Enrollment with that Student Id not found."));
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
        [Route("studentEnrollment/{studentLastName}")]
        public async Task<IActionResult> GetStudentEnrollmentHistoryByStudentLastName([FromRoute] string studentLastName)
        {

            try
            {
                var studentEnrollments = await studentEnrollmentDao.GetStudentEnrollmentHistoryByStudentLastName(studentLastName);

                if (studentEnrollments.Count() == 0)
                {
                    return NotFound(new ApiResponse(404, $"Student Enrollment with that Student Last Name not found."));
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
        [Route("studentActiveEnrollment/{studentPhone}")]
        public async Task<IActionResult> GetActiveStudentEnrollmentByStudentPhone([FromRoute] string studentPhone)
        {
            try
            {
                var activeStudentPhoneEnrollments = await studentEnrollmentDao.GetActiveStudentEnrollmentByStudentPhone(studentPhone);
                if (activeStudentPhoneEnrollments.Count() == 0)
                {
                    return StatusCode(404, "No Student with Active Courses found.");
                }
                return Ok(activeStudentPhoneEnrollments);
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
        [Route("studentsInCourse/{courseId}")]
        public async Task<IActionResult> GetStudentsInCourseByCourseId([FromRoute] Guid courseId)
        {
            try
            {
                var addStudentToCourse = await studentEnrollmentDao.GetStudentsInCourseByCourseId(courseId);
                return Ok(addStudentToCourse);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
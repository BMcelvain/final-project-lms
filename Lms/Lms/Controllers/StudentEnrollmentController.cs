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
using System.Net;
using Lms.APIErrorHandling;
using Azure.Core;
using System.Reflection;

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
        /// Get Student Enrollment history by Student Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("studentEnrollment/byStudentId/{id}")]
        public async Task<IActionResult> GetStudentEnrollmentHistoryById([FromRoute] Guid id)
        {
            try
            {
                var studentEnrollments = await studentEnrollmentDao.GetStudentEnrollmentHistoryById(id);

                if (studentEnrollments.Count() == 0)
                {
                    return NotFound(new ApiResponse(404, $"Student not found with id {id}"));
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
        [Route("studentEnrollment/byStudentLastName/{studentLastName}")]
        public async Task<IActionResult> GetStudentEnrollmentHistoryByStudentLastName([FromRoute] string studentLastName)
        {

            try
            {
                var studentEnrollments = await studentEnrollmentDao.GetStudentEnrollmentHistoryByStudentLastName(studentLastName);

                if (studentEnrollments.Count() == 0)
                {
                    return NotFound(new ApiResponse(404, $"Student not found with last name {studentLastName}"));
                }

                return Ok(new ApiOkResponse(studentEnrollments));

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        /// <summary>
        /// Get current Student Enrollment in Active Courses
        /// </summary>
        /// <param name="studentPhone"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("studentActiveEnrollment/byStudentPhone/{studentPhone}")]
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
        [Route("studentsInCourse/byCourseId/{courseId}")]
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
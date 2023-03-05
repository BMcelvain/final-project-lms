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
    public class StudentEnrollmentController : ControllerBase
    {
        private IStudentEnrollmentDao studentEnrollmentDao;

        public StudentEnrollmentController(IStudentEnrollmentDao studentEnrollmentDao)
        {
            this.studentEnrollmentDao = studentEnrollmentDao;
        }

        [HttpGet]
        [Route("studentEnrollment/byStudentId/{id}")]
        public async Task<IActionResult> GetStudentEnrollmentHistoryById([FromRoute] int id)
        {
            try
            {
                var studentEnrollments = await studentEnrollmentDao.GetStudentEnrollmentHistoryById(id);
                return Ok(studentEnrollments);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("studentEnrollment/byStudentLastName/{studentLastName}")]
        public async Task<IActionResult> GetStudentEnrollmentHistoryByStudentLastName([FromRoute] string studentLastName)
        {
            try
            {
                var studentEnrollments = await studentEnrollmentDao.GetStudentEnrollmentHistoryByStudentLastName(studentLastName);
                return Ok(studentEnrollments);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


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

        [HttpPost]
        [Route("addStudentToCourse")]
        public async Task<IActionResult> AddStudentToCourse(StudentEnrollmentModel addStudentToCourse)
        {
            try
            {
                await studentEnrollmentDao.AddStudentToCourse(addStudentToCourse);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("addStudentToCourse/byCourseId/{courseId}")]
        public async Task<IActionResult> GetCourseByCourseId([FromRoute] int courseId)
        {
            try
            {
                var addStudentToCourse = await studentEnrollmentDao.GetCourseByCourseId(courseId);
                return Ok(addStudentToCourse);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpPatch]
        [Route("addStudentToCourse/byStudentCourseId/{studentId},{courseId}")]
        public async Task<IActionResult> PartiallyUpdateStudentInCourseByCourseStudentId([FromRoute] int studentId, int courseId, JsonPatchDocument<StudentEnrollmentModel> addStudentCourseUpdates)
        {
            try
            {
                var addStudentToCourse = await studentEnrollmentDao.GetCourseByCourseId(courseId);

                if (addStudentToCourse == null)
                {
                    return NotFound();
                }

                addStudentCourseUpdates.ApplyTo(addStudentToCourse);
                await studentEnrollmentDao.PartiallyUpdateStudentInCourseByCourseStudentId(addStudentToCourse);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("addStudentToCourse/byStudentCourseId/{studentId},{courseId}")]
        public async Task<IActionResult> DeleteStudentInCourseByStudentCourseId([FromRoute] int studentId, int courseId)
        {
            try
            {
                var addStudentToCourse = await studentEnrollmentDao.GetCourseByCourseId(courseId);
                if (addStudentToCourse == null)
                {
                    return NotFound();
                }

                await studentEnrollmentDao.DeleteStudentInCourseByStudentCourseId(studentId, courseId);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}

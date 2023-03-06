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
    }
}


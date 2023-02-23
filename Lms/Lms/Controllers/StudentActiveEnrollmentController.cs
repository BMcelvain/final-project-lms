using Lms.Daos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Lms.Controllers
{
    [ApiController]
    public class StudentActiveEnrollmentController : ControllerBase
    {
        private readonly IStudentActiveEnrollmentDao studentActiveEnrollmentDao;

        public StudentActiveEnrollmentController(IStudentActiveEnrollmentDao studentActiveEnrollmentDao)
        {
            this.studentActiveEnrollmentDao = studentActiveEnrollmentDao;
        }

        [HttpGet]
        [Route("studentActiveEnrollment/byStudentLastName/{studentLastName}")]
        public async Task<IActionResult> GetActiveStudentEnrollmentByStudentLastName([FromRoute] string studentLastName)
        {
            try
            {
                var activeStudentLastNameEnrollments = await studentActiveEnrollmentDao.GetActiveStudentEnrollmentByStudentLastName(studentLastName);
                if (activeStudentLastNameEnrollments == null)
                {
                    return StatusCode(200, "No Student with Active Courses found.");
                }
                return Ok(activeStudentLastNameEnrollments);
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
                var activeStudentPhoneEnrollments = await studentActiveEnrollmentDao.GetActiveStudentEnrollmentByStudentPhone(studentPhone);
                if (activeStudentPhoneEnrollments == null) 
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
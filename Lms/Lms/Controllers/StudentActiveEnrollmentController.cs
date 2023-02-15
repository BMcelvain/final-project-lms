using Lms.Daos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace Lms.Controllers
{
    [ApiController]
    public class StudentActiveEnrollmentController : ControllerBase
    {
        private IStudentActiveEnrollmentDao studentActiveEnrollmentDao;

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
                return Ok(activeStudentPhoneEnrollments);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
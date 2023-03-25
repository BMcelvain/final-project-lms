using Lms.APIErrorHandling;
using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Lms.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentDao studentDao;

        public StudentController(IStudentDao studentDao)
        {
            this.studentDao = studentDao;
        }

        [HttpPost]
        [Route("student")]
        public async Task<IActionResult> CreateStudent(StudentModel newStudent)
        {
            try
            {
                await studentDao.CreateStudent(newStudent);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("student/{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            try
            {
                var student = await studentDao.GetStudentById(id);

                if (student == null)
                {
                    return NotFound(new ApiResponse(404, $"Student with id {id} not found."));
                }

                return Ok(new ApiOkResponse(student));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("student/{id}")]
        public async Task<IActionResult> PartiallyUpdateStudentById([FromRoute] int id, JsonPatchDocument<StudentModel> studentUpdates)
        {
            try
            {
                var student = await studentDao.GetStudentById(id);

                if (student == null)
                {
                    return NotFound(new ApiResponse(404, $"Student with id {id} not found."));
                }

                studentUpdates.ApplyTo(student);
                await studentDao.PartiallyUpdateStudentById(student);

                return Ok(new ApiOkResponse(student));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("student/{id}")]
        public async Task<IActionResult> DeleteStudentById([FromRoute] int id)
        {
            try
            {
                var student = await studentDao.GetStudentById(id);

                if (student == null)
                {
                    return NotFound(new ApiResponse(404, $"Student with id {id} not found."));
                }

                await studentDao.DeleteStudentById(id);
                return Ok(new ApiOkResponse(student));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
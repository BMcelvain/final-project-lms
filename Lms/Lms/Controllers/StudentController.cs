using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {


        private IStudentDao studentDao;
        private readonly StudentDao _studentDao;

        public StudentController(IStudentDao studentDao)
        {
            this.studentDao = studentDao;
        }

        [ActivatorUtilitiesConstructor]
        public StudentController(StudentDao studentDao)
        {
            _studentDao = studentDao;
        }

        [NonAction]
        public void CallDao()
        {
            studentDao.GetStudent();
        }

        [HttpPost]
        [Route("student")]
        public async Task<IActionResult> CreateStudent(StudentModel newStudent)
        {
            try
            {
                await _studentDao.CreateStudent(newStudent);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("student")]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var students = await _studentDao.GetStudents();
                return Ok(students);
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
                var student = await _studentDao.GetStudentById(id);
                if (student == null)
                {
                    return StatusCode(404);
                }

                return Ok(student);
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
                var student = await _studentDao.GetStudentById(id);

                if (student == null)
                {
                    return NotFound();
                }

                studentUpdates.ApplyTo(student);
                await _studentDao.PartiallyUpdateStudentById(student);

                return StatusCode(200);
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
                var student = await _studentDao.GetStudentById(id);
                if (student == null)
                {
                    return StatusCode(404);
                }

                await _studentDao.DeleteStudentById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}

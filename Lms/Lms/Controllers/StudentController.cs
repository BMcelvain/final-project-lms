using Lms.APIErrorHandling;
using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using System.Linq;
using FluentAssertions.Equivalency.Tracing;

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
                return Ok();
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
        [Route("student/{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] Guid id)
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

        //[HttpPatch]
        //[Route("student/{id}")]
        //public async Task<IActionResult> PartiallyUpdateStudentById([FromRoute] Guid id, JsonPatchDocument<StudentModel> studentUpdates)
        //{
        //    try
        //    {
        //        var student = await studentDao.GetStudentById(id);

        //        if (student == null)
        //        {
        //            return NotFound(new ApiResponse(404, $"Student with id {id} not found."));
        //        }

        //        studentUpdates.ApplyTo(student);
        //        await studentDao.PartiallyUpdateStudentById(student);

        //        return Ok(new ApiOkResponse(student));
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        /// <summary>
        /// Replace StudentFirstName, StudentLastName, StudentPhone, StudentEmail,or Student Status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentUpdates"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("student/{id}")]
        public async Task<IActionResult> PartiallyUpdateStudentById(Guid id, [FromBody] JsonPatchDocument<StudentModel> studentUpdates)
        {
            if (studentUpdates == null)
            {
                return NotFound(new ApiResponse(404, $"Student with id {id} not found."));
            }

            var allowedOperations = new[] { "replace" };

            foreach (Operation<StudentModel> operation in studentUpdates.Operations)
            {
                if (!allowedOperations.Contains(operation.op.ToLower()))
                {
                    return BadRequest(new ApiResponse(400,"Only 'replace' operation is allowed."));
                }

                switch (operation.path.ToLower()) //need to add OKResponses for when it works
                {
                    //need to figure out how to exclude numbers in name
                    case "/studentfirstname":
                        string StudentFirstName = operation.value?.ToString();
                        break;
                    case "/studentlastname":
                        string StudentLastName = operation.value?.ToString();
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
                    case "/totalpasscourses":
                        string TotalPassCourses = operation.value?.ToString();
                        break;
                    default:
                        return BadRequest(new ApiResponse(500));
                }
            }

            // process the patch operations
            var student = await studentDao.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }

            studentUpdates.ApplyTo(student);
            await studentDao.PartiallyUpdateStudentById(student);

            return Ok(new ApiOkResponse(student));
        }

        /// <summary>
        /// Delete Student by Using Guid Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("student/{id}")]
        public async Task<IActionResult> DeleteStudentById([FromRoute] Guid id)
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
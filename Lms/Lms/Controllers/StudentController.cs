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
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace Lms.Controllers
{

    [ApiController]
    [Authorize]
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
        //[Authorize("AdminOnly")] //need to figure out how to consider me admin
        [ResponseCache(Duration = 60)]
        [Route("student/{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] Guid id)
        {

            try
            {
                var student = await studentDao.GetStudentById(id);

                if (student == null)
                {
                    return NotFound(new ApiResponse(404, $"Student with that id not found."));
                }

                return Ok(new ApiOkResponse(student));
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
            if (updateRequest == null)
            {
                return NotFound(new ApiResponse(404, $"Student with that id not found."));
            }

            var allowedOperations = new[] { "replace" };

            foreach (Operation<StudentModel> operation in updateRequest.Operations)
            {
                if (!allowedOperations.Contains(operation.op.ToLower()))
                {
                    return BadRequest(new ApiResponse(400,"Only 'replace' operation is allowed."));
                }

                switch (operation.path.ToLower())
                {
                    case "/studentfirstname":
                        string StudentFirstName = operation.value?.ToString();
                        if (!Regex.IsMatch(StudentFirstName, @"^[A-Z][A-Za-z]+}$"))
                        {
                            return BadRequest(new ApiResponse(400, "Please enter first name starting with capital letter, lowercase for the remaining letters."));
                        }
                        break;
                    case "/studentlastname":
                        string StudentLastName = operation.value?.ToString();
                        if (!Regex.IsMatch(StudentLastName, @"^[A-Z][A-Za-z-]+}$"))
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
                    case "/totalpasscourses": //delete
                        string TotalPassCourses = operation.value?.ToString();
                        break;
                    default:
                        return BadRequest(new ApiResponse(400, "The JSON patch document is missing."));
                }
            }

            // process the patch operations
            var student = await studentDao.GetStudentById(id);
            if (student == null)
            {
                return NotFound(new ApiResponse(404, $"Student with that id not found."));
            }

            updateRequest.ApplyTo(student);
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
                    return NotFound(new ApiResponse(404, $"Student with that id not found."));
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
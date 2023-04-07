using FluentAssertions.Equivalency.Tracing;
using Lms.APIErrorHandling;
using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;


namespace Lms.Controllers
{
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherDao teacherDao;

        public TeacherController(ITeacherDao teacherDao)
        {
            this.teacherDao = teacherDao;
        }

        /// <summary>
        /// Create Teacher 
        /// </summary>
        /// <param name="newTeacher"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("teacher")]
        public async Task<IActionResult> CreateTeacher(TeacherModel newTeacher)
        {
            try
            {
                await teacherDao.CreateTeacher(newTeacher);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get Teacher by Teacher Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("teacher/{id}")]
        public async Task<IActionResult> GetTeacherById([FromRoute] Guid id)
        {
            try
            {
                var teacher = await teacherDao.GetTeacherById(id);

                if (teacher == null)
                {
                    return NotFound(new ApiResponse(404, $"Teacher with that id not found."));
                }

                return Ok(new ApiOkResponse(teacher));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        /// <summary>
        /// Get Teacher by Active or Inactive Status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("teachers/{status}")]
        public async Task<IActionResult> GetTeacherByStatus([FromRoute] string status)
        {
            try
            {
                if (status.ToLower() != "inactive" && status.ToLower() != "active")
                {
                    return BadRequest(new ApiResponse(400, "Please enter Active or Inactive status."));
                }

                var teachers = await teacherDao.GetTeacherByStatus(status);

                if (teachers.IsNullOrEmpty())
                {
                    return NotFound(new ApiResponse(404, $"Teacher with status {status} not found."));
                }

                return Ok(new ApiOkResponse(teachers));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Update CourseName, CourseStatus, TeacherId, StartDate, or EndDate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateRequest"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("teacher/{id}")]
        public async Task<IActionResult> PartiallyUpdateTeacherById(Guid id, [FromBody] JsonPatchDocument<TeacherModel> updateRequest)
        {
            if (updateRequest == null)
            {
                return NotFound(new ApiResponse(404, $"Teacher with that id not found."));
            }

            var allowedOperations = new[] { "replace" };

            foreach (Operation<TeacherModel> operation in updateRequest.Operations)
            {
                if (!allowedOperations.Contains(operation.op.ToLower()))
                {
                    return BadRequest(new ApiResponse(400, "Only 'replace' operation is allowed."));
                }

                switch (operation.path.ToLower())
                {

                    case "/teacherfirstname":
                        string TeacherFirstName = operation.value?.ToString();
                        if (!Regex.IsMatch(TeacherFirstName, @"^[A-Z][a-z]+$"))
                        {
                            return BadRequest(new ApiResponse(400, "Please enter a name starting with a capital letter, followed by lowercase letters."));
                        }
                        break;
                    case "/teacherlastname":
                        string TeacherLastName = operation.value?.ToString();
                        if (!Regex.IsMatch(TeacherLastName, @"^[A-Z][a-z]+$"))
                        {
                            return BadRequest(new ApiResponse(400, "Please enter a name starting with a capital letter, followed by lowercase letters."));
                        }
                        break;
                    case "/teacherphone":
                        string TeacherPhone = operation.value?.ToString();
                        if (!Regex.IsMatch(TeacherPhone, @"^\d{3}-\d{3}-\d{4}$"))
                        {
                            return BadRequest(new ApiResponse(400, "Please enter a phone number in a valid format: XXX-XXX-XXXX."));
                        }
                        break;
                    case "/teacheremail":
                        string TeacherEmail = operation.value?.ToString();
                        if (!Regex.IsMatch(TeacherEmail, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
                        {
                            return BadRequest(new ApiResponse(400, "Please enter an E-Mail in a valid format: example@vu.com."));
                        }
                        break;
                    case "/teacherstatus":
                        string TeacherStatus = operation.value?.ToString();
                        if (TeacherStatus != "Inactive" && TeacherStatus != "Active")
                        {
                            return BadRequest(new ApiResponse(400, "Please enter Active or Inactive status."));
                        }
                        break;
                    default:
                        return BadRequest(new ApiResponse(500, "The JSON patch document is missing."));


                }
            }

            // process the patch operations
            var teacher = await teacherDao.GetTeacherById(id);
            if (teacher == null)
            {
                return NotFound(new ApiResponse(404, $"Teacher with that id not found."));
            }

            updateRequest.ApplyTo(teacher);
            await teacherDao.PartiallyUpdateTeacherById(teacher);

            return Ok(new ApiOkResponse(teacher));

        }

        /// <summary>
        /// Delete Teacher by Teacher Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("teacher/{id}")]
        public async Task<IActionResult> DeleteTeacherById([FromRoute] Guid id)
        {
            try
            {
                var teacher = await teacherDao.GetTeacherById(id);

                if (teacher == null)
                {
                    return NotFound(new ApiResponse(404, $"Teacher with that id not found."));
                }

                await teacherDao.DeleteTeacherById(id);
                return Ok(new ApiOkResponse(teacher));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
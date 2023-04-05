using Lms.Daos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;
using Lms.APIErrorHandling;
using Microsoft.IdentityModel.Tokens;

namespace Lms.Controllers
{
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private ITeacherDao teacherDao;

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
                    return NotFound(new ApiResponse(404, $"Teacher with id {id} not found."));
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
        [Route("teachers/byStatus/{status}")]
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
        /// Update TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail, or TeacherStatus 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="teacherUpdates"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("teacher/{id}")]
        public async Task<IActionResult> PartiallyUpdateTeacherById([FromRoute] Guid id, JsonPatchDocument<TeacherModel> teacherUpdates)
        {
            try
            {
                var teacher = await teacherDao.GetTeacherById(id);

                if (teacher == null)
                {
                    return NotFound(new ApiResponse(404, $"Teacher with id {id} not found."));
                }

                teacherUpdates.ApplyTo(teacher);
                await teacherDao.PartiallyUpdateTeacherById(teacher);

                return Ok(new ApiOkResponse(teacher));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
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
                    return NotFound(new ApiResponse(404, $"Teacher with id {id} not found."));
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
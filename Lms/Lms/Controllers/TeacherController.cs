using Lms.Daos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;
using Lms.APIErrorHandling;

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

                if (teachers == null)
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
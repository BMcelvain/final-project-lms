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

                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("teacher/{id}")]
        public async Task<IActionResult> GetTeacherById([FromRoute] int id)
        {
            try
            {
                var teacher = await teacherDao.GetTeacherById(id);
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }

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
                var teachers = await teacherDao.GetTeacherByStatus(status);
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }

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
        public async Task<IActionResult> PartiallyUpdateTeacherById([FromRoute] int id, JsonPatchDocument<TeacherModel> teacherUpdates)
        {
            try
            {
                var teacher = await teacherDao.GetTeacherById(id);

                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }

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
        public async Task<IActionResult> DeleteTeacherById([FromRoute] int id)
        {
            try
            {
                var teacher = await teacherDao.GetTeacherById(id);
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiBadRequestResponse(ModelState));
                }

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
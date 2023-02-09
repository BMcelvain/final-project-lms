using Lms.Daos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lms.Models;
using System.Drawing.Printing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.JsonPatch;

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
        [Route("teacher")]
        public async Task<IActionResult> GetTeachers()
        {
            try
            {
                var teachers = await teacherDao.GetTeachers();
                return Ok(teachers);
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
                if (teacher == null)
                {
                    return StatusCode(404);
                }

                return Ok(teacher);
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
                return Ok(teachers);
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

                if (teacher == null)
                {
                    return NotFound();
                }

                teacherUpdates.ApplyTo(teacher);
                await teacherDao.PartiallyUpdateTeacherById(teacher);

                return Ok();
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
                if (teacher == null)
                {
                    return NotFound();
                }

                await teacherDao.DeleteTeacherById(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
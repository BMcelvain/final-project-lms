using Lms.Daos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;


namespace Lms.Controllers
{
    [ApiController]
    public class AddStudentToCourseController : ControllerBase
    {
        private readonly IAddStudentToCourseDao addStudentToCourseDao;

        public AddStudentToCourseController(IAddStudentToCourseDao addStudentToCourseDao)
        {
            this.addStudentToCourseDao = addStudentToCourseDao;
        }

        [HttpPost]
        [Route("addStudentToCourse")]
        public async Task<IActionResult> AddStudentToCourse(AddStudentToCourseModel addStudentToCourse)
        {
            try
            {
                await addStudentToCourseDao.AddStudentToCourse(addStudentToCourse);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("addStudentToCourse/byCourseId/{courseId}")]
        public async Task<IActionResult> GetCourseByCourseId([FromRoute] int courseId)
        {
            try
            {
                var addStudentToCourse = await addStudentToCourseDao.GetCourseByCourseId(courseId);
                return Ok(addStudentToCourse);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpPatch]
        [Route("addStudentToCourse/byStudentCourseId/{studentId},{courseId}")]
        public async Task<IActionResult> PartiallyUpdateStudentInCourseByCourseStudentId([FromRoute]int studentId, int courseId ,JsonPatchDocument<AddStudentToCourseModel> addStudentCourseUpdates)
        {
            try
            {
                var addStudentToCourse = await addStudentToCourseDao.GetCourseByCourseId(courseId);

                if (addStudentToCourse == null)
                {
                    return NotFound();
                }

                addStudentCourseUpdates.ApplyTo(addStudentToCourse);
                await addStudentToCourseDao.PartiallyUpdateStudentInCourseByCourseStudentId(addStudentToCourse);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("addStudentToCourse/byStudentCourseId/{studentId},{courseId}")]
        public async Task<IActionResult> DeleteStudentInCourseByStudentCourseId([FromRoute] int studentId, int courseId)
        {
            try
            {
                var addStudentToCourse = await addStudentToCourseDao.GetCourseByCourseId(courseId);
                if (addStudentToCourse == null)
                {
                    return NotFound();
                }

                await addStudentToCourseDao.DeleteStudentInCourseByStudentCourseId(studentId, courseId);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);

            }

        }

    }
    
}








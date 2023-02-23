using Lms.Daos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Lms.Controllers
{
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private ISemesterDao SemesterDao;

        public SemesterController(ISemesterDao SemesterDao)
        {
            this.SemesterDao = SemesterDao;
        }

        [HttpPost]
        [Route("semesters")]
        public async Task<IActionResult> CreateSemester(SemesterModel newSemester)
        {
            try
            {
                await SemesterDao.CreateSemester(newSemester);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("semesters")]
        public async Task<IActionResult> GetSemesters()
        {
            try
            {
                var semesters = await SemesterDao.GetSemesters();
                return Ok(semesters);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("semesters/byId/{id}")]
        public async Task<IActionResult> GetSemesterById([FromRoute] int id)
        {
            try
            {
                var semester = await SemesterDao.GetSemesterById(id);
                if (semester == null)
                {
                    return NotFound();
                }

                return Ok(semester);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpDelete]
        [Route("semesters/{id}")]
        public async Task<IActionResult> DeleteSemesterById([FromRoute] int id)
        {
            try
            {
                var semester = await SemesterDao.GetSemesterById(id);
                if (semester == null)
                {
                    return NotFound();
                }

                await SemesterDao.DeleteSemesterById(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
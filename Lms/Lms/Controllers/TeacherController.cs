using Lms.APIErrorHandling;
using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;
using FluentAssertions;

namespace Lms.Controllers
{
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private IMemoryCache cache;
        private ITeacherDao teacherDao;

        public TeacherController(ITeacherDao teacherDao, IMemoryCache cache)
        {
            this.teacherDao = teacherDao;
            this.cache = cache;
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

                cache.Remove($"teachersKey{newTeacher.TeacherStatus}");

                return Ok(new ApiOkResponse(newTeacher));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get Teacher By Filters
        /// </summary>
        /// <param name="TeacherId"></param>
        /// /// <param name="TeacherLastName"></param>
        /// /// <param name="TeacherPhone"></param>
        /// /// <param name="TeacherStatus"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("teacher")]
        public async Task<IActionResult> GetTeacher([FromQuery] Guid TeacherId, string TeacherLastName, string TeacherPhone, string TeacherStatus)
        {
            try
            {
                if (cache.TryGetValue($"teacherKey{TeacherId}", out IEnumerable<TeacherModel> teacher))
                {
                    Log.Information($"Teacher with id {TeacherId} found in cache");
                }
                else
                {
                    Log.Information($"Teacher with id {TeacherId} not found in cache. Checking database.");

                    teacher = await teacherDao.GetTeacher(TeacherId, TeacherLastName, TeacherPhone, TeacherStatus);
                    if (teacher == null)
                    {
                        return NotFound(new ApiResponse(404, $"Teacher not found."));
                    }

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(180))
                        .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                        .SetSize(1024);

                    cache.Set($"teacherKey{TeacherId}", teacher, cacheEntryOptions);
                }

                return Ok(new ApiOkResponse(teacher));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Update TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail, TeacherStatus
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateRequest"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("teacher/{id}")]
        public async Task<IActionResult> PartiallyUpdateTeacherById(Guid id, [FromBody] JsonPatchDocument<TeacherModel> updateRequest)
        {
            try
            {
                if (updateRequest == null )
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
                            if (!Regex.IsMatch(TeacherLastName, @"^[A-Z][A-Za-z-]+$"))
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
                            return BadRequest(new ApiResponse(400, "The JSON patch document is missing."));
                    }
                }

                var teacher = await teacherDao.GetTeacher(id, null, null, null);

                if(teacher == null || !teacher.Any())
                {
                    return NotFound(new ApiResponse(404, $"Teacher with that id not found."));
                }

                var teacherToUpdate = teacher.FirstOrDefault();

                updateRequest.ApplyTo(teacherToUpdate);

                await teacherDao.PartiallyUpdateTeacherById(teacherToUpdate);

                foreach (var teach in teacher)
                {
                    if (teach.TeacherId != null)
                    {
                        cache.Remove($"teacherKey{teach.TeacherId}");
                    }

                    if (teach.TeacherStatus != null)
                    {
                        cache.Remove($"teachersKey{teach.TeacherStatus}");
                    }
                }

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
        public async Task<IActionResult> DeleteTeacherById(Guid id)
        {
            try
            {
                var teacher = await teacherDao.GetTeacher(id, null, null, null);

                if (teacher == null || !teacher.Any())
                {
                    return NotFound(new ApiResponse(404, $"Teacher with that id not found."));
                }

                foreach (var teach in teacher)
                {
                    if (teach.TeacherId != null)
                    {
                        cache.Remove($"teacherKey{teach.TeacherId}");
                    }

                    if (teach.TeacherStatus != null)
                    {
                        cache.Remove($"teachersKey{teach.TeacherStatus}");
                    }
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
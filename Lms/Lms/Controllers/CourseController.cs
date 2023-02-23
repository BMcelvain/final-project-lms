﻿using Lms.Daos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Lms.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Lms.Controllers
{
    [ApiController]
    public class CourseController : ControllerBase
    {
        private ICourseDao courseDao;

        public CourseController(ICourseDao courseDao)
        {
            this.courseDao = courseDao;
        }

        [HttpPost]
        [Route("courses")]
        public async Task<IActionResult> CreateCourse(CourseModel newCourse)
        {
            try
            {
                await courseDao.CreateCourse(newCourse);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("courses")]
        public async Task<IActionResult> GetCourses()
        {
            try
            {
                var courses = await courseDao.GetCourses();
                return Ok(courses);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("courses/byId/{id}")]
        public async Task<IActionResult> GetCourseById([FromRoute] int id)
        {
            try
            {
                var course = await courseDao.GetCourseById(id);
                if (course == null)
                {
                    return NotFound();
                }

                return Ok(course);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("courses/byStatus/{status}")]
        public async Task<IActionResult> GetCourseByStatus([FromRoute] string status)
        {
            try
            {
                var courses = await courseDao.GetCourseByStatus(status);
                return Ok(courses);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("courses/{id}")]
        public async Task<IActionResult> PartiallyUpdateCourseById([FromRoute]int id, JsonPatchDocument<CourseModel> courseUpdates)
        {
            try
            {
                var course = await courseDao.GetCourseById(id);

                if (course == null)
                {
                    return NotFound();
                }

                courseUpdates.ApplyTo(course);
                await courseDao.PartiallyUpdateCourseById(course);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("courses/{id}")]
        public async Task<IActionResult> DeleteCourseById([FromRoute] int id)
        {
            try
            {
                var course = await courseDao.GetCourseById(id);
                if (course == null)
                {
                    return NotFound();
                }

                await courseDao.DeleteCourseById(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
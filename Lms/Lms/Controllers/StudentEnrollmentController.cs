﻿using Lms.Daos;
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
    public class StudentEnrollmentController : ControllerBase
    {
        private IStudentEnrollmentDao studentEnrollmentDao;

        public StudentEnrollmentController(IStudentEnrollmentDao studentEnrollmentDao)
        {
            this.studentEnrollmentDao = studentEnrollmentDao;
        }

        [HttpGet]
        [Route("studentEnrollment/byId/{id}")]
        public async Task<IActionResult> GetstudentEnrollments([FromRoute] int id)
        {
            try
            {
                var studentEnrollments = await studentEnrollmentDao.GetStudentEnrollmentHistory(id);
                return Ok(studentEnrollments);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
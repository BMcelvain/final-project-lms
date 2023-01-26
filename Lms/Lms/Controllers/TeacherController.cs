using Lms.Daos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [NonAction]
        public void CallDao()
        {
            teacherDao.GetTeacher();
        }
    }
}

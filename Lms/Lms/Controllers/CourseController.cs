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
    public class CourseController : ControllerBase
    {
        private ICourseDao courseDao;

        public CourseController(ICourseDao courseDao)
        {
            this.courseDao = courseDao;
        }

        public void CallDao()
        {
            CourseDao courseDao = new CourseDao();
            throw new NotImplementedException();
        }
    }
}

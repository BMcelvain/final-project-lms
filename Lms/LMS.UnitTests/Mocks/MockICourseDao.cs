using Lms.Daos;
using Lms.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.UnitTests.Mocks
{
    public class MockICourseDao
    {
        public static Mock<ICourseDao> MockDao() 
        {
            var mockCourseDao = new Mock<ICourseDao>();
            var courses = new List<CourseModel>
            {
                new CourseModel()
                {
                    CourseId = new Guid("0AE43554-0BB1-42B1-94C7-04420A2167A6"),
                    TeacherId = new Guid("5CF5D4C1-CC4C-417E-8458-007F5E4BB913"),
                    CourseName = "Test Course",
                    StartDate = "01/01/2023",
                    EndDate = "03/01/2023",
                    CourseStatus = "Active"
                }
            };

            mockCourseDao
                .Setup( x => x.CreateCourse(It.IsAny<CourseModel>()))
                .Callback(() => { return; });

            mockCourseDao
                .Setup(x => x.GetCourseByStatus(It.IsAny<String>()))
                .ReturnsAsync((string status) => courses.Where(course => course.CourseStatus == status));

            mockCourseDao
                .Setup(x => x.GetCourseById<CourseModel>(It.IsAny<Guid>()))
                .ReturnsAsync(courses.First());

            mockCourseDao
                .Setup(x => x.PartiallyUpdateCourseById(It.IsAny<CourseModel>()))
                .Callback(() => { return; });

            mockCourseDao
                .Setup(x => x.DeleteCourseById(It.IsAny<Guid>()))
                .Callback(() => { return; });

            mockCourseDao
                .Setup(x => x.StudentInCourse(It.IsAny<StudentInCourseModel>()))
                .Callback(() => { return; });

            mockCourseDao
                .Setup(x => x.PartiallyUpdateStudentInCourseByCourseStudentId(It.IsAny<StudentInCourseModel>()))
                .Callback(() => { return; });

            mockCourseDao
                .Setup(x => x.DeleteStudentInCourseByStudentCourseId(It.IsAny<StudentInCourseModel>()))
                .Callback(() => { return; });

            return mockCourseDao;
        }
    }
}

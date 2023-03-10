using Lms.Wrappers;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Lms.Models;

namespace Lms.Daos
{
    public class CourseDao : ICourseDao
    {
        private readonly ISqlWrapper sqlWrapper;

        public CourseDao(ISqlWrapper sqlWrapper) 
        {
            this.sqlWrapper = sqlWrapper;
        }

        // POST a new course within the Course table. 
        public async Task CreateCourse(CourseModel newCourse)
        {
            var query = "INSERT Course (TeacherId, CourseName, SemesterId, StartDate, EndDate, CourseStatus)" +
                         $"VALUES(@TeacherId, @CourseName, @SemesterId, @StartDate, @EndDate, @CourseStatus)";

            var parameters = new DynamicParameters();
            parameters.Add("TeacherId", newCourse.TeacherId, DbType.Int32);
            parameters.Add("CourseName", newCourse.CourseName, DbType.String);
            parameters.Add("SemesterId", newCourse.SemesterId, DbType.Int32);
            parameters.Add("StartDate", newCourse.StartDate, DbType.String);
            parameters.Add("EndDate", newCourse?.EndDate, DbType.String);
            parameters.Add("CourseStatus", newCourse.CourseStatus, DbType.String);


            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }


        // GET a single course (by Id) within the Course table.
        public async Task<CourseModel> GetCourseById(int id)
        {
            var query = $"SELECT * FROM Course WHERE CourseId = {id}";

            using (sqlWrapper.CreateConnection())
            {
                var course = await sqlWrapper.QueryFirstOrDefaultAsync<CourseModel>(query);
                return course;
            }
        }

        // GET all courses within the Course table. 
        public async Task<IEnumerable<CourseModel>> GetCourseByStatus(string status)
        {
            var query = $"SELECT * FROM Course WHERE CourseStatus = '{status}'";
            using (sqlWrapper.CreateConnection())
            {
                var courses = await sqlWrapper.QueryAsync<CourseModel>(query);

                return courses.ToList();
            }
        }

        // PATCH a course within the Course table. 
        public async Task PartiallyUpdateCourseById(CourseModel updateRequest)
        {
            var query = "UPDATE Course SET TeacherId=@TeacherId, CourseName=@CourseName, SemesterId=@SemesterId, " +
                        $"StartDate=@StartDate, EndDate=@EndDate, CourseStatus=@CourseStatus WHERE CourseId=@CourseId";

            var parameters = new DynamicParameters();
            parameters.Add("CourseId", updateRequest.CourseId, DbType.Int32);
            parameters.Add("TeacherId", updateRequest.TeacherId, DbType.Int32);
            parameters.Add("CourseName", updateRequest.CourseName, DbType.String);
            parameters.Add("SemesterId", updateRequest.SemesterId, DbType.Int32);
            parameters.Add("StartDate", updateRequest.StartDate, DbType.String);
            parameters.Add("EndDate", updateRequest?.EndDate, DbType.String);
            parameters.Add("CourseStatus", updateRequest.CourseStatus, DbType.String);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }

        // DELETE a single course (by Id) within the Course table. 
        public async Task DeleteCourseById(int id)
        {
            var query = $"DELETE FROM Course WHERE CourseId = {id}";

            using (sqlWrapper.CreateConnection())
            {
               await sqlWrapper.ExecuteAsync(query);
            }
        }
        //-------------Add Student in Course Section-----------------------------------------------------------------------------
        public async Task StudentInCourse(StudentInCourseModel addStudentInCourse)
        {
            var query = "INSERT StudentEnrollmentLog (CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed)" +
                        $"VALUES (@CourseId, @StudentId, @EnrollmentDate, @Cancelled, @CancellationReason, @HasPassed)";

            var parameters = new DynamicParameters();
            parameters.Add("CourseId", addStudentInCourse.CourseId, DbType.Int32);
            parameters.Add("StudentId", addStudentInCourse.StudentId, DbType.Int32);
            parameters.Add("EnrollmentDate", addStudentInCourse.EnrollmentDate, DbType.String);
            parameters.Add("Cancelled", addStudentInCourse.Cancelled, DbType.Boolean);
            parameters.Add("CancellationReason", addStudentInCourse?.CancellationReason, DbType.String);
            parameters.Add("HasPassed", addStudentInCourse.HasPassed, DbType.Boolean);


            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }


        // PATCH a student within the Enrollment Log.
        public async Task PartiallyUpdateStudentInCourseByCourseStudentId(StudentInCourseModel updateRequest)
        {
            var query = "UPDATE StudentEnrollmentLog SET CourseId=@CourseId, StudentId=@StudentId, " +
                        $"EnrollmentDate=@EnrollmentDate, Cancelled=@Cancelled, CancellationReason=@CancellationReason, HasPassed=@HasPassed WHERE StudentId=@StudentId AND CourseId=@CourseId";

            var parameters = new DynamicParameters();
            parameters.Add("CourseId", updateRequest.CourseId, DbType.Int32);
            parameters.Add("StudentId", updateRequest.StudentId, DbType.Int32);
            parameters.Add("EnrollmentDate", updateRequest.EnrollmentDate, DbType.String);
            parameters.Add("Cancelled", updateRequest.Cancelled, DbType.Boolean);
            parameters.Add("CancellationReason", updateRequest?.CancellationReason, DbType.String);
            parameters.Add("HasPassed", updateRequest.HasPassed, DbType.Boolean);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }

        // DELETE a single course (by Id) within the Student Enrollment Log 
        public async Task DeleteStudentInCourseByStudentCourseId(int studentId, int courseId)
        {
            var query = $"DELETE FROM StudentEnrollmentLog WHERE StudentId = {studentId} AND CourseId = {courseId}";

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query);
            }
        }


    }
}
using Dapper;
using Lms.Models;
using Lms.Wrappers;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System;


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
            var query = "INSERT Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus)" +
                         $"VALUES(@CourseId, @TeacherId, @CourseName, @StartDate, @EndDate, @CourseStatus)";

            var parameters = new DynamicParameters();
            parameters.Add("CourseId", Guid.NewGuid(), DbType.Guid);
            parameters.Add("TeacherId", newCourse.TeacherId, DbType.Guid);
            parameters.Add("CourseName", newCourse.CourseName, DbType.String);
            parameters.Add("StartDate", newCourse.StartDate, DbType.String);
            parameters.Add("EndDate", newCourse?.EndDate, DbType.String);
            parameters.Add("CourseStatus", newCourse.CourseStatus, DbType.String);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }

        // GET a single course (by Guid) within the Course table.
        public async Task<CourseModel> GetCourseById<CourseModel>(Guid id)
        {
            var query = $"SELECT * FROM Course WHERE CourseId = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);

            using (sqlWrapper.CreateConnection())
            {
                var course = await sqlWrapper.QueryFirstOrDefaultAsync<CourseModel>(query, parameters);
                return course;
            }
        }

        // GET all courses with status of 'inactive' or 'active'. 
        public async Task<IEnumerable<CourseModel>> GetCourseByStatus(string status)
        {
            var query = "SELECT * FROM Course WHERE CourseStatus = @courseStatus ORDER BY StartDate ASC";
            var courseStatus = new { courseStatus = new DbString { Value = status, IsFixedLength = false, IsAnsi = true } };

            using (sqlWrapper.CreateConnection())
            {
                var courses = await sqlWrapper.QueryAsync<CourseModel>(query, courseStatus);

                return courses.ToList();
            }
        }

        // PATCH a course within the Course table. 
        public async Task PartiallyUpdateCourseById(CourseModel updateRequest)
        {
            var query = "UPDATE Course SET TeacherId=@TeacherId, CourseName=@CourseName, " +
                        $"StartDate=@StartDate, EndDate=@EndDate, CourseStatus=@CourseStatus WHERE CourseId=@CourseId";

            var parameters = new DynamicParameters();
            parameters.Add("CourseId", updateRequest.CourseId, DbType.Guid);
            parameters.Add("TeacherId", updateRequest.TeacherId, DbType.Guid);
            parameters.Add("CourseName", updateRequest.CourseName, DbType.String);
            parameters.Add("StartDate", updateRequest.StartDate, DbType.String);
            parameters.Add("EndDate", updateRequest?.EndDate, DbType.String);
            parameters.Add("CourseStatus", updateRequest.CourseStatus, DbType.String);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }

        // DELETE a single course (by Guid) within the Course table. 
        public async Task DeleteCourseById(Guid id)
        {
            var query = $"DELETE FROM Course WHERE CourseId = @CourseId";

            var parameters = new DynamicParameters();
            parameters.Add("CourseId", id, DbType.Guid);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }
    }
}
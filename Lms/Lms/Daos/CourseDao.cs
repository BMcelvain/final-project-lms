using Lms.Wrappers;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Lms.Models;
using System;

namespace Lms.Daos
{
    public class CourseDao : ICourseDao
    {
        private readonly DapperContext _context;
        private readonly ISqlWrapper sqlWrapper;

        public CourseDao(DapperContext context)
        {
            _context = context;
        }

        public CourseDao(ISqlWrapper sqlWrapper) 
        {
            this.sqlWrapper = sqlWrapper;
        }

       //when testing -- update to your database name LMS

        public void GetCourse(bool shouldCallSql = true)
        {
            if (shouldCallSql) 
            {
                sqlWrapper.Query<CourseModel>("SELECT * FROM [DBO.[LearningManagementSystem]");
            }
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


            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        // GET all courses within the Course table. 
        public async Task<IEnumerable<CourseModel>> GetCourses()
        {
            var query = "SELECT * FROM Course";
            using (var connection = _context.CreateConnection())
            {
                var courses = await connection.QueryAsync<CourseModel>(query);

                return courses.ToList();
            }
        }

        // GET a single course (by Id) within the Course table.
        public async Task<CourseModel> GetCourseById(int id)
        {
            var query = $"SELECT * FROM Course WHERE CourseId = {id}";

            using (var connection = _context.CreateConnection())
            {
                var course = await connection.QueryFirstOrDefaultAsync<CourseModel>(query);
                return course;
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

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        // DELETE a single course (by Id) within the Course table. 
        public async Task DeleteCourseById(int id)
        {
            var query = $"DELETE FROM Course WHERE CourseId = {id}";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }
    }
}
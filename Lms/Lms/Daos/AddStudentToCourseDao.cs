using Lms.Wrappers;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Lms.Models;
using System;

namespace Lms.Daos
{
    public class AddStudentToCourseDao : IAddStudentToCourseDao
    {
        private readonly ISqlWrapper sqlWrapper;

        public AddStudentToCourseDao(ISqlWrapper sqlWrapper) 
        {
            this.sqlWrapper = sqlWrapper;
        }

        // POST a new student within the Enrollment Log.
        public async Task AddStudentToCourse(AddStudentToCourseModel addStudentToCourse)
        {
            var query = "INSERT StudentEnrollmentLog (CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed)" +
                        $"VALUES (@CourseId, @SemesterId, @StudentId, @EnrollmentDate, @Cancelled, @CancellationReason, @HasPassed)";

            var parameters = new DynamicParameters();
            parameters.Add("CourseId", addStudentToCourse.CourseId, DbType.Int32);
            parameters.Add("SemesterId", addStudentToCourse.SemesterId, DbType.Int32);
            parameters.Add("StudentId", addStudentToCourse.StudentId, DbType.Int32);
            parameters.Add("EnrollmentDate", addStudentToCourse.EnrollmentDate, DbType.String);
            parameters.Add("Cancelled", addStudentToCourse.Cancelled, DbType.Boolean);
            parameters.Add("CancellationReason", addStudentToCourse?.CancellationReason, DbType.String);
            parameters.Add("HasPassed", addStudentToCourse.HasPassed, DbType.Boolean);


            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }

        // GET specific CourseId within the Enrollment Log.
        public async Task<AddStudentToCourseModel> GetCourseByCourseId(int courseId) //update to Course Name
        {
            var query = $"SELECT * FROM StudentEnrollmentLog WHERE CourseId = {courseId}";

            using (sqlWrapper.CreateConnection())
            {
                var course = await sqlWrapper.QueryFirstOrDefaultAsync<AddStudentToCourseModel>(query);
                return course;
            }
        }

        // PATCH a student within the Enrollment Log.
        public async Task PartiallyUpdateStudentInCourseByCourseStudentId(AddStudentToCourseModel updateRequest)
        {
            var query = "UPDATE StudentEnrollmentLog SET CourseId=@CourseId, SemesterId=@SemesterId, StudentId=@StudentId, " +
                        $"EnrollmentDate=@EnrollmentDate, Cancelled=@Cancelled, CancellationReason=@CancellationReason, HasPassed=@HasPassed WHERE StudentId=@StudentId AND CourseId=@CourseId";

            var parameters = new DynamicParameters();
            parameters.Add("CourseId", updateRequest.CourseId, DbType.Int32);
            parameters.Add("SemesterId", updateRequest.SemesterId, DbType.Int32);
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
            var query = $"DELETE FROM StudentEnrollmentLog WHERE StudentId = {studentId} AND CourseId = {courseId}"; //update to First Name & Last Name 

            using (sqlWrapper.CreateConnection())
            {
               await sqlWrapper.ExecuteAsync(query);
            }
        }

     
    }
}
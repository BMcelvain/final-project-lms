using Dapper;
using Lms.Models;
using Lms.Wrappers;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System;


namespace Lms.Daos
{
    public class StudentEnrollmentDao : IStudentEnrollmentDao
    {
        private readonly ISqlWrapper sqlWrapper;

        public StudentEnrollmentDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        public async Task<IEnumerable<StudentEnrollmentModel>> GetStudentEnrollmentHistoryByStudentId(Guid id)
        {
            var query = $"SELECT" +
            $" [Course].[CourseId]" +
            $", [Course].[CourseName]" +
            $", [Course].[StartDate]" +
            $", [Course].[EndDate]" +
            $", [StudentEnrollmentLog].[Cancelled]" +
            $", [StudentEnrollmentLog].[CancellationReason]" +
            $", [StudentEnrollmentLog].[HasPassed]" +
            $", [Teacher].[TeacherEmail]" +
            $", [Student].[StudentPhone]" +
            $", [Student].[TotalPassCourses]" +
            $" FROM [StudentEnrollmentLog]" +
            $" INNER JOIN [Course] ON [StudentEnrollmentLog].[CourseId] = [Course].[CourseId]" +
            $" INNER JOIN [Teacher] ON [Course].[TeacherId] = [Teacher].[TeacherId]" +
            $" INNER JOIN [Student] ON [StudentEnrollmentLog].[StudentId] = [Student].[StudentId]" +
            $" WHERE [StudentEnrollmentLog].[StudentId] = @StudentId"+
            $" ORDER BY HasPassed ASC,CourseName";

            var parameters = new DynamicParameters();
            parameters.Add("StudentId", id, DbType.Guid);

            using (sqlWrapper.CreateConnection())
            {
                var studentHistory = await sqlWrapper.QueryAsync<StudentEnrollmentModel>(query, parameters);

                return studentHistory.ToList();
            }
        }

        public async Task<IEnumerable<StudentEnrollmentModel>> GetStudentEnrollmentHistoryByStudentLastName(string studentLastName)
        {
            var query = $"SELECT" +
            $" [Course].[CourseId]" +
            $", [Course].[CourseName]" +
            $", [Course].[StartDate]" +
            $", [Course].[EndDate]" +
            $", [StudentEnrollmentLog].[Cancelled]" +
            $", [StudentEnrollmentLog].[CancellationReason]" +
            $", [StudentEnrollmentLog].[HasPassed]" +
            $", [Teacher].[TeacherEmail]" +
            $", [Student].[StudentPhone]" +
            $", [Student].[TotalPassCourses]" +
            $" FROM [StudentEnrollmentLog]" +
            $" INNER JOIN [Course] ON [StudentEnrollmentLog].[CourseId] = [Course].[CourseId]" +
            $" INNER JOIN [Teacher] ON [Course].[TeacherId] = [Teacher].[TeacherId]" +
            $" INNER JOIN [Student] ON [StudentEnrollmentLog].[StudentId] = [Student].[StudentId]" +
            $"  WHERE [Student].[StudentLastName] = @studentLastName"+
            $" ORDER BY HasPassed ASC,CourseName";
            var lastName = new { studentLastName = new DbString { Value = studentLastName, IsFixedLength = false, IsAnsi = true } };

            using (sqlWrapper.CreateConnection())
            {
                var studentHistory = await sqlWrapper.QueryAsync<StudentEnrollmentModel>(query, lastName);

                return studentHistory.ToList();
            }
        }

        public async Task<IEnumerable<StudentEnrollmentModel>> GetActiveStudentEnrollmentByStudentPhone(string studentPhone)
        {
            var query = $"SELECT" +
            $" [Course].[CourseId]" +
            $", [Course].[CourseName]" +
            $", [Course].[StartDate]" +
            $", [Course].[EndDate]" +
            $", [StudentEnrollmentLog].[Cancelled]" +
            $", [StudentEnrollmentLog].[CancellationReason]" +
            $", [StudentEnrollmentLog].[HasPassed]" +
            $", [Teacher].[TeacherEmail]" +
            $", [Student].[StudentEmail]" +
            $", [Student].[TotalPassCourses]" +
            $" FROM [Student]" +
            $" INNER JOIN [StudentEnrollmentLog] ON [StudentEnrollmentLog].[StudentId] = [Student].[StudentId]" +
            $" INNER JOIN [Course] ON [StudentEnrollmentLog].[CourseId] = [Course].[CourseId]" +
            $" INNER JOIN [Teacher] ON [Course].[TeacherId] = [Teacher].[TeacherId]" +
            $" WHERE [Student].[StudentPhone] = @studentPhone AND [Course].[CourseStatus] = 'Active' AND [StudentEnrollmentLog].[Cancelled] = 0 AND [StudentEnrollmentLog].[HasPassed] IS NULL ORDER BY StartDate ASC,CourseName";
            var studentPhoneNum = new { studentPhone = new DbString { Value = studentPhone, IsFixedLength =false, IsAnsi = true } };

            using (sqlWrapper.CreateConnection())
            {
                var studentHistory = await sqlWrapper.QueryAsync<StudentEnrollmentModel>(query, studentPhoneNum);

                return studentHistory;
            }
        }

        public async Task<IEnumerable<StudentModel>> GetStudentsInCourseByCourseId(Guid courseId) 
        {
            var query = $"SELECT * " +
            $"FROM [StudentEnrollmentLog] " +
            $"INNER JOIN [Student] ON [Student].[StudentId] = [StudentEnrollmentLog].[StudentId] " +
            $"WHERE CourseId = @CourseId "+
            $"ORDER BY StudentLastName";

            var parameters = new DynamicParameters();
            parameters.Add("CourseId", courseId, DbType.Guid);

            using (sqlWrapper.CreateConnection())
            {
                var course = await sqlWrapper.QueryAsync<StudentModel>(query, parameters);
                return course;
            }
        }
    }
}
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

        public async Task<IEnumerable<StudentEnrollmentModel>> GetStudentEnrollmentHistory(Guid StudentId, string StudentPhone = null, string StudentStatus = null, string Cancelled = null, string HasPassed = null)
        {
            var query = $"SELECT" +
            $"  C.[CourseId]" +
            $", C.[CourseName]" +
            $", C.[CourseStatus]" +
            $", C.[StartDate]" +
            $", C.[EndDate]" +
            $", SE.[Cancelled]" +
            $", SE.[CancellationReason]" +
            $", SE.[HasPassed]" +
            $", T.[TeacherEmail]" +
            $", S.[StudentEmail]" +
            $" FROM [StudentEnrollmentLog] as SE" +
            $" INNER JOIN [Course] as C ON SE.[CourseId] = C.[CourseId]" +
            $" INNER JOIN [Teacher] as T ON C.[TeacherId] = T.[TeacherId]" +
            $" INNER JOIN [Student] as S ON SE.[StudentId] = S.[StudentId]" +
            $" WHERE 1=1"; 

            var parameters = new DynamicParameters();
            if (StudentId != Guid.Empty)
            {
                query += " AND S.StudentId = @StudentId";
                parameters.Add("StudentId", StudentId, DbType.Guid);
            }
            if (!string.IsNullOrEmpty(StudentPhone))
            {
                query += " AND StudentPhone = @StudentPhone";
                parameters.Add("StudentPhone", StudentPhone, DbType.String);
            }
            if (!string.IsNullOrEmpty(StudentStatus))
            {
                query += " AND StudentStatus = @StudentStatus";
                parameters.Add("StudentStatus", StudentStatus, DbType.String);
            }
            if (!string.IsNullOrEmpty(Cancelled))
            {
                query += " AND Cancelled = @Cancelled";
                parameters.Add("Cancelled", Cancelled, DbType.String);
            }
            if (!string.IsNullOrEmpty(HasPassed))
            {
                query += " AND HasPassed = @HasPassed";
                parameters.Add("HasPassed", HasPassed, DbType.String);
            }

            using (sqlWrapper.CreateConnection())
            {
                var studentHistory = await sqlWrapper.QueryAsync<StudentEnrollmentModel>(query, parameters);

                return studentHistory.ToList();
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
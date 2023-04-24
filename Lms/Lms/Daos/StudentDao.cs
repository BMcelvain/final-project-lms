using Dapper;
using Lms.Models;
using Lms.Wrappers;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public class StudentDao : IStudentDao
    {
        private readonly ISqlWrapper sqlWrapper;

        public StudentDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        // POST a new student within the Student table. 
        public async Task CreateStudent(StudentModel newStudent)
        {
            var query = "INSERT Student (StudentId, StudentFirstName, StudentLastName,StudentPhone, StudentEmail, StudentStatus)" +
                         $"VALUES(@StudentId, @StudentFirstName, @StudentLastName, @StudentPhone, @StudentEmail, @StudentStatus)";

            var parameters = new DynamicParameters();
            parameters.Add("StudentId", Guid.NewGuid(), DbType.Guid);
            parameters.Add("StudentFirstName", newStudent.StudentFirstName, DbType.String);
            parameters.Add("StudentLastName", newStudent.StudentLastName, DbType.String);
            parameters.Add("StudentPhone ", newStudent.StudentPhone, DbType.String);
            parameters.Add("StudentEmail", newStudent.StudentEmail, DbType.String);
            parameters.Add("StudentStatus", newStudent.StudentStatus, DbType.String);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }

        // GET a single student (by Guid) within the Student table.
        public async Task<StudentModel> GetStudentById<StudentModel>(Guid id)
        {
            var query = $"SELECT * FROM Student WHERE StudentId = @StudentId";

            var parameters = new DynamicParameters();
            parameters.Add("StudentID", id, DbType.Guid);

            using (sqlWrapper.CreateConnection())
            {
                var student = await sqlWrapper.QueryFirstOrDefaultAsync<StudentModel>(query, parameters);
                return student;
            }
        }

        //Get Student Enrollment by Parameters
        public async Task<IEnumerable<StudentEnrollmentModel>> GetStudentEnrollmentHistory(Guid StudentId, string StudentPhone = null, string Cancelled = null, string HasPassed = null)
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
            if (!string.IsNullOrEmpty(Cancelled))
            {
                query += " AND Cancelled = @Cancelled";
                parameters.Add("Cancelled", Cancelled, DbType.String);
            }
            if (!string.IsNullOrEmpty(HasPassed))
            {
                query += " OR HasPassed = @HasPassed";
                parameters.Add("HasPassed", HasPassed, DbType.String);
            }

            using (sqlWrapper.CreateConnection())
            {
                var studentHistory = await sqlWrapper.QueryAsync<StudentEnrollmentModel>(query, parameters);

                return studentHistory.ToList();
            }
        }

        //Get Student Enrollment by CourseId
        public async Task<IEnumerable<StudentEnrollmentModel>> GetStudentsInCourseByCourseId(Guid courseId)
        {
            var query = $"SELECT * " +
            $"FROM [StudentEnrollmentLog] " +
            $"INNER JOIN [Student] ON [Student].[StudentId] = [StudentEnrollmentLog].[StudentId] " +
            $"WHERE CourseId = @CourseId " +
            $"ORDER BY StudentLastName";

            var parameters = new DynamicParameters();
            parameters.Add("CourseId", courseId, DbType.Guid);

            using (sqlWrapper.CreateConnection())
            {
                var course = await sqlWrapper.QueryAsync<StudentEnrollmentModel>(query, parameters);
                return course;
            }
        }
    

        // PATCH a student within the Student table. 
        public async Task PartiallyUpdateStudentById(StudentModel updateRequest)
        {
            var query = "UPDATE Student SET StudentFirstName=@StudentFirstName, StudentLastName=@StudentLastName, " +
                        $"StudentPhone=@StudentPhone, StudentEmail=@StudentEmail, StudentStatus=@StudentStatus " +
                        $"WHERE StudentId=@StudentId";

            var parameters = new DynamicParameters();   
            parameters.Add("StudentId", updateRequest.StudentId, DbType.Guid);
            parameters.Add("StudentFirstName", updateRequest.StudentFirstName, DbType.String);
            parameters.Add("StudentLastName", updateRequest.StudentLastName, DbType.String);
            parameters.Add("StudentPhone ", updateRequest.StudentPhone, DbType.String);
            parameters.Add("StudentEmail", updateRequest.StudentEmail, DbType.String);
            parameters.Add("StudentStatus", updateRequest.StudentStatus, DbType.String);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }

        // DELETE a single student (by Guid) within the Student table. 
        public async Task DeleteStudentById(Guid id)
        {
            var query = $"DELETE FROM Student WHERE StudentId = @StudentId";

            var parameters = new DynamicParameters();
            parameters.Add("StudentId", id, DbType.Guid);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }
    }
}
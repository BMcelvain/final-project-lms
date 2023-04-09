using Dapper;
using Lms.Models;
using Lms.Wrappers;
using System;
using System.Data;
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
            var query = "INSERT Student (StudentId, StudentFirstName, StudentLastName,StudentPhone, StudentEmail, StudentStatus, TotalPassCourses)" +
                         $"VALUES(@StudentId, @StudentFirstName, @StudentLastName, @StudentPhone, @StudentEmail, @StudentStatus, @TotalPassCourses)";

            var parameters = new DynamicParameters();
            parameters.Add("StudentId", Guid.NewGuid(), DbType.Guid);
            parameters.Add("StudentFirstName", newStudent.StudentFirstName, DbType.String);
            parameters.Add("StudentLastName", newStudent.StudentLastName, DbType.String);
            parameters.Add("StudentPhone ", newStudent.StudentPhone, DbType.String);
            parameters.Add("StudentEmail", newStudent.StudentEmail, DbType.String);
            parameters.Add("StudentStatus", newStudent.StudentStatus, DbType.String);
            parameters.Add("TotalPassCourses", newStudent.TotalPassCourses, DbType.Int32);

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

        // PATCH a student within the Student table. 
        public async Task PartiallyUpdateStudentById(StudentModel updateRequest)
        {
            var query = "UPDATE Student SET StudentFirstName=@StudentFirstName, StudentLastName=@StudentLastName, " +
                        $"StudentPhone=@StudentPhone, StudentEmail=@StudentEmail, StudentStatus=@StudentStatus, TotalPassCourses=@TotalPassCourses " +
                        $"WHERE StudentId=@StudentId";

            var parameters = new DynamicParameters();   
            parameters.Add("StudentId", updateRequest.StudentId, DbType.Guid);
            parameters.Add("StudentFirstName", updateRequest.StudentFirstName, DbType.String);
            parameters.Add("StudentLastName", updateRequest.StudentLastName, DbType.String);
            parameters.Add("StudentPhone ", updateRequest.StudentPhone, DbType.String);
            parameters.Add("StudentEmail", updateRequest.StudentEmail, DbType.String);
            parameters.Add("StudentStatus", updateRequest.StudentStatus, DbType.String);
            parameters.Add("TotalPassCourses", updateRequest.TotalPassCourses, DbType.Int32);

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
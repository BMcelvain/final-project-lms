using Dapper;
using Lms.Models;
using Lms.Wrappers;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public class StudentDao : IStudentDao
    {
        private ISqlWrapper sqlWrapper;

        public StudentDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        // POST a new student within the Student table. 
        public async Task CreateStudent(StudentModel newStudent)
        {
            var query = "INSERT Student (StudentFirstName, StudentLastName,StudentPhone, StudentEmail, StudentStatus, TotalPassCourses)" +
                         $"VALUES(@StudentFirstName, @StudentLastName, @StudentPhone, @StudentEmail, @StudentStatus, @TotalPassCourses)";

            var parameters = new DynamicParameters();

            parameters.Add("StudentFirstName", newStudent.StudentFirstName, DbType.String);
            parameters.Add("StudentLastName", newStudent.StudentLastName, DbType.String);
            parameters.Add("StudentPhone ", newStudent.StudentPhone, DbType.String);
            parameters.Add("StudentEmail", newStudent.StudentEmail, DbType.String);
            parameters.Add("StudentStatus", newStudent.StudentStatus, DbType.String);
            parameters.Add("TotalPassCourses", newStudent.TotalPassCourses, DbType.Int32);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsyncWithParameters(query, parameters);
            }
        }

        // GET all students within the Student table. 
        public async Task<IEnumerable<StudentModel>> GetStudents()
        {
            var query = "SELECT * FROM Student";
            
            using (sqlWrapper.CreateConnection())
            {
                var students = await sqlWrapper.QueryAsync<StudentModel>(query);

                return students.ToList();
            }  
        }

        // GET a single student (by Id) within the Student table.
        public async Task<StudentModel> GetStudentById(int id)
        {
            var query = $"SELECT * FROM Student WHERE StudentId = {id}";

            using (sqlWrapper.CreateConnection())
            {
                var student = await sqlWrapper.QueryFirstOrDefaultAsync<StudentModel>(query);
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
      
            parameters.Add("StudentId", updateRequest.StudentId, DbType.Int32);
            parameters.Add("StudentFirstName", updateRequest.StudentFirstName, DbType.String);
            parameters.Add("StudentLastName", updateRequest.StudentLastName, DbType.String);
            parameters.Add("StudentPhone ", updateRequest.StudentPhone, DbType.String);
            parameters.Add("StudentEmail", updateRequest.StudentEmail, DbType.String);
            parameters.Add("StudentStatus", updateRequest.StudentStatus, DbType.String);
            parameters.Add("TotalPassCourses", updateRequest.TotalPassCourses, DbType.Int32);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsyncWithParameters(query, parameters);
            }
        }

        // DELETE a single student (by Id) within the Student table. 
        public async Task DeleteStudentById(int id)
        {
            var query = $"DELETE FROM Student WHERE StudentId = {id}";

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query);
            }
        }
    }

}

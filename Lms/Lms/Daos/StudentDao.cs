using Dapper;
using Lms.Models;
using Lms.Wrappers;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public class StudentDao : IStudentDao
    {
        private readonly DapperContext _context;

        public StudentDao(DapperContext context)
        {
            _context = context;
        }

        // POST a new student within the Student table. 
        public async Task CreateStudent(StudentModel newStudent)
        {
            var query = "INSERT Student (StudentId, StudentFirstName, StudentLastName,StudentPhone, StudentEmail, StudentStatus, TotalPassCourses)" +
                         $"VALUES(@StudentId, @StudentFirstName, @StudentLastName, @StudentPhone, @StudentEmail, @StudentStatus, @TotalPassCourses)";

            var parameters = new DynamicParameters();
            parameters.Add("StudentId", newStudent.StudentId, DbType.Int32);
            parameters.Add("StudentFirstName", newStudent.StudentFirstName, DbType.String);
            parameters.Add("StudentLastName", newStudent.StudentLastName, DbType.String);
            parameters.Add("StudentPhone ", newStudent.StudentLastName, DbType.String);
            parameters.Add("StudentEmail", newStudent.StudentPhone, DbType.String);
            parameters.Add("StudenStatus", newStudent.StudentEmail, DbType.String);
            parameters.Add("TotalPassCourses", newStudent.StudentStatus, DbType.String);
            parameters.Add("TotalPassCourses", newStudent.TotalPassCourses, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        // GET all students within the Student table. 
        public async Task<IEnumerable<StudentModel>> GetStudents()
        {
            var query = "SELECT * FROM Student";
            using (var connection = _context.CreateConnection())
            {
                var students = await connection.QueryAsync<StudentModel>(query);

                return students.ToList();
            }
        }

        // GET a single student (by Id) within the Student table.
        public async Task<StudentModel> GetStudentById(int id)
        {
            var query = $"SELECT * FROM Student WHERE StudentId = {id}";

            using (var connection = _context.CreateConnection())
            {
                var student = await connection.QueryFirstOrDefaultAsync<StudentModel>(query);
                return student;
            }
        }

        // PATCH a student within the Student table. 
        public async Task PartiallyUpdateStudentById(StudentModel updateRequest)
        {
            var query = "UPDATE Student SET StudentId=@StudentId, StudentFirstName=@StudentFirstName, StudentLastName=@StudentLastName, " +
                        $"StudentPhone=@StudentPhone, StudentEmail=@StudentEmail, StudentStatus=@StudentStatus, TotalPassCourses=@TotalPassCourses" +
                        $"WHERE StudentId=@StudentId";

            var parameters = new DynamicParameters();
            parameters.Add("StudentId", updateRequest.StudentId, DbType.Int32);
            parameters.Add("StudentFirstName", updateRequest.StudentFirstName, DbType.String);
            parameters.Add("StudentLastName", updateRequest.StudentLastName, DbType.String);
            parameters.Add("StudentPhone ", updateRequest.StudentLastName, DbType.String);
            parameters.Add("StudentEmail", updateRequest.StudentPhone, DbType.String);
            parameters.Add("StudenStatus", updateRequest.StudentEmail, DbType.String);
            parameters.Add("TotalPassCourses", updateRequest.StudentStatus, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        // DELETE a single student (by Id) within the Student table. 
        public async Task DeleteStudentById(int id)
        {
            var query = $"DELETE FROM Student WHERE StudentId = {id}";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }
    }

}

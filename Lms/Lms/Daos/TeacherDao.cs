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
    public class TeacherDao : ITeacherDao
    {
        private readonly DapperContext _context;
        private readonly ISqlWrapper sqlWrapper;

        public TeacherDao(DapperContext context)
        {
            _context = context;
        }

        public TeacherDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        // POST a new teacher within the Teacher table. 
        public async Task CreateTeacher(TeacherModel newTeacher)
        {
            var query = "INSERT Teacher (TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail,TeacherStatus)" +
                         $"VALUES(@TeacherFirstName, @TeacherLastName, @TeacherPhone, @TeacherEmail, @TeacherStatus)";

            var parameters = new DynamicParameters();
            parameters.Add("TeacherId", newTeacher.TeacherId, DbType.Int32);
            parameters.Add("TeacherFirstName", newTeacher.TeacherFirstName, DbType.String);
            parameters.Add("TeacherLastName", newTeacher.TeacherLastName, DbType.String);
            parameters.Add("TeacherPhone", newTeacher.TeacherPhone, DbType.String);
            parameters.Add("TeacherEmail", newTeacher?.TeacherEmail, DbType.String);
            parameters.Add("TeacherStatus", newTeacher.TeacherStatus, DbType.String);


            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        // GET all teachers within the Teacher table. 
        public async Task<IEnumerable<TeacherModel>> GetTeachers()
        {
            var query = "SELECT * FROM Teacher";
            //replaces the connection async
            var teachers = await sqlWrapper.Query<TeacherModel>(query);
            return teachers.ToList();
            
        }

        // GET a single teacher (by Id) within the Teacher table.
        public async Task<TeacherModel> GetTeacherById(int id)
        {
            var query = $"SELECT * FROM Teacher WHERE TeacherId = {id}";

            using (var connection = _context.CreateConnection())
            {
                var teacher = await connection.QueryFirstOrDefaultAsync<TeacherModel>(query);
                return teacher;
            }
        }

        // PATCH a teacher within the Teacher table. 
        public async Task PartiallyUpdateTeacherById(TeacherModel updateRequest)
        {
            var query = "UPDATE Teacher SET TeacherFirstName=@TeacherFirstName, TeacherLastName=@TeacherLastName, " +
                        $"TeacherPhone=@TeacherPhone, TeacherEmail=@TeacherEmail, TeacherStatus=@TeacherStatus WHERE TeacherId=@TeacherId";

            var parameters = new DynamicParameters();
            parameters.Add("TeacherId", updateRequest.TeacherId, DbType.Int32);
            parameters.Add("TeacherFirstName", updateRequest.TeacherFirstName, DbType.String);
            parameters.Add("TeacherLastName", updateRequest.TeacherLastName, DbType.String);
            parameters.Add("TeacherPhone", updateRequest.TeacherPhone, DbType.String);
            parameters.Add("TeacherEmail", updateRequest?.TeacherEmail, DbType.String);
            parameters.Add("TeacherStatus", updateRequest.TeacherStatus, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        // DELETE a single teacher (by Id) within the Teacher table. 
        public async Task DeleteTeacherById(int id)
        {
            var query = $"DELETE FROM Teacher WHERE TeacherId = {id}";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }
    }
}
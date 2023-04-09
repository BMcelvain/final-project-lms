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
    public class TeacherDao : ITeacherDao
    {
        private readonly ISqlWrapper sqlWrapper;

        public TeacherDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        // POST a new teacher within the Teacher table. 
        public async Task CreateTeacher(TeacherModel newTeacher)
        {
            var query = "INSERT Teacher(TeacherId, TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail,TeacherStatus)" +
                         $"VALUES(@TeacherId, @TeacherFirstName, @TeacherLastName, @TeacherPhone, @TeacherEmail, @TeacherStatus)";

            var parameters = new DynamicParameters();
            parameters.Add("TeacherId", Guid.NewGuid(), DbType.Guid);
            parameters.Add("TeacherFirstName", newTeacher.TeacherFirstName, DbType.String);
            parameters.Add("TeacherLastName", newTeacher.TeacherLastName, DbType.String);
            parameters.Add("TeacherPhone", newTeacher.TeacherPhone, DbType.String);
            parameters.Add("TeacherEmail", newTeacher?.TeacherEmail, DbType.String);
            parameters.Add("TeacherStatus", newTeacher.TeacherStatus, DbType.String);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }

        // GET a single teacher (by Id) within the Teacher table.
        public async Task<TeacherModel> GetTeacherById<TeacherModel>(Guid id)
        {
            var query = $"SELECT * FROM Teacher WHERE TeacherId = @TeacherId";

            var parameters = new DynamicParameters();
            parameters.Add("TeacherId", id, DbType.Guid);

            using (sqlWrapper.CreateConnection())
            {
                var teacher = await sqlWrapper.QueryFirstOrDefaultAsync<TeacherModel>(query, parameters);
                return teacher;
            }
        }

        //Get teachers by status within the Teacher table
        public async Task<IEnumerable<TeacherModel>> GetTeacherByStatus(string status)
        {
            var query = $"SELECT * FROM Teacher WHERE TeacherStatus = @teacherStatus ORDER BY TeacherLastName ASC";
            var teacherStatus = new { teacherStatus = new DbString { Value = status, IsFixedLength = false, IsAnsi = true } };

            using (sqlWrapper.CreateConnection())
            {
                var teachers = await sqlWrapper.QueryAsync<TeacherModel>(query, teacherStatus);

                return teachers.ToList();
            }
        }

        // PATCH a teacher within the Teacher table. 
        public async Task PartiallyUpdateTeacherById(TeacherModel updateRequest)
        {
            var query = "UPDATE Teacher SET TeacherFirstName=@TeacherFirstName, TeacherLastName=@TeacherLastName, " +
                        $"TeacherPhone=@TeacherPhone, TeacherEmail=@TeacherEmail, TeacherStatus=@TeacherStatus WHERE TeacherId=@TeacherId";

            var parameters = new DynamicParameters();
            parameters.Add("TeacherId", updateRequest.TeacherId, DbType.Guid);
            parameters.Add("TeacherFirstName", updateRequest.TeacherFirstName, DbType.String);
            parameters.Add("TeacherLastName", updateRequest.TeacherLastName, DbType.String);
            parameters.Add("TeacherPhone", updateRequest.TeacherPhone, DbType.String);
            parameters.Add("TeacherEmail", updateRequest?.TeacherEmail, DbType.String);
            parameters.Add("TeacherStatus", updateRequest.TeacherStatus, DbType.String);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }

        // DELETE a single teacher (by Id) within the Teacher table. 
        public async Task DeleteTeacherById(Guid id)
        {
            var query = $"DELETE FROM Teacher WHERE TeacherId = @TeacherId";
            
            var parameters = new DynamicParameters();
            parameters.Add("TeacherId", id, DbType.Guid);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }
    }
}
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

        // GET a teacher by filters
        public async Task<IEnumerable<TeacherModel>> GetTeacher(Guid TeacherId, string TeacherLastName = null, string TeacherPhone = null, string TeacherStatus = null)
        {
            var query = $"SELECT * FROM Teacher WHERE 1=1";


            var parameters = new DynamicParameters();
            if (TeacherId != Guid.Empty)
            {
                query += " AND TeacherId = @TeacherId";
                parameters.Add("TeacherId", TeacherId, DbType.Guid);
            }
            if (!string.IsNullOrEmpty(TeacherLastName))
            {
                query += " AND TeacherLastName = @TeacherLastName";
                parameters.Add("TeacherLastName", TeacherLastName, DbType.String);
            }
            if (!string.IsNullOrEmpty(TeacherPhone))
            {
                query += " AND TeacherPhone = @TeacherPhone";
                parameters.Add("TeacherPhone", TeacherPhone, DbType.String);
            }
            if (!string.IsNullOrEmpty(TeacherStatus))
            {
                query += " AND TeacherStatus = @TeacherStatus";
                parameters.Add("TeacherStatus", TeacherStatus, DbType.String);
            }

            using (sqlWrapper.CreateConnection())
            {
                var teacher = await sqlWrapper.QueryAsync<TeacherModel>(query, parameters);
                return teacher.ToList();
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
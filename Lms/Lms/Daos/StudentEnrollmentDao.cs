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
    public class StudentEnrollmentDao : IStudentEnrollmentDao
    {
        private readonly DapperContext _context;

        public StudentEnrollmentDao(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentEnrollmentModel>> GetStudentEnrollmentHistory(int id)
        {
            var query = $"SELECT * FROM StudentEnrollmentLog WHERE StudentId = {id}";
            using (var connection = _context.CreateConnection())
            {
                var studentHistory = await connection.QueryAsync<StudentEnrollmentModel>(query);

                return studentHistory.ToList();
            }
        }
    }
}
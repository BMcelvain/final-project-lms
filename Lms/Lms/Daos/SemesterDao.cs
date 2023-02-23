using Lms.Wrappers;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Lms.Models;

namespace Lms.Daos
{
    public class SemesterDao : ISemesterDao
    {
        private readonly ISqlWrapper sqlWrapper;

        public SemesterDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        // POST a new semester within the Semester table. 
        public async Task CreateSemester(SemesterModel newSemester)
        {
            var query = "INSERT Semester (Semester, Year) VALUES(@Semester, @Year)";

            var parameters = new DynamicParameters();
            parameters.Add("Semester", newSemester.Semester, DbType.String);
            parameters.Add("Year", newSemester.Year, DbType.String);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }

        // GET all semesters within the Semester table. 
        public async Task<IEnumerable<SemesterModel>> GetSemesters()
        {
            var query = "SELECT * FROM Semester";

            using (sqlWrapper.CreateConnection())
            {
                var semesters = await sqlWrapper.QueryAsync<SemesterModel>(query);
                return semesters.ToList();
            }
        }

        // GET a single semester (by Id) within the Semester table.
        public async Task<SemesterModel> GetSemesterById(int id)
        {
            var query = $"SELECT * FROM Semester WHERE SemesterId = {id}";

            using (sqlWrapper.CreateConnection())
            {
                var semester = await sqlWrapper.QueryFirstOrDefaultAsync<SemesterModel>(query);
                return semester;
            }
        }

        // DELETE a single semester (by Id) within the Semester table. 
        public async Task DeleteSemesterById(int id)
        {
            var query = $"DELETE FROM Semester WHERE SemesterId = {id}";

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query);
            }
        }
    }
}
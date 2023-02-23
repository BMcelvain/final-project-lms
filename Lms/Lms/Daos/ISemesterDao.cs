using Lms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public interface ISemesterDao
    {
        Task CreateSemester(SemesterModel newSemester);
        Task<IEnumerable<SemesterModel>> GetSemesters();
        Task<SemesterModel> GetSemesterById(int id);
        Task DeleteSemesterById(int id);
    }
}

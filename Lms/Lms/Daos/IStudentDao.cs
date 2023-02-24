using Lms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public interface IStudentDao
    {
        Task CreateStudent(StudentModel newStudent);
        Task<IEnumerable<StudentModel>> GetStudents();
        Task<StudentModel> GetStudentById(int id);
        Task PartiallyUpdateStudentById(StudentModel updateRequest);
        Task DeleteStudentById(int id);      
    }
}

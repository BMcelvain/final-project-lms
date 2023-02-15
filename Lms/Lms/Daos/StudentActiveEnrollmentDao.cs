using Lms.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Lms.Models;

namespace Lms.Daos
{
    public class StudentActiveEnrollmentDao : IStudentActiveEnrollmentDao
    {
        private readonly ISqlWrapper sqlWrapper;

        public StudentActiveEnrollmentDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        public async Task<IEnumerable<StudentActiveEnrollmentModel>> GetActiveStudentEnrollmentByStudentLastName(string studentLastName)
        {
            var query = $"SELECT [LearningManagementSystem].[dbo].[Student].[StudentLastName]" +
            $",  [LearningManagementSystem].[dbo].[Student].[StudentFirstName]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentPhone]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentEmail]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseName]" +
            $", [LearningManagementSystem].[dbo].[Course].[StartDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[EndDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseStatus]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[SemesterId]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[EnrollmentDate]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherFirstName]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherLastName]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherPhone]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherEmail]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherStatus]" +
            $" FROM [LearningManagementSystem].[dbo].[Student]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[StudentEnrollmentLog]  ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Course] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CourseId] = [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Teacher] ON [LearningManagementSystem].[dbo].[Course].[TeacherId] = [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $" WHERE [LearningManagementSystem].[dbo].[Student].[StudentLastName] = '{studentLastName}' AND [LearningManagementSystem].[dbo].[Course].[CourseStatus] = 'Active'";

            using (sqlWrapper.CreateConnection())
            {
                var studentHistory = await sqlWrapper.QueryAsync<StudentActiveEnrollmentModel>(query);

                return studentHistory;
            }
        }

        public async Task<IEnumerable<StudentActiveEnrollmentModel>> GetActiveStudentEnrollmentByStudentPhone(string studentPhone)
        {
            var query = $"SELECT [LearningManagementSystem].[dbo].[Student].[StudentLastName]" +
            $",  [LearningManagementSystem].[dbo].[Student].[StudentFirstName]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentPhone]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentEmail]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseName]" +
            $", [LearningManagementSystem].[dbo].[Course].[StartDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[EndDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseStatus]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[SemesterId]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[EnrollmentDate]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherFirstName]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherLastName]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherPhone]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherEmail]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherStatus]" +
            $" FROM [LearningManagementSystem].[dbo].[Student]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[StudentEnrollmentLog]  ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Course] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CourseId] = [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Teacher] ON [LearningManagementSystem].[dbo].[Course].[TeacherId] = [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $" WHERE [LearningManagementSystem].[dbo].[Student].[StudentPhone] = '{studentPhone}' AND [LearningManagementSystem].[dbo].[Course].[CourseStatus] = 'Active'";

            using (sqlWrapper.CreateConnection())
            {
                var studentHistory = await sqlWrapper.QueryAsync<StudentActiveEnrollmentModel>(query);

                return studentHistory;
            }
        }

    }
}



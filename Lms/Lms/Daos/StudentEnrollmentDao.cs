using Lms.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Lms.Models;

namespace Lms.Daos
{
    public class StudentEnrollmentDao : IStudentEnrollmentDao
    {
        private readonly ISqlWrapper sqlWrapper;

        public StudentEnrollmentDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        public async Task<IEnumerable<StudentEnrollmentModel>> GetStudentEnrollmentHistoryById(int id)
        {
            var query = $"SELECT [Id]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseName]" +
            $", [LearningManagementSystem].[dbo].[Course].[StartDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[EndDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseStatus]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[SemesterId]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[EnrollmentDate]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[Cancelled]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CancellationReason]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[HasPassed]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherFirstName]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherLastName]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherPhone]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherEmail]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherStatus]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentFirstName]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentLastName]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentPhone]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentEmail]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentStatus]" +
            $", [LearningManagementSystem].[dbo].[Student].[TotalPassCourses]" +
            $" FROM[LearningManagementSystem].[dbo].[StudentEnrollmentLog]" +
            $" INNER JOIN[LearningManagementSystem].[dbo].[Course] ON[LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CourseId] = [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $" INNER JOIN[LearningManagementSystem].[dbo].[Teacher] ON[LearningManagementSystem].[dbo].[Course].[TeacherId] = [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $" INNER JOIN[LearningManagementSystem].[dbo].[Student] ON[LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $" WHERE [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = {id}";

            using (sqlWrapper.CreateConnection())
            {
                var studentHistory = await sqlWrapper.QueryAsync<StudentEnrollmentModel>(query);

                return studentHistory.ToList();
            }
        }

        public async Task<IEnumerable<StudentEnrollmentModel>> GetStudentEnrollmentHistoryByStudentFirstName(string studentFirstName)
        {
            var query = $"SELECT [LearningManagementSystem].[dbo].[Student].[StudentFirstName]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentLastName]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentPhone]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentEmail]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentStatus]" +
            $", [LearningManagementSystem].[dbo].[Student].[TotalPassCourses]" + 
            $", [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseName]" +
            $", [LearningManagementSystem].[dbo].[Course].[StartDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[EndDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseStatus]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[SemesterId]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[EnrollmentDate]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[Cancelled]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CancellationReason]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[HasPassed]" +
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
            $" WHERE [LearningManagementSystem].[dbo].[Student].[StudentFirstName] = '{studentFirstName}'";

            using (sqlWrapper.CreateConnection())
            {
                var studentHistory = await sqlWrapper.QueryAsync<StudentEnrollmentModel>(query);

                return studentHistory;
            }
        }
    }
}

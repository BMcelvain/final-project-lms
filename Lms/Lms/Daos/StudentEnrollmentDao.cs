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
            var query = $"SELECT" +
            $" [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseName]" +
            $", [LearningManagementSystem].[dbo].[Course].[StartDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[EndDate]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[Cancelled]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CancellationReason]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[HasPassed]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherEmail]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentPhone]" +
            $", [LearningManagementSystem].[dbo].[Student].[TotalPassCourses]" +
            $" FROM [LearningManagementSystem].[dbo].[StudentEnrollmentLog]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Course] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CourseId] = [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Teacher] ON [LearningManagementSystem].[dbo].[Course].[TeacherId] = [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Student] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $" WHERE [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = {id}";

            using (sqlWrapper.CreateConnection())
            {
                var studentHistory = await sqlWrapper.QueryAsync<StudentEnrollmentModel>(query);

                return studentHistory.ToList();
            }
        }

        public async Task<IEnumerable<StudentEnrollmentModel>> GetStudentEnrollmentHistoryByStudentLastName(string studentLastName)
        {
            var query = $"SELECT" +
            $" [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $", [LearningManagementSystem].[dbo].[Course].[CourseName]" +
            $", [LearningManagementSystem].[dbo].[Course].[StartDate]" +
            $", [LearningManagementSystem].[dbo].[Course].[EndDate]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[Cancelled]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CancellationReason]" +
            $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[HasPassed]" +
            $", [LearningManagementSystem].[dbo].[Teacher].[TeacherEmail]" +
            $", [LearningManagementSystem].[dbo].[Student].[StudentPhone]" +
            $", [LearningManagementSystem].[dbo].[Student].[TotalPassCourses]" +
            $" FROM [LearningManagementSystem].[dbo].[StudentEnrollmentLog]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Course] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CourseId] = [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Teacher] ON [LearningManagementSystem].[dbo].[Course].[TeacherId] = [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Student] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $"  WHERE [LearningManagementSystem].[dbo].[Student].[StudentLastName] = '{studentLastName}'";

            using (sqlWrapper.CreateConnection())
            {
                var studentHistory = await sqlWrapper.QueryAsync<StudentEnrollmentModel>(query);

                return studentHistory.ToList();
            }
        }

        public async Task<IEnumerable<StudentEnrollmentModel>> GetActiveStudentEnrollmentByStudentPhone(string studentPhone)
        {
            var query = $"SELECT" +
           $" [LearningManagementSystem].[dbo].[Course].[CourseId]" +
           $", [LearningManagementSystem].[dbo].[Course].[CourseName]" +
           $", [LearningManagementSystem].[dbo].[Course].[StartDate]" +
           $", [LearningManagementSystem].[dbo].[Course].[EndDate]" +
           $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[Cancelled]" +
           $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CancellationReason]" +
           $", [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[HasPassed]" +
           $", [LearningManagementSystem].[dbo].[Teacher].[TeacherEmail]" +
           $", [LearningManagementSystem].[dbo].[Student].[StudentEmail]" +
           $", [LearningManagementSystem].[dbo].[Student].[TotalPassCourses]" +
           $" FROM [LearningManagementSystem].[dbo].[Student]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[StudentEnrollmentLog] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = [LearningManagementSystem].[dbo].[Student].[StudentId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Course] ON [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[CourseId] = [LearningManagementSystem].[dbo].[Course].[CourseId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Teacher] ON [LearningManagementSystem].[dbo].[Course].[TeacherId] = [LearningManagementSystem].[dbo].[Teacher].[TeacherId]" +
            $" INNER JOIN [LearningManagementSystem].[dbo].[Semester] ON [LearningManagementSystem].[dbo].[Course].[SemesterId] = [LearningManagementSystem].[dbo].[Semester].[SemesterId]" +
            $" WHERE [LearningManagementSystem].[dbo].[Student].[StudentPhone] = 'Test' AND [LearningManagementSystem].[dbo].[Course].[CourseStatus] = 'Active'";

            using (sqlWrapper.CreateConnection())
            {
                var studentHistory = await sqlWrapper.QueryAsync<StudentEnrollmentModel>(query);

                return studentHistory;
            }

        }

        public async Task AddStudentToCourse(StudentEnrollmentModel addStudentToCourse)
        {
            var query = "INSERT StudentEnrollmentLog (CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed)" +
                        $"VALUES (@CourseId, @SemesterId, @StudentId, @EnrollmentDate, @Cancelled, @CancellationReason, @HasPassed)";

            var parameters = new DynamicParameters();
            parameters.Add("CourseId", addStudentToCourse.CourseId, DbType.Int32);
            parameters.Add("SemesterId", addStudentToCourse.SemesterId, DbType.Int32);
            parameters.Add("StudentId", addStudentToCourse.StudentId, DbType.Int32);
            parameters.Add("EnrollmentDate", addStudentToCourse.EnrollmentDate, DbType.String);
            parameters.Add("Cancelled", addStudentToCourse.Cancelled, DbType.Boolean);
            parameters.Add("CancellationReason", addStudentToCourse?.CancellationReason, DbType.String);
            parameters.Add("HasPassed", addStudentToCourse.HasPassed, DbType.Boolean);


            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }

        // GET specific CourseId within the Enrollment Log.
        public async Task<StudentEnrollmentModel> GetCourseByCourseId(int courseId) //update to Course Name
        {
            var query = $"SELECT * FROM StudentEnrollmentLog WHERE CourseId = {courseId}";

            using (sqlWrapper.CreateConnection())
            {
                var course = await sqlWrapper.QueryFirstOrDefaultAsync<StudentEnrollmentModel>(query);
                return course;
            }
        }

        // PATCH a student within the Enrollment Log.
        public async Task PartiallyUpdateStudentInCourseByCourseStudentId(StudentEnrollmentModel updateRequest)
        {
            var query = "UPDATE StudentEnrollmentLog SET CourseId=@CourseId, SemesterId=@SemesterId, StudentId=@StudentId, " +
                        $"EnrollmentDate=@EnrollmentDate, Cancelled=@Cancelled, CancellationReason=@CancellationReason, HasPassed=@HasPassed WHERE StudentId=@StudentId AND CourseId=@CourseId";

            var parameters = new DynamicParameters();
            parameters.Add("CourseId", updateRequest.CourseId, DbType.Int32);
            parameters.Add("SemesterId", updateRequest.SemesterId, DbType.Int32);
            parameters.Add("StudentId", updateRequest.StudentId, DbType.Int32);
            parameters.Add("EnrollmentDate", updateRequest.EnrollmentDate, DbType.String);
            parameters.Add("Cancelled", updateRequest.Cancelled, DbType.Boolean);
            parameters.Add("CancellationReason", updateRequest?.CancellationReason, DbType.String);
            parameters.Add("HasPassed", updateRequest.HasPassed, DbType.Boolean);

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query, parameters);
            }
        }

        // DELETE a single course (by Id) within the Student Enrollment Log 
        public async Task DeleteStudentInCourseByStudentCourseId(int studentId, int courseId)
        {
            var query = $"DELETE FROM StudentEnrollmentLog WHERE StudentId = {studentId} AND CourseId = {courseId}"; //update to First Name & Last Name 

            using (sqlWrapper.CreateConnection())
            {
                await sqlWrapper.ExecuteAsync(query);
            }
        }
    }

}



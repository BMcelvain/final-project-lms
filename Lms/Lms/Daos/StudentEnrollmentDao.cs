﻿using Lms.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Lms.Models;
using System.Data;
using System;

namespace Lms.Daos
{
    public class StudentEnrollmentDao : IStudentEnrollmentDao
    {
        private readonly ISqlWrapper sqlWrapper;

        public StudentEnrollmentDao(ISqlWrapper sqlWrapper)
        {
            this.sqlWrapper = sqlWrapper;
        }

        public async Task<IEnumerable<StudentEnrollmentModel>> GetStudentEnrollmentHistoryById(Guid id)
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
            $" WHERE [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] = @StudentId"+
            $" ORDER BY HasPassed ASC,CourseName";

            var parameters = new DynamicParameters();
            parameters.Add("StudentId", id, DbType.Guid);

            using (sqlWrapper.CreateConnection())
            {
                var studentHistory = await sqlWrapper.QueryAsync<StudentEnrollmentModel>(query, parameters);

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
            $"  WHERE [LearningManagementSystem].[dbo].[Student].[StudentLastName] = @studentLastName"+
            $" ORDER BY HasPassed ASC,CourseName";
            var lastName = new { studentLastName = new DbString { Value = studentLastName, IsFixedLength = false, IsAnsi = true } };

            using (sqlWrapper.CreateConnection())
            {
                var studentHistory = await sqlWrapper.QueryAsync<StudentEnrollmentModel>(query, lastName);

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
            $" WHERE [LearningManagementSystem].[dbo].[Student].[StudentPhone] = @studentPhone AND [LearningManagementSystem].[dbo].[Course].[CourseStatus] = 'Active'"+
            $" ORDER BY StartDate ASC,CourseName";
            var studentPhoneNum = new { studentPhone = new DbString { Value = studentPhone, IsFixedLength =false, IsAnsi = true } };

            using (sqlWrapper.CreateConnection())
            {
                var studentHistory = await sqlWrapper.QueryAsync<StudentEnrollmentModel>(query, studentPhoneNum);

                return studentHistory;
            }
        }

        public async Task<IEnumerable<StudentModel>> GetStudentsInCourseByCourseId(Guid courseId) 
        {
            var query = $"SELECT * " +
            $"FROM [LearningManagementSystem].[dbo].[StudentEnrollmentLog] " +
            $"INNER JOIN [LearningManagementSystem].[dbo].[Student] ON [LearningManagementSystem].[dbo].[Student].[StudentId] = [LearningManagementSystem].[dbo].[StudentEnrollmentLog].[StudentId] " +
            $"WHERE CourseId = @CourseId "+
            $"ORDER BY StudentLastName";

            var parameters = new DynamicParameters();
            parameters.Add("CourseId", courseId, DbType.Guid);

            using (sqlWrapper.CreateConnection())
            {
                var course = await sqlWrapper.QueryAsync<StudentModel>(query, parameters);
                return course;
            }
        }
    }
}
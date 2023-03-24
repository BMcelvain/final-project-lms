CREATE TABLE Teacher (
TeacherId int IDENTITY(1,1) PRIMARY KEY,
TeacherFirstName varchar(255) not NULL,
TeacherLastName varchar(255) not NULL,
TeacherPhone varchar(255) not NULL,
TeacherEmail varchar(255) not NULL,
TeacherStatus varchar(255) not Null
);

CREATE TABLE Course (
CourseId int IDENTITY(1,1) PRIMARY KEY,
TeacherId int FOREIGN KEY REFERENCES Teacher(TeacherId) ON UPDATE CASCADE,
CourseName varchar(255) not NULL,
StartDate date,
EndDate date,
CourseStatus varchar(255),
);

CREATE TABLE Student (
StudentId int IDENTITY(1,1) PRIMARY KEY,
StudentFirstName varchar(255) not NULL,
StudentLastName varchar(255) not NULL,
StudentPhone varchar(255) not NULL,
StudentEmail varchar(255) not NULL,
StudentStatus varchar(255), 
TotalPassCourses int, 
);

CREATE TABLE StudentEnrollmentLog (
Id int IDENTITY(1,1) PRIMARY KEY,
CourseId int FOREIGN KEY REFERENCES Course(CourseId) ON DELETE CASCADE ON UPDATE CASCADE,
StudentId int FOREIGN KEY REFERENCES Student(StudentId) ON DELETE CASCADE ON UPDATE CASCADE,
EnrollmentDate date,
Cancelled bit,
CancellationReason varchar(255),
HasPassed bit
);

SET IDENTITY_INSERT Teacher ON;
insert into Teacher (TeacherId, TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail, TeacherStatus) values (1, 'Roseline', 'Blaver', '113-393-3844', 'rblaver0@smugmug.com', 'Active');
insert into Teacher (TeacherId, TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail, TeacherStatus) values (2, 'Lenette', 'Van der Linde', '516-350-4479', 'lvanderlinde1@cafepress.com', 'Active');
insert into Teacher (TeacherId, TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail, TeacherStatus) values (3, 'Reeta', 'Laydel', '631-883-5112', 'rlaydel2@elpais.com', 'Active');
insert into Teacher (TeacherId, TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail, TeacherStatus) values (4, 'Cirilo', 'Rossant', '221-471-3470', 'crossant3@live.com', 'Inactive');
insert into Teacher (TeacherId, TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail, TeacherStatus) values (5, 'Whitman', 'Ridsdell', '317-376-5914', 'wridsdell4@nymag.com', 'Active');
SET IDENTITY_INSERT Teacher OFF;

SET IDENTITY_INSERT Course ON
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values (1, 4, 'All Group', '7/1/2021', '9/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values (2, 2, 'Passwords 101', '7/1/2021', '9/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values (3, 5, 'Safety', '7/1/2021', '9/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values (4, 3, 'CE Training', '7/1/2021', '9/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values (5, 1, 'Loans R Us', '7/1/2021', '9/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values (6, 2, 'Book Club', '10/1/2021', '12/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values (7, 3, 'Health & Wellbeing', '10/1/2021', '12/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values (8, 1, 'Tech for dummies', '10/1/2021', '12/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values (9, 5, 'Where is Waldo?', '10/1/2021', '12/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values (10, 4, 'Morning Coffee', '10/1/2021', '12/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values (11, 2, 'CodeVU', '1/1/2022', '3/1/2022', 'Active');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values (12, 3, 'Top LO Training', '1/1/2022', '3/1/2022', 'Active');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values (13, 1, 'Top LS Training', '1/1/2022', '3/1/2022', 'Active');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values (14, 5, 'LH Training', '1/1/2022', '3/1/2022', 'Active');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values (15, 4, 'All Group 2', '1/1/2022', '3/1/2022', 'Inactive');
SET IDENTITY_INSERT Course OFF;

SET IDENTITY_INSERT Student ON;
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (1, 'Giraud', 'Ford', '988-726-7318', 'gford0@bravesites.com', 'Active', 1);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (2, 'Wittie', 'Faulkener', '747-673-5031', 'wfaulkener1@pen.io', 'Active', 1);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (3, 'Nye', 'Lapsley', '375-259-7257', 'nlapsley2@army.mil', 'Active', 1);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (4, 'Pier', 'Ramelet', '921-718-9148', 'pramelet3@cloudflare.com', 'Active', 1);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (5, 'Baillie', 'Iacivelli', '653-361-4346', 'biacivelli4@msn.com', 'Active', 1);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (6, 'Ludwig', 'Serris', '535-137-1605', 'lserris5@hexun.com', 'Active', 2);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (7, 'Grissel', 'Shingfield', '770-216-7148', 'gshingfield6@sourceforge.net', 'Active', 2);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (8, 'Arnoldo', 'Milmoe', '203-366-2872', 'amilmoe7@springer.com', 'Active', 1);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (9, 'Giles', 'Purdy', '983-568-1573', 'gpurdy8@redcross.org', 'Active', 2);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (10, 'Daphna', 'McCardle', '945-932-8283', 'dmccardle9@histats.com', 'Active', 1);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (11, 'Venita', 'Fuster', '338-536-0834', 'vfustera@apache.org', 'Active', 2);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (12, 'Virginie', 'Huske', '411-698-1469', 'vhuskeb@eepurl.com', 'Active', 2);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (13, 'Bridgette', 'Cashmore', '177-567-8992', 'bcashmorec@ow.ly', 'Active', 1);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (14, 'Elysha', 'Whiteside', '853-298-5597', 'ewhitesided@yellowpages.com', 'Ative', 1);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (15, 'Wynne', 'Gronowe', '273-251-2737', 'wgronowee@lycos.com', 'Active', 2);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (16, 'Elvin', 'Comiam', '522-270-7716', 'ecomiamf@npr.org', 'Active', 1);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (17, 'Eyde', 'Membry', '257-557-2593', 'emembryg@nydailynews.com', 'Active', 2);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (18, 'Kaja', 'Fairbank', '418-879-4473', 'kfairbankh@about.me', 'Active', 1);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (19, 'Waite', 'Burney', '543-903-3086', 'wburneyi@godaddy.com', 'Active', 1);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (20, 'Howey', 'Karolovsky', '576-649-3226', 'hkarolovskyj@ning.com', 'Active', 2);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (21, 'Brewer', 'Cauldwell', '221-917-6598', 'bcauldwellk@wikipedia.org', 'Active', 1);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (22, 'Clary', 'Peperell', '590-302-7664', 'cpeperelll@acquirethisname.com', 'Active', 2);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (23, 'Thornton', 'McOrkill', '235-467-6021', 'tmcorkillm@pcworld.com', 'Active', 1);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (24, 'Flossy', 'Baylis', '820-633-3662', 'fbaylisn@cornell.edu', 'Active', 2);
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus, TotalPassCourses) values (25, 'Alisha', 'Salzen', '412-196-1087', 'asalzeno@wikia.com', 'Active', 1);
SET IDENTITY_INSERT Student OFF;

SET IDENTITY_INSERT StudentEnrollmentLog On;
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (1, 2, 1, '6/8/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (2, 5, 2, '6/14/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (3, 1, 3, '6/1/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (4, 2, 4, '6/2/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (5, 5, 5, '6/1/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (6, 5, 6, '6/10/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (7, 4, 7, '6/14/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (8, 3, 8, '6/26/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (9, 5, 9, '6/6/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (10, 4, 10, '6/1/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (11, 5, 11, '6/12/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (12, 1, 12, '6/29/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (13, 3, 13, '6/18/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (14, 1, 14, '6/5/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (15, 5, 15, '6/17/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (16, 3, 16, '6/16/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (17, 2, 17, '6/11/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (18, 3, 18, '6/19/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (19, 5, 19, '6/24/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (20, 3, 20, '6/10/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (21, 1, 21, '6/17/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (22, 2, 22, '6/22/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (23, 3, 23, '6/10/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (24, 5, 24, '6/6/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (25, 1, 25, '6/25/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (26, 8, 1, '9/19/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (27, 6, 2, '9/14/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (28, 7, 3, '9/27/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (29, 10, 4, '9/28/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (30, 9, 5, '9/13/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (31, 9, 6, '9/12/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (32, 10, 7, '9/10/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (33, 6, 8, '9/8/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (34, 7, 9, '9/22/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (35, 9, 10, '9/10/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (36, 7, 11, '9/5/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (37, 10, 12, '9/29/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (38, 9, 13, '9/1/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (39, 6, 14, '9/22/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (40, 9,15, '9/27/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (41, 6, 16, '9/2/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (42, 6, 17, '9/12/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (43, 7, 18, '9/4/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (44, 7, 19, '9/28/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (45, 6, 20, '9/25/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (46, 8, 21, '9/26/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (47, 10, 22, '9/11/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (48, 9, 23, '9/22/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (49, 6, 24, '9/27/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (50, 9, 25, '9/21/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (51, 15, 1, '12/8/2021', 1, 'Teacher Inactive', 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (52, 4, 2, '12/20/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (53, 15, 3, '12/14/2021', 1, 'Teacher Inactive', 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (54, 1, 4, '12/2/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (55, 13, 5, '12/12/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (56, 8, 6, '12/9/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (57, 10, 7, '12/4/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (58, 14, 8, '12/18/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (59, 2, 9, '12/3/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (60, 12, 10, '12/2/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (61, 2, 11, '12/22/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (62, 4, 12, '12/18/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (63, 3, 13, '12/17/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (64, 2, 14, '12/31/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (65, 6, 15, '12/12/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (66, 6, 16, '12/25/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (67, 5, 17, '12/17/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (68, 15, 18, '12/9/2021', 1, 'Teacher Inactive', 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (69, 11, 19, '12/28/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (70, 11, 20, '12/6/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (71, 10, 21, '12/6/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (72, 6, 22, '12/3/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (73, 9, 23, '12/22/2021', 0, NULL, 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (74, 8, 24, '12/23/2021', 0, NULL, 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (75, 4, 25, '12/1/2021', 0, NULL, 0);
SET IDENTITY_INSERT StudentEnrollmentLog OFF;

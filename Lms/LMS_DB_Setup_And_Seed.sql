CREATE TABLE Teacher (
TeacherId int IDENTITY(1,1) PRIMARY KEY,
FirstName varchar(255) not NULL,
LastName varchar(255) not NULL,
Phone varchar(255) not NULL,
Email varchar(255) not NULL,
TeacherStatus varchar(255) not Null
);

CREATE TABLE Semester (
SemesterId int IDENTITY(1,1) PRIMARY KEY,
Semester varchar(255) not NULL,
Year varchar(255) not NULL
);

CREATE TABLE Course (
CourseId int IDENTITY(1,1) PRIMARY KEY,
TeacherId int FOREIGN KEY REFERENCES Teacher(TeacherId) ON UPDATE CASCADE,
CourseName varchar(255) not NULL,
SemesterId int FOREIGN KEY REFERENCES Semester(SemesterId) ON UPDATE CASCADE,
StartDate date,
EndDate date,
CourseStatus varchar(255),
);

CREATE TABLE Student (
StudentId int IDENTITY(1,1) PRIMARY KEY,
FirstName varchar(255) not NULL,
LastName varchar(255) not NULL,
Phone varchar(255) not NULL,
Email varchar(255) not NULL,
StudentStatus varchar(255), 
TotalPassCourses int, 
);

CREATE TABLE StudentEnrollmentLog (
Id int IDENTITY(1,1) PRIMARY KEY,
CourseId int FOREIGN KEY REFERENCES Course(CourseId) ON DELETE NO ACTION,
SemesterId int FOREIGN KEY REFERENCES Semester(SemesterId) ON DELETE NO ACTION,
StudentId int FOREIGN KEY REFERENCES Student(StudentId) ON DELETE NO ACTION,
EnrollmentDate date,
Cancelled bit,
CancellationReason varchar(255),
hasPassed bit
);

SET IDENTITY_INSERT Teacher ON;
insert into Teacher (TeacherId, FirstName, LastName, Phone, Email, TeacherStatus) values (1, 'Roseline', 'Blaver', '113-393-3844', 'rblaver0@smugmug.com', 'Active');
insert into Teacher (TeacherId, FirstName, LastName, Phone, Email, TeacherStatus) values (2, 'Lenette', 'Van der Linde', '516-350-4479', 'lvanderlinde1@cafepress.com', 'Active');
insert into Teacher (TeacherId, FirstName, LastName, Phone, Email, TeacherStatus) values (3, 'Reeta', 'Laydel', '631-883-5112', 'rlaydel2@elpais.com', 'Active');
insert into Teacher (TeacherId, FirstName, LastName, Phone, Email, TeacherStatus) values (4, 'Cirilo', 'Rossant', '221-471-3470', 'crossant3@live.com', 'Active');
insert into Teacher (TeacherId, FirstName, LastName, Phone, Email, TeacherStatus) values (5, 'Whitman', 'Ridsdell', '317-376-5914', 'wridsdell4@nymag.com', 'Active');
SET IDENTITY_INSERT Teacher OFF;

SET IDENTITY_INSERT Semester ON;
insert into Semester (SemesterId, Semester, Year) values (1, 'Summer', '2021');
insert into Semester (SemesterId, Semester, Year) values (2, 'Fall', '2021');
SET IDENTITY_INSERT Semester OFF;

SET IDENTITY_INSERT Course ON
insert into Course (CourseId, TeacherId, CourseName, SemesterId, StartDate, EndDate, CourseStatus) values (1, 4, 'All Group', 1, '7/1/2021', '9/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, SemesterId, StartDate, EndDate, CourseStatus) values (2, 2, 'Passwords 101', 1, '7/1/2021', '9/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, SemesterId, StartDate, EndDate, CourseStatus) values (3, 5, 'Safety', 1, '7/1/2021', '9/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, SemesterId, StartDate, EndDate, CourseStatus) values (4, 3, 'CE Training', 1, '7/1/2021', '9/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, SemesterId, StartDate, EndDate, CourseStatus) values (5, 1, 'Loans R Us', 1, '7/1/2021', '9/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, SemesterId, StartDate, EndDate, CourseStatus) values (6, 2, 'Book Club', 2, '10/1/2021', '12/1/2021', 1);
insert into Course (CourseId, TeacherId, CourseName, SemesterId, StartDate, EndDate, CourseStatus) values (7, 3, 'Health & Wellbeing', 2, '10/1/2021', '12/1/2021', 1);
insert into Course (CourseId, TeacherId, CourseName, SemesterId, StartDate, EndDate, CourseStatus) values (8, 1, 'Tech for dummies', 2, '10/1/2021', '12/1/2021', 1);
insert into Course (CourseId, TeacherId, CourseName, SemesterId, StartDate, EndDate, CourseStatus) values (9, 5, 'Where is Waldo?', 2, '10/1/2021', '12/1/2021', 1);
insert into Course (CourseId, TeacherId, CourseName, SemesterId, StartDate, EndDate, CourseStatus) values (10, 4, 'Morning Coffee', 2, '10/1/2021', '12/1/2021', 1);
SET IDENTITY_INSERT Course OFF;

SET IDENTITY_INSERT Student ON;
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (1, 'Giraud', 'Ford', '988-726-7318', 'gford0@bravesites.com', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (2, 'Wittie', 'Faulkener', '747-673-5031', 'wfaulkener1@pen.io', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (3, 'Nye', 'Lapsley', '375-259-7257', 'nlapsley2@army.mil', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (4, 'Pier', 'Ramelet', '921-718-9148', 'pramelet3@cloudflare.com', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (5, 'Baillie', 'Iacivelli', '653-361-4346', 'biacivelli4@msn.com', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (6, 'Ludwig', 'Serris', '535-137-1605', 'lserris5@hexun.com', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (7, 'Grissel', 'Shingfield', '770-216-7148', 'gshingfield6@sourceforge.net', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (8, 'Arnoldo', 'Milmoe', '203-366-2872', 'amilmoe7@springer.com', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (9, 'Giles', 'Purdy', '983-568-1573', 'gpurdy8@redcross.org', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (10, 'Daphna', 'McCardle', '945-932-8283', 'dmccardle9@histats.com', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (11, 'Venita', 'Fuster', '338-536-0834', 'vfustera@apache.org', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (12, 'Virginie', 'Huske', '411-698-1469', 'vhuskeb@eepurl.com', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (13, 'Bridgette', 'Cashmore', '177-567-8992', 'bcashmorec@ow.ly', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (14, 'Elysha', 'Whiteside', '853-298-5597', 'ewhitesided@yellowpages.com', 'Ative', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (15, 'Wynne', 'Gronowe', '273-251-2737', 'wgronowee@lycos.com', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (16, 'Elvin', 'Comiam', '522-270-7716', 'ecomiamf@npr.org', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (17, 'Eyde', 'Membry', '257-557-2593', 'emembryg@nydailynews.com', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (18, 'Kaja', 'Fairbank', '418-879-4473', 'kfairbankh@about.me', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (19, 'Waite', 'Burney', '543-903-3086', 'wburneyi@godaddy.com', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (20, 'Howey', 'Karolovsky', '576-649-3226', 'hkarolovskyj@ning.com', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (21, 'Brewer', 'Cauldwell', '221-917-6598', 'bcauldwellk@wikipedia.org', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (22, 'Clary', 'Peperell', '590-302-7664', 'cpeperelll@acquirethisname.com', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (23, 'Thornton', 'McOrkill', '235-467-6021', 'tmcorkillm@pcworld.com', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (24, 'Flossy', 'Baylis', '820-633-3662', 'fbaylisn@cornell.edu', 'Active', 1);
insert into Student (StudentId, FirstName, LastName, Phone, Email, StudentStatus, TotalPassCourses) values (25, 'Alisha', 'Salzen', '412-196-1087', 'asalzeno@wikia.com', 'Active', 1);
SET IDENTITY_INSERT Student OFF;

SET IDENTITY_INSERT StudentEnrollmentLog On;
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (1, 2, 1, 1, '6/8/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (2, 5, 1, 2, '6/14/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (3, 1, 1, 3, '6/1/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (4, 2, 1, 4, '6/2/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (5, 5, 1, 5, '6/1/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (6, 5, 1, 6, '6/10/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (7, 4, 1, 7, '6/14/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (8, 3, 1, 8, '6/26/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (9, 5, 1, 9, '6/6/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (10, 4, 1, 10, '6/1/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (11, 5, 1, 11, '6/12/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (12, 1, 1, 12, '6/29/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (13, 3, 1, 13, '6/18/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (14, 1, 1, 14, '6/5/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (15, 5, 1, 15, '6/17/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (16, 3, 1, 16, '6/16/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (17, 2, 1, 17, '6/11/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (18, 3, 1, 18, '6/19/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (19, 5, 1, 19, '6/24/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (20, 3, 1, 20, '6/10/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (21, 1, 1, 21, '6/17/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (22, 2, 1, 22, '6/22/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (23, 3, 1, 23, '6/10/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (24, 5, 1, 24, '6/6/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (25, 1, 1, 25, '6/25/2021', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (26, 8, 2, 1, '9/19/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (27, 6, 2, 2, '9/14/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (28, 7, 2, 3, '9/27/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (29, 10, 2, 4, '9/28/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (30, 9, 2, 5, '9/13/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (31, 9, 2, 6, '9/12/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (32, 10, 2, 7, '9/10/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (33, 6, 2, 8, '9/8/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (34, 7, 2, 9, '9/22/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (35, 9, 2, 10, '9/10/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (36, 7, 2, 11, '9/5/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (37, 10, 2, 12, '9/29/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (38, 9, 2, 13, '9/1/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (39, 6, 2, 14, '9/22/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (40, 9, 2, 15, '9/27/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (41, 6, 2, 16, '9/2/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (42, 6, 2, 17, '9/12/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (43, 7, 2, 18, '9/4/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (44, 7, 2, 19, '9/28/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (45, 6, 2, 20, '9/25/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (46, 8, 2, 21, '9/26/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (47, 10, 2, 22, '9/11/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (48, 9, 2, 23, '9/22/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (49, 6, 2, 24, '9/27/2021', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, SemesterId, StudentId, EnrollmentDate, Cancelled, CancellationReason, HasPassed) values (50, 9, 2, 25, '9/21/2021', 0, 'NULL', 1);
SET IDENTITY_INSERT StudentEnrollmentLog OFF;
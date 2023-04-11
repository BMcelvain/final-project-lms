CREATE TABLE Teacher (
TeacherId UNIQUEIDENTIFIER PRIMARY KEY,
TeacherFirstName VARCHAR(255) not NULL,
TeacherLastName VARCHAR(255) not NULL,
TeacherPhone VARCHAR(255) not NULL,
TeacherEmail VARCHAR(255) not NULL,
TeacherStatus VARCHAR(255) not NULL
);

CREATE TABLE Course (
CourseId UNIQUEIDENTIFIER PRIMARY KEY,
TeacherId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Teacher(TeacherId) ON UPDATE CASCADE,
CourseName VARCHAR(255) not NULL,
StartDate DATE,
EndDate DATE,
CourseStatus VARCHAR(255),
);

CREATE TABLE Student (
StudentId UNIQUEIDENTIFIER PRIMARY KEY,
StudentFirstName VARCHAR(255) not NULL,
StudentLastName VARCHAR(255) not NULL,
StudentPhone VARCHAR(255) not NULL,
StudentEmail VARCHAR(255) not NULL,
StudentStatus VARCHAR(255) not NULL
);

CREATE TABLE StudentEnrollmentLog (
Id UNIQUEIDENTIFIER PRIMARY KEY,
CourseId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Course(CourseId) ON DELETE CASCADE ON UPDATE CASCADE,
StudentId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Student(StudentId) ON DELETE CASCADE ON UPDATE CASCADE,
Cancelled BIT,
CancellationReason VARCHAR(255),
HasPassed? BIT
);

insert into Teacher (TeacherId, TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail, TeacherStatus) values ('df51d667-731e-4369-b688-925adf8678a4', 'Maisie', 'Sherwyn', '360-350-8354', 'msherwyn0@mozilla.org', 'Active');
insert into Teacher (TeacherId, TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail, TeacherStatus) values ('1adf906f-bd2d-4424-bf3a-1ad89dcee16d', 'Constanta', 'Urien', '253-709-2887', 'curien1@geocities.jp', 'Active');
insert into Teacher (TeacherId, TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail, TeacherStatus) values ('390f748a-cd73-4bc9-9b4c-1cfbef24eb0c', 'Hildagard', 'Edghinn', '230-946-3782', 'hedghinn2@sfgate.com', 'Active');
insert into Teacher (TeacherId, TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail, TeacherStatus) values ('1f5ea658-f3ab-433e-b10d-02824f79c5e8', 'Reed', 'Tuffley', '236-798-4411', 'rtuffley3@wikia.com', 'Active');
insert into Teacher (TeacherId, TeacherFirstName, TeacherLastName, TeacherPhone, TeacherEmail, TeacherStatus) values ('5cf5d4c1-cc4c-417e-8458-007f5e4bb912', 'Martie', 'Hartigan', '566-753-7026', 'mhartigan4@telegraph.co.uk', 'Inactive');

insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values ('cd3221db-7fe9-4bb6-b801-8f51748090b2', 'df51d667-731e-4369-b688-925adf8678a4', 'All Group', '7/1/2021', '9/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values ('fedebd74-d0c1-4183-97c2-c5f97b6db082', '1adf906f-bd2d-4424-bf3a-1ad89dcee16d', 'Passwords 101', '7/1/2021', '9/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values ('18731bdb-67a9-402d-8a8a-2d8ca507cefc', '390f748a-cd73-4bc9-9b4c-1cfbef24eb0c', 'Safety', '7/1/2021', '9/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values ('5cad6ff6-17c7-42b1-b938-f6c8c1de3565', '1f5ea658-f3ab-433e-b10d-02824f79c5e8', 'CE Training', '7/1/2021', '9/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values ('5ab32a15-1bb2-4a44-89ad-7f82ab28a8a0', '5cf5d4c1-cc4c-417e-8458-007f5e4bb912', 'Loans R Us', '7/1/2021', '9/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values ('55b99661-0f9f-442e-a4b1-b019cbfe8bd1', 'df51d667-731e-4369-b688-925adf8678a4', 'Book Club', '10/1/2021', '12/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values ('f5cb9f09-ceae-4742-9cfb-c83ac3cc3cbe', '1adf906f-bd2d-4424-bf3a-1ad89dcee16d', 'Health & Wellbeing', '10/1/2021', '12/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values ('50120f83-6156-47c4-bf2e-5998a7842b6a', '390f748a-cd73-4bc9-9b4c-1cfbef24eb0c', 'Tech for Dummies', '10/1/2021', '12/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values ('0ae43554-0bb1-42b1-94c7-04420a2167a5', '1f5ea658-f3ab-433e-b10d-02824f79c5e8', 'Where is Waldo?', '10/1/2021', '12/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values ('9abc9efe-c62b-4dfb-9b43-37ad3adb55a6', '5cf5d4c1-cc4c-417e-8458-007f5e4bb912', 'Morning Coffee', '10/1/2021', '12/1/2021', 'Inactive');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values ('6b95edd2-03ab-4568-88c7-eab469a8f975', 'df51d667-731e-4369-b688-925adf8678a4', 'CodeVU', '1/1/2022', '3/1/2022', 'Active');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values ('ec88e5f8-d137-4af3-a39c-dd9e4e55c352', '1adf906f-bd2d-4424-bf3a-1ad89dcee16d', 'Top LO Training', '1/1/2022', '3/1/2022', 'Active');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values ('3bbf5f05-1ab1-4c0a-ab1c-e4530ff3bf96', '390f748a-cd73-4bc9-9b4c-1cfbef24eb0c', 'Top LS Training', '1/1/2022', '3/1/2022', 'Active');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values ('4a80c08f-fb9d-4698-a7ef-fc5f4bb8e5f1', '1f5ea658-f3ab-433e-b10d-02824f79c5e8', 'Top LH Training', '1/1/2022', '3/1/2022', 'Active');
insert into Course (CourseId, TeacherId, CourseName, StartDate, EndDate, CourseStatus) values ('85064e66-f0f1-4a8e-a59a-3e989ea23cf1', '5cf5d4c1-cc4c-417e-8458-007f5e4bb912', 'All Group 2', '1/1/2022', '3/1/2022', 'Inactive');

insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('b9235a1b-5220-46ec-9191-ef6c960079c9', 'Giraud', 'Ford', '291-133-5470', 'gford0@cornell.edu', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('fd6e0339-8468-4e27-9ada-3708c030046d', 'Rakel', 'Runsey', '768-762-0513', 'rrunsey1@newyorker.com', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('d982bf5e-5711-4a7e-b68b-f104ac238a42', 'Tallia', 'Maybury', '177-498-8737', 'tmaybury2@deliciousdays.com', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('1d184562-68e2-4b09-9d04-a768c5eb2cbf', 'Ned', 'Messruther', '556-419-7914', 'nmessruther3@phpbb.com', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('5503b426-add6-4093-a0c2-2dc9b497e8db', 'Yale', 'Widdecombe', '808-785-3037', 'ywiddecombe4@sina.com.cn', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('7a03af7d-bce0-434b-a2c5-153651ea3314', 'Quincey', 'Dulake', '911-150-8172', 'qdulake5@php.net', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('6fdef7ad-9ef8-4d75-bf16-1aed628bbb9c', 'Ashlan', 'Bentzen', '900-544-2175', 'abentzen6@w3.org', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('155bb637-7e98-4f59-a161-df1457f4e18c', 'Germain', 'Bonsey', '127-815-5731', 'gbonsey7@over-blog.com', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('8ff7cf1b-8127-4c83-a77c-e94d3a560ca3', 'Christin', 'Heigho', '405-430-0718', 'cheigho8@addtoany.com', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('e462890c-faa3-4222-9872-48ba6acf24a3', 'Jdavie', 'Goodrich', '643-619-8320', 'jgoodrich9@printfriendly.com', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('ba3e4b32-658a-4078-8e96-d94ba8bf5887', 'Wallie', 'Franchyonok', '799-783-7113', 'wfranchyonoka@ted.com', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('15212c1f-d2b2-4f3c-bcca-a3046203bbf3', 'Myles', 'Jakucewicz', '976-392-2461', 'mjakucewiczb@cbc.ca', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('2ba270c1-adbc-4ff1-a454-ff36ffebaba3', 'Ainsley', 'Dilke', '372-525-6315', 'adilkec@barnesandnoble.com', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('df74809d-a5a7-4ea4-991d-7e952216f369', 'Ciel', 'Warde', '851-112-5473', 'cwarded@paginegialle.it', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('953a43a2-29bf-4cdd-8c5c-3b85412b22a4', 'Barbabra', 'Hamnett', '953-176-8568', 'bhamnette@weebly.com', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('9ea50088-01c8-4bc5-8f49-508e372fbca7', 'Austin', 'Feore', '575-327-5973', 'afeoref@auda.org.au', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('acfeaf0c-9173-45f4-95ff-a5f0bbe52162', 'Gaylord', 'Pearle', '526-775-0411', 'gpearleg@virginia.edu', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('fa8cbb96-5c8b-44ae-b30a-16a9c6dd7d9f', 'Lloyd', 'Houlson', '918-903-4208', 'lhoulsonh@google.ru', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('0ced0d89-03e4-46ef-990f-0c058ec93b91', 'Ole', 'Ebbings', '643-652-9029', 'oebbingsi@buzzfeed.com', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('73c2d57e-e4f8-4a28-bdf0-8c63868dfe70', 'Mirabel', 'O''Dooghaine', '511-862-0081', 'modooghainej@simplemachines.org', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('e88949e4-1f5a-47bd-839a-cd3d244dee40', 'Thain', 'Harburtson', '388-695-8633', 'tharburtsonk@apache.org', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('b9637a54-0ac0-4e02-a951-8705065e727e', 'Caitrin', 'Bend', '800-400-5253', 'cbendl@google.fr', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('03983a5c-8627-49c4-b5c8-32f9fd41f381', 'Kariotta', 'Byne', '338-642-2326', 'kbynem@macromedia.com', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('f482d3b9-cca0-4f91-ba18-18d2b98e8cdf', 'Del', 'Chucks', '925-616-9607', 'dchucksn@zimbio.com', 'Active');
insert into Student (StudentId, StudentFirstName, StudentLastName, StudentPhone, StudentEmail, StudentStatus) values ('ac1a4129-0af5-4911-98bd-08be7b62cd3e', 'Welbie', 'Mathivat', '629-862-2835', 'wmathivato@skype.com', 'Active');

insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('14a9a43b-c35d-4f50-9aaf-8ad4d32ab769', 'cd3221db-7fe9-4bb6-b801-8f51748090b2', 'b9235a1b-5220-46ec-9191-ef6c960079c9', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('e97e7188-e8f2-4f62-add3-c527313dac5b', 'fedebd74-d0c1-4183-97c2-c5f97b6db082', 'fd6e0339-8468-4e27-9ada-3708c030046d', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('88bd6e7e-c31c-49ae-beac-136ec17d6892', '18731bdb-67a9-402d-8a8a-2d8ca507cefc', 'd982bf5e-5711-4a7e-b68b-f104ac238a42', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('94480b39-f13c-44ff-a5b0-a35a15b40872', '5cad6ff6-17c7-42b1-b938-f6c8c1de3565', '1d184562-68e2-4b09-9d04-a768c5eb2cbf', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('c99b4e3b-1114-4d72-be32-369a3f51656a', '5ab32a15-1bb2-4a44-89ad-7f82ab28a8a0', '5503b426-add6-4093-a0c2-2dc9b497e8db', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('023df45f-d353-461b-9b5f-7f5dfad36a99', 'cd3221db-7fe9-4bb6-b801-8f51748090b2', '7a03af7d-bce0-434b-a2c5-153651ea3314', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('85823dcc-19a7-4c3b-9101-f8e8a37b8be7', 'fedebd74-d0c1-4183-97c2-c5f97b6db082', '6fdef7ad-9ef8-4d75-bf16-1aed628bbb9c', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('d0a2f3d9-62d9-410d-9388-bd416100659e', '18731bdb-67a9-402d-8a8a-2d8ca507cefc', '155bb637-7e98-4f59-a161-df1457f4e18c', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('14430fc4-7705-496d-9082-1e480d81633e', '5cad6ff6-17c7-42b1-b938-f6c8c1de3565', '8ff7cf1b-8127-4c83-a77c-e94d3a560ca3', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('597b16b9-fa43-41dd-96a7-40d4df386d35', '5ab32a15-1bb2-4a44-89ad-7f82ab28a8a0', 'e462890c-faa3-4222-9872-48ba6acf24a3', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('9791435e-b2fc-43ca-b43f-0eb042ccfcf6', 'cd3221db-7fe9-4bb6-b801-8f51748090b2', 'ba3e4b32-658a-4078-8e96-d94ba8bf5887', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('e420ad47-64f3-49e5-a70f-ef05668ff09b', 'fedebd74-d0c1-4183-97c2-c5f97b6db082', '15212c1f-d2b2-4f3c-bcca-a3046203bbf3', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('ea543567-f628-4d37-a6ea-9824c760cfe8', '18731bdb-67a9-402d-8a8a-2d8ca507cefc', '2ba270c1-adbc-4ff1-a454-ff36ffebaba3', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('fe42faf8-8bb7-4099-9427-abe88f58248a', '5cad6ff6-17c7-42b1-b938-f6c8c1de3565', 'df74809d-a5a7-4ea4-991d-7e952216f369', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('d0d84dc2-b9b1-461d-8e0a-c03fed22bbda', '5ab32a15-1bb2-4a44-89ad-7f82ab28a8a0', '953a43a2-29bf-4cdd-8c5c-3b85412b22a4', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('ff723362-2408-4182-9ae6-43fbe3fe5987', 'cd3221db-7fe9-4bb6-b801-8f51748090b2', '9ea50088-01c8-4bc5-8f49-508e372fbca7', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('23368b8a-e870-43dc-822e-99ec136ca825', 'fedebd74-d0c1-4183-97c2-c5f97b6db082', 'acfeaf0c-9173-45f4-95ff-a5f0bbe52162', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('5896896a-2103-409d-9e12-672bda465ab4', '18731bdb-67a9-402d-8a8a-2d8ca507cefc', 'fa8cbb96-5c8b-44ae-b30a-16a9c6dd7d9f', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('7292156b-3ed6-48b9-8a6b-a16f6d6dc049', '5cad6ff6-17c7-42b1-b938-f6c8c1de3565', '0ced0d89-03e4-46ef-990f-0c058ec93b91', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('6fc802f1-bfa8-4c0d-aec7-e2d3202de14f', '5ab32a15-1bb2-4a44-89ad-7f82ab28a8a0', '73c2d57e-e4f8-4a28-bdf0-8c63868dfe70', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('7f04a0c6-284f-4d7c-97eb-36409427e6f4', 'cd3221db-7fe9-4bb6-b801-8f51748090b2', 'e88949e4-1f5a-47bd-839a-cd3d244dee40', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('4dc75a97-ca31-41b8-8b7f-cdf1ee0ad258', 'fedebd74-d0c1-4183-97c2-c5f97b6db082', 'b9637a54-0ac0-4e02-a951-8705065e727e', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('f2a6bd68-bb40-4b4f-a2db-8e12d7f87dd8', '18731bdb-67a9-402d-8a8a-2d8ca507cefc', '03983a5c-8627-49c4-b5c8-32f9fd41f381', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('5fa7d294-664e-4e6a-891d-016a68a4f36b', '5cad6ff6-17c7-42b1-b938-f6c8c1de3565', 'f482d3b9-cca0-4f91-ba18-18d2b98e8cdf', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('5626e770-59e6-45a4-878f-be0cd15f0563', '5ab32a15-1bb2-4a44-89ad-7f82ab28a8a0', 'ac1a4129-0af5-4911-98bd-08be7b62cd3e', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('b98e225a-7728-459e-a47c-f42d561d3dde', '55b99661-0f9f-442e-a4b1-b019cbfe8bd1', 'b9235a1b-5220-46ec-9191-ef6c960079c9', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('12a961d3-ad41-4c7b-bb72-e89be1b5b285', 'f5cb9f09-ceae-4742-9cfb-c83ac3cc3cbe', 'fd6e0339-8468-4e27-9ada-3708c030046d', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('84696f4d-7568-483e-9ca0-9cec455d2a9c', '50120f83-6156-47c4-bf2e-5998a7842b6a', 'd982bf5e-5711-4a7e-b68b-f104ac238a42', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('6f65df81-d9ff-49fb-8eca-cba3b40a1e54', '0ae43554-0bb1-42b1-94c7-04420a2167a5', '1d184562-68e2-4b09-9d04-a768c5eb2cbf', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('ec357d01-f4a7-4e6a-9d53-e468541195fc', '9abc9efe-c62b-4dfb-9b43-37ad3adb55a6', '5503b426-add6-4093-a0c2-2dc9b497e8db', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('f3b17aa5-b94a-4aca-836d-91d97f610342', '55b99661-0f9f-442e-a4b1-b019cbfe8bd1', '7a03af7d-bce0-434b-a2c5-153651ea3314', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('252512f6-08ec-42ad-ba0a-2c09e7e47130', 'f5cb9f09-ceae-4742-9cfb-c83ac3cc3cbe', '6fdef7ad-9ef8-4d75-bf16-1aed628bbb9c', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('8cf8b0fc-8e4e-489e-92c4-f459c1e62365', '50120f83-6156-47c4-bf2e-5998a7842b6a', '155bb637-7e98-4f59-a161-df1457f4e18c', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('693aa089-229e-4b51-824d-3c0be50f503a', '0ae43554-0bb1-42b1-94c7-04420a2167a5', '8ff7cf1b-8127-4c83-a77c-e94d3a560ca3', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('e5010f14-76da-41c3-8485-10241577c3d0', '9abc9efe-c62b-4dfb-9b43-37ad3adb55a6', 'e462890c-faa3-4222-9872-48ba6acf24a3', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('6d3b3be1-6faf-4981-895f-9f3a48b1c595', '55b99661-0f9f-442e-a4b1-b019cbfe8bd1', 'ba3e4b32-658a-4078-8e96-d94ba8bf5887', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('c779be62-2db1-4d16-8cf2-9c98645f4220', 'f5cb9f09-ceae-4742-9cfb-c83ac3cc3cbe', '15212c1f-d2b2-4f3c-bcca-a3046203bbf3', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('9e41f038-f0f4-4fb9-99e3-d42b450226f3', '50120f83-6156-47c4-bf2e-5998a7842b6a', '2ba270c1-adbc-4ff1-a454-ff36ffebaba3', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('f67a0825-561b-432e-8578-984af7a348a3', '0ae43554-0bb1-42b1-94c7-04420a2167a5', 'df74809d-a5a7-4ea4-991d-7e952216f369', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('dc52f57b-703c-435a-a174-83eaced0be46', '9abc9efe-c62b-4dfb-9b43-37ad3adb55a6', '953a43a2-29bf-4cdd-8c5c-3b85412b22a4', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('1f9fb155-fb67-43c0-8e58-98e6afb34f95', '55b99661-0f9f-442e-a4b1-b019cbfe8bd1', '9ea50088-01c8-4bc5-8f49-508e372fbca7', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('cb284de1-917d-45a1-ac17-ef222a563eae', 'f5cb9f09-ceae-4742-9cfb-c83ac3cc3cbe', 'acfeaf0c-9173-45f4-95ff-a5f0bbe52162', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('b6b12025-57f7-4f53-a95e-ad7ff665da4e', '50120f83-6156-47c4-bf2e-5998a7842b6a', 'fa8cbb96-5c8b-44ae-b30a-16a9c6dd7d9f', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('ec29669b-f8c4-4f1c-ba5d-495797716f6f', '0ae43554-0bb1-42b1-94c7-04420a2167a5', '0ced0d89-03e4-46ef-990f-0c058ec93b91', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('963c9edd-7bbd-4b4b-9ece-24726bddce6a', '9abc9efe-c62b-4dfb-9b43-37ad3adb55a6', '73c2d57e-e4f8-4a28-bdf0-8c63868dfe70', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('6a810f79-58d2-4344-89e6-2d2321bb5034', '55b99661-0f9f-442e-a4b1-b019cbfe8bd1', 'e88949e4-1f5a-47bd-839a-cd3d244dee40', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('5b64e49b-4089-45ab-95c3-7a1d9c12fb26', 'f5cb9f09-ceae-4742-9cfb-c83ac3cc3cbe', 'b9637a54-0ac0-4e02-a951-8705065e727e', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('e6694437-1277-4b4c-bd65-41a1bbec63b7', '50120f83-6156-47c4-bf2e-5998a7842b6a', '03983a5c-8627-49c4-b5c8-32f9fd41f381', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('3f002c22-ec98-4978-bfdd-4c950a5aca13', '0ae43554-0bb1-42b1-94c7-04420a2167a5', 'f482d3b9-cca0-4f91-ba18-18d2b98e8cdf', 0, 'NULL', 1);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('136673f9-b841-44ae-8832-fcd13ef38fda', '9abc9efe-c62b-4dfb-9b43-37ad3adb55a6', 'ac1a4129-0af5-4911-98bd-08be7b62cd3e', 0, 'NULL', 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('025160ea-6df8-4955-af44-71ff11a32db3', '6b95edd2-03ab-4568-88c7-eab469a8f975', 'b9235a1b-5220-46ec-9191-ef6c960079c9', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('4e742cd9-b52d-4464-b19f-43684073c920', 'ec88e5f8-d137-4af3-a39c-dd9e4e55c352', 'fd6e0339-8468-4e27-9ada-3708c030046d', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('14df9425-dca5-4125-9e8a-9b8821b54cee', '3bbf5f05-1ab1-4c0a-ab1c-e4530ff3bf96', 'd982bf5e-5711-4a7e-b68b-f104ac238a42', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('ed7f61b2-adf0-4871-994a-1f0f7243ef58', '4a80c08f-fb9d-4698-a7ef-fc5f4bb8e5f1', '1d184562-68e2-4b09-9d04-a768c5eb2cbf', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('7db5ee1d-4127-4c71-abbe-7f0f10de329a', '85064e66-f0f1-4a8e-a59a-3e989ea23cf1', '5503b426-add6-4093-a0c2-2dc9b497e8db', 1, 'Teacher Inactive', 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('4ee6f25e-4a6a-4765-89f1-be95215a390e', '6b95edd2-03ab-4568-88c7-eab469a8f975', '7a03af7d-bce0-434b-a2c5-153651ea3314', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('f063d226-5bf4-4597-b36a-dfc4aa9bdd9c', 'ec88e5f8-d137-4af3-a39c-dd9e4e55c352', '6fdef7ad-9ef8-4d75-bf16-1aed628bbb9c', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('39232cf7-1ce5-44a0-9279-aaa24938f664', '3bbf5f05-1ab1-4c0a-ab1c-e4530ff3bf96', '155bb637-7e98-4f59-a161-df1457f4e18c', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('ea7700ec-8034-4e44-9459-448fc70ae33d', '4a80c08f-fb9d-4698-a7ef-fc5f4bb8e5f1', '8ff7cf1b-8127-4c83-a77c-e94d3a560ca3', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('918f761c-5d46-417f-a2da-c5bcc9df12ba', '85064e66-f0f1-4a8e-a59a-3e989ea23cf1', 'e462890c-faa3-4222-9872-48ba6acf24a3', 1, 'Teacher Inactive', 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('2adcc7ac-fe42-4402-8016-6aa0470bfe76', '6b95edd2-03ab-4568-88c7-eab469a8f975', 'ba3e4b32-658a-4078-8e96-d94ba8bf5887', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('57fc378c-3e80-4fa9-9ef2-b8681b5d4dcb', 'ec88e5f8-d137-4af3-a39c-dd9e4e55c352', '15212c1f-d2b2-4f3c-bcca-a3046203bbf3', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('e63c497f-51bb-41ee-bc92-320ef82c9d24', '3bbf5f05-1ab1-4c0a-ab1c-e4530ff3bf96', '2ba270c1-adbc-4ff1-a454-ff36ffebaba3', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('ad77fd11-66bb-462f-b7ba-00a490c213a5', '4a80c08f-fb9d-4698-a7ef-fc5f4bb8e5f1', 'df74809d-a5a7-4ea4-991d-7e952216f369', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('f2edcc51-7b58-471b-a79a-7ae7caaa76bc', '85064e66-f0f1-4a8e-a59a-3e989ea23cf1', '953a43a2-29bf-4cdd-8c5c-3b85412b22a4', 1, 'Teacher Inactive', 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('744840c1-b51d-482a-accf-2b2ec7d2d8c0', '6b95edd2-03ab-4568-88c7-eab469a8f975', '9ea50088-01c8-4bc5-8f49-508e372fbca7', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('5b693255-d450-4972-86fe-88a4aed134f2', 'ec88e5f8-d137-4af3-a39c-dd9e4e55c352', 'acfeaf0c-9173-45f4-95ff-a5f0bbe52162', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('9394162c-6f2b-4f65-9f54-a72d54e0e0c1', '3bbf5f05-1ab1-4c0a-ab1c-e4530ff3bf96', 'fa8cbb96-5c8b-44ae-b30a-16a9c6dd7d9f', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('564b2c79-3d59-4943-b59a-304719f0fd23', '4a80c08f-fb9d-4698-a7ef-fc5f4bb8e5f1', '0ced0d89-03e4-46ef-990f-0c058ec93b91', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('0482daf0-c3da-4da6-b665-0364011f639d', '85064e66-f0f1-4a8e-a59a-3e989ea23cf1', '73c2d57e-e4f8-4a28-bdf0-8c63868dfe70', 1, 'Teacher Inactive', 0);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('757b2ea2-50d2-4b5c-85d5-503b7e4e60df', '6b95edd2-03ab-4568-88c7-eab469a8f975', 'e88949e4-1f5a-47bd-839a-cd3d244dee40', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('5192a3d5-8bb1-40d4-9ea4-e90cccfcd5d5', 'ec88e5f8-d137-4af3-a39c-dd9e4e55c352', 'b9637a54-0ac0-4e02-a951-8705065e727e', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('7d57667e-855d-4e34-b560-892989f5ca83', '3bbf5f05-1ab1-4c0a-ab1c-e4530ff3bf96', '03983a5c-8627-49c4-b5c8-32f9fd41f381', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('e425b3d4-1051-4f4f-97e1-8d571a5bea2d', '4a80c08f-fb9d-4698-a7ef-fc5f4bb8e5f1', 'f482d3b9-cca0-4f91-ba18-18d2b98e8cdf', 0, 'NULL', NULL);
insert into StudentEnrollmentLog (Id, CourseId, StudentId, Cancelled, CancellationReason, HasPassed) values ('4331983e-2023-4f73-b34b-ee1f553e3daf', '85064e66-f0f1-4a8e-a59a-3e989ea23cf1', 'ac1a4129-0af5-4911-98bd-08be7b62cd3e', 1, 'Teacher Inactive', 0);

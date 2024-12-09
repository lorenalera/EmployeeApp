use companyDB;
CREATE table users(
    userID int Primary key,
    name varchar(30),
    username varchar(30),
    password varchar(50),
    profilePicture Nvarchar(255),
    role VARCHAR(50) NOT NULL CHECK (role IN ('ADMIN', 'EMPLOYEE')),
)
CREATE table Project(
    projectID int Primary key,
    name varchar(30),
)
CREATE table Task(
    taskID int Primary key,
    name varchar(30),
    status varchar(30) NOT NULL CHECK (status IN('OPENED', 'COMPLETED')),
    projectID int FOREIGN key References Project(projectID),
)
CREATE table user_project(
    userID int FOREIGN key References users(userID),
    projectID int FOREIGN key References Project(projectID),
    Primary key(userID, projectID)
)
EXEC sp_rename 'users', 'User';
EXEC sp_rename 'companyDb.UserProjects', 'UserProjects';


INSERT INTO Users (userID, name, username, password, profilePicture, role) VALUES
(1, 'Alice Johnson', 'alice.j', 'password123', 'path/to/alice.jpg', 'ADMIN'),
(2, 'Bob Smith', 'bob.s', 'securePass!', 'path/to/bob.jpg', 'EMPLOYEE'),
(3, 'Charlie Davis', 'charlie.d', 'charliePass!', 'path/to/charlie.jpg', 'EMPLOYEE');

INSERT INTO Users (userID, name, username, password, profilePicture, role) VALUES
(4, 'Anne Marie', 'anne.a', 'goodLuck!', 'path/to/anne.jpg', 'ADMIN'),
(5, 'Jack Smith', 'jack.j', 'vibesy9$', 'path/to/jack.jpg', 'EMPLOYEE'),
(6, 'Sara Davis', 'sara.s', 'evenOdd.', 'path/to/sara.jpg', 'EMPLOYEE');

-- Insert sample data into Projects table
INSERT INTO Project (projectID, name) VALUES
(1, 'Website Redesign'),
(2, 'Mobile App Development'),
(3, 'Database Migration');

-- Insert sample data into Tasks table
INSERT INTO Task (taskID, name, status, projectID) VALUES
(1, 'Design Landing Page', 'OPENED', 1),
(2, 'Implement Authentication', 'COMPLETED', 2),
(3, 'Database Schema Update', 'OPENED', 3),
(4, 'User Testing', 'OPENED', 1),
(5, 'Data Backup', 'COMPLETED', 3);

-- Insert sample data into user_project table (assigning users to projects)
INSERT INTO user_project (userID, projectID) VALUES
(1, 1),  -- Alice is assigned to Website Redesign
(2, 2),  -- Bob is assigned to Mobile App Development
(3, 3)  -- Charlie is assigned to Database Migration

select userID, projectID from user_project;
select projectID, Name from Project;
INSERT into Task(taskID, name, status, projectID) VALUES
(6, 'Task6','OPENED', 3),
(7, 'Task7', 'COMPLETED', 1),
(8, 'Task8', 'OPENED', 2),
(9, 'Task9', 'COMPLETED', 3),
(10,'Task10', 'OPENED', 2);
SELECT * from Task;
SELECT * from [User];
SELECT * from [Project];
select * from UserProjects;
UPDATE users set name = 'Belissa Fejzaj' WHERE userID=2;
DELETE FROM users WHERE userID=3;
select * from companyDB.dbo.Task;



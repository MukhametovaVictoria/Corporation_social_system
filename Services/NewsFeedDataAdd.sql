-- Вставка записей в таблицу Employee
INSERT INTO Employee (Id, Firstname, Surname, Position, IsDeleted, IsAdmin)
VALUES (NEWID(), 'John', 'Doe', 'Developer', 0, 1),
       (NEWID(), 'Jane', 'Smith', 'Manager', 0, 0),
       (NEWID(), 'Alice', 'Johnson', 'HR', 0, 0);

-- Вставка записей в таблицу Hashtag
INSERT INTO Hashtag (Id, Name)
VALUES (NEWID(), '#Technology'),
       (NEWID(), '#Business'),
       (NEWID(), '#HR');

-- Вставка записей в таблицу News
INSERT INTO News (Id, Title, Content, ShortDescription, CreatedAt, UpdatedAt, AuthorId, IsPublished, IsArchived)
VALUES (NEWID(), 'New Technology Trends', 'Content about technology...', 'Tech news short description', GETDATE(), GETDATE(), 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'John'), 1, 0),
       (NEWID(), 'Business Growth', 'Content about business growth...', 'Business short description', GETDATE(), GETDATE(), 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Jane'), 1, 0),
       (NEWID(), 'HR Practices', 'Content about HR practices...', 'HR short description', GETDATE(), GETDATE(), 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Alice'), 0, 0);

-- Вставка записей в таблицу HashtagNews (связывает News и Hashtag)
INSERT INTO HashtagNews (Id, HashtagId, NewsId)
VALUES (NEWID(), 
        (SELECT TOP 1 Id FROM Hashtag WHERE Name = '#Technology'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'New Technology Trends')),
       (NEWID(), 
        (SELECT TOP 1 Id FROM Hashtag WHERE Name = '#Business'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'Business Growth')),
       (NEWID(), 
        (SELECT TOP 1 Id FROM Hashtag WHERE Name = '#HR'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'HR Practices'));

-- Вставка записей в таблицу NewsComment
INSERT INTO NewsComment (Id, Content, AuthorId, NewsId, CreatedAt, UpdatedAt)
VALUES (NEWID(), 'Great article about technology!', 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Jane'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'New Technology Trends'), GETDATE(), GETDATE()),
       (NEWID(), 'Very informative on business growth.', 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Alice'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'Business Growth'), GETDATE(), GETDATE()),
       (NEWID(), 'Useful HR tips, thanks!', 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'John'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'HR Practices'), GETDATE(), GETDATE());

-- Вставка записей в таблицу LikedNews (лайки от сотрудников)
INSERT INTO LikedNews (Id, EmployeeId, NewsId)
VALUES (NEWID(), 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'John'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'Business Growth')),
       (NEWID(), 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Jane'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'New Technology Trends')),
       (NEWID(), 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Alice'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'HR Practices'));

		-- Вставка дополнительных записей в таблицу News
INSERT INTO News (Id, Title, Content, ShortDescription, CreatedAt, UpdatedAt, AuthorId, IsPublished, IsArchived)
VALUES 
(NEWID(), 'Tech Revolution', 'Content about the tech revolution...', 'Tech revolution short description', GETDATE(), GETDATE(), 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'John'), 1, 0),
(NEWID(), 'Future of Business', 'Content about the future of business...', 'Future of business short description', GETDATE(), GETDATE(), 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Jane'), 1, 0),
(NEWID(), 'AI in HR', 'Content about AI applications in HR...', 'AI in HR short description', GETDATE(), GETDATE(), 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Alice'), 1, 0),
(NEWID(), 'Sustainability in Tech', 'Content about sustainability and green technology...', 'Sustainability short description', GETDATE(), GETDATE(), 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'John'), 1, 0),
(NEWID(), 'Business Ethics', 'Content about ethical practices in business...', 'Ethics in business short description', GETDATE(), GETDATE(), 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Jane'), 0, 0),
(NEWID(), 'Remote Work', 'Content about remote work trends...', 'Remote work short description', GETDATE(), GETDATE(), 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Alice'), 1, 0),
(NEWID(), 'Cloud Computing', 'Content about cloud computing trends...', 'Cloud computing short description', GETDATE(), GETDATE(), 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'John'), 1, 0),
(NEWID(), 'Digital Marketing', 'Content about digital marketing strategies...', 'Digital marketing short description', GETDATE(), GETDATE(), 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Jane'), 1, 0),
(NEWID(), 'Employee Wellness', 'Content about employee wellness programs...', 'Employee wellness short description', GETDATE(), GETDATE(), 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Alice'), 1, 0);

-- Вставка дополнительных записей в таблицу NewsComment
INSERT INTO NewsComment (Id, Content, AuthorId, NewsId, CreatedAt, UpdatedAt)
VALUES 
(NEWID(), 'This is a great article on tech!', 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'John'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'Tech Revolution'), GETDATE(), GETDATE()),
(NEWID(), 'Very insightful content on business ethics.', 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Jane'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'Business Ethics'), GETDATE(), GETDATE()),
(NEWID(), 'HR will definitely benefit from AI!', 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Alice'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'AI in HR'), GETDATE(), GETDATE()),
(NEWID(), 'Remote work is the future. This article nails it!', 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'John'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'Remote Work'), GETDATE(), GETDATE()),
(NEWID(), 'Cloud computing is changing everything.', 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Jane'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'Cloud Computing'), GETDATE(), GETDATE()),
(NEWID(), 'This is very informative about sustainability in tech!', 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Alice'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'Sustainability in Tech'), GETDATE(), GETDATE()),
(NEWID(), 'I really enjoyed reading about digital marketing strategies.', 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'John'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'Digital Marketing'), GETDATE(), GETDATE()),
(NEWID(), 'Employee wellness is so important. Good points made in this article.', 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Jane'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'Employee Wellness'), GETDATE(), GETDATE()),
(NEWID(), 'This is a fantastic overview of future business trends.', 
        (SELECT TOP 1 Id FROM Employee WHERE Firstname = 'Alice'), 
        (SELECT TOP 1 Id FROM News WHERE Title = 'Future of Business'), GETDATE(), GETDATE());
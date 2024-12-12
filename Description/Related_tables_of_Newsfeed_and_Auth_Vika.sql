CREATE TABLE [Users] (
  [Id] uuid PRIMARY KEY,
  [Login] nvarchar(255),
  [Password] nvarchar(255),
  [IsActive] bit,
  [CreatedAt] timestamp
)
GO

CREATE TABLE [AdditionalUserInfo] (
  [Id] uuid PRIMARY KEY,
  [UserId] uuid,
  [Name] nvarchar(255),
  [Lastname] nvarchar(255),
  [Patronymic] nvarchar(255),
  [Age] integer,
  [Phone] nvarchar(255),
  [Email] nvarchar(255),
  [Address] nvarchar(255),
  [RoleId] uuid
)
GO

CREATE TABLE [Enters] (
  [Id] uuid PRIMARY KEY,
  [UserId] uuid,
  [Date] timestamp,
  [IsFailed] bit
)
GO

CREATE TABLE [FailedAttempts] (
  [Id] uuid PRIMARY KEY,
  [UserId] uuid,
  [Count] integer
)
GO

CREATE TABLE [Posts] (
  [Id] uuid PRIMARY KEY,
  [Title] nvarchar(255),
  [Body] text,
  [UserId] uuid,
  [CreatedAt] timestamp,
  [Likes] integer
)
GO

CREATE TABLE [Hashtags] (
  [Id] uuid PRIMARY KEY,
  [Hashtag] nvarchar(255)
)
GO

CREATE TABLE [HashtagPost] (
  [Id] uuid PRIMARY KEY,
  [HashtagId] uuid,
  [PostId] uuid
)
GO

CREATE TABLE [Files] (
  [Id] uuid PRIMARY KEY,
  [Size] integer,
  [Title] nvarchar(255),
  [Data] varbinary,
  [MIMEtype] nvarchar(255),
  [PostId] uuid,
  [CreatedAt] timestamp
)
GO

CREATE TABLE [PostComments] (
  [Id] uuid PRIMARY KEY,
  [PostId] uuid,
  [UserId] uuid,
  [Body] text,
  [CreatedAt] timestamp
)
GO

CREATE TABLE [Roles] (
  [Id] uuid PRIMARY KEY,
  [Name] nvarchar(255)
)
GO

CREATE TABLE [UserRoles] (
  [Id] uuid,
  [UserId] uuid,
  [RoleId] uuid
)
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Content of the post',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Posts',
@level2type = N'Column', @level2name = 'Body';
GO

ALTER TABLE [Posts] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [HashtagPost] ADD FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id])
GO

ALTER TABLE [HashtagPost] ADD FOREIGN KEY ([HashtagId]) REFERENCES [Hashtags] ([Id])
GO

ALTER TABLE [Files] ADD FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id])
GO

ALTER TABLE [PostComments] ADD FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id])
GO

ALTER TABLE [PostComments] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [Enters] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [Users] ADD FOREIGN KEY ([Id]) REFERENCES [FailedAttempts] ([UserId])
GO

ALTER TABLE [Users] ADD FOREIGN KEY ([Id]) REFERENCES [AdditionalUserInfo] ([UserId])
GO

ALTER TABLE [Users] ADD FOREIGN KEY ([Id]) REFERENCES [UserRoles] ([UserId])
GO

ALTER TABLE [Roles] ADD FOREIGN KEY ([Id]) REFERENCES [UserRoles] ([RoleId])
GO

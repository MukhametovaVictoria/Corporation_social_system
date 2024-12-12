--Сотрудник
CREATE TABLE Employee (
	Id uniqueidentifier NOT NULL,
	Firstname nvarchar(max) NOT NULL,
	Surname nvarchar(max) NULL,
	Position nvarchar(max) NULL,
	IsDeleted bit NOT NULL,
	IsAdmin bit NOT NULL
)
ALTER TABLE Employee ADD  CONSTRAINT [DFHuuuvW8Ndkv0vWuFOKvZq9Fs]  DEFAULT (newid()) FOR Id
ALTER TABLE Employee ADD  DEFAULT (0) FOR IsDeleted
ALTER TABLE Employee ADD  DEFAULT (0) FOR IsAdmin

--Хэштег
CREATE TABLE Hashtag (
	Id uniqueidentifier NOT NULL,
	Name nvarchar(max) NOT NULL
)
ALTER TABLE Hashtag ADD  CONSTRAINT [DFHuuuvW8Ndkv0vWuFOKvZr9Fs]  DEFAULT (newid()) FOR Id

--Новость
CREATE TABLE News(
	Id uniqueidentifier NOT NULL,
	Title nvarchar(max) NULL,
	Content nvarchar(max) NULL,
	ShortDescription nvarchar(max) NULL,
	CreatedAt datetime2(7) NOT NULL,
	UpdatedAt datetime2(7) NOT NULL,
	AuthorId uniqueidentifier NOT NULL,
	IsPublished bit NOT NULL,
	IsArchived bit NOT NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE News ADD  CONSTRAINT [DFHuuyuW8Ndkv0uWuFOKvZi9Fs]  DEFAULT (newid()) FOR Id
ALTER TABLE News ADD  DEFAULT (GETDATE()) FOR CreatedAt
ALTER TABLE News ADD  DEFAULT (GETDATE()) FOR UpdatedAt
ALTER TABLE News ADD  DEFAULT ((0)) FOR IsPublished
ALTER TABLE News ADD  DEFAULT ((0)) FOR IsArchived
ALTER TABLE News  WITH CHECK ADD  CONSTRAINT [FK_News_Employee_AuthorId] FOREIGN KEY(AuthorId)
REFERENCES Employee (Id)
ON DELETE CASCADE
ALTER TABLE News CHECK CONSTRAINT [FK_News_Employee_AuthorId]

--Связь хэштега и новости
CREATE TABLE HashtagNews (
	Id uniqueidentifier NOT NULL,
	HashtagId uniqueidentifier NOT NULL,
	NewsId uniqueidentifier NOT NULL,
 CONSTRAINT [PK_HashtagNews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE HashtagNews ADD  CONSTRAINT [DFHuuuvW8Ndkv0vWuFOKvZo9Fs]  DEFAULT (newid()) FOR Id
ALTER TABLE HashtagNews  WITH CHECK ADD  CONSTRAINT [FK_HashtagNews_Hashtag_HashtagId] FOREIGN KEY(HashtagId)
REFERENCES Hashtag (Id)
ON DELETE CASCADE
ALTER TABLE HashtagNews CHECK CONSTRAINT [FK_HashtagNews_Hashtag_HashtagId]
ALTER TABLE HashtagNews  WITH CHECK ADD  CONSTRAINT [FK_HashtagNews_News_NewsId] FOREIGN KEY(NewsId)
REFERENCES News (Id)
ON DELETE CASCADE
ALTER TABLE HashtagNews CHECK CONSTRAINT [FK_HashtagNews_News_NewsId]

--Комментарий
CREATE TABLE NewsComment(
	Id uniqueidentifier NOT NULL,
	Content nvarchar(max) NOT NULL,
	AuthorId uniqueidentifier NOT NULL,
	NewsId uniqueidentifier NOT NULL,
	CreatedAt datetime2(7) NOT NULL,
	UpdatedAt datetime2(7) NOT NULL,
 CONSTRAINT [PK_NewsComment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE NewsComment ADD  CONSTRAINT [DFHuuyuW8Ndkv0wWuFOKvZp9Fs]  DEFAULT (newid()) FOR Id
ALTER TABLE NewsComment ADD  DEFAULT (GETDATE()) FOR CreatedAt
ALTER TABLE NewsComment ADD  DEFAULT (GETDATE()) FOR UpdatedAt
ALTER TABLE NewsComment WITH CHECK ADD  CONSTRAINT [FK_NewsComment_Employee_AuthorId] FOREIGN KEY(AuthorId)
REFERENCES Employee (Id)
ON DELETE CASCADE
ALTER TABLE NewsComment CHECK CONSTRAINT [FK_NewsComment_Employee_AuthorId]
ALTER TABLE NewsComment  WITH CHECK ADD  CONSTRAINT [FK_NewsComment_News_NewsId] FOREIGN KEY(NewsId)
REFERENCES News (Id)
ALTER TABLE NewsComment CHECK CONSTRAINT [FK_NewsComment_News_NewsId]

--Лайк
CREATE TABLE LikedNews(
	Id uniqueidentifier NOT NULL,
	EmployeeId uniqueidentifier NOT NULL,
	NewsId uniqueidentifier NOT NULL,
CONSTRAINT [PK_LikedNews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE LikedNews ADD  CONSTRAINT [DFHuuyuW8Ndkv0wWuFOKvZu9Fs]  DEFAULT (newid()) FOR Id
ALTER TABLE LikedNews WITH CHECK ADD  CONSTRAINT [FK_LikedNews_Employee_EmployeeId] FOREIGN KEY(EmployeeId)
REFERENCES Employee (Id)
ON DELETE CASCADE
ALTER TABLE LikedNews CHECK CONSTRAINT [FK_LikedNews_Employee_EmployeeId]
ALTER TABLE LikedNews  WITH CHECK ADD  CONSTRAINT [FK_LikedNews_News_NewsId] FOREIGN KEY(NewsId)
REFERENCES News (Id)
ALTER TABLE LikedNews CHECK CONSTRAINT [FK_LikedNews_News_NewsId]

--Картинки
CREATE TABLE Picture(
	Id uniqueidentifier NOT NULL,
	[Name] nvarchar(500) NOT NULL,
	[Description] nvarchar(max) NULL,
	ByteAsString nvarchar(max) NULL,
	[Format] nvarchar(250) NULL,
	AuthorId uniqueidentifier NOT NULL,
	NewsId uniqueidentifier NOT NULL,
	CreatedAt datetime2(7) NOT NULL,
	Size int NOT NULL,
	[Data] varbinary(max) NOT NULL,
 CONSTRAINT [PK_Picture] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE Picture ADD  CONSTRAINT [DFHuuyuW8Ndkh0wWuFOKvZp9Fs]  DEFAULT (newid()) FOR Id
ALTER TABLE Picture ADD  DEFAULT (GETDATE()) FOR CreatedAt
ALTER TABLE Picture WITH CHECK ADD  CONSTRAINT [FK_Picture_Employee_AuthorId] FOREIGN KEY(AuthorId)
REFERENCES Employee (Id)
ON DELETE CASCADE
ALTER TABLE Picture CHECK CONSTRAINT [FK_Picture_Employee_AuthorId]
ALTER TABLE Picture  WITH CHECK ADD  CONSTRAINT [FK_Picture_News_NewsId] FOREIGN KEY(NewsId)
REFERENCES News (Id)
ALTER TABLE Picture CHECK CONSTRAINT [FK_Picture_News_NewsId]
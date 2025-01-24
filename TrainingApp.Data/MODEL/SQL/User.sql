-- THIS FILE CREATE USER TABLE --

GO

-- In SQL Server, square brackets [] are used to delimit identifiers, such as table names, column names, and other database objects --
-- VARCHAR (Variable Character) stores non-Unicode characters. It's typically used to store ASCII data.
-- NVARCHAR (National Variable Character) stores Unicode characters. It can store any character from any language,
-- making it more versatile for globalized applications.
-- VARCHAR uses 1 byte per character for ASCII data.
-- NVARCHAR uses 2 bytes per character, allowing it to support a wider range of characters.

CREATE TABLE [User]
(
	[Id] int IDENTITY(1,1),
	[Guid] uniqueidentifier,
	[Name] varchar(50),
	[Description] varchar(100),
	[CreateDate] DateTime,
	[UpdateDate] DateTime,
	[DeleteDate] DateTime,
	[Email] varchar(100),
	[UserName] varchar(50),
	[Age] tinyint,
	[Password] nvarchar(100),
	[VersionId] smallint NOT NULL
)

GO

ALTER TABLE [User]
ADD CONSTRAINT PK_USERTABLE PRIMARY KEY ([Id],[VersionId])

GO

ALTER TABLE [User]
ADD CONSTRAINT DEFAULT_CREATE_DATE DEFAULT GETDATE() FOR [CreateDate]

-- PROCEDURE --

GO

CREATE PROCEDURE [SelectUserById](@Id int) AS 
BEGIN
	select * from [User] where [User].Id = @Id AND [User].DeleteDate IS NULL AND [User].VersionId = (select MAX([User].VersionId) from [User] where [User].Id = @Id)
END

GO

CREATE PROCEDURE [SelectUserByGuid](@Guid uniqueidentifier) AS 
BEGIN
	select * from [User] where [User].Guid = @Guid AND [User].DeleteDate IS NULL AND [User].VersionId = (select MAX([User].VersionId) from [User] where [User].Guid = @Guid)
END

GO 

CREATE PROCEDURE [SelectUserList] AS
BEGIN
	SELECT * FROM
	(
		SELECT *, (ROW_NUMBER() OVER(partition by [User].Id order by [User].VersionId DESC)) as rowNumber FROM [User] WHERE [User].DeleteDate IS NULL
	) as ResultPartitionUser
	WHERE rowNumber = 1
END

GO

CREATE PROCEDURE [CreateUser]
(
	@Id int = NULL,
	@Guid uniqueidentifier,
	@Name varchar(50),
	@Description varchar(100),
	@UpdateDate datetime,
	@DeleteDate datetime,
	@Email varchar(100),
	@UserName varchar(50),
	@Age tinyint,
	@Password varchar(100),
	@VersionId smallint
) AS
BEGIN
	IF (@UpdateDate IS NULL)
	BEGIN
		INSERT INTO [User] (Guid, Name, Description, UpdateDate, DeleteDate, Email, UserName, Age, Password, VersionId)
		VALUES (@Guid, @Name, @Description, @UpdateDate, @DeleteDate, @Email, @UserName, @Age, @Password, @VersionId)
	END
	ELSE
	BEGIN
		SET IDENTITY_INSERT [User] ON;
		INSERT INTO [User] (Id, Guid, Name, Description, UpdateDate, DeleteDate, Email, UserName, Age, Password, VersionId)
		VALUES (@Id, @Guid, @Name, @Description, @UpdateDate, @DeleteDate, @Email, @UserName, @Age, @Password, @VersionId)
		SET IDENTITY_INSERT [User] OFF;
	END
END

GO

CREATE PROCEDURE [DeleteUser] (@Id int, @DeleteDate datetime) AS
BEGIN
	UPDATE [User] SET [DeleteDate] = @DeleteDate WHERE [User].Id = @Id
END


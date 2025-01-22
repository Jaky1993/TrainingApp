GO

CREATE PROCEDURE [SelectMaxVersionId]
(
	@UserId int
)
AS
BEGIN

SELECT MAX([VersionId]) FROM [User] WHERE [User].Id = @UserId

END


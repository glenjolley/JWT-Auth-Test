CREATE PROCEDURE [dbo].[FindByUsername]
	@username nvarchar(50)
AS
	SELECT 
		*
	FROM
		Users u
	WHERE
		u.Username = @username
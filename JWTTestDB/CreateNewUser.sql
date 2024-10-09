CREATE PROCEDURE [dbo].[CreateNewUser]
	@userid int,
	@username NVARCHAR(50),
	@password NVARCHAR(255),
	@salt NVARCHAR(255),
	@role NVARCHAR(50)
AS
	INSERT INTO Users ([Username],[Password],[Salt],[Role]) VALUES (@username, @password, @salt, @role)

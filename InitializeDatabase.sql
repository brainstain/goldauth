CREATE DATABASE GoldsteinAuth
GO
USE GoldsteinAuth;
GO
CREATE TABLE dbo.Users_
(
Id uniqueidentifier DEFAULT NEWSEQUENTIALID() PRIMARY KEY,
UserName varchar(50),
Email varchar(100),
LastLogin datetime,
IsActive bit,
IsLocked bit,
IsReset bit,
LoginAttemptCount int,
PasswordHash varbinary(64),
Salt varbinary(64),
ResetToken varbinary(64) DEFAULT null
);
GO
CREATE UNIQUE INDEX IX_Users_UserName
	ON dbo.Users_ (UserName);
GO
CREATE UNIQUE INDEX IX_Users_Email
	ON dbo.Users_ (Email);
GO
CREATE PROCEDURE dbo.AddUser
	@UserName varchar(50),
	@Email varchar(100),
	@LastLogin datetime,
	@IsActive bit,
	@IsLocked bit,
	@LoginAttemptCount int,
	@PasswordHash varbinary(64),
	@Salt varbinary(64)
AS
BEGIN
	INSERT INTO [GoldsteinAuth].[dbo].[Users_]
	(Id, UserName, Email, LastLogin, IsActive, IsLocked, LoginAttemptCount, PasswordHash, Salt)
	VALUES(DEFAULT, @UserName, @Email, @LastLogin, @IsActive, @IsLocked, @LoginAttemptCount, @PasswordHash, @Salt)
END
GO
CREATE PROCEDURE dbo.GetUserByUserName
	@SearchUserName varchar(50),
	@UserName varchar(50) OUTPUT,
	@Email varchar(100) OUTPUT,
	@LastLogin datetime OUTPUT,
	@IsActive bit OUTPUT,
	@IsLocked bit OUTPUT,
	@LoginAttemptCount int OUTPUT,
	@PasswordHash varbinary(64) OUTPUT,
	@Salt varbinary(64) OUTPUT
AS
BEGIN
	SELECT 	
		@UserName = UserName,
		@Email = Email,
		@LastLogin = LastLogin,
		@IsActive = IsActive,
		@IsLocked = IsLocked,
		@LoginAttemptCount = LoginAttemptCount,
		@PasswordHash = PasswordHash,
		@Salt = Salt
	FROM
		Users_ u
	WHERE
		u.UserName = @SearchUserName
END
GO
CREATE PROCEDURE dbo.UpdateUserByUserName
	@UserName varchar(50) OUTPUT,
	@Email varchar(100) OUTPUT,
	@LastLogin datetime OUTPUT,
	@IsActive bit OUTPUT,
	@IsLocked bit OUTPUT,
	@LoginAttemptCount int OUTPUT,
	@PasswordHash varbinary(64) OUTPUT,
	@Salt varbinary(64) OUTPUT
AS
BEGIN
	UPDATE Users_
	SET 	
		Email = @Email,
		LastLogin = @LastLogin,
		IsActive = @IsActive,
		IsLocked = @IsLocked,
		LoginAttemptCount = @LoginAttemptCount,
		PasswordHash = @PasswordHash,
		Salt = @Salt 
	WHERE
		UserName = @UserName
END
GO
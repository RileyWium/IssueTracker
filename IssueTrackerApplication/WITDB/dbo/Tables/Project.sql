CREATE TABLE [dbo].[Project]
(
	[ProjectID] INT NOT NULL PRIMARY KEY IDENTITY, 
    --[UserID] INT NOT NULL, implement once login is added
    [ProjectName] NVARCHAR(50) NOT NULL, 
    [ProjectKey] NCHAR(3) NOT NULL
)

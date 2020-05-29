CREATE TABLE [dbo].[Issue]
(
	[IssueID] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProjectID] INT NOT NULL,
	[IssueName] NVARCHAR(50) NOT NULL,
	--[IssueAssignee] USERNAME NOT NULL,
	--[IssueReporter] USERNAME NOT NULL,
	--[IssueStatus] BOOLEAN NOT NULL,
	--[IssuePriority] int? maybe customtype NOT NULL, look in tutorial
	[IssueDescription] NVARCHAR(350) NULL
)

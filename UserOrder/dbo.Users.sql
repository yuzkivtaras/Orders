CREATE TABLE [dbo].[Users] (
    [UserID]      INT           IDENTITY (1, 1) NOT NULL,
    [Login]       NVARCHAR (20) NOT NULL,
    [Password]    NVARCHAR (50) NOT NULL,
    [FirstName]   NVARCHAR (40) NOT NULL,
    [LastName]    NVARCHAR (40) NOT NULL,
    [DateOfBirth] DATETIME2 (7) NOT NULL,
    [Gender]      NVARCHAR (1)  NOT NULL CHECK (Gender IN ('M', 'F')),
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserID] ASC),
	CONSTRAINT [UQ_Users_Login] UNIQUE ([Login])
);


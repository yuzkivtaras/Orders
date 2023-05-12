CREATE PROCEDURE [dbo].[CreateUserOrdersDB]
AS
BEGIN
    IF DB_ID('UserOrders') IS NULL
    BEGIN
        CREATE DATABASE UserOrders;
        ALTER DATABASE UserOrders SET RECOVERY SIMPLE;
    END;
    GO
    
    USE UserOrders;
    GO
    
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
    GO
    
    CREATE TABLE [dbo].[Orders] (
        [OrderID]          INT             IDENTITY (1, 1) NOT NULL,
        [UserID]           INT             NOT NULL,
        [OrderDate]        DATETIME2 (7)   NOT NULL,
        [OrderCost]        DECIMAL (18, 2) NOT NULL,
        [ItemsDescription] NVARCHAR (1000) NOT NULL,
        [ShippingAddress]  NVARCHAR (1000) NOT NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderID] ASC),
        CONSTRAINT [FK_Orders_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE CASCADE,
	    CONSTRAINT [CK_Orders_OrderCost] CHECK (OrderCost >= 0)
    );
    GO
    
    CREATE NONCLUSTERED INDEX [IX_Orders_UserID]
        ON [dbo].[Orders]([UserID] ASC);
    GO
END;

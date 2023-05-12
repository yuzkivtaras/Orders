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


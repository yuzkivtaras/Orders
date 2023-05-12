-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
USE UserOrders;

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE FillUsersAndOrders
AS
BEGIN
  INSERT INTO Users (Login, Password, FirstName, LastName, DateOfBirth, Gender)
  VALUES
  ('user1', 'password1', 'John', 'Doe', '1990-01-01', 'M'),
  ('user2', 'password2', 'Jane', 'Doe', '1991-02-02', 'F'),
  ('user3', 'password3', 'Bob', 'Smith', '1992-03-03', 'M'),
  ('user4', 'password4', 'Alice', 'Smith', '1993-04-04', 'F'),
  ('user5', 'password5', 'Mike', 'Johnson', '1994-05-05', 'M'),
  ('user6', 'password6', 'Sara', 'Johnson', '1995-06-06', 'F'),
  ('user7', 'password7', 'Tom', 'Williams', '1996-07-07', 'M'),
  ('user8', 'password8', 'Emily', 'Williams', '1997-08-08', 'F'),
  ('user9', 'password9', 'David', 'Brown', '1998-09-09', 'M'),
  ('user10', 'password10', 'Emma', 'Brown', '1999-10-10', 'F');

  INSERT INTO Orders (UserID, OrderDate, OrderCost, ItemsDescription, ShippingAddress)
  VALUES
  (1, '2023-05-12', 100.00, 'Item 1', '123 Main St'),
  (1, '2023-05-13', 50.00, 'Item 2', '456 Elm St'),
  (2, '2023-05-14', 75.00, 'Item 3', '789 Oak St'),
  (2, '2023-05-15', 200.00, 'Item 4', '321 Maple St'),
  (3, '2023-05-16', 150.00, 'Item 5', '654 Pine St'),
  (3, '2023-05-17', 300.00, 'Item 6', '987 Cedar St'),
  (4, '2023-05-18', 125.00, 'Item 7', '654 Pine St'),
  (4, '2023-05-19', 75.00, 'Item 8', '321 Maple St'),
  (5, '2023-05-20', 50.00, 'Item 9', '123 Main St'),
  (5, '2023-05-21', 100.00, 'Item 10', '456 Elm St');
END;

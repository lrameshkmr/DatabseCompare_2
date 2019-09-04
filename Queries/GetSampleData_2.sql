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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.GetSampleData_2
AS
BEGIN
	
	CREATE TABLE #Customers
	(
		ID INT,
		Name varchar(100),
		Salary INT
	)

	INSERT INTO #Customers(ID,Name,Salary)
	Values (1,'Ramesh',10000),
			(2,'Suryanaryana',20000),
			(3,'Girija',30000),
			(4,'Jagadeeswari',40000)
	
	SELECT * FROM #Customers

END
GO

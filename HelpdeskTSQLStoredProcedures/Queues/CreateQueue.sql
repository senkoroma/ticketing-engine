USE [Helpdesk]
GO
/****** Object:  StoredProcedure [dbo].[CreateQueue]    Script Date: 11/28/2007 15:08:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateQueue] 
	-- Add the parameters for the stored procedure here
	@Name nvarchar(50),
	@Description nvarchar(Max),
	@CreationDate datetime,
	@Creator nvarchar(50), 
	@IsActive bit
AS
BEGIN
	
	--Insert the values into the database.
	INSERT INTO Queue ([name], description, creation_date, creator, is_active)
		VALUES (@Name, @Description, @CreationDate, @Creator, @IsActive);
	--Return the row Id to the scalar query.
	SELECT SCOPE_IDENTITY();

END


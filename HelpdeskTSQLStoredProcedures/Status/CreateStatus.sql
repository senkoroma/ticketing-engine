USE [Helpdesk]
GO
/****** Object:  StoredProcedure [dbo].[CreateStatus]    Script Date: 11/28/2007 15:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateStatus]
	@Name nvarchar(50),
	@Description nvarchar(MAX),
	@StatusOrder int,
	@IsActive bit
AS
BEGIN

	INSERT INTO Status ([name], description, status_order, is_active) 
		VALUES (@Name, @Description, UpdateStatusOrder(@StatusOrder), @IsActive);

	SELECT SCOPE_IDENTITY();	

END



USE [Helpdesk]
GO
/****** Object:  StoredProcedure [dbo].[CreateModule]    Script Date: 11/28/2007 15:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateModule] 
	@Name nvarchar(50),
	@Description nvarchar(Max),
	@QueueId int,
	@IsActive bit
AS
BEGIN

	INSERT INTO Module ([name], description, queue_id, is_active) VALUES
		('asdf', 'adf', 1, 1);
	
	SELECT SCOPE_IDENTITY();

END


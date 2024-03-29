USE [Helpdesk]
GO
/****** Object:  StoredProcedure [dbo].[EditModule]    Script Date: 11/28/2007 15:08:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[EditModule] 
	-- Add the parameters for the stored procedure here
	@Name nvarchar(50),
	@Description nvarchar(Max),
	@QueueId int,
	@IsActive bit,
	@ModuleId int
AS
BEGIN
	
	UPDATE Module SET [name] = @name, description = @Description, queue_id = @QueueId, is_active = @IsActive
		WHERE Module.module_id = @ModuleId;

END

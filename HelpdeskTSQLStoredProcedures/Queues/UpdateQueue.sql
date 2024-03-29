USE [Helpdesk]
GO
/****** Object:  StoredProcedure [dbo].[UpdateQueue]    Script Date: 11/28/2007 15:10:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateQueue]
	-- Add the parameters for the stored procedure here
	@Name nvarchar(50),
	@Description nvarchar(Max),
	@Creator nvarchar(50),
	@CreationDate datetime,
	@QueueId int,
	@IsActive bit

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [Queue] SET [name] = @Name, description = @Description, 
		creator = @creator, creation_date = @CreationDate,
		is_active = @IsActive
		WHERE [Queue].queue_id = @QueueId;

END

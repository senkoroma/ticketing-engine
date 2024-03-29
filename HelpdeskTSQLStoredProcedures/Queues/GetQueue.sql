USE [Helpdesk]
GO
/****** Object:  StoredProcedure [dbo].[GetQueue]    Script Date: 11/28/2007 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetQueue]
	-- Add the parameters for the stored procedure here
	@QueueId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [name], description, creation_date, creator, is_active
		FROM Queue
		WHERE Queue.queue_id = @QueueId;

END

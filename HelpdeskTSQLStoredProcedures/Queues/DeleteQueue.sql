USE [Helpdesk]
GO
/****** Object:  StoredProcedure [dbo].[DeleteQueue]    Script Date: 11/28/2007 15:08:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteQueue]
	-- Add the parameters for the stored procedure here
	@QueueId int
AS
BEGIN
	
	DELETE FROM [Queue] WHERE [Queue].queue_id = @QueueId;

END

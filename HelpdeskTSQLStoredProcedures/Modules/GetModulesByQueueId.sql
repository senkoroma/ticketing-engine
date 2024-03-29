USE [Helpdesk]
GO
/****** Object:  StoredProcedure [dbo].[GetModulesByQueueId]    Script Date: 11/28/2007 15:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetModulesByQueueId]
	@QueueId int
	
AS
BEGIN
	
	SELECT module_id, [name], description, queue_id, is_active
		FROM Module
		WHERE Module.queue_id = @QueueId;

END

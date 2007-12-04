USE [Helpdesk]
GO
/****** Object:  StoredProcedure [dbo].[GetModule]    Script Date: 11/28/2007 15:09:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetModule]
	-- Add the parameters for the stored procedure here
	@ModuleId int
AS
BEGIN
	
	SELECT [name], description, queue_id, is_active
		FROM Module
		WHERE Module.module_id = @ModuleId;

END

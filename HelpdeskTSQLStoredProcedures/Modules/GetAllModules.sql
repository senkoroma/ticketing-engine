USE [Helpdesk]
GO
/****** Object:  StoredProcedure [dbo].[GetAllModules]    Script Date: 11/28/2007 15:09:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllModules]

	
AS
BEGIN
	
	SELECT module_id, [name], description, queue_id, is_active
		FROM Module;

END

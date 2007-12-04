USE [Helpdesk]
GO
/****** Object:  StoredProcedure [dbo].[GetAllEnabledQueues]    Script Date: 11/28/2007 15:09:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllEnabledQueues]
	
AS
BEGIN
	SELECT queue_id, [name], description, creation_date, creator, is_active
		FROM [Queue]
	WHERE is_active = 1;

END

USE [Helpdesk]
GO
/****** Object:  StoredProcedure [dbo].[GetAllQueues]    Script Date: 11/28/2007 15:09:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllQueues] 

AS
BEGIN

	SELECT queue_id, [name], description, creation_date, creator, is_active
		FROM [Queue]

END

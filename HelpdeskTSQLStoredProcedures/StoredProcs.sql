IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateModuleTest]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CreateModuleTest]
AS
EXTERNAL NAME [HelpdeskStoredProcedures].[StoredProcedures].[CreateModuleTest]' 
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'AutoDeployed' , N'SCHEMA',N'dbo', N'PROCEDURE',N'CreateModuleTest', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'AutoDeployed', @value=N'yes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'CreateModuleTest'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'SqlAssemblyFile' , N'SCHEMA',N'dbo', N'PROCEDURE',N'CreateModuleTest', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'SqlAssemblyFile', @value=N'CreateModule.cs' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'CreateModuleTest'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'SqlAssemblyFileLine' , N'SCHEMA',N'dbo', N'PROCEDURE',N'CreateModuleTest', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'SqlAssemblyFileLine', @value=10 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'CreateModuleTest'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateQueue]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateQueue] 
	-- Add the parameters for the stored procedure here
	@Name nvarchar(50),
	@Description nvarchar(Max),
	@CreationDate datetime,
	@Creator nvarchar(50), 
	@IsActive bit
AS
BEGIN
	
	--Insert the values into the database.
	INSERT INTO Queue ([name], description, creation_date, creator, is_active)
		VALUES (@Name, @Description, @CreationDate, @Creator, @IsActive);
	--Return the row Id to the scalar query.
	SELECT SCOPE_IDENTITY();

END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetQueue]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateQueue]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteQueue]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllQueues]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllEnabledQueues]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateModule]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
		(@Name, @Description, @QueueId, @IsActive);
	
	SELECT SCOPE_IDENTITY();

END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetModule]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EditModule]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllModules]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetModulesByQueueId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateStatus]
	@Name nvarchar(50),
	@Description nvarchar(MAX),
	@StatusOrder int,
	@IsActive bit
AS
BEGIN

	--Declare some vars for operations.
	DECLARE @CurrentId int, 
		@CurrentStatusOrder int, 
		@Iter int, 
		@TotalRows int, 
		@NewInsertionOrder int

	--Select the current Status Items into a temp table.
	SELECT status_id, status_order INTO #MyTempStatusTable FROM Status
	WHERE is_active = 1 
	ORDER BY status_order

	SET @TotalRows = @@ROWCOUNT

	
	--Set sqlserver to return only 1 row from a selection and select the 1st row in the set.
	SET ROWCOUNT 1	
	SELECT @CurrentId = status_id, @CurrentStatusOrder = status_order from #MyTempStatusTable
	ORDER BY status_order	

	--Set our Iterator equal to 1.
	SET @Iter = 1

	--Start the loop to update all the items.
	WHILE @@ROWCOUNT <> 0
	BEGIN

		IF @StatusOrder = @CurrentStatusOrder
		BEGIN
			--Set the insertion order of the new row...
			SET @NewInsertionOrder = @CurrentStatusOrder
			
			--Increase the Iter by 1
			SET @Iter = @Iter + 1

		END

		/*IF @Iter = @TotalRows
			BEGIN
			
			--Set the insertion order of the new row...
			SET @NewInsertionOrder = @Iter + 1

			END*/

		--Set the sort order, starting from 1.
		UPDATE Status SET @StatusOrder = @Iter

		--Increment the Iter
		SET @Iter = @Iter + 1	
			
		--Delete the current record from the temp table...
		DELETE FROM #MyTempStatusTable WHERE status_id = @CurrentId
		
		--Set sqlserver to return only 1 row from a selection and select the 1st row in the set.
		SET ROWCOUNT 1	
		SELECT @CurrentId = status_id, @CurrentStatusOrder = status_order from #MyTempStatusTable
		ORDER BY status_order

	END

	INSERT INTO Status ([name], description, status_order, is_active) 
		VALUES (@Name, @Description, @NewInsertionOrder, @IsActive);

	SELECT SCOPE_IDENTITY();	

END


' 
END

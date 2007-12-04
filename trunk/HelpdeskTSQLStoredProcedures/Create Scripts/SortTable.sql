USE [Helpdesk]
GO
/****** Object:  StoredProcedure [dbo].[Test]    Script Date: 12/03/2007 15:42:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SortTable] 
	-- Add the parameters for the stored procedure here
	@TableName nvarchar(50),
	@SortColumnName nvarchar(50),
	@IndexColumnName nvarchar(50),
	@RequestedIndex int
AS
BEGIN

	DECLARE @InsertIntoTemp nvarchar(MAX),
			@UpdateQuery nvarchar(MAX),
			@DeleteQuery nvarchar(MAX);
	DECLARE	@Id int,
			@Iter int, 
			@WasUpdated bit;

	-- Set the vars.
	SET @Iter = 1;
	SET @WasUpdated = 0;

	--Initial query to get all the values into a temp table.
	SET @InsertIntoTemp = 'INSERT INTO #Temp(Id) SELECT ' + @IndexColumnName + 
		' FROM ' + @Tablename + ' ORDER BY ' + @SortColumnName + ';';

	CREATE TABLE #Temp(Id int );

	--Execute the queries, inserting the rows into the temp tabel and setting teh 1st @Id var.
	SET ROWCOUNT 0;
	EXEC sp_executesql @InsertIntoTemp;

	-- Set @Id equal to the 1st id column in the newly sorted table.
	SET ROWCOUNT 1; --Makes sure only 1 row is returned.
	SELECT @Id = Id FROM #Temp	;
	
	WHILE @@rowcount <> 0
	BEGIN	
		IF @Iter = @RequestedIndex
		BEGIN
			-- Trigger the flag that we found the place for our new record
			-- Increment the iter by one, leaving a place for the new row.
			SET @WasUpdated = 1;
			SET @Iter = @Iter + 1;
		END

		--Update the current row to its new sort order...
		SET @UpdateQuery = 'UPDATE ' + @TableName + ' SET ' + @SortColumnName + ' = ' + CAST(@Iter AS nvarchar(5)) +
			' WHERE ' + @IndexColumnName + ' = ' + CAST(@Id AS nvarchar(5));
				
		--Update the sort or of the table row, based on the currnet value of @Id.
		EXEC sp_executesql @UpdateQuery ;

		-- Increment the iter for the next row.
		SET @Iter = @Iter + 1;
		
		SET @DeleteQuery = 'DELETE FROM #Temp WHERE Id = ' + Cast(@Id as nvarchar(5));

		--  Delete the processed record from the TempTable...
		EXEC sp_executesql @DeleteQuery;
	
		-- Do another selection and continue the loop until there are no more rows.
		SELECT @Id = Id FROM #Temp;

	END

	-- If the loop did not match the requested id with any of the current ones, place the 
	-- new record at the end of the list.
	IF @WasUpdated = 0
	BEGIN
		SET @RequestedIndex = @Iter;
	END

	DROP TABLE #Temp;

	RETURN @RequestedIndex

END

 
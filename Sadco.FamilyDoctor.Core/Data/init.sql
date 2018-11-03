EXEC sp_configure 'show advanced options', 1
RECONFIGURE;
GO

EXEC sp_configure 'Ole Automation Procedures', 1; 
RECONFIGURE; 
GO 

EXEC sp_configure 'xp_cmdshell', 1
RECONFIGURE;
GO

CREATE PROCEDURE CreateFile
	@root_path NVARCHAR(1000),
	@path NVARCHAR(3000),
	@file_name sysname,
	@var VARBINARY(MAX)
AS
BEGIN
	DECLARE @cmd NVARCHAR(4000)
	SET @cmd = 'mkdir "' + @root_path + '\' + @path + '"'
	EXEC master..xp_cmdshell @cmd, NO_OUTPUT

	select @cmd

	DECLARE @Obj INT
	DECLARE @Path2OutFile NVARCHAR (4000);
	Set @Path2OutFile = @root_path + '\' + @path + '\' + @file_name;

	EXEC sp_OACreate 'ADODB.Stream' ,@Obj OUTPUT;
    EXEC sp_OASetProperty @Obj ,'Type',1;
    EXEC sp_OAMethod @Obj,'Open';
    EXEC sp_OAMethod @Obj,'Write', NULL, @var;
    EXEC sp_OAMethod @Obj,'SaveToFile', NULL, @Path2OutFile, 2;
    EXEC sp_OAMethod @Obj,'Close';
    EXEC sp_OADestroy @Obj;
END
GO

CREATE PROCEDURE [dbo].[GetFile]
	@path NVARCHAR(4000),
	@binaryout varbinary(Max) OUTPUT
AS
BEGIN
	DECLARE @hr int
	declare @ADODBStream int
	DECLARE @v_buffer VARBINARY(8000)
	Declare @v_file as Varchar(MAX);
	DECLARE @Size int
	DECLARE @countBlocks int
	Set @v_file = REPLACE(@path,'/','\');

	EXEC @hr = sp_OACreate 'ADODB.stream', @ADODBStream OUT
	EXEC @hr = sp_OASetProperty @ADODBStream,'type', 1
	EXEC @hr = sp_OAMethod @ADODBStream, 'open', null
	EXEC @hr = sp_OAMethod @ADODBStream, 'LoadFromFile', null, @v_file
	EXEC @hr = sp_OAGetProperty @ADODBStream, 'Size', @Size OUTPUT
	Set @countBlocks = @Size/8000
	Print @countBlocks

	EXEC @hr = sp_OAMethod @ADODBStream, 'Read', @v_buffer out, 8000

	Declare @i int
	Set @i = 0

	Set @binaryout = @v_buffer
	WHILE @hr >= 0 and @i < @countBlocks
	BEGIN
		EXEC @hr = sp_OAMethod @ADODBStream, 'Read', @v_buffer out, 8000
		Set @binaryout = @binaryout + @v_buffer
		Set @i = @i + 1
		Print @hr
	END

	EXECUTE @hr = sp_OADestroy @v_file
	EXECUTE @hr = sp_OADestroy @ADODBStream

	RETURN(0);
END
GO

CREATE PROCEDURE [dbo].[RemoveFile]
	@path sysname
AS
BEGIN
	DECLARE @cmd sysname
	SET @cmd = 'del "' + REPLACE(@path,'/','\');
	EXEC master..xp_cmdshell @cmd, NO_OUTPUT
END

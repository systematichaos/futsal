-- Description : Selects all ACTIVE the aspnet user roles
-- Usuage :  Used under admin section to view user roles (search, filter, sort, pagination )
-- CreatedBy : pinkzen

CREATE PROCEDURE [dbo].[UserRoles_Search]
(
	@Id int = null
	,@Name VARCHAR(MAX)  = NULL 
    ,@AssignedByUser VARCHAR(MAX) = NULL
	,@AssignedDate varchar(max)=null
    ,@PageNumber INT = 0 
    ,@PageSize INT = 25
    ,@OrderBy NVARCHAR(MAX) = N'AspNetRoles.Id DESC'
	)
	AS
	BEGIN
	SET NOCOUNT ON;
	IF @OrderBy IS NULL 
		OR (@OrderBy NOT IN ('AspNetUsers.UserName', 'AspNetUsers.UserName ASC', 'AspNetUsers.UserName DESC')
		AND @OrderBy NOT IN ('AspNetRoles.Name', 'AspNetRoles.Name ASC', 'AspNetRoles.Name DESC')
		AND @OrderBy NOT IN ('AspNetRoles.Id', 'AspNetRoles.Id ASC', 'AspNetRoles.Id DESC')
		AND @OrderBy NOT IN ('AspNetUserRoles.CreatedDate', 'AspNetUserRoles.CreatedDate ASC', 'AspNetUserRoles.CreatedDate DESC')	
		)                
	BEGIN 
		SET @OrderBy = N'AspNetUserRoles.Id ASC'
	END
	

	--print @OrderBy
	--build sql statement 
	DECLARE @SqlSelect NVARCHAR(MAX) = N'
	SELECT
		AspNetUserRoles.Id as UserRoleId, 
		AspNetUserRoles.UserId,
		AspNetUserRoles.RoleId as RoleId,
		COUNT(*) OVER () AS [RowCount],
		CEILING(CONVERT(DECIMAL(15,2), (COUNT(*) OVER())/CONVERT(DECIMAL(15,2), @PageSize))) AS [Count]
	FROM   
		AspNetUserRoles
    '

	--print @SqlSelect
	--print 'this is it'


	IF @OrderBy LIKE 'AspNetRoles.Name%' OR @OrderBy LIKE 'AspNetRoles.Id%'
	BEGIN 
		SET @SqlSelect += N'
			JOIN dbo.AspNetRoles 
				ON AspNetUserRoles.RoleId= AspNetRoles.Id 
		'
	END
	IF @OrderBy LIKE 'AspNetUsers.UserName%'
	BEGIN 
		SET @SqlSelect += N'
			 join AspNetUsers  
				on AspNetUsers.Id =AspNetUserRoles.UserId
		'
	END	
	--IF @OrderBy LIKE 'AspNetUserRoles.CreatedDate%'
	--BEGIN 
	--	SET @SqlSelect += N'
	--		join AspNetUserRoles
	--			on AspNetUsers.Id= AspNetUserRoles.UserId 
	--	'
	--END


	DECLARE @SqlWhere NVARCHAR(MAX) = N'
        WHERE 
			AspNetUserRoles.IsDeleted = 0 
        '
	IF @Id IS NOT NULL 
    BEGIN 
        SET @SqlWhere += N'
			AND AspNetUsers.Id  = @Id
        '
    END 

	IF @Name IS NOT NULL 
    BEGIN 
        SET @SqlWhere += N'
			AND AspNetRoles.[Name] LIKE  @Name ''%''
        '
    END	

	IF @AssignedByUser IS NOT NULL 
    BEGIN 
        SET @SqlWhere += N'
			AND au.UserName LIKE  @AssignedByUser ''%''
        '
    END
	
	IF @AssignedDate IS NOT NULL 
    BEGIN 
        SET @SqlWhere += N'
			AND AspNetUserRoles.CreatedDate LIKE  @AssignedDate ''%''
        '
    END


		DECLARE @Sql NVARCHAR(MAX) = @SqlSelect + @SqlWhere + N'
        ORDER BY 
        ' + @OrderBy + '
        OFFSET (@PageNumber * @PageSize) ROW FETCH NEXT @PageSize ROWS ONLY'




	--PRINT @Sql
	DECLARE @tbl TABLE
    (
		Id INT NOT NULL IDENTITY(1,1), 
		UserRoleId INT NOT NULL PRIMARY KEY	
		,UserId INT NOT NULL 
		,RoleId INT NOT NULL
       ,[RowCount] INT
       ,[Count] INT
    )
    INSERT INTO @tbl
    EXEC sp_executesql 
        @Sql, 
        N'@Id INT, @Name VARCHAR(50), @AssignedByUser VARCHAR(MAX), @AssignedDate VARCHAR(MAX) , @PageNumber INT, @PageSize INT', 
        @Id, @Name,@AssignedByUser,@AssignedDate, @PageNumber, @PageSize;
            
			--print 'yo this is it'
			--select * from @tbl

	SELECT
		AspNetUsers.Id							AS UserId
		,AspNetUserRoles.Id					AS UserRoleId
		,AspNetRoles.Id							AS RoleId
		,AspNetRoles.[Name]					as [Role] 
		,AspNetUsers.UserName
		,CONCAT(AspNetUsers.FirstName,' ',ISNULL(AspNetUsers.MiddleInitial,' '),AspNetUsers.MiddleInitial,AspNetUsers.LastName)as FullName		
		,AspNetUsers.UserName				as RoleAssignedBy
		,AspNetUserRoles.CreatedDate	as RoleAssignedDate 
		,Search.[RowCount]
		,Search.[Count]							AS [PageCount]
	FROM               
		@tbl AS Search
		INNER JOIN dbo.AspNetUsers 
			ON AspNetUsers.Id = Search.UserId 	
				left JOIN dbo.AspNetUserRoles
			ON  Search.UserRoleId =AspNetUserRoles.Id 
		 left JOIN dbo.AspNetRoles
			ON Search.RoleId= AspNetRoles.Id 
	ORDER BY 
		Search.Id
		



END
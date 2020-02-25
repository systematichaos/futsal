-- Description : Assigns User to a Role
-- Usuage :  Used under admin section to assign user to a role
-- CreatedBy : pinkzen

CREATE PROCEDURE [dbo].[UserRole_Assign]
	@UserId int = 0,
	@RoleName varchar(50),
	@LoggedInUserId int = 0,
	@Result  BIT OUTPUT
AS
BEGIN 
	SET NOCOUNT ON;
	DECLARE @RoleId int
	DECLARE @UserRoleId int

	-- get role id based on the role provided 
	select @RoleId = Id from AspNetRoles where [Name] = @RoleName 
	
	if ISNULL(@RoleId,'') <>''
	begin 
			IF NOT EXISTS(SELECT * FROM AspNetUserRoles where UserId=@UserId and RoleId =@RoleId and IsDeleted= 0) --  if user role is assigned and not deleted then, proceed
			begin 

			 --insert into aspnetusertoles 
			 Insert into dbo.AspNetUserRoles (UserId,RoleId,CreatedByUser)
			 values(@UserId, @RoleId,@LoggedInUserId)
			 set @UserRoleId = scope_identity()
				  

				if(ISNULL(@UserRoleId,'')<>'')				
				begin 
				SET @Result = 1
				end 
				else 
				Set @Result= 0 
						
			end 
	end 
	Return @Result
END
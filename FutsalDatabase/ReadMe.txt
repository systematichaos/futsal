

Below are some of the precaution to be taken while dealing with the database project. 
Please follow the instructions provided below
1. Make sure you get the Latest from the repo
2. Please do schema compare with your local db instance 
3. Run the following scripts


. Insert into static tables: 


insert into Futsal.dbo.Applications ([Name])
values('Futsal')

insert into AspNetRoles (Name, CreatedByUser )
values	 ('Admin', 1)




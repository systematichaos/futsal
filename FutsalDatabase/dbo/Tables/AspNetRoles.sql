CREATE TABLE [dbo].[AspNetRoles] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (256) NOT NULL,
    [IsDeleted]         BIT            CONSTRAINT [DF_AspNetRoles_ON_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedDate]       SMALLDATETIME  CONSTRAINT [DF_AspNetRoles_ON_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [CreatedByUser]     INT            NOT NULL,
    [LastUpdatedDate]   SMALLDATETIME  CONSTRAINT [DF_AspNetRoles_ON_LastUpdatedDate] DEFAULT (getdate()) NOT NULL,
    [LastUpdatedByUser] INT            NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);


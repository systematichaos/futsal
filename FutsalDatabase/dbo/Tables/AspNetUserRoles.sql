CREATE TABLE [dbo].[AspNetUserRoles] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [UserId]            INT           NOT NULL,
    [RoleId]            INT           NOT NULL,
    [IsDeleted]         BIT           CONSTRAINT [DF_AspNetUserRoles_ON_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedDate]       SMALLDATETIME CONSTRAINT [DF_AspNetUserRoles_ON_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [CreatedByUser]     INT           NULL,
    [LastUpdatedDate]   SMALLDATETIME CONSTRAINT [DF_AspNetUserRoles_ON_LastUpdatedDate] DEFAULT (getdate()) NOT NULL,
    [LastUpdatedByUser] INT           NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserRoles]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[AspNetUserRoles]([RoleId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [index_name]
    ON [dbo].[AspNetUserRoles]([UserId] ASC, [RoleId] ASC);


CREATE TABLE [dbo].[ApplicationLogs] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Username]           NVARCHAR (100) NULL,
    [Controller]         NVARCHAR (200) NULL,
    [Action]             NVARCHAR (100) NULL,
    [IPAddress]          NVARCHAR (100) NULL,
    [UserAgent]          NVARCHAR (100) NULL,
    [UserId]             INT            NULL,
    [ImpersonatedUserId] INT            NULL,
    [ApplicationId]      INT            NULL,
    [SessionId]          NVARCHAR (MAX) NULL,
    [LogDateTime]        SMALLDATETIME  NULL,
    CONSTRAINT [PK_dbo.ApplicationLogs] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ApplicationLogs_dbo.Applications_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[Applications] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.ApplicationLogs_dbo.AspNetUsers_ImpersonatedUserId] FOREIGN KEY ([ImpersonatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.ApplicationLogs_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


CREATE TABLE [dbo].[UserHistory] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [UserId]      INT            NOT NULL,
    [Application] NVARCHAR (MAX) NULL,
    [MachineName] NVARCHAR (MAX) NULL,
    [Properties]  NVARCHAR (MAX) NULL,
    [Operation]   NVARCHAR (MAX) NULL,
    [TimeStamp]   DATETIME       NOT NULL,
    [Exception]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserHistory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.UserHistory_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


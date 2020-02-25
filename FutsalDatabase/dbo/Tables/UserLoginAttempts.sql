CREATE TABLE [dbo].[UserLoginAttempts] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [UserId]          INT            NOT NULL,
    [PasswordHash]    NVARCHAR (MAX) NOT NULL,
    [IPAddress]       NVARCHAR (MAX) NULL,
    [Reason]          NVARCHAR (MAX) NULL,
    [ApplicationId]   INT            NULL,
    [AttemptDateTime] DATE           NULL,
    [IsSuccessful]    BIT            NOT NULL,
    CONSTRAINT [PK_dbo.UserLoginAttempts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.UserLoginAttempts_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


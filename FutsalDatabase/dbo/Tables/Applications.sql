CREATE TABLE [dbo].[Applications] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.Applications] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    [FirstName]            NVARCHAR (50)  NOT NULL,
    [MiddleInitial]        NVARCHAR (50)  NULL,
    [LastName]             NVARCHAR (50)  NOT NULL,
    [Address1]             NVARCHAR (200) NOT NULL,
    [Address2]             NVARCHAR (200) NULL,
    [City]                 NVARCHAR (200) NOT NULL,
    [District]             NVARCHAR (200) NOT NULL,
    [Zone]                 NVARCHAR (50)  NULL,
    [Province]             NVARCHAR (50)  NOT NULL,
    [PostalCode]           INT            NULL,
    [IsUserActive]         BIT            NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [AlternatePhoneNumber] NVARCHAR (10)  NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [StartDate]            SMALLDATETIME  NULL,
    [EndDate]              SMALLDATETIME  NULL,
    [IsDeleted]            BIT            CONSTRAINT [DF_AspNetUsers_ON_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedDate]          SMALLDATETIME  CONSTRAINT [DF_AspNetUsers_ON_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastUpdatedDate]      SMALLDATETIME  CONSTRAINT [DF_AspNetUsers_ON_LastUpdatedDate] DEFAULT (getdate()) NOT NULL,
    [LastUpdatedByUser]    INT            NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([UserName] ASC);


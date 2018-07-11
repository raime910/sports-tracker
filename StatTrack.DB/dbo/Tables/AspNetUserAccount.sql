CREATE TABLE [dbo].[AspNetUserAccount] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [RegisterDate]           DATETIME       NOT NULL,
    [AccountStatus]          INT            NOT NULL,
    [EmailConfirmationToken] NVARCHAR (MAX) NULL,
    [PasswordResetToken]     NVARCHAR (MAX) NULL,
    [Email]                  NVARCHAR (MAX) NULL,
    [EmailConfirmed]         BIT            NOT NULL,
    [PasswordHash]           NVARCHAR (MAX) NULL,
    [SecurityStamp]          NVARCHAR (MAX) NULL,
    [PhoneNumber]            NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed]   BIT            NOT NULL,
    [TwoFactorEnabled]       BIT            NOT NULL,
    [LockoutEndDateUtc]      DATETIME       NULL,
    [LockoutEnabled]         BIT            NOT NULL,
    [AccessFailedCount]      INT            NOT NULL,
    [UserName]               NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserAccount] PRIMARY KEY CLUSTERED ([Id] ASC)
);


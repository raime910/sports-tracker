CREATE TABLE [dbo].[Teams] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Code]           NVARCHAR (10)  NOT NULL,
    [Name]           NVARCHAR (50)  NOT NULL,
    [Description]    NVARCHAR (MAX) NOT NULL,
    [LockedSince]    DATETIME       NULL,
    [DateCreated]    DATETIME       NOT NULL,
    [OwnerId]        INT            NOT NULL,
    [LockedByUserId] INT            NULL,
    [BannerImageUrl] NVARCHAR (MAX) NULL,
    [LogoImageUrl]   NVARCHAR (MAX) NULL,
    [Status]         INT            NOT NULL,
    CONSTRAINT [PK_dbo.Teams] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Teams_dbo.AspNetUserAccount_LockedByUserId] FOREIGN KEY ([LockedByUserId]) REFERENCES [dbo].[AspNetUserAccount] ([Id]),
    CONSTRAINT [FK_dbo.Teams_dbo.AspNetUserAccount_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[AspNetUserAccount] ([Id])
);














GO
CREATE NONCLUSTERED INDEX [IX_LockedByUserId]
    ON [dbo].[Teams]([LockedByUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_OwnerId]
    ON [dbo].[Teams]([OwnerId] ASC);


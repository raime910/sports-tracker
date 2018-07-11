CREATE TABLE [dbo].[Sports] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Code]           NVARCHAR (10)  NOT NULL,
    [Name]           NVARCHAR (50)  NOT NULL,
    [Description]    NVARCHAR (MAX) NOT NULL,
    [Status]         INT            NOT NULL,
    [SportTypeId]    INT            NOT NULL,
    [LockedSince]    DATETIME       NULL,
    [LockedByUserId] INT            NULL,
    CONSTRAINT [PK_dbo.Sports] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Sports_dbo.AspNetUserAccount_LockedByUserId] FOREIGN KEY ([LockedByUserId]) REFERENCES [dbo].[AspNetUserAccount] ([Id]),
    CONSTRAINT [FK_dbo.Sports_dbo.SportTypes_SportTypeId] FOREIGN KEY ([SportTypeId]) REFERENCES [dbo].[SportTypes] ([Id]) ON DELETE CASCADE
);












GO
CREATE NONCLUSTERED INDEX [IX_SportTypeId]
    ON [dbo].[Sports]([SportTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LockedByUserId]
    ON [dbo].[Sports]([LockedByUserId] ASC);


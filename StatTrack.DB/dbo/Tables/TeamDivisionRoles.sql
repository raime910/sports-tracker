CREATE TABLE [dbo].[TeamDivisionRoles] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (50)  NOT NULL,
    [Description]    NVARCHAR (MAX) NOT NULL,
    [SportId]        INT            NOT NULL,
    [LockedSince]    DATETIME       NULL,
    [LockedByUserId] INT            NULL,
    [AccessLevel]    INT            NOT NULL,
    [Status]         INT            NOT NULL,
    CONSTRAINT [PK_dbo.TeamDivisionRoles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.TeamDivisionRoles_dbo.AspNetUserAccount_LockedByUserId] FOREIGN KEY ([LockedByUserId]) REFERENCES [dbo].[AspNetUserAccount] ([Id]),
    CONSTRAINT [FK_dbo.TeamDivisionRoles_dbo.Sports_SportId] FOREIGN KEY ([SportId]) REFERENCES [dbo].[Sports] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_LockedByUserId]
    ON [dbo].[TeamDivisionRoles]([LockedByUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SportId]
    ON [dbo].[TeamDivisionRoles]([SportId] ASC);


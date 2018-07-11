CREATE TABLE [dbo].[Leagues] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Code]           NVARCHAR (10)  NOT NULL,
    [Name]           NVARCHAR (50)  NOT NULL,
    [Description]    NVARCHAR (MAX) NOT NULL,
    [LeagueTypeId]   INT            NOT NULL,
    [SportId]        INT            NOT NULL,
    [LockedSince]    DATETIME       NULL,
    [LockedByUserId] INT            NULL,
    [Status]         INT            NOT NULL,
    CONSTRAINT [PK_dbo.Leagues] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Leagues_dbo.AspNetUserAccount_LockedByUserId] FOREIGN KEY ([LockedByUserId]) REFERENCES [dbo].[AspNetUserAccount] ([Id]),
    CONSTRAINT [FK_dbo.Leagues_dbo.LeagueTypes_LeagueTypeId] FOREIGN KEY ([LeagueTypeId]) REFERENCES [dbo].[LeagueTypes] ([Id]),
    CONSTRAINT [FK_dbo.Leagues_dbo.Sports_SportId] FOREIGN KEY ([SportId]) REFERENCES [dbo].[Sports] ([Id]) ON DELETE CASCADE
);












GO
CREATE NONCLUSTERED INDEX [IX_SportId]
    ON [dbo].[Leagues]([SportId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LockedByUserId]
    ON [dbo].[Leagues]([LockedByUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LeagueTypeId]
    ON [dbo].[Leagues]([LeagueTypeId] ASC);


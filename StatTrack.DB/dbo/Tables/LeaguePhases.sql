CREATE TABLE [dbo].[LeaguePhases] (
    [Id]             INT            NOT NULL,
    [Code]           NVARCHAR (10)  NOT NULL,
    [Name]           NVARCHAR (50)  NOT NULL,
    [Description]    NVARCHAR (MAX) NOT NULL,
    [LeagueTypeId]   INT            NOT NULL,
    [LockedSince]    DATETIME       NULL,
    [LockedByUserId] INT            NULL,
    CONSTRAINT [PK_dbo.LeaguePhases] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.LeaguePhases_dbo.AspNetUserAccount_LockedByUserId] FOREIGN KEY ([LockedByUserId]) REFERENCES [dbo].[AspNetUserAccount] ([Id]),
    CONSTRAINT [FK_dbo.LeaguePhases_dbo.LeagueTypes_LeagueTypeId] FOREIGN KEY ([LeagueTypeId]) REFERENCES [dbo].[LeagueTypes] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_LockedByUserId]
    ON [dbo].[LeaguePhases]([LockedByUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LeagueTypeId]
    ON [dbo].[LeaguePhases]([LeagueTypeId] ASC);


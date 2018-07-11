CREATE TABLE [dbo].[LeagueSets] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [LeagueId] INT NOT NULL,
    [PhaseId]  INT NOT NULL,
    [Status]   INT NOT NULL,
    CONSTRAINT [PK_dbo.LeagueSets] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.LeagueSets_dbo.LeaguePhases_PhaseId] FOREIGN KEY ([PhaseId]) REFERENCES [dbo].[LeaguePhases] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.LeagueSets_dbo.Leagues_LeagueId] FOREIGN KEY ([LeagueId]) REFERENCES [dbo].[Leagues] ([Id]) ON DELETE CASCADE
);








GO
CREATE NONCLUSTERED INDEX [IX_PhaseId]
    ON [dbo].[LeagueSets]([PhaseId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LeagueId]
    ON [dbo].[LeagueSets]([LeagueId] ASC);


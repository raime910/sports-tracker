CREATE TABLE [dbo].[TeamDivisions] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (50) NULL,
    [SportId] INT           NOT NULL,
    [TeamId]  INT           NOT NULL,
    CONSTRAINT [PK_dbo.TeamDivisions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.TeamDivisions_dbo.Sports_SportId] FOREIGN KEY ([SportId]) REFERENCES [dbo].[Sports] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.TeamDivisions_dbo.Teams_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_TeamId]
    ON [dbo].[TeamDivisions]([TeamId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SportId]
    ON [dbo].[TeamDivisions]([SportId] ASC);


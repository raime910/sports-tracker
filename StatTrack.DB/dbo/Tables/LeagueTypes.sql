CREATE TABLE [dbo].[LeagueTypes] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Code]        NVARCHAR (10)  NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [Status]      INT            NOT NULL,
    CONSTRAINT [PK_dbo.LeagueTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);








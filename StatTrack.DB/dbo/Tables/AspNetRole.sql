CREATE TABLE [dbo].[AspNetRole] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [AccessLevel] INT            NOT NULL,
    [Name]        NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetRole] PRIMARY KEY CLUSTERED ([Id] ASC)
);


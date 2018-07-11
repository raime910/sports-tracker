CREATE TABLE [dbo].[Countries] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (100) NOT NULL,
    [Code]      NVARCHAR (2)   NOT NULL,
    [FlagThumb] NVARCHAR (50)  NULL,
    CONSTRAINT [PK_dbo.Countries] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Code]
    ON [dbo].[Countries]([Code] ASC);


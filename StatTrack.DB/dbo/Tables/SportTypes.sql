CREATE TABLE [dbo].[SportTypes] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Code]           NVARCHAR (10)  NOT NULL,
    [Name]           NVARCHAR (50)  NOT NULL,
    [Description]    NVARCHAR (MAX) NOT NULL,
    [LockedSince]    DATETIME       NULL,
    [LockedByUserId] INT            NULL,
    CONSTRAINT [PK_dbo.SportTypes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.SportTypes_dbo.AspNetUserAccount_LockedByUserId] FOREIGN KEY ([LockedByUserId]) REFERENCES [dbo].[AspNetUserAccount] ([Id])
);










GO
CREATE NONCLUSTERED INDEX [IX_LockedByUserId]
    ON [dbo].[SportTypes]([LockedByUserId] ASC);


CREATE TABLE [dbo].[AspNetUserProfile] (
    [UserId]              INT            NOT NULL,
    [FirstName]           NVARCHAR (50)  NOT NULL,
    [LastName]            NVARCHAR (50)  NOT NULL,
    [BirthDate]           DATE           NULL,
    [LastUpdateDate]      DATE           NULL,
    [SubscribeNewsletter] BIT            NOT NULL,
    [CountryId]           INT            NULL,
    [Bio]                 NVARCHAR (MAX) NULL,
    [AvatarUrl]           NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserProfile] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserProfile_dbo.AspNetUserAccount_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUserAccount] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserProfile]([UserId] ASC);


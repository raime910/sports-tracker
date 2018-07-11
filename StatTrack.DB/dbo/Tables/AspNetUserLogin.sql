CREATE TABLE [dbo].[AspNetUserLogin] (
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [LoginProvider] NVARCHAR (MAX) NULL,
    [UserId]        INT            NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogin] PRIMARY KEY CLUSTERED ([ProviderKey] ASC),
    CONSTRAINT [FK_dbo.AspNetUserLogin_dbo.AspNetUserAccount_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUserAccount] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserLogin]([UserId] ASC);


CREATE TABLE [dbo].[AspNetUserRole] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRole] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRole_dbo.AspNetRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRole] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRole_dbo.AspNetUserAccount_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUserAccount] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[AspNetUserRole]([RoleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserRole]([UserId] ASC);


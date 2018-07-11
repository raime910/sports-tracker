CREATE TABLE [dbo].[TeamDivisionMemberships] (
    [TeamDivisionId] INT      NOT NULL,
    [UserId]         INT      NOT NULL,
    [TeamRoleId]     INT      NOT NULL,
    [Status]         INT      NOT NULL,
    [LockedSince]    DATETIME NULL,
    [LockedByUserId] INT      NULL,
    CONSTRAINT [PK_dbo.TeamDivisionMemberships] PRIMARY KEY CLUSTERED ([TeamDivisionId] ASC, [UserId] ASC, [TeamRoleId] ASC, [Status] ASC),
    CONSTRAINT [FK_dbo.TeamDivisionMemberships_dbo.AspNetUserAccount_LockedByUserId] FOREIGN KEY ([LockedByUserId]) REFERENCES [dbo].[AspNetUserAccount] ([Id]),
    CONSTRAINT [FK_dbo.TeamDivisionMemberships_dbo.AspNetUserAccount_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUserAccount] ([Id]),
    CONSTRAINT [FK_dbo.TeamDivisionMemberships_dbo.AspNetUserProfile_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUserProfile] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.TeamDivisionMemberships_dbo.TeamDivisionRoles_TeamRoleId] FOREIGN KEY ([TeamRoleId]) REFERENCES [dbo].[TeamDivisionRoles] ([Id]),
    CONSTRAINT [FK_dbo.TeamDivisionMemberships_dbo.TeamDivisions_TeamDivisionId] FOREIGN KEY ([TeamDivisionId]) REFERENCES [dbo].[TeamDivisions] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_LockedByUserId]
    ON [dbo].[TeamDivisionMemberships]([LockedByUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TeamRoleId]
    ON [dbo].[TeamDivisionMemberships]([TeamRoleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[TeamDivisionMemberships]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TeamDivisionId]
    ON [dbo].[TeamDivisionMemberships]([TeamDivisionId] ASC);


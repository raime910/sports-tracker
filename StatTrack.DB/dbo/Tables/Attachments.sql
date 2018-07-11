CREATE TABLE [dbo].[Attachments] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [FileName]     NVARCHAR (100)  NULL,
    [FileSizeKb]   INT             NOT NULL,
    [Content]      VARBINARY (MAX) NULL,
    [UploadedById] INT             NOT NULL,
    CONSTRAINT [PK_dbo.Attachments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Attachments_dbo.AspNetUserAccount_UploadedById] FOREIGN KEY ([UploadedById]) REFERENCES [dbo].[AspNetUserAccount] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UploadedById]
    ON [dbo].[Attachments]([UploadedById] ASC);


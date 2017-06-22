-- <Migration ID="6f2ff2ab-3205-4f1a-874c-0a0b6344233a" />
GO

PRINT N'Adding foreign keys to [dbo].[Runners]'
GO
ALTER TABLE [dbo].[Runners] ADD CONSTRAINT [FK_Category_Runners] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([CategoryId])
GO

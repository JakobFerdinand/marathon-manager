-- <Migration ID="a18b2f4a-c580-49ab-8f96-d77271ff8f4d" />
GO

PRINT N'Adding foreign keys to [dbo].[Runners]'
GO
ALTER TABLE [dbo].[Runners] ADD CONSTRAINT [FK_Category_Runners] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([CategoryId])
GO

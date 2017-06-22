-- <Migration ID="35411700-a195-41b8-a54e-0f97d4c718ab" />
GO

PRINT N'Altering [dbo].[Runners]'
GO
ALTER TABLE [dbo].[Runners] ADD
[Startnumber] [int] NULL
GO

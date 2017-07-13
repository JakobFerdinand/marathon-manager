-- <Migration ID="4edbf4ba-6647-4eab-a690-d7a78d2c4d0e" />
GO

PRINT N'Dropping constraints from [dbo].[ChangeLogs]'
GO
ALTER TABLE [dbo].[ChangeLogs] DROP CONSTRAINT [PK_ChangeLogs]
GO
PRINT N'Rebuilding [dbo].[ChangeLogs]'
GO
CREATE TABLE [dbo].[RG_Recovery_1_ChangeLogs]
(
[ChangeLogId] [int] NOT NULL IDENTITY(1, 1),
[EntityId] [varchar] (10) NOT NULL,
[ChangeTime] [datetime2] NOT NULL,
[TypeName] [varchar] (50) NOT NULL,
[PropertyName] [varchar] (50) NOT NULL,
[OldValue] [nvarchar] (max) NULL,
[NewValue] [nvarchar] (max) NULL
)
GO
SET IDENTITY_INSERT [dbo].[RG_Recovery_1_ChangeLogs] ON
GO
INSERT INTO [dbo].[RG_Recovery_1_ChangeLogs]([ChangeLogId], [EntityId], [ChangeTime], [TypeName], [PropertyName], [OldValue], [NewValue]) SELECT [ChangeLogId], [EntityId], [ChangeTime], [TypeName], [PropertyName], [OldValue], [NewValue] FROM [dbo].[ChangeLogs]
GO
SET IDENTITY_INSERT [dbo].[RG_Recovery_1_ChangeLogs] OFF
GO
DROP TABLE [dbo].[ChangeLogs]
GO
EXEC sp_rename N'[dbo].[RG_Recovery_1_ChangeLogs]', N'ChangeLogs', N'OBJECT'
GO

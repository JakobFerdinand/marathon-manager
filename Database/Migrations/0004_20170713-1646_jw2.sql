-- <Migration ID="bbbefcf4-b1d8-46dd-95c3-d3ab73cb7650" />
GO

PRINT N'Creating [dbo].[ChangeLogs]'
GO
CREATE TABLE [dbo].[ChangeLogs]
(
[ChangeLogId] [int] NOT NULL,
[EntityId] [varchar] (10) NOT NULL,
[ChangeTime] [datetime2] NOT NULL,
[TypeName] [varchar] (50) NOT NULL,
[PropertyName] [varchar] (50) NOT NULL,
[OldValue] [nvarchar] (max) NULL,
[NewValue] [nvarchar] (max) NULL
)
GO
PRINT N'Creating primary key [PK_ChangeLogs] on [dbo].[ChangeLogs]'
GO
ALTER TABLE [dbo].[ChangeLogs] ADD CONSTRAINT [PK_ChangeLogs] PRIMARY KEY CLUSTERED  ([ChangeLogId])
GO

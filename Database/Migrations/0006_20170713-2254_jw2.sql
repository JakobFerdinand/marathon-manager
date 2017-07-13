-- <Migration ID="7133f006-3118-4b09-a341-1a4da25c3d63" />
GO

PRINT N'Dropping foreign keys from [dbo].[Runners]'
GO
ALTER TABLE [dbo].[Runners] DROP CONSTRAINT [FK_Category_Runners]
GO
PRINT N'Dropping constraints from [dbo].[Runners]'
GO
ALTER TABLE [dbo].[Runners] DROP CONSTRAINT [PK_Runners]
GO
PRINT N'Rebuilding [dbo].[Runners]'
GO
CREATE TABLE [dbo].[RG_Recovery_1_Runners]
(
[RunnerId] [int] NOT NULL IDENTITY(1, 1),
[Startnumber] [int] NULL,
[Firstname] [nvarchar] (50) NOT NULL,
[Lastname] [nvarchar] (50) NOT NULL,
[SportsClub] [nvarchar] (200) NULL,
[YearOfBirth] [int] NOT NULL,
[ChipId] [nchar] (10) NULL,
[TimeAtDestination] [datetime2] NULL,
[RunningTime] [time] NULL,
[CategoryId] [int] NOT NULL,
[Gender] [tinyint] NOT NULL,
[City] [nvarchar] (50) NULL,
[Email] [nvarchar] (50) NULL
)
GO
SET IDENTITY_INSERT [dbo].[RG_Recovery_1_Runners] ON
GO
INSERT INTO [dbo].[RG_Recovery_1_Runners]([RunnerId], [Startnumber], [Firstname], [Lastname], [SportsClub], [YearOfBirth], [ChipId], [TimeAtDestination], [RunningTime], [CategoryId]) SELECT [RunnerId], [Startnumber], [Firstname], [Lastname], [SportsClub], [YearOfBirth], [ChipId], [TimeAtDestination], [RunningTime], [CategoryId] FROM [dbo].[Runners]
GO
SET IDENTITY_INSERT [dbo].[RG_Recovery_1_Runners] OFF
GO
DECLARE @idVal BIGINT
SELECT @idVal = IDENT_CURRENT(N'[dbo].[Runners]')
IF @idVal IS NOT NULL
    DBCC CHECKIDENT(N'[dbo].[RG_Recovery_1_Runners]', RESEED, @idVal)
GO
DROP TABLE [dbo].[Runners]
GO
EXEC sp_rename N'[dbo].[RG_Recovery_1_Runners]', N'Runners', N'OBJECT'
GO
PRINT N'Creating primary key [PK_Runners] on [dbo].[Runners]'
GO
ALTER TABLE [dbo].[Runners] ADD CONSTRAINT [PK_Runners] PRIMARY KEY CLUSTERED  ([RunnerId])
GO
PRINT N'Adding foreign keys to [dbo].[Runners]'
GO
ALTER TABLE [dbo].[Runners] ADD CONSTRAINT [FK_Category_Runners] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([CategoryId])
GO

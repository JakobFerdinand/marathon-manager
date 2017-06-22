-- <Migration ID="bf921440-fee0-492d-97a0-c95cd1c11362" />
GO

PRINT N'Dropping foreign keys from [dbo].[Runners]'
GO
ALTER TABLE [dbo].[Runners] DROP CONSTRAINT [FK_Category_Runners]
GO
PRINT N'Dropping constraints from [dbo].[Categories]'
GO
ALTER TABLE [dbo].[Categories] DROP CONSTRAINT [PK_Categories]
GO
PRINT N'Dropping constraints from [dbo].[Runners]'
GO
ALTER TABLE [dbo].[Runners] DROP CONSTRAINT [PK_Runners]
GO
PRINT N'Rebuilding [dbo].[Categories]'
GO
CREATE TABLE [dbo].[RG_Recovery_1_Categories]
(
[CategoryId] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (50) NOT NULL,
[Starttime] [datetime2] NULL,
[PlannedStarttime] [datetime2] NOT NULL
)
GO
SET IDENTITY_INSERT [dbo].[RG_Recovery_1_Categories] ON
GO
INSERT INTO [dbo].[RG_Recovery_1_Categories]([CategoryId], [Name], [Starttime], [PlannedStarttime]) SELECT [CategoryId], [Name], [Starttime], [PlannedStarttime] FROM [dbo].[Categories]
GO
SET IDENTITY_INSERT [dbo].[RG_Recovery_1_Categories] OFF
GO
DROP TABLE [dbo].[Categories]
GO
EXEC sp_rename N'[dbo].[RG_Recovery_1_Categories]', N'Categories', N'OBJECT'
GO
PRINT N'Creating primary key [PK_Categories] on [dbo].[Categories]'
GO
ALTER TABLE [dbo].[Categories] ADD CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED  ([CategoryId])
GO
PRINT N'Rebuilding [dbo].[Runners]'
GO
CREATE TABLE [dbo].[RG_Recovery_2_Runners]
(
[RunnerId] [int] NOT NULL IDENTITY(1, 1),
[Firstname] [nvarchar] (50) NOT NULL,
[Lastname] [nvarchar] (50) NOT NULL,
[YearOfBirth] [int] NOT NULL,
[ChipId] [nchar] (10) NULL,
[TimeAtDestination] [datetime2] NULL,
[RunningTime] [datetime2] NULL,
[CategoryId] [int] NOT NULL
)
GO
SET IDENTITY_INSERT [dbo].[RG_Recovery_2_Runners] ON
GO
INSERT INTO [dbo].[RG_Recovery_2_Runners]([RunnerId], [Firstname], [Lastname], [YearOfBirth], [ChipId], [TimeAtDestination], [RunningTime], [CategoryId]) SELECT [RunnerId], [Firstname], [Lastname], [YearOfBirth], [ChipId], [TimeAtDestination], [RunningTime], [CategoryId] FROM [dbo].[Runners]
GO
SET IDENTITY_INSERT [dbo].[RG_Recovery_2_Runners] OFF
GO
DROP TABLE [dbo].[Runners]
GO
EXEC sp_rename N'[dbo].[RG_Recovery_2_Runners]', N'Runners', N'OBJECT'
GO
PRINT N'Creating primary key [PK_Runners] on [dbo].[Runners]'
GO
ALTER TABLE [dbo].[Runners] ADD CONSTRAINT [PK_Runners] PRIMARY KEY CLUSTERED  ([RunnerId])
GO
PRINT N'Adding foreign keys to [dbo].[Runners]'
GO
ALTER TABLE [dbo].[Runners] ADD CONSTRAINT [FK_Category_Runners] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([CategoryId])
GO

-- <Migration ID="122062cf-5534-481a-9be5-88de174d9346" />
GO

PRINT N'Creating [dbo].[Categories]'
GO
CREATE TABLE [dbo].[Categories]
(
[CategoryId] [int] NOT NULL,
[Name] [nvarchar] (50) NOT NULL,
[Starttime] [datetime2] NULL,
[PlannedStarttime] [datetime2] NOT NULL
)
GO
PRINT N'Creating primary key [PK_Categories] on [dbo].[Categories]'
GO
ALTER TABLE [dbo].[Categories] ADD CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED  ([CategoryId])
GO
PRINT N'Creating [dbo].[Runners]'
GO
CREATE TABLE [dbo].[Runners]
(
[RunnerId] [int] NOT NULL,
[Firstname] [nvarchar] (50) NOT NULL,
[Lastname] [nvarchar] (50) NOT NULL,
[YearOfBirth] [int] NOT NULL,
[ChipId] [nchar] (10) NULL,
[TimeAtDestination] [datetime2] NULL,
[RunningTime] [datetime2] NULL,
[CategoryId] [int] NOT NULL
)
GO
PRINT N'Creating primary key [PK_Runners] on [dbo].[Runners]'
GO
ALTER TABLE [dbo].[Runners] ADD CONSTRAINT [PK_Runners] PRIMARY KEY CLUSTERED  ([RunnerId])
GO

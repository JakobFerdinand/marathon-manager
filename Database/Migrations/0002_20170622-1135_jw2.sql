-- <Migration ID="285406ef-2bd4-41fd-a2b6-2629df0abf04" />
GO

PRINT N'Creating [dbo].[Runners]'
GO
CREATE TABLE [dbo].[Runners]
(
[RunnerId] [int] NOT NULL IDENTITY(1, 1),
[Startnumber] [int] NULL,
[Firstname] [nvarchar] (50) NOT NULL,
[Lastname] [nvarchar] (50) NOT NULL,
[YearOfBirth] [int] NOT NULL,
[ChipId] [nchar] (10) NULL,
[TimeAtDestination] [datetime2] NULL,
[RunningTime] [time] NULL,
[CategoryId] [int] NOT NULL
)
GO
PRINT N'Creating primary key [PK_Runners] on [dbo].[Runners]'
GO
ALTER TABLE [dbo].[Runners] ADD CONSTRAINT [PK_Runners] PRIMARY KEY CLUSTERED  ([RunnerId])
GO

-- <Migration ID="d35b5996-9d1e-4538-8933-f6de18590747" />
GO

PRINT N'Creating [dbo].[GetRankByGenderAndCategory]'
GO

CREATE PROCEDURE [dbo].[GetRankByGenderAndCategory]
	@gender int,
	@categoryId int
AS
BEGIN
	
	SET NOCOUNT ON;

    select
		Rank() over (Order by RunningTime) as Rang
		, Startnumber = Startnumber 
		, Vorname = Firstname
		, Nachname = Lastname
		, Zeit = RunningTime
		, Verein = SportsClub
		from Runners
		where Gender = @gender and CategoryId = @categoryId
		order by RunningTime asc
END
GO
PRINT N'Creating [dbo].[GetOldestRunnerByGender]'
GO

CREATE PROCEDURE [dbo].[GetOldestRunnerByGender]
	@gender int
AS
BEGIN
	
	SET NOCOUNT ON;

    select Top(1)
		Startnumber = Startnumber 
		, Vorname = Firstname
		, Nachname = Lastname
		, Geburtsjahr = YearOfBirth
		, Zeit = RunningTime
		from Runners
		where YearOfBirth > 1900 And Gender = @gender
		Order by YearOfBirth asc
END
GO
PRINT N'Creating [dbo].[GetYoungestRunnerByGender]'
GO

CREATE PROCEDURE [dbo].[GetYoungestRunnerByGender]
	@gender int
AS
BEGIN
	
	SET NOCOUNT ON;

    select Top(1)
		Startnumber = Startnumber 
		, Vorname = Firstname
		, Nachname = Lastname
		, Geburtsjahr = YearOfBirth
		, Zeit = RunningTime
		from Runners
		where YearOfBirth > 1900 And Gender = @gender
		Order by YearOfBirth desc
END
GO
PRINT N'Creating [dbo].[GetFastestRunnerFromMiningByGender]'
GO

CREATE PROCEDURE [dbo].[GetFastestRunnerFromMiningByGender]
	@gender int
AS
BEGIN
	
	SET NOCOUNT ON;

    select Top(1)
		Startnumber = Startnumber 
		, Vorname = Firstname
		, Nachname = Lastname
		, Geburtsjahr = YearOfBirth
		, Zeit = RunningTime
		from Runners
		where City like '%Mining%' and Gender = @gender
		Order by RunningTime asc
END
GO
PRINT N'Creating [dbo].[GetFastestRunnerFromEringByGender]'
GO

CREATE PROCEDURE [dbo].[GetFastestRunnerFromEringByGender]
	@gender int
AS
BEGIN
	
	SET NOCOUNT ON;

    select Top(1)
		Startnumber = Startnumber 
		, Vorname = Firstname
		, Nachname = Lastname
		, Geburtsjahr = YearOfBirth
		, Zeit = RunningTime
		from Runners
		where City like '%Ering%' and Gender = @gender
		Order by RunningTime asc
END
GO
PRINT N'Creating [dbo].[GetSportsClubsWithMostRunners]'
GO

CREATE PROCEDURE [dbo].[GetSportsClubsWithMostRunners]
AS
BEGIN
	
	SET NOCOUNT ON;

    select 
		Rank() over (Order by Count(RunnerId) desc) as Rang
		, Anzahl = Count(RunnerId)
		, Verein = SportsClub
		from Runners
		Where SportsClub != ''
		Group by SportsClub
		order by Anzahl desc
END
GO

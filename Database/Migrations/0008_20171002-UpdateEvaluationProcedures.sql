-- <Migration ID="6cc7baeb-653e-4b38-be60-21d78f26f91e" />
GO

PRINT N'Dropping [dbo].[GetYoungestRunnerByGender]'
GO
DROP PROCEDURE [dbo].[GetYoungestRunnerByGender]
GO
PRINT N'Creating [dbo].[GetRankByCategory]'
GO

CREATE PROCEDURE [dbo].[GetRankByCategory]
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
		where CategoryId = @categoryId
		order by RunningTime asc
END
GO

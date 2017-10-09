use MarathonManager

select * from Runners
	where CategoryId = 14
select * from Categories

update Runners set RunningTime = '00:39:31.0000000' Where Startnumber = 45

select
	Rank() over (Order by RunningTime) as Rang
	, Startnumber = Startnumber 
	, Vorname = Firstname
	, Nachname = Lastname
	, Zeit = RunningTime
	from Runners
	where Gender = 1 and CategoryId = 12
	order by RunningTime asc

	GetRankByGenderAndCategory 1, 13
	GetRankByCategory 14
	GetOldestRunnerByGender 2
	GetYoungestRunnerByGender 2
	GetFastestRunnerFromMiningByGender 2

select Top(1)
	Startnumber = Startnumber 
	, Vorname = Firstname
	, Nachname = Lastname
	, Geburtsjahr = YearOfBirth
	, Zeit = RunningTime
	from Runners
	where City like '%Mining%' and Gender = 2
	Order by RunningTime asc

select 
	Rank() over (Order by Count(RunnerId) desc) as Rang
	, Anzahl = Count(RunnerId)
	, Verein = SportsClub
	from Runners
	Where SportsClub != ''
	Group by SportsClub
	order by Anzahl desc


	 -- IP. 192.168.255.58

	 Grant exec on dbo.[GetFastestRunnerFromMiningByGenderAndCategory] to public

	 Select * from ChangeLogs order by ChangeTime desc
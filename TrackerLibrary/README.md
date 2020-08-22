# C# App Start To Finish 

https://www.youtube.com/playlist?list=PLLWMQd6PeGY3t63w-8MMIjIyYS7MsFcCi

# Sections

### 6. Class Library: https://www.youtube.com/watch?v=GPlgjb6AXDw

### 7. Form Building: https://www.youtube.com/watch?v=h8HknjkQ9SI

### 8. SQL Database Design: https://www.youtube.com/watch?v=6ixfTsrhPgM

https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15

Login to SSMS just `localhost` works but not `localhost:1433`.  Do not know why.

```sql
create database Tournaments;
```

```sql
USE [Tournaments]
GO

/****** Object:  Table [dbo].[Prizes]    Script Date: 7/9/2020 10:30:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Prizes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PlaceNumber] [int] NOT NULL,
	[PlaceName] [nvarchar](50) NOT NULL,
	[PrizeAmount] [money] NOT NULL,
	[PrizePercentage] [float] NOT NULL
) ON [PRIMARY]
GO
```

**Notes**

- right click on Tables for New Table 
- right click on particular Table and Design
- Edit Top 200 Rows when right-click on Table to modify/insert rows
- Identity Specification set to Yes for primary key field

**Stored Procedures Example**

```
CREATE PROCEDURE dbo.spTestPerson_GetByLastName
  @LastName nvarchar(100)
AS
BEGIN
  SET NOCOUNT ON;

  select *
  from dbo.TestPerson
  where LastName = @LastName;
END
```

**Run stored procedure**

```sql
exec dbo.spTestPerson_GetByLastName 'Corey'
```

#### Issues

https://github.com/microsoft/mssql-docker/issues/136

### 9. Prize Form Wire Up: https://www.youtube.com/watch?v=5oHfcyrlHeE

**Pseudocode**

```
if (usingSQL == true)
{
  open database connection
  save the data
  get back the update model
}

if (usingTextFile == true)
{
  open text file
  generate id
  save the data
}
```

#### Questions

- How do we get that connection information?
- How do we connect to two different data sources to the same task?

**Notes**

- Static class for the data source info
- Interface for data sources

### 10. SQL Connection: https://www.youtube.com/watch?v=WGbTM198-eA

#### Table schema diagram info

https://www.youtube.com/watch?v=6ixfTsrhPgM  2:24 in

#### Create stored procedure spPrizes_Insert

```sql
CREATE PROCEDURE dbo.spPrizes_Insert
	@PlaceNumber int,
	@PlaceName nvarchar(50),
	@PrizeAmount money,
	@PrizePercentage float,
	@id int = 0 output
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO dbo.Prizes (PlaceNumber, PlaceName, PrizeAmount, PrizePercentage)
	VALUES (@PlaceNumber, @PlaceName, @PrizeAmount, @PrizePercentage);

	SELECT @id = SCOPE_IDENTITY();
END
GO
```

#### Connection String

https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=dotnet-plat-ext-3.1

**Info on SqlConnectionStringBuilder**

https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnectionstringbuilder?view=dotnet-plat-ext-3.1

### 11. Text Connection: https://www.youtube.com/watch?v=X_P70uukPrU

TC sets global setting `filePath` to `c:\data\TournamentTracker`

### 12. Create Team Form: https://www.youtube.com/watch?v=AB0MJkbFEYg&frags=pl%2Cwn

#### People table

| name | type | allow null |
| --- | --- |
| id | int | |
| FirstName | nvarchar(100) | |
| LastName | nvarchar(100) | |
| EmailAddress | nvarchar(100) | |
| CellphoneNumber | varchar(20) | yes |

```sql
USE [Tournaments]
GO

/****** Object:  Table [dbo].[People]    Script Date: 7/24/2020 10:28:21 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[People](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[EmailAddress] [nvarchar](100) NOT NULL,
	[CellphoneNumber] [varchar](20) NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
```

#### TeamMembers table

| name | type |
| --- | --- |
| id | int |
| TeamId | int |
| PersonId | int |

```sql
USE [Tournaments]
GO

/****** Object:  Table [dbo].[TeamMembers]    Script Date: 7/24/2020 3:57:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TeamMembers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TeamId] [int] NOT NULL,
	[PersonId] [int] NOT NULL,
 CONSTRAINT [PK_TeamMembers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
```

**Store Procedure**

```sql
CREATE PROCEDURE dbo.spPeople_insert
  @FirstName nvarchar(100),
  @LastName nvarchar(100),
  @EmailAddress nvarchar(100),
  @CellphoneNumber varchar(20),
  @id int = 0 output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO dbo.People (FirstName, LastName, EmailAddress, CellphoneNumber)
	VALUES (@FirstName, @LastName, @EmailAddress, @CellphoneNumber);

	SELECT @id = SCOPE_IDENTITY();
END
GO
```

### 13. Create Team Form Part 2 https://www.youtube.com/watch?v=QTdfiZpoabk

TODO: Create Prodedure spPeople_GetAll

```sql
CREATE PROCEDURE dbo.spPeople_GetAll
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id, FirstName, LastName, EmailAddress, CellphoneNumber FROM dbo.People;
END
GO
```

### 14. Create Team Form Part 3 https://www.youtube.com/watch?v=RBfY446QN_A

#### Create dbo.Teams

| name | type |
| id | int |
| TeamName | nvarchar(100) |

```sql
USE [Tournaments]
GO

/****** Object:  Table [dbo].[Teams]    Script Date: 7/30/2020 1:12:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Teams](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TeamName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
```

**Store Procedure**

```sql
CREATE PROCEDURE dbo.spTeams_Insert
	@TeamName nvarchar(100),
	@id int = 0 output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO dbo.Teams (TeamName) VALUES (@TeamName);

	SELECT @id = SCOPE_IDENTITY();
END
GO
```

```sql
CREATE PROCEDURE spTeamMembers_Insert
	@TeamId int,
	@PersonId int,
	@id int = 0 output
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO dbo.TeamMembers (TeamId, PersonId) VALUES (@TeamId, @PersonId);

	select @id = SCOPE_IDENTITY();
END
GO
```

### 15. Create Tournament Part 1 https://www.youtube.com/watch?v=gpCrmbZR9yE

**Store Prodedure**

```sql
CREATE PROCEDURE [dbo].[spTeam_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id, TeamName FROM dbo.Teams;
END
GO
```

```sql
CREATE PROCEDURE dbo.spTeamMembers_GetByTeam
  @TeamId int
AS
BEGIN
  SET NOCOUNT ON;

  SELECT p.*
  FROM dbo.TeamMembers m
  INNER JOIN dbo.People p ON m.PersonId = p.id
  WHERE m.TeamId = @TeamId;
END
```

### 17. Create Tournament Part 3 https://www.youtube.com/watch?v=bpPBPi4laEM

#### Tournaments table

| name | type |
| --- | --- |
| id | int |
| TournamentName | nvarchar(200) |
| EntryFee | money |
| Active | bit |

```sql
CREATE TABLE [dbo].[Tournaments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TournamentName] [nvarchar](200) NOT NULL,
	[EntryFee] [money] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Tournaments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
```

**Stored Procedure**

```sql
CREATE PROCEDURE dbo.spTournaments_Insert
  @TournamentName nvarchar(200),
  @EntryFee money,
  @id int = 0 output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO dbo.Tournaments (TournamentName, EntryFee, Active) VALUES (@TournamentName, @EntryFee, 1);

	SELECT @id = SCOPE_IDENTITY();
END
GO
```

#### TournamentPrizes Table

```sql
CREATE TABLE [dbo].[TournamentPrizes](
	[id] [int] NOT NULL,
	[TournamentId] [int] NOT NULL,
	[PrizeId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_TournamentPrizes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
```

```sql
CREATE PROCEDURE dbo.spTournamentPrizes_Insert
  @TournamentId int,
  @PrizeId int,
  @id int = 0 output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO dbo.TournamentPrizes (TournamentId, PrizeId) VALUES (@TournamentId, @PrizeId);

	SELECT @id = SCOPE_IDENTITY();
END
GO
```


#### TournamentEntries Table

```sql
CREATE TABLE [dbo].[TournamentEntries](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TournamentId] [int] NOT NULL,
	[TeamId] [int] NOT NULL,
 CONSTRAINT [PK_TournamentEntries] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
```

```sql
CREATE PROCEDURE dbo.spTournamentEntries_Insert
	-- Add the parameters for the stored procedure here
	@TournamentId int,
	@TeamId int,
	@id int = 0 output
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.TournamentEntries (TournamentId, TeamId) VALUES (@TournamentId, @TeamId);

	SELECT @id = SCOPE_IDENTITY();
END
```

#### Matchups table

```sql
CREATE TABLE [dbo].[Matchups](
	[id] [int] NOT NULL,
	[TournamentId] [int] NOT NULL,
	[WinnerId] [int] NULL,
	[MatchupRound] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Matchups] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
```

#### MatchupEntries table

```sql
CREATE TABLE [dbo].[MatchupEntries](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[MatchupId] [int] NOT NULL,
	[ParentMatchupId] [int] NULL,
	[TeamCompetingId] [int] NOT NULL,
	[Score] [float] NULL,
 CONSTRAINT [PK_MatchupEntries] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
```
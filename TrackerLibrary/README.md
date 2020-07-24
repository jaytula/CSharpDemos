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
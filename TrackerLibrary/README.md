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


#### Issues

https://github.com/microsoft/mssql-docker/issues/136
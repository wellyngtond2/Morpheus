# Morpheus

## 01 - Run Follow database script

```sh
USE [master]
GO

CREATE DATABASE [Morpheus]
GO

USE [Morpheus]
GO

CREATE TABLE [dbo].[Person](
	[Id] [varchar](32) NOT NULL,
	[Name] [varchar](60) NOT NULL,
	[Email] [varchar](80) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```

## 02 - 

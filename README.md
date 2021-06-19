# Morpheus

## 01 - Run the following database script

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

## 02 - Install Docker and Run Rabbitmq image

# 1º docker pull rabbitmq:3-management
# 2º docker run -d -p 15672:15672 -p 5672:5672 --name rabbit-test-for-medium rabbitmq:3-management

## 03 - 

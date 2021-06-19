# Morpheus

### What is Morpheus?

Morpheus is a project that uses the concept of several technologies and patterns, namely:
- NodeJs
- .Net Core C#
- Rabbitmq
- Mediator
- Unit of Work
- Dapper
- API Rest
- Swagger
- ...

# Setup to start

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

#### 1º docker pull rabbitmq:3-management
#### 2º docker run -d -p 15672:15672 -p 5672:5672 --name rabbit-test-for-medium rabbitmq:3-management

## 03 - Enviroment variables

#### 1º ADD the .env file into the nodeJS project with parameters from your email server like as bellow:

--file .env--
```sh
EMAIL_SMTP="smtp.live.com"
EMAIL_USER="your_email@hotmail.com"
EMAIL_PASS="your_password"
EMAIL_PORT=587
```
#### 2º Into the .net project on appsettings.json replace the connection string "dapperConnectionString" value.


##### About Developer:

Wellyngton A. Borges
- [Linkedin ](https://www.linkedin.com/in/wellyngtonborges)

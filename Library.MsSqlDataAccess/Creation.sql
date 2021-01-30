CREATE DATABASE MicroServicesSampleDb
GO

USE MicroServicesSampleDb
GO

CREATE TABLE CompanyAccount (
    CompanyId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(500) NOT NULL,
    Identifier NVARCHAR(50) NOT NULL,
	InsertAt DATETIME NOT NULL
);  
GO

CREATE TABLE InFile (
    InFileId NVARCHAR(50) NOT NULL PRIMARY KEY,
    ReceiverCompanyId INT NOT NULL,
    DocumentName NVARCHAR(500) NOT NULL,
    SenderIdentifier NVARCHAR(50) NOT NULL,
	[State] INT NOT NULL,
	InsertAt DATETIME NOT NULL
);     
GO 

CREATE TABLE OutFile (
    OutFileId NVARCHAR(50) NOT NULL PRIMARY KEY,
    CompanyId INT NOT NULL,
    DocumentName NVARCHAR(500) NOT NULL,
    ReceiverIdentifier NVARCHAR(50) NOT NULL,
	[State] INT NOT NULL,
	InsertAt DATETIME NOT NULL
);     
GO
 
CREATE TABLE InMail (
    MailId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    ReceiverCompanyId INT NOT NULL,
    Subject NVARCHAR(500) NOT NULL,
    Body NVARCHAR(500) NOT NULL,
    SenderIdentifier NVARCHAR(50) NOT NULL,
	[State] INT NOT NULL,
	InsertAt DATETIME NOT NULL
);     
GO
 
CREATE TABLE OutMail (
    MailId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    CompanyId INT NOT NULL,
    Subject NVARCHAR(500) NOT NULL,
    Body NVARCHAR(500) NOT NULL,
    ReceiverIdentifier NVARCHAR(50) NOT NULL,
	[State] INT NOT NULL,
	InsertAt DATETIME NOT NULL
);     
GO
 
CREATE TABLE [User] (
    UserId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(500) NOT NULL,
    Password NVARCHAR(500) NOT NULL,
    CompanyId INT NOT NULL,
	InsertAt DATETIME NOT NULL
);     
GO
 
CREATE TABLE UserAuthentication (
    AuthKey NVARCHAR(50) NOT NULL PRIMARY KEY,
    UserId INT NOT NULL,
    ExpireAt datetime NOT NULL,
	InsertAt DATETIME NOT NULL
);     
GO

SET IDENTITY_INSERT [dbo].[CompanyAccount] ON 
GO
INSERT [dbo].[CompanyAccount] ([CompanyId], [Title], [Identifier], [InsertAt]) VALUES (1, N'A Company', N'1111111111', CAST(N'2021-01-30T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[CompanyAccount] ([CompanyId], [Title], [Identifier], [InsertAt]) VALUES (2, N'B Company', N'2222222222', CAST(N'2021-01-30T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[CompanyAccount] ([CompanyId], [Title], [Identifier], [InsertAt]) VALUES (3, N'C Company', N'3333333333', CAST(N'2021-01-30T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[CompanyAccount] OFF
GO

SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([UserId], [UserName], [Password], [CompanyId], [InsertAt]) VALUES (1, N'aUser', N'fEqNCco3Yq9h5ZUglD3CZJT4lBs=', 1, CAST(N'2021-01-30T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [Password], [CompanyId], [InsertAt]) VALUES (2, N'bUser', N'fEqNCco3Yq9h5ZUglD3CZJT4lBs=', 2, CAST(N'2021-01-30T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [Password], [CompanyId], [InsertAt]) VALUES (3, N'cUser', N'fEqNCco3Yq9h5ZUglD3CZJT4lBs=', 3, CAST(N'2021-01-30T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO

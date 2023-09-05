USE [SmartwayTask]
GO
ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_Companies]
GO
ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_Passports]
GO
ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_Departments]
GO
DROP TABLE [dbo].[Employees] 
GO 
DROP TABLE [dbo].[Companies] 
GO 
DROP TABLE [dbo].[Passports] 
GO 
DROP TABLE [dbo].[Departments] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](60) NOT NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Passports](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Number] [nvarchar](60) NOT NULL,
 CONSTRAINT [PK_Passports] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](60) NOT NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[PassportId] [int] NOT NULL,
	[DepartmentId] [int] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET IDENTITY_INSERT [dbo].[Companies] ON

INSERT [dbo].[Companies] ([Id], [Name], [Address]) VALUES (1, N'IT_Solutions Ltd', N'583 Wall Dr. Gwynn Oak, MD 21207')
INSERT [dbo].[Companies] ([Id], [Name], [Address]) VALUES (2, N'Admin_Solutions Ltd', N'312 Forest Avenue, BF 923')
SET IDENTITY_INSERT [dbo].[Companies] OFF

SET IDENTITY_INSERT [dbo].[Passports] ON
INSERT [dbo].[Passports] ([Id], [Type], [Number]) VALUES (1, N'Passport', N'123PC4567890')
INSERT [dbo].[Passports] ([Id], [Type], [Number]) VALUES (2, N'Passport', N'0987654321')
INSERT [dbo].[Passports] ([Id], [Type], [Number]) VALUES (3, N'ID card', N'0987654321')
SET IDENTITY_INSERT [dbo].[Passports] OFF

SET IDENTITY_INSERT [dbo].[Departments] ON
INSERT [dbo].[Departments] ([Id], [Name], [Phone]) VALUES (1, N'Development', N'+7-843-555-12-34')
INSERT [dbo].[Departments] ([Id], [Name], [Phone]) VALUES (2, N'Administration', N'8-499-123-45-67')
SET IDENTITY_INSERT [dbo].[Departments] OFF

SET IDENTITY_INSERT [dbo].[Employees] ON
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Phone], [CompanyId], [PassportId], [DepartmentId]) VALUES (1, N'Ivan', N'Ivanov', N'+7-999-555-1111', 1, 1, 1)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Phone], [CompanyId], [PassportId], [DepartmentId]) VALUES (2, N'Anna', N'Egorova', N'+7-999-555-2222', 2, 2, 2)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Phone], [CompanyId], [PassportId], [DepartmentId]) VALUES (3, N'Jana', N'Miller', N'+7-999-555-3333', 1, 3, 1)
SET IDENTITY_INSERT [dbo].[Employees] OFF

ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Companies] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Companies]
GO

ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Passports] FOREIGN KEY([PassportId])
REFERENCES [dbo].[Passports] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Passports]
GO

ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Departments]
GO

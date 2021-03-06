USE [master]
GO
/****** Object:  Database [AutoService]    Script Date: 26.04.2016 2:43:58 ******/
set dateformat ymd;
CREATE DATABASE [AutoService]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AutoService', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\AutoService.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AutoService_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\AutoService_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AutoService] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AutoService].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AutoService] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AutoService] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AutoService] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AutoService] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AutoService] SET ARITHABORT OFF 
GO
ALTER DATABASE [AutoService] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AutoService] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AutoService] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AutoService] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AutoService] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AutoService] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AutoService] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AutoService] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AutoService] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AutoService] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AutoService] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AutoService] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AutoService] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AutoService] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AutoService] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AutoService] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AutoService] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AutoService] SET RECOVERY FULL 
GO
ALTER DATABASE [AutoService] SET  MULTI_USER 
GO
ALTER DATABASE [AutoService] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AutoService] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AutoService] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AutoService] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [AutoService] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'AutoService', N'ON'
GO
USE [AutoService]
GO
/****** Object:  Table [dbo].[Automobiles]    Script Date: 26.04.2016 2:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Automobiles](
	[AutoID] [int] IDENTITY(1,1) NOT NULL,
	[CarBrand] [varchar](100) NOT NULL,
	[CarModel] [varchar](100) NOT NULL,
	[YearMade] [int] NOT NULL,
	[TransmissionType] [varchar](10) NOT NULL,
	[EnginePower] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 26.04.2016 2:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [varchar](255) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Patronymic] [varchar](255) NULL,
	[Birthday] [date] NULL,
	[Phone] [varchar](12) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 26.04.2016 2:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[WorksStart] [datetime] NOT NULL,
	[WorksFinish] [datetime] NULL,
	[WorksPrice] [int] NOT NULL,
	[CustomerID] [int] NOT NULL,
	[AutoID] [int] NOT NULL,
	[WorksID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Works]    Script Date: 26.04.2016 2:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Works](
	[WorksID] [int] IDENTITY(1,1) NOT NULL,
	[WorksName] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[WorksID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[WorksAutoOrder]    Script Date: 26.04.2016 2:43:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create View [dbo].[WorksAutoOrder] as
(select OrderID, CarBrand, CarModel, TransmissionType, EnginePower, YearMade, WorksName, WorksStart, WorksFinish, WorksPrice from Orders
left join Automobiles on
Automobiles.AutoID = Orders.AutoID
left join Works on
Works.WorksID = Orders.WorksID)
GO
SET IDENTITY_INSERT [dbo].[Automobiles] ON 

INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (1, N'Lada', N'Granta', 2011, N'МКПП', 87)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (2, N'Lada', N'Kalina', 2013, N'АКПП', 98)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (3, N'Lada', N'Vesta', 2016, N'АКПП', 110)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (4, N'Renault', N'Logan', 2007, N'МКПП', 75)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (5, N'Mitsubishi', N'Lancer', 2008, N'МКПП', 109)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (6, N'Mitsubishi', N'Lancer', 2009, N'АКПП', 109)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (7, N'Opel', N'Vectra B', 2000, N'МКПП', 136)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (8, N'Volkswagen', N'Passat B5', 1998, N'МКПП', 150)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (9, N'Mercedes-Benz', N'CLA-klasse I', 2013, N'АКПП', 156)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (10, N'Toyota', N'Avensis III', 2011, N'Вариатор', 147)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (11, N'Chery', N'M11', 2011, N'МКПП', 117)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (12, N'Hyundai', N'Elantra', 2011, N'МКПП', 122)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (13, N'Renault', N'Fluence I', 2011, N'МКПП', 110)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (14, N'Nissan', N'Qashqai I', 2011, N'Вариатор', 141)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (15, N'Skoda', N'Fabia II', 2011, N'МКПП', 105)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (16, N'Volkswagen', N'Polo V', 2011, N'МКПП', 105)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (17, N'Volkswagen', N'Golf VI', 2011, N'МКПП', 102)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (18, N'Mazda', N'3 II', 2011, N'АКПП', 105)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (19, N'Toyota', N'Verso I', 2011, N'Вариатор', 147)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (20, N'Volvo', N'XC90 I', 2011, N'АКПП', 185)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (21, N'Kia', N'Sportage III', 2011, N'АКПП', 150)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (22, N'Opel', N'Astra J', 2011, N'МКПП', 115)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (23, N'Hyundai', N'Solaris I', 2011, N'МКПП', 123)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (24, N'Nissan', N'Teana II', 2011, N'АКПП', 136)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (25, N'Toyota', N'Corolla XI', 2013, N'Вариатор', 122)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (26, N'Hyundai', N'Starex (H-1) II', 2015, N'АКПП', 170)
INSERT [dbo].[Automobiles] ([AutoID], [CarBrand], [CarModel], [YearMade], [TransmissionType], [EnginePower]) VALUES (27, N'Ford', N'Focus II', 2010, N'МКПП', 125)
SET IDENTITY_INSERT [dbo].[Automobiles] OFF
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (1, N'Иванов', N'Иван', N'Иванович', CAST(N'1980-02-21' AS Date), N'89000000001')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (2, N'Петров', N'Петр', N'Петрович', CAST(N'1970-11-20' AS Date), N'89000000002')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (3, N'Сидоров', N'Василий', N'Александрович', CAST(N'1984-06-15' AS Date), N'89000000003')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (4, N'Кузнецов', N'Игорь', N'Семенович', CAST(N'1965-05-22' AS Date), N'89000000004')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (5, N'Якубович', N'Леонид', N'Аркадьевич', CAST(N'1945-06-30' AS Date), N'89000000005')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (6, N'Бендер', N'Остап', NULL, NULL, N'89000000006')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (7, N'Морозов', N'Павел', N'Трофимович', NULL, N'89000000007')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (8, N'Уткин', N'Дмитрий', N'Александрович', CAST(N'1987-11-29' AS Date), N'89000000008')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (9, N'Слепцов', N'Семен', N'Юрьевич', CAST(N'1990-04-18' AS Date), N'89000000009')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (10, N'Щеблыкин', N'Аркадий', N'Дмитриевич', CAST(N'1976-08-03' AS Date), N'89000000010')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (11, N'Соколов', N'Иван', N'Петрович', CAST(N'1973-05-09' AS Date), N'89000000011')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (12, N'Портной', N'Семен', NULL, CAST(N'1965-04-20' AS Date), N'89000000012')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (13, N'Лебедев', N'Михаил', N'Михайлович', CAST(N'1977-05-30' AS Date), N'89000000013')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (14, N'Максим', N'Новиков', N'Романович', NULL, N'89000000014')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (15, N'Волков', N'Роман', N'Максимович', CAST(N'1971-07-25' AS Date), N'89000000015')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (16, N'Зайцев', N'Григорий', N'Викторович', CAST(N'1988-11-04' AS Date), N'89000000016')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (17, N'Тарасов', N'Евгений', N'Леонидович', CAST(N'1991-09-17' AS Date), N'89000000017')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (18, N'Сергей', N'Федоров', NULL, NULL, N'89000000018')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (19, N'Ковалев', N'Сергей', N'Петрович', CAST(N'1955-06-23' AS Date), N'89000000019')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (20, N'Чехов', N'Антон', N'Павлович', NULL, N'89000000020')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (21, N'Гусев', N'Дмитрий', N'Александрович', CAST(N'1970-04-10' AS Date), N'89000000021')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (22, N'Захаров', N'Борис', N'Васильевич', CAST(N'1969-01-02' AS Date), N'89000000022')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (23, N'Жуков', N'Илья', N'Константинович', CAST(N'1975-03-22' AS Date), N'89000000023')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (24, N'Медведев', N'Глеб', N'Ярославович', CAST(N'1988-02-07' AS Date), N'89000000024')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (25, N'Сорокин', N'Александр', N'Алексеевич', CAST(N'1984-02-25' AS Date), N'89000000025')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (26, N'Фролов', N'Максим', N'Маркович', CAST(N'1984-04-04' AS Date), N'89000000026')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (27, N'Журавлев', N'Иван', N'Владимирович', CAST(N'1984-06-09' AS Date), N'89000000027')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (28, N'Николаев', N'Игорь', N'Юрьевич', CAST(N'1960-01-17' AS Date), N'89000000028')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (29, N'Егоров', N'Артем', N'Артурович', CAST(N'1984-10-10' AS Date), N'89000000029')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (30, N'Дорофеев', N'Никита', N'Ильич', CAST(N'1984-12-12' AS Date), N'89000000030')
INSERT [dbo].[Customers] ([CustomerID], [Surname], [Name], [Patronymic], [Birthday], [Phone]) VALUES (31, N'Белоусов', N'Петр', N'Сергеевич', CAST(N'1988-03-17' AS Date), N'89000000031')
SET IDENTITY_INSERT [dbo].[Customers] OFF
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (1, CAST(N'2016-02-12 12:30:00.000' AS DateTime), CAST(N'2016-02-12 13:00:00.000' AS DateTime), 500, 1, 1, 1)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (2, CAST(N'2016-02-12 15:00:00.000' AS DateTime), CAST(N'2016-02-12 19:00:00.000' AS DateTime), 3000, 2, 6, 2)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (3, CAST(N'2016-02-13 11:45:00.000' AS DateTime), CAST(N'2016-02-13 12:15:00.000' AS DateTime), 500, 1, 2, 1)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (4, CAST(N'2016-02-13 12:30:00.000' AS DateTime), CAST(N'2016-02-13 15:15:00.000' AS DateTime), 1000, 3, 1, 3)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (5, CAST(N'2016-02-14 10:30:00.000' AS DateTime), CAST(N'2016-02-14 11:00:00.000' AS DateTime), 500, 1, 3, 1)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (6, CAST(N'2016-02-14 11:30:00.000' AS DateTime), CAST(N'2016-02-14 12:30:00.000' AS DateTime), 450, 4, 7, 4)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (7, CAST(N'2016-02-14 10:30:00.000' AS DateTime), CAST(N'2016-02-15 15:00:00.000' AS DateTime), 40000, 5, 1, 5)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (8, CAST(N'2016-02-15 15:00:00.000' AS DateTime), CAST(N'2016-02-15 15:30:00.000' AS DateTime), 500, 5, 1, 1)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (9, CAST(N'2016-02-16 10:00:00.000' AS DateTime), CAST(N'2016-02-16 10:30:00.000' AS DateTime), 500, 6, 10, 6)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (10, CAST(N'2016-02-16 11:00:00.000' AS DateTime), CAST(N'2016-02-16 12:00:00.000' AS DateTime), 500, 7, 12, 7)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (11, CAST(N'2016-02-16 12:15:00.000' AS DateTime), CAST(N'2016-02-16 12:30:00.000' AS DateTime), 500, 8, 27, 8)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (12, CAST(N'2016-02-16 12:30:00.000' AS DateTime), CAST(N'2016-02-16 13:00:00.000' AS DateTime), 500, 8, 27, 9)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (13, CAST(N'2016-02-16 15:00:00.000' AS DateTime), CAST(N'2016-02-16 16:00:00.000' AS DateTime), 500, 9, 3, 10)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (14, CAST(N'2016-02-17 10:00:00.000' AS DateTime), CAST(N'2016-02-17 10:30:00.000' AS DateTime), 1200, 10, 14, 11)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (15, CAST(N'2016-02-17 11:00:00.000' AS DateTime), CAST(N'2016-02-17 13:00:00.000' AS DateTime), 1500, 11, 9, 12)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (16, CAST(N'2016-02-17 14:00:00.000' AS DateTime), CAST(N'2016-02-17 14:30:00.000' AS DateTime), 500, 12, 22, 13)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (17, CAST(N'2016-02-18 10:00:00.000' AS DateTime), CAST(N'2016-02-18 10:45:00.000' AS DateTime), 500, 12, 17, 9)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (18, CAST(N'2016-02-18 11:00:00.000' AS DateTime), CAST(N'2016-02-18 12:00:00.000' AS DateTime), 1500, 13, 26, 14)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (19, CAST(N'2016-02-18 12:15:00.000' AS DateTime), CAST(N'2016-02-18 12:30:00.000' AS DateTime), 450, 14, 11, 4)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (20, CAST(N'2016-02-18 13:00:00.000' AS DateTime), CAST(N'2016-02-18 17:00:00.000' AS DateTime), 2000, 15, 13, 15)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (21, CAST(N'2016-02-19 10:00:00.000' AS DateTime), CAST(N'2016-02-19 13:00:00.000' AS DateTime), 800, 16, 16, 16)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (22, CAST(N'2016-02-19 14:00:00.000' AS DateTime), CAST(N'2016-02-19 15:00:00.000' AS DateTime), 850, 17, 5, 17)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (23, CAST(N'2016-02-19 15:30:00.000' AS DateTime), CAST(N'2016-02-19 16:00:00.000' AS DateTime), 1000, 18, 6, 4)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (24, CAST(N'2016-02-19 16:30:00.000' AS DateTime), CAST(N'2016-02-19 17:00:00.000' AS DateTime), 500, 19, 6, 1)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (25, CAST(N'2016-02-20 10:00:00.000' AS DateTime), CAST(N'2016-02-20 10:30:00.000' AS DateTime), 400, 19, 18, 18)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (26, CAST(N'2016-02-20 10:30:00.000' AS DateTime), CAST(N'2016-02-20 11:00:00.000' AS DateTime), 1500, 19, 18, 3)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (27, CAST(N'2016-02-20 11:00:00.000' AS DateTime), CAST(N'2016-02-20 11:30:00.000' AS DateTime), 300, 19, 18, 19)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (28, CAST(N'2016-02-20 12:30:00.000' AS DateTime), CAST(N'2016-02-20 16:30:00.000' AS DateTime), 1200, 20, 15, 20)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (29, CAST(N'2016-02-21 10:30:00.000' AS DateTime), CAST(N'2016-02-21 12:30:00.000' AS DateTime), 1200, 21, 2, 21)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (30, CAST(N'2016-02-21 12:30:00.000' AS DateTime), CAST(N'2016-02-21 16:30:00.000' AS DateTime), 2500, 21, 2, 22)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (31, CAST(N'2016-02-22 10:00:00.000' AS DateTime), CAST(N'2016-02-22 17:30:00.000' AS DateTime), 10000, 22, 20, 23)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (32, CAST(N'2016-02-23 10:00:00.000' AS DateTime), CAST(N'2016-02-23 11:30:00.000' AS DateTime), 900, 23, 14, 24)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (33, CAST(N'2016-02-23 12:00:00.000' AS DateTime), CAST(N'2016-02-23 12:30:00.000' AS DateTime), 2600, 24, 21, 25)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (34, CAST(N'2016-02-23 14:00:00.000' AS DateTime), CAST(N'2016-02-23 14:30:00.000' AS DateTime), 2600, 25, 9, 26)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (35, CAST(N'2016-02-24 11:00:00.000' AS DateTime), CAST(N'2016-02-24 12:30:00.000' AS DateTime), 550, 26, 27, 27)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (36, CAST(N'2016-02-24 12:45:00.000' AS DateTime), CAST(N'2016-02-24 13:00:00.000' AS DateTime), 300, 27, 1, 28)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (37, CAST(N'2016-02-25 10:00:00.000' AS DateTime), CAST(N'2016-02-25 10:30:00.000' AS DateTime), 1600, 28, 3, 25)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (38, CAST(N'2016-02-25 10:30:00.000' AS DateTime), CAST(N'2016-02-25 10:45:00.000' AS DateTime), 500, 28, 3, 1)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (39, CAST(N'2016-02-26 10:00:00.000' AS DateTime), CAST(N'2016-02-26 10:30:00.000' AS DateTime), 300, 29, 13, 29)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (40, CAST(N'2016-02-26 12:00:00.000' AS DateTime), CAST(N'2016-02-26 12:45:00.000' AS DateTime), 400, 30, 17, 30)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (41, CAST(N'2016-02-27 10:00:00.000' AS DateTime), CAST(N'2016-02-27 14:45:00.000' AS DateTime), 3500, 31, 22, 31)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (42, CAST(N'2016-02-27 15:00:00.000' AS DateTime), CAST(N'2016-02-27 15:30:00.000' AS DateTime), 500, 22, 20, 1)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (43, CAST(N'2016-02-27 15:30:00.000' AS DateTime), CAST(N'2016-02-27 16:00:00.000' AS DateTime), 500, 19, 18, 1)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (44, CAST(N'2016-02-27 16:00:00.000' AS DateTime), CAST(N'2016-02-27 16:30:00.000' AS DateTime), 500, 8, 27, 1)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (45, CAST(N'2016-02-27 17:00:00.000' AS DateTime), CAST(N'2016-02-27 20:00:00.000' AS DateTime), 6000, 1, 1, 32)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (46, CAST(N'2016-02-28 10:00:00.000' AS DateTime), CAST(N'2016-02-28 13:00:00.000' AS DateTime), 5000, 1, 1, 33)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (47, CAST(N'2016-02-28 13:00:00.000' AS DateTime), CAST(N'2016-02-28 18:00:00.000' AS DateTime), 6500, 1, 1, 34)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (48, CAST(N'2016-03-01 10:00:00.000' AS DateTime), NULL, 10000, 1, 1, 35)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (49, CAST(N'2016-03-02 12:00:00.000' AS DateTime), CAST(N'2016-03-02 13:00:00.000' AS DateTime), 600, 2, 6, 36)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (50, CAST(N'2016-03-02 13:00:00.000' AS DateTime), CAST(N'2016-03-02 13:30:00.000' AS DateTime), 1400, 4, 7, 25)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (51, CAST(N'2016-03-02 14:00:00.000' AS DateTime), CAST(N'2016-03-02 14:30:00.000' AS DateTime), 1400, 27, 1, 25)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (52, CAST(N'2016-03-03 11:00:00.000' AS DateTime), NULL, 20000, 15, 25, 5)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (53, CAST(N'2016-03-05 11:00:00.000' AS DateTime), CAST(N'2016-03-05 11:30:00.000' AS DateTime), 1400, 5, 9, 25)
INSERT [dbo].[Orders] ([OrderID], [WorksStart], [WorksFinish], [WorksPrice], [CustomerID], [AutoID], [WorksID]) VALUES (54, CAST(N'2016-03-05 14:00:00.000' AS DateTime), CAST(N'2016-03-05 14:30:00.000' AS DateTime), 1400, 25, 14, 25)
SET IDENTITY_INSERT [dbo].[Orders] OFF
SET IDENTITY_INSERT [dbo].[Works] ON 

INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (1, N'Замена масла в двигателе, легковые а/м')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (2, N'Замена топливного насоса')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (3, N'Замена бампера')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (4, N'Установка защиты картера')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (5, N'Шумоизоляция салона')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (6, N'Диагностика подвески и рулевого управления')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (7, N'Замена дисковых тормозных колодок')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (8, N'Замена салонного фильтра')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (9, N'Замена топливного фильтра')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (10, N'Ремонт стартера')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (11, N'Регулировка передней оси')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (12, N'Замена масла в АКПП полная')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (13, N'Прокачка тормозов')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (14, N'Замена стоек')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (15, N'Замена лобового стекла')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (16, N'Замена привода в сборе')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (17, N'Замена внутреннего ШРУС')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (18, N'Диагностика тормозной системы')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (19, N'Регулировка стояночного тормоза')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (20, N'Ремонт генератора')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (21, N'Установка ксенона')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (22, N'Замена аккустики')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (23, N'Устранение перекоса кузова средней сложности')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (24, N'Замена зеркала бокового с электроприводом')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (25, N'Переобувка к-т 4 шт.')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (26, N'Охлаждающей жидкости замена')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (27, N'Приемной трубы замена')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (28, N'Ремонт замка двери')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (29, N'Диагностика')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (30, N'Замена передней фары')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (31, N'Боковина седан — ремонт №1')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (32, N'Капот — ремонт №3')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (33, N'Крыло переднее — ремонт №3')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (34, N'Боковина седан — ремонт №3')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (35, N'Полная переборка двигателя со снятием')
INSERT [dbo].[Works] ([WorksID], [WorksName]) VALUES (36, N'Регулировка клапанов')
SET IDENTITY_INSERT [dbo].[Works] OFF
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [fkAutomobilesOrders] FOREIGN KEY([AutoID])
REFERENCES [dbo].[Automobiles] ([AutoID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [fkAutomobilesOrders]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [fkCustomersOrders] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [fkCustomersOrders]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [fkWorksOrders] FOREIGN KEY([WorksID])
REFERENCES [dbo].[Works] ([WorksID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [fkWorksOrders]
GO
ALTER TABLE [dbo].[Automobiles]  WITH CHECK ADD CHECK  (([EnginePower]>(0)))
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD CHECK  (([WorksPrice]>(0)))
GO
USE [master]
GO
ALTER DATABASE [AutoService] SET  READ_WRITE 
GO

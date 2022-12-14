USE [master]
GO
/****** Object:  Database [RevenueAI]    Script Date: 22/08/2022 11:54:34 ص ******/
CREATE DATABASE [RevenueAI]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RevenueAI', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\RevenueAI.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RevenueAI_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\RevenueAI_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [RevenueAI] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RevenueAI].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RevenueAI] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RevenueAI] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RevenueAI] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RevenueAI] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RevenueAI] SET ARITHABORT OFF 
GO
ALTER DATABASE [RevenueAI] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RevenueAI] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RevenueAI] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RevenueAI] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RevenueAI] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RevenueAI] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RevenueAI] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RevenueAI] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RevenueAI] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RevenueAI] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RevenueAI] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RevenueAI] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RevenueAI] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RevenueAI] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RevenueAI] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RevenueAI] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RevenueAI] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RevenueAI] SET RECOVERY FULL 
GO
ALTER DATABASE [RevenueAI] SET  MULTI_USER 
GO
ALTER DATABASE [RevenueAI] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RevenueAI] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RevenueAI] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RevenueAI] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RevenueAI] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RevenueAI] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'RevenueAI', N'ON'
GO
ALTER DATABASE [RevenueAI] SET QUERY_STORE = OFF
GO
USE [RevenueAI]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 22/08/2022 11:54:34 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Balance] [float] NULL,
	[AccountTypeID] [int] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountType]    Script Date: 22/08/2022 11:54:34 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountType](
	[AccountTypeID] [int] IDENTITY(1,1) NOT NULL,
	[AccountTypeDesc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_AccountType] PRIMARY KEY CLUSTERED 
(
	[AccountTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Card]    Script Date: 22/08/2022 11:54:34 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Card](
	[CardNumber] [char](16) NOT NULL,
	[valid] [char](3) NULL,
	[StateID] [int] NOT NULL,
	[CardTypeID] [int] NOT NULL,
	[currencyID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_Card] PRIMARY KEY CLUSTERED 
(
	[CardNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CardState]    Script Date: 22/08/2022 11:54:34 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CardState](
	[StateID] [int] IDENTITY(1,1) NOT NULL,
	[StateDescription] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CardState] PRIMARY KEY CLUSTERED 
(
	[StateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CardType]    Script Date: 22/08/2022 11:54:34 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CardType](
	[CardTypeID] [int] IDENTITY(1,1) NOT NULL,
	[CardType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CardType] PRIMARY KEY CLUSTERED 
(
	[CardTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[currency]    Script Date: 22/08/2022 11:54:34 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[currency](
	[CurrencyID] [int] IDENTITY(1,1) NOT NULL,
	[Currency] [varchar](50) NOT NULL,
 CONSTRAINT [PK_currency] PRIMARY KEY CLUSTERED 
(
	[CurrencyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PasswordRecoveryHistory]    Script Date: 22/08/2022 11:54:34 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PasswordRecoveryHistory](
	[ID] [int] IDENTITY(100000000,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RecoveryInitialPassword] [varchar](50) NULL,
	[SendDate] [datetime] NULL,
	[ExpireDate] [datetime] NULL,
	[isUsed] [varchar](1) NULL,
 CONSTRAINT [PK_RecoveryPassword] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 22/08/2022 11:54:34 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[Amount] [float] NULL,
	[TransactionTypeID] [int] NOT NULL,
	[CardNumber] [char](16) NOT NULL,
	[VendorID] [int] NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionTypes]    Script Date: 22/08/2022 11:54:34 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionTypes](
	[TransactionTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionTypeName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TransactionTypes] PRIMARY KEY CLUSTERED 
(
	[TransactionTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 22/08/2022 11:54:34 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [varchar](50) NULL,
	[FirstName] [varchar](50) NULL,
	[UserName] [varchar](50) NULL,
	[UserTypeID] [int] NOT NULL,
	[Password] [varchar](50) NULL,
	[LastLoginTime] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[LastPasswordChangeDate] [datetime] NULL,
 CONSTRAINT [PK_User_1] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 22/08/2022 11:54:34 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[UserTypeID] [int] IDENTITY(1,1) NOT NULL,
	[UserTypeDesc] [nchar](10) NOT NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[UserTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendor]    Script Date: 22/08/2022 11:54:34 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendor](
	[VendorID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[mail] [varchar](50) NULL,
 CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED 
(
	[VendorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([AccountID], [UserID], [Balance], [AccountTypeID]) VALUES (1, 3, 10, 1)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[AccountType] ON 

INSERT [dbo].[AccountType] ([AccountTypeID], [AccountTypeDesc]) VALUES (1, N'Deposit')
INSERT [dbo].[AccountType] ([AccountTypeID], [AccountTypeDesc]) VALUES (2, N'Credit')
INSERT [dbo].[AccountType] ([AccountTypeID], [AccountTypeDesc]) VALUES (3, N'Currency')
SET IDENTITY_INSERT [dbo].[AccountType] OFF
GO
INSERT [dbo].[Card] ([CardNumber], [valid], [StateID], [CardTypeID], [currencyID], [UserID]) VALUES (N'0000000000000000', N'123', 1, 2, 1, 3)
INSERT [dbo].[Card] ([CardNumber], [valid], [StateID], [CardTypeID], [currencyID], [UserID]) VALUES (N'27812161200391  ', N'000', 1, 1, 1, 3)
GO
SET IDENTITY_INSERT [dbo].[CardState] ON 

INSERT [dbo].[CardState] ([StateID], [StateDescription]) VALUES (1, N'Active')
INSERT [dbo].[CardState] ([StateID], [StateDescription]) VALUES (2, N'Inactive')
INSERT [dbo].[CardState] ([StateID], [StateDescription]) VALUES (3, N'Disabled')
INSERT [dbo].[CardState] ([StateID], [StateDescription]) VALUES (4, N'Expired')
SET IDENTITY_INSERT [dbo].[CardState] OFF
GO
SET IDENTITY_INSERT [dbo].[CardType] ON 

INSERT [dbo].[CardType] ([CardTypeID], [CardType]) VALUES (1, N'Forint')
INSERT [dbo].[CardType] ([CardTypeID], [CardType]) VALUES (2, N'Currency')
INSERT [dbo].[CardType] ([CardTypeID], [CardType]) VALUES (3, N'Credit')
SET IDENTITY_INSERT [dbo].[CardType] OFF
GO
SET IDENTITY_INSERT [dbo].[currency] ON 

INSERT [dbo].[currency] ([CurrencyID], [Currency]) VALUES (1, N'EUR')
INSERT [dbo].[currency] ([CurrencyID], [Currency]) VALUES (2, N'USD')
SET IDENTITY_INSERT [dbo].[currency] OFF
GO
SET IDENTITY_INSERT [dbo].[PasswordRecoveryHistory] ON 

INSERT [dbo].[PasswordRecoveryHistory] ([ID], [UserID], [RecoveryInitialPassword], [SendDate], [ExpireDate], [isUsed]) VALUES (100000000, 3, N'PjIxSQGqb0fBiBEoI4I+gqvFsxOrhUUB3J8ftWyhEjY=', CAST(N'2022-08-22T00:41:56.723' AS DateTime), CAST(N'2022-08-22T00:56:56.727' AS DateTime), NULL)
INSERT [dbo].[PasswordRecoveryHistory] ([ID], [UserID], [RecoveryInitialPassword], [SendDate], [ExpireDate], [isUsed]) VALUES (100000001, 3, N'kUsLEPB3Xy1q30bihNhisLTZVBaUa/IJjD7e7R+BS40=', CAST(N'2022-08-22T00:44:47.020' AS DateTime), CAST(N'2022-08-22T00:59:47.023' AS DateTime), NULL)
INSERT [dbo].[PasswordRecoveryHistory] ([ID], [UserID], [RecoveryInitialPassword], [SendDate], [ExpireDate], [isUsed]) VALUES (100000002, 3, N'8G^s9-R8u*W1%WA;', CAST(N'2022-08-22T00:50:11.103' AS DateTime), CAST(N'2022-08-22T01:05:11.103' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[PasswordRecoveryHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[Transaction] ON 

INSERT [dbo].[Transaction] ([id], [Date], [Amount], [TransactionTypeID], [CardNumber], [VendorID]) VALUES (1, CAST(N'2022-08-21T08:41:00.000' AS DateTime), 88, 1, N'27812161200391  ', 1)
INSERT [dbo].[Transaction] ([id], [Date], [Amount], [TransactionTypeID], [CardNumber], [VendorID]) VALUES (2, CAST(N'2022-08-25T06:00:00.000' AS DateTime), 555, 2, N'27812161200391  ', 1)
INSERT [dbo].[Transaction] ([id], [Date], [Amount], [TransactionTypeID], [CardNumber], [VendorID]) VALUES (3, CAST(N'2022-08-11T11:35:00.000' AS DateTime), 44, 1, N'0000000000000000', 1)
SET IDENTITY_INSERT [dbo].[Transaction] OFF
GO
SET IDENTITY_INSERT [dbo].[TransactionTypes] ON 

INSERT [dbo].[TransactionTypes] ([TransactionTypeID], [TransactionTypeName]) VALUES (1, N'Normal')
INSERT [dbo].[TransactionTypes] ([TransactionTypeID], [TransactionTypeName]) VALUES (2, N'Cancelled')
SET IDENTITY_INSERT [dbo].[TransactionTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [LastName], [FirstName], [UserName], [UserTypeID], [Password], [LastLoginTime], [CreatedDate], [LastPasswordChangeDate]) VALUES (3, N'Rafie', N'Mohamed', N'eng.rafie@gmail.com', 1, N'TY+50vQHnqNKj7SMs9ipJotI8TDePS0HZ2DyUmoovYk=', CAST(N'2022-08-22T03:05:58.930' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserType] ON 

INSERT [dbo].[UserType] ([UserTypeID], [UserTypeDesc]) VALUES (1, N'Admin     ')
INSERT [dbo].[UserType] ([UserTypeID], [UserTypeDesc]) VALUES (2, N'User      ')
SET IDENTITY_INSERT [dbo].[UserType] OFF
GO
SET IDENTITY_INSERT [dbo].[Vendor] ON 

INSERT [dbo].[Vendor] ([VendorID], [Name], [Address], [Phone], [mail]) VALUES (1, N'CITC', N'Obour', N'01005795066', N'citc@gmail.com')
SET IDENTITY_INSERT [dbo].[Vendor] OFF
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_AccountType] FOREIGN KEY([AccountTypeID])
REFERENCES [dbo].[AccountType] ([AccountTypeID])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_AccountType]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_User]
GO
ALTER TABLE [dbo].[Card]  WITH CHECK ADD  CONSTRAINT [FK_Card_CardState] FOREIGN KEY([StateID])
REFERENCES [dbo].[CardState] ([StateID])
GO
ALTER TABLE [dbo].[Card] CHECK CONSTRAINT [FK_Card_CardState]
GO
ALTER TABLE [dbo].[Card]  WITH CHECK ADD  CONSTRAINT [FK_Card_CardType] FOREIGN KEY([CardTypeID])
REFERENCES [dbo].[CardType] ([CardTypeID])
GO
ALTER TABLE [dbo].[Card] CHECK CONSTRAINT [FK_Card_CardType]
GO
ALTER TABLE [dbo].[Card]  WITH CHECK ADD  CONSTRAINT [FK_Card_currency] FOREIGN KEY([currencyID])
REFERENCES [dbo].[currency] ([CurrencyID])
GO
ALTER TABLE [dbo].[Card] CHECK CONSTRAINT [FK_Card_currency]
GO
ALTER TABLE [dbo].[Card]  WITH CHECK ADD  CONSTRAINT [FK_Card_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Card] CHECK CONSTRAINT [FK_Card_User]
GO
ALTER TABLE [dbo].[PasswordRecoveryHistory]  WITH CHECK ADD  CONSTRAINT [FK_PasswordRecoveryHistory_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[PasswordRecoveryHistory] CHECK CONSTRAINT [FK_PasswordRecoveryHistory_User]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Card] FOREIGN KEY([CardNumber])
REFERENCES [dbo].[Card] ([CardNumber])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Card]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_TransactionTypes] FOREIGN KEY([TransactionTypeID])
REFERENCES [dbo].[TransactionTypes] ([TransactionTypeID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_TransactionTypes]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Vendor] FOREIGN KEY([VendorID])
REFERENCES [dbo].[Vendor] ([VendorID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Vendor]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserType] FOREIGN KEY([UserTypeID])
REFERENCES [dbo].[UserType] ([UserTypeID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserType]
GO
USE [master]
GO
ALTER DATABASE [RevenueAI] SET  READ_WRITE 
GO

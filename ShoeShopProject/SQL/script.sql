USE [master]
GO
/****** Object:  Database [DBShoesShop]    Script Date: 5/13/2024 1:07:01 AM ******/
CREATE DATABASE [DBShoesShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBShoesShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DBShoesShop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DBShoesShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DBShoesShop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DBShoesShop] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBShoesShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBShoesShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBShoesShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBShoesShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBShoesShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBShoesShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBShoesShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBShoesShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBShoesShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBShoesShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBShoesShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBShoesShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBShoesShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBShoesShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBShoesShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBShoesShop] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DBShoesShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBShoesShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBShoesShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBShoesShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBShoesShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBShoesShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBShoesShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBShoesShop] SET RECOVERY FULL 
GO
ALTER DATABASE [DBShoesShop] SET  MULTI_USER 
GO
ALTER DATABASE [DBShoesShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBShoesShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBShoesShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBShoesShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBShoesShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DBShoesShop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DBShoesShop', N'ON'
GO
ALTER DATABASE [DBShoesShop] SET QUERY_STORE = ON
GO
ALTER DATABASE [DBShoesShop] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DBShoesShop]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 5/13/2024 1:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](256) NOT NULL,
	[password] [varchar](256) NOT NULL,
	[fullname] [nvarchar](256) NOT NULL,
	[phone] [varchar](11) NOT NULL,
	[image] [nvarchar](max) NOT NULL,
	[roleId] [int] NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 5/13/2024 1:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 5/13/2024 1:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NOT NULL,
	[update_date] [datetime] NOT NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartItems]    Script Date: 5/13/2024 1:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[cartID] [int] NOT NULL,
	[productID] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[price_amount] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_CartItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 5/13/2024 1:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Color]    Script Date: 5/13/2024 1:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Color](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cname] [nvarchar](50) NOT NULL,
	[cvalue] [nvarchar](50) NULL,
 CONSTRAINT [PK_Color] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 5/13/2024 1:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[contact_name] [nvarchar](256) NOT NULL,
	[contact_description] [nvarchar](max) NULL,
	[imageUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 5/13/2024 1:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NOT NULL,
	[order_address] [nvarchar](max) NOT NULL,
	[payment_method] [int] NOT NULL,
	[payment_status] [bit] NOT NULL,
	[order_status] [int] NOT NULL,
	[update_date] [datetime] NOT NULL,
	[total_amount] [decimal](18, 0) NOT NULL,
	[order_desc] [nvarchar](max) NULL,
	[saleID] [int] NULL,
	[order_phone] [varchar](12) NOT NULL,
	[order_name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 5/13/2024 1:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[orderID] [int] NOT NULL,
	[productID] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[amount_price] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 5/13/2024 1:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[payment_method] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NULL,
	[image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 5/13/2024 1:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[price] [decimal](18, 0) NOT NULL,
	[desciption] [nvarchar](max) NULL,
	[categoryID] [int] NOT NULL,
	[brandID] [int] NOT NULL,
	[thumbnail] [nvarchar](max) NOT NULL,
	[imagePath] [nvarchar](max) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductVariant]    Script Date: 5/13/2024 1:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductVariant](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[productID] [int] NOT NULL,
	[colorID] [int] NOT NULL,
	[sizeID] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[update_date] [datetime] NOT NULL,
	[update_user] [int] NOT NULL,
 CONSTRAINT [PK_ProductVariant] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 5/13/2024 1:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](256) NOT NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Size]    Script Date: 5/13/2024 1:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Size](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[size] [int] NOT NULL,
 CONSTRAINT [PK_Size] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slider]    Script Date: 5/13/2024 1:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slider](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[image] [nvarchar](max) NOT NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_Slider] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/13/2024 1:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](256) NOT NULL,
	[fullname] [nvarchar](256) NOT NULL,
	[address] [nvarchar](max) NULL,
	[phone] [varchar](11) NULL,
	[image] [nvarchar](max) NULL,
	[birthday] [date] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Admin] ON 

INSERT [dbo].[Admin] ([id], [username], [password], [fullname], [phone], [image], [roleId]) VALUES (3, N'admin', N'admin', N'ADMIN', N'123456789', N'/assets/img/account/profile/profile_admin_20240326_210317.jpg', 1)
INSERT [dbo].[Admin] ([id], [username], [password], [fullname], [phone], [image], [roleId]) VALUES (21, N'sale1', N'123456', N'Nhân viên 1', N'1234567890', N'/assets/img/account/profile/profile_admin_20240326_204817.jpg', 2)
INSERT [dbo].[Admin] ([id], [username], [password], [fullname], [phone], [image], [roleId]) VALUES (23, N'sale2', N'654321', N'Nhân viên 2', N'1234567890', N'/assets/img/account/profile/profile_admin_20240326_204817.jpg', 2)
SET IDENTITY_INSERT [dbo].[Admin] OFF
GO
SET IDENTITY_INSERT [dbo].[Brand] ON 

INSERT [dbo].[Brand] ([id], [name]) VALUES (1, N'Nike')
INSERT [dbo].[Brand] ([id], [name]) VALUES (2, N'Adidas')
INSERT [dbo].[Brand] ([id], [name]) VALUES (3, N'Puma')
INSERT [dbo].[Brand] ([id], [name]) VALUES (4, N'MLB')
INSERT [dbo].[Brand] ([id], [name]) VALUES (5, N'FILA')
INSERT [dbo].[Brand] ([id], [name]) VALUES (6, N'Gucci')
INSERT [dbo].[Brand] ([id], [name]) VALUES (7, N'Converse')
INSERT [dbo].[Brand] ([id], [name]) VALUES (8, N'Jordan')
INSERT [dbo].[Brand] ([id], [name]) VALUES (9, N'Vans')
SET IDENTITY_INSERT [dbo].[Brand] OFF
GO
SET IDENTITY_INSERT [dbo].[Cart] ON 

INSERT [dbo].[Cart] ([id], [userID], [update_date]) VALUES (1, 4, CAST(N'2024-03-22T01:41:44.773' AS DateTime))
INSERT [dbo].[Cart] ([id], [userID], [update_date]) VALUES (9, 5, CAST(N'2024-03-24T14:19:53.893' AS DateTime))
SET IDENTITY_INSERT [dbo].[Cart] OFF
GO
SET IDENTITY_INSERT [dbo].[CartItems] ON 

INSERT [dbo].[CartItems] ([Id], [cartID], [productID], [quantity], [price_amount]) VALUES (26, 1, 3, 1, CAST(2500000 AS Decimal(18, 0)))
INSERT [dbo].[CartItems] ([Id], [cartID], [productID], [quantity], [price_amount]) VALUES (27, 1, 35, 1, CAST(3500000 AS Decimal(18, 0)))
INSERT [dbo].[CartItems] ([Id], [cartID], [productID], [quantity], [price_amount]) VALUES (32, 9, 7, 1, CAST(3000000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[CartItems] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([id], [name]) VALUES (1, N'Low-top')
INSERT [dbo].[Category] ([id], [name]) VALUES (2, N'Mid-top')
INSERT [dbo].[Category] ([id], [name]) VALUES (3, N'High-top')
INSERT [dbo].[Category] ([id], [name]) VALUES (8, N'Boot')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Color] ON 

INSERT [dbo].[Color] ([id], [cname], [cvalue]) VALUES (1, N'Đỏ', N'#e02844')
INSERT [dbo].[Color] ([id], [cname], [cvalue]) VALUES (2, N'Hồng', N'#eb46c7')
INSERT [dbo].[Color] ([id], [cname], [cvalue]) VALUES (4, N'Lam', N'#50a4f2')
INSERT [dbo].[Color] ([id], [cname], [cvalue]) VALUES (5, N'Lục', N'#50f276')
INSERT [dbo].[Color] ([id], [cname], [cvalue]) VALUES (6, N'Vàng', N'#f2f765')
INSERT [dbo].[Color] ([id], [cname], [cvalue]) VALUES (7, N'Cam', N'#ed9a55')
INSERT [dbo].[Color] ([id], [cname], [cvalue]) VALUES (8, N'Đen', N'#000000')
INSERT [dbo].[Color] ([id], [cname], [cvalue]) VALUES (9, N'Trắng', N'#ffffff')
INSERT [dbo].[Color] ([id], [cname], [cvalue]) VALUES (10, N'Xám', N'#8f8d8c')
INSERT [dbo].[Color] ([id], [cname], [cvalue]) VALUES (11, N'Nâu', N'#574b46')
SET IDENTITY_INSERT [dbo].[Color] OFF
GO
SET IDENTITY_INSERT [dbo].[Contact] ON 

INSERT [dbo].[Contact] ([id], [contact_name], [contact_description], [imageUrl]) VALUES (1, N'Facebook', N'https://www.facebook.com/', NULL)
INSERT [dbo].[Contact] ([id], [contact_name], [contact_description], [imageUrl]) VALUES (2, N'Zalo', N'https://zalo.me/pc', NULL)
INSERT [dbo].[Contact] ([id], [contact_name], [contact_description], [imageUrl]) VALUES (3, N'Instagram', N'https://www.instagram.com/', NULL)
SET IDENTITY_INSERT [dbo].[Contact] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([id], [userID], [order_address], [payment_method], [payment_status], [order_status], [update_date], [total_amount], [order_desc], [saleID], [order_phone], [order_name]) VALUES (6, 4, N'Thái Nguyên, Việt Nam', 1, 1, 1, CAST(N'2024-03-31T21:01:50.387' AS DateTime), CAST(3000000 AS Decimal(18, 0)), N'ABC', 3, N'0399522127', N'Đồng Quang Linh')
INSERT [dbo].[Order] ([id], [userID], [order_address], [payment_method], [payment_status], [order_status], [update_date], [total_amount], [order_desc], [saleID], [order_phone], [order_name]) VALUES (7, 4, N'Thái Nguyên, Việt Nam', 3, 1, 3, CAST(N'2024-04-03T00:14:08.983' AS DateTime), CAST(8000000 AS Decimal(18, 0)), N'XYZ', 21, N'0399522127', N'Nguyễn Văn A')
INSERT [dbo].[Order] ([id], [userID], [order_address], [payment_method], [payment_status], [order_status], [update_date], [total_amount], [order_desc], [saleID], [order_phone], [order_name]) VALUES (8, 4, N'Test giao hàng', 2, 1, 2, CAST(N'2024-03-24T01:22:01.623' AS DateTime), CAST(3000000 AS Decimal(18, 0)), N'123 321', 3, N'123456789', N'Đồng Quang Linh')
INSERT [dbo].[Order] ([id], [userID], [order_address], [payment_method], [payment_status], [order_status], [update_date], [total_amount], [order_desc], [saleID], [order_phone], [order_name]) VALUES (9, 4, N'Test giao hàng', 3, 1, 0, CAST(N'2024-03-31T21:19:41.023' AS DateTime), CAST(18500000 AS Decimal(18, 0)), N'321', 21, N'123456789', N'Đồng Quang Linh')
INSERT [dbo].[Order] ([id], [userID], [order_address], [payment_method], [payment_status], [order_status], [update_date], [total_amount], [order_desc], [saleID], [order_phone], [order_name]) VALUES (10, 4, N'Hà Nội, Việt Nam', 2, 1, 2, CAST(N'2024-04-03T00:43:55.410' AS DateTime), CAST(3000000 AS Decimal(18, 0)), NULL, 3, N'123', N'Đồng Quang Linh')
INSERT [dbo].[Order] ([id], [userID], [order_address], [payment_method], [payment_status], [order_status], [update_date], [total_amount], [order_desc], [saleID], [order_phone], [order_name]) VALUES (11, 4, N'Hà Nội, Việt Nam', 1, 1, 1, CAST(N'2024-04-03T01:01:34.740' AS DateTime), CAST(5000000 AS Decimal(18, 0)), NULL, 21, N'123', N'Đồng Quang Linh')
INSERT [dbo].[Order] ([id], [userID], [order_address], [payment_method], [payment_status], [order_status], [update_date], [total_amount], [order_desc], [saleID], [order_phone], [order_name]) VALUES (12, 4, N'Hà Nội, Việt Nam', 1, 0, 0, CAST(N'2024-03-26T20:44:03.277' AS DateTime), CAST(17500000 AS Decimal(18, 0)), N'abc', NULL, N'123', N'Đồng Quang Linh 2')
INSERT [dbo].[Order] ([id], [userID], [order_address], [payment_method], [payment_status], [order_status], [update_date], [total_amount], [order_desc], [saleID], [order_phone], [order_name]) VALUES (13, 4, N'Hà Nội, Việt Nam', 1, 0, 0, CAST(N'2024-04-02T01:40:12.567' AS DateTime), CAST(5500000 AS Decimal(18, 0)), N'a23', NULL, N'123', N'Đồng Quang Linh')
INSERT [dbo].[Order] ([id], [userID], [order_address], [payment_method], [payment_status], [order_status], [update_date], [total_amount], [order_desc], [saleID], [order_phone], [order_name]) VALUES (14, 5, N'Test giao hàng', 2, 0, 0, CAST(N'2024-05-13T00:02:34.490' AS DateTime), CAST(9600000 AS Decimal(18, 0)), NULL, NULL, N'123456789', N'Dong Quang Linh (K14 HL)')
INSERT [dbo].[Order] ([id], [userID], [order_address], [payment_method], [payment_status], [order_status], [update_date], [total_amount], [order_desc], [saleID], [order_phone], [order_name]) VALUES (15, 5, N'Test giao hàng', 3, 0, 0, CAST(N'2024-05-13T00:06:51.177' AS DateTime), CAST(10000000 AS Decimal(18, 0)), NULL, NULL, N'123456789', N'Dong Quang Linh (K14 HL)')
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (1, 6, 7, 1, CAST(3000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (2, 7, 6, 1, CAST(3000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (3, 7, 3, 2, CAST(5000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (4, 8, 7, 1, CAST(3000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (5, 9, 3, 1, CAST(2500000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (6, 9, 1, 4, CAST(10000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (7, 9, 6, 2, CAST(6000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (8, 10, 5, 1, CAST(3000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (9, 11, 3, 2, CAST(5000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (10, 12, 1, 5, CAST(12500000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (11, 12, 9, 2, CAST(5000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (12, 13, 6, 1, CAST(3000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (13, 13, 3, 1, CAST(2500000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (14, 14, 11, 2, CAST(4400000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (15, 14, 12, 1, CAST(2200000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (16, 14, 6, 1, CAST(3000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetails] ([id], [orderID], [productID], [quantity], [amount_price]) VALUES (17, 15, 3, 4, CAST(10000000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Payment] ON 

INSERT [dbo].[Payment] ([id], [payment_method], [description], [image]) VALUES (1, N'COD', N'Thanh toán khi nhận hàng.', N'')
INSERT [dbo].[Payment] ([id], [payment_method], [description], [image]) VALUES (2, N'Chuyển khoản', N'Chuyển khoản qua số tài khoản: 9998899988 - Ngân hàng ABC', N'/assets/img/demo/qr_code.jpg')
INSERT [dbo].[Payment] ([id], [payment_method], [description], [image]) VALUES (3, N'Momo', N'Chuyển khoản qua momo: 0123456789', N'/assets/img/demo/qr_code.jpg')
INSERT [dbo].[Payment] ([id], [payment_method], [description], [image]) VALUES (5, N'ZaloPay', N'Thanh toán qua zaloPay', N'/assets/img/payment/payment_20240513_010239.png')
SET IDENTITY_INSERT [dbo].[Payment] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (1, N'Converse Run Star Hike – High White', CAST(2500000 AS Decimal(18, 0)), N'Giày đẹp của converse', 1, 7, N'/assets/img/products/thumbnails/converse-run-star-hike-high-white-166799c-600x600.jpeg', N'/assets/img/products/product_1/')
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (5, N'Air Jordan 1 Zoom CMFT – Patent Chicago', CAST(3000000 AS Decimal(18, 0)), N'Giày đẹp của nike', 2, 8, N'/assets/img/products/thumbnails/air-jordan-1-zoom-cmft-patent-chicago-ct0979-610-600x600.jpg', N'/assets/img/products/product_5/')
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (33, N'Converse Chuck 1970s High – Tortoise Black', CAST(2200000 AS Decimal(18, 0)), N'Converse Chuck 1970s High – Tortoise Black', 3, 7, N'/assets/img/products/thumbnails/product_image_20240403_012910.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (34, N'Converse Chuck 1970s Vintage Canvas High – Cocoon Blue', CAST(1900000 AS Decimal(18, 0)), N'Converse Chuck 1970s Vintage Canvas High – Cocoon Blue', 3, 7, N'/assets/img/products/thumbnails/product_image_20240403_013000.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (35, N'Converse Chuck 1970s At Cx Counter Climate – Vintage White', CAST(2900000 AS Decimal(18, 0)), N'Converse Chuck 1970s At Cx Counter Climate – Vintage White', 3, 7, N'/assets/img/products/thumbnails/product_image_20240403_013301.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (36, N'Converse Chuck 1970s Mule Recycled – Black', CAST(2000000 AS Decimal(18, 0)), N'Converse Chuck 1970s Mule Recycled – Black', 1, 7, N'/assets/img/products/thumbnails/product_image_20240403_013715.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (37, N'Converse Chuck 1970s High – Crafted Patchwork', CAST(2000000 AS Decimal(18, 0)), N'Converse Chuck 1970s High – Crafted Patchwork', 2, 7, N'/assets/img/products/thumbnails/product_image_20240403_014245.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (38, N'Nike Air Force 1 Low SE – Basketball Pins', CAST(3500000 AS Decimal(18, 0)), N'Nike Air Force 1 Low SE – Basketball Pins', 2, 1, N'/assets/img/products/thumbnails/product_image_20240403_014457.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (39, N'Nike Air Force 1 Shadow – Coconut Milk', CAST(3500000 AS Decimal(18, 0)), N'Nike Air Force 1 Shadow – Coconut Milk', 2, 1, N'/assets/img/products/thumbnails/product_image_20240403_014650.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (40, N'Nike Air Force 1 Low – Medium Blue', CAST(2300000 AS Decimal(18, 0)), N'Nike Air Force 1 Low – Medium Blue', 2, 1, N'/assets/img/products/thumbnails/product_image_20240403_015445.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (41, N'Nike Air Force 1 Low – Pitch Green', CAST(2800000 AS Decimal(18, 0)), N'Nike Air Force 1 Low – Pitch Green', 2, 1, N'/assets/img/products/thumbnails/product_image_20240403_015613.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (42, N'Nike Air Force 1 Low FM Joy By You – Black Multicolor', CAST(4500000 AS Decimal(18, 0)), N'Nike Air Force 1 Low FM Joy By You – Black Multicolor', 1, 1, N'/assets/img/products/thumbnails/product_image_20240403_015741.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (43, N'adidas Centennial 85 Low – White Green', CAST(2800000 AS Decimal(18, 0)), N'adidas Centennial 85 Low – White Green', 1, 2, N'/assets/img/products/thumbnails/product_image_20240403_015905.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (44, N'adidas Ultra Boost 20 – Pink', CAST(2500000 AS Decimal(18, 0)), N'adidas Ultra Boost 20 – Pink', 1, 2, N'/assets/img/products/thumbnails/product_image_20240403_020026.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (45, N'Puma Carina 2.0 – White Gold Black', CAST(1250000 AS Decimal(18, 0)), N'Puma Carina 2.0 – White Gold Black', 2, 3, N'/assets/img/products/thumbnails/product_image_20240403_020151.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (46, N'Puma Delphin RDL FS – All White', CAST(2150000 AS Decimal(18, 0)), N'Puma Delphin RDL FS – All White', 2, 3, N'/assets/img/products/thumbnails/product_image_20240403_020301.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (47, N'MLB Big Ball Chunky P Mega – New York Yankees', CAST(2200000 AS Decimal(18, 0)), N'MLB Big Ball Chunky P Mega – New York Yankees', 2, 4, N'/assets/img/products/thumbnails/product_image_20240403_020404.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (48, N'MLB Playball Monogram NY – Denim Blue', CAST(1500000 AS Decimal(18, 0)), N'MLB Playball Monogram NY – Denim Blue', 1, 4, N'/assets/img/products/thumbnails/product_image_20240403_020457.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (49, N'MLB Big Ball Chunky Monogram – White/Gold', CAST(2900000 AS Decimal(18, 0)), N'MLB Big Ball Chunky Monogram – White/Gold', 2, 4, N'/assets/img/products/thumbnails/product_image_20240403_020544.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (50, N'Vans Authentic – National Geographic', CAST(1900000 AS Decimal(18, 0)), N'Vans Authentic – National Geographic', 1, 9, N'/assets/img/products/thumbnails/product_image_20240403_020655.jpg', NULL)
INSERT [dbo].[Product] ([id], [name], [price], [desciption], [categoryID], [brandID], [thumbnail], [imagePath]) VALUES (51, N'Air Jordan 1 Mid – Lucky Green', CAST(3300000 AS Decimal(18, 0)), N'Air Jordan 1 Mid – Lucky Green', 3, 8, N'/assets/img/products/thumbnails/product_image_20240403_020749.jpg', NULL)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductVariant] ON 

INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (1, 1, 8, 7, 6500, CAST(N'2024-04-03T01:43:37.090' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (3, 1, 9, 6, 3900, CAST(N'2024-04-03T01:46:01.503' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (4, 1, 8, 9, 5700, CAST(N'2024-04-03T01:47:32.160' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (5, 5, 1, 6, 1000, CAST(N'2024-04-03T01:31:28.547' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (6, 5, 1, 7, 3500, CAST(N'2024-04-03T01:47:18.400' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (7, 5, 1, 8, 1000, CAST(N'2024-04-03T01:31:37.347' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (8, 5, 1, 9, 1500, CAST(N'2024-04-03T01:31:41.440' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (9, 1, 2, 5, 4000, CAST(N'2024-04-03T01:49:41.993' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (10, 1, 6, 7, 3000, CAST(N'2024-04-03T01:32:06.497' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (11, 33, 9, 7, 6000, CAST(N'2024-04-03T01:40:24.610' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (12, 33, 2, 3, 1000, CAST(N'2024-04-03T01:28:10.710' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (13, 34, 4, 6, 1000, CAST(N'2024-04-03T01:30:24.293' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (14, 34, 4, 8, 2000, CAST(N'2024-04-03T01:30:46.113' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (15, 34, 5, 7, 1000, CAST(N'2024-04-03T01:30:56.177' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (16, 34, 2, 8, 1000, CAST(N'2024-04-03T01:31:14.370' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (17, 35, 9, 8, 3000, CAST(N'2024-04-03T01:45:48.617' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (18, 35, 9, 9, 1000, CAST(N'2024-04-03T01:33:53.367' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (19, 35, 8, 8, 1000, CAST(N'2024-04-03T01:35:49.250' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (20, 35, 9, 11, 1000, CAST(N'2024-04-03T01:36:22.103' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (21, 36, 8, 5, 1000, CAST(N'2024-04-03T01:37:32.427' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (22, 36, 9, 4, 1000, CAST(N'2024-04-03T01:37:41.737' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (23, 36, 8, 6, 1000, CAST(N'2024-04-03T01:38:45.943' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (24, 36, 8, 4, 1000, CAST(N'2024-04-03T01:39:27.847' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (25, 37, 11, 11, 1000, CAST(N'2024-04-03T01:43:01.290' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (26, 37, 11, 7, 1000, CAST(N'2024-04-03T01:43:08.953' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (27, 37, 11, 8, 1000, CAST(N'2024-04-03T01:43:17.723' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (28, 38, 2, 7, 2000, CAST(N'2024-04-03T01:52:22.547' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (29, 38, 9, 5, 2000, CAST(N'2024-04-03T01:50:02.440' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (30, 38, 2, 6, 2000, CAST(N'2024-04-03T01:47:07.937' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (31, 38, 2, 4, 1000, CAST(N'2024-04-03T01:45:38.503' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (32, 39, 2, 17, 1000, CAST(N'2024-04-03T01:49:53.403' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (33, 39, 2, 7, 1000, CAST(N'2024-04-03T01:53:42.493' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (34, 39, 2, 5, 1000, CAST(N'2024-04-03T01:53:51.457' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (35, 39, 9, 7, 1000, CAST(N'2024-04-03T01:54:03.113' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (36, 40, 5, 5, 1000, CAST(N'2024-04-03T01:55:03.627' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (37, 40, 5, 7, 1000, CAST(N'2024-04-03T01:55:10.600' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (38, 40, 5, 8, 1000, CAST(N'2024-04-03T01:55:17.937' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (39, 40, 9, 6, 1000, CAST(N'2024-04-03T01:55:25.187' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (40, 40, 8, 11, 1000, CAST(N'2024-04-03T01:55:33.737' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (41, 41, 5, 5, 1000, CAST(N'2024-04-03T01:56:33.033' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (42, 41, 5, 6, 1000, CAST(N'2024-04-03T01:56:40.647' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (43, 41, 10, 5, 1000, CAST(N'2024-04-03T01:56:49.880' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (44, 42, 8, 10, 1000, CAST(N'2024-04-03T01:57:56.347' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (45, 42, 8, 9, 1000, CAST(N'2024-04-03T01:58:04.250' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (46, 42, 1, 7, 1000, CAST(N'2024-04-03T01:58:14.017' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (47, 42, 8, 7, 1000, CAST(N'2024-04-03T01:58:21.703' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (48, 43, 9, 4, 1000, CAST(N'2024-04-03T01:59:23.067' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (49, 43, 9, 5, 1000, CAST(N'2024-04-03T01:59:31.240' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (50, 43, 5, 7, 1000, CAST(N'2024-04-03T01:59:39.320' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (51, 44, 2, 5, 1000, CAST(N'2024-04-03T02:00:38.770' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (52, 44, 2, 6, 1000, CAST(N'2024-04-03T02:00:58.250' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (53, 45, 9, 5, 1000, CAST(N'2024-04-03T02:02:05.060' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (54, 45, 9, 15, 1000, CAST(N'2024-04-03T02:02:14.047' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (55, 45, 9, 7, 1000, CAST(N'2024-04-03T02:02:23.480' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (56, 46, 9, 6, 1000, CAST(N'2024-04-03T02:03:15.460' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (57, 46, 9, 7, 1000, CAST(N'2024-04-03T02:03:22.023' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (58, 47, 9, 9, 1000, CAST(N'2024-04-03T02:04:20.330' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (59, 48, 4, 7, 1000, CAST(N'2024-04-03T02:05:12.557' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (60, 49, 6, 6, 1000, CAST(N'2024-04-03T02:05:57.227' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (61, 49, 9, 12, 1000, CAST(N'2024-04-03T02:06:06.487' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (62, 50, 4, 7, 1000, CAST(N'2024-04-03T02:07:13.917' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (63, 51, 5, 9, 1000, CAST(N'2024-04-03T02:08:05.290' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (64, 51, 5, 6, 1000, CAST(N'2024-04-03T02:08:12.497' AS DateTime), 3)
INSERT [dbo].[ProductVariant] ([id], [productID], [colorID], [sizeID], [quantity], [update_date], [update_user]) VALUES (65, 51, 5, 16, 1000, CAST(N'2024-04-03T02:08:21.113' AS DateTime), 3)
SET IDENTITY_INSERT [dbo].[ProductVariant] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([id], [role_name], [description]) VALUES (1, N'admin', N'quan ly cua hang')
INSERT [dbo].[Role] ([id], [role_name], [description]) VALUES (2, N'sale', N'nguoi ban hang')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Size] ON 

INSERT [dbo].[Size] ([id], [size]) VALUES (2, 35)
INSERT [dbo].[Size] ([id], [size]) VALUES (3, 36)
INSERT [dbo].[Size] ([id], [size]) VALUES (4, 37)
INSERT [dbo].[Size] ([id], [size]) VALUES (5, 38)
INSERT [dbo].[Size] ([id], [size]) VALUES (6, 39)
INSERT [dbo].[Size] ([id], [size]) VALUES (7, 40)
INSERT [dbo].[Size] ([id], [size]) VALUES (8, 41)
INSERT [dbo].[Size] ([id], [size]) VALUES (9, 42)
INSERT [dbo].[Size] ([id], [size]) VALUES (10, 43)
INSERT [dbo].[Size] ([id], [size]) VALUES (11, 44)
INSERT [dbo].[Size] ([id], [size]) VALUES (12, 45)
INSERT [dbo].[Size] ([id], [size]) VALUES (15, 46)
INSERT [dbo].[Size] ([id], [size]) VALUES (16, 47)
INSERT [dbo].[Size] ([id], [size]) VALUES (17, 48)
INSERT [dbo].[Size] ([id], [size]) VALUES (18, 49)
INSERT [dbo].[Size] ([id], [size]) VALUES (19, 50)
SET IDENTITY_INSERT [dbo].[Size] OFF
GO
SET IDENTITY_INSERT [dbo].[Slider] ON 

INSERT [dbo].[Slider] ([id], [image], [status]) VALUES (1, N'/assets/img/slider/banner-tet.png', 1)
INSERT [dbo].[Slider] ([id], [image], [status]) VALUES (3, N'/assets/img/slider/sale-cuoimua.jpg', 1)
INSERT [dbo].[Slider] ([id], [image], [status]) VALUES (7, N'/assets/img/slider/slider_20240331_185544.jpg', 1)
SET IDENTITY_INSERT [dbo].[Slider] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([id], [email], [fullname], [address], [phone], [image], [birthday]) VALUES (4, N'dongquanglinhsc@gmail.com', N'Đồng Quang Linh', N'Hà Nội, Việt Nam', N'123', N'/assets/img/account/profile/profile_4_20240325_004819.jpeg', CAST(N'2000-12-22' AS Date))
INSERT [dbo].[User] ([id], [email], [fullname], [address], [phone], [image], [birthday]) VALUES (5, N'linhdqhe140751@fpt.edu.vn', N'Dong Quang Linh (K14 HL)', N'', N'', N'/assets/img/illustrations/profiles/profile-2.png', NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Admin]  WITH CHECK ADD  CONSTRAINT [FK_Admin_Role] FOREIGN KEY([roleId])
REFERENCES [dbo].[Role] ([id])
GO
ALTER TABLE [dbo].[Admin] CHECK CONSTRAINT [FK_Admin_Role]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_User] FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_User]
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD  CONSTRAINT [FK_CartItems_Cart] FOREIGN KEY([cartID])
REFERENCES [dbo].[Cart] ([id])
GO
ALTER TABLE [dbo].[CartItems] CHECK CONSTRAINT [FK_CartItems_Cart]
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD  CONSTRAINT [FK_CartItems_ProductVariant] FOREIGN KEY([productID])
REFERENCES [dbo].[ProductVariant] ([id])
GO
ALTER TABLE [dbo].[CartItems] CHECK CONSTRAINT [FK_CartItems_ProductVariant]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Admin] FOREIGN KEY([saleID])
REFERENCES [dbo].[Admin] ([id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Admin]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Payment] FOREIGN KEY([payment_method])
REFERENCES [dbo].[Payment] ([id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Payment]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Order] FOREIGN KEY([orderID])
REFERENCES [dbo].[Order] ([id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Order]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_ProductVariant] FOREIGN KEY([productID])
REFERENCES [dbo].[ProductVariant] ([id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_ProductVariant]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Brand] FOREIGN KEY([brandID])
REFERENCES [dbo].[Brand] ([id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Brand]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([categoryID])
REFERENCES [dbo].[Category] ([id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[ProductVariant]  WITH CHECK ADD  CONSTRAINT [FK_ProductVariant_Color] FOREIGN KEY([colorID])
REFERENCES [dbo].[Color] ([id])
GO
ALTER TABLE [dbo].[ProductVariant] CHECK CONSTRAINT [FK_ProductVariant_Color]
GO
ALTER TABLE [dbo].[ProductVariant]  WITH CHECK ADD  CONSTRAINT [FK_ProductVariant_Product] FOREIGN KEY([productID])
REFERENCES [dbo].[Product] ([id])
GO
ALTER TABLE [dbo].[ProductVariant] CHECK CONSTRAINT [FK_ProductVariant_Product]
GO
ALTER TABLE [dbo].[ProductVariant]  WITH CHECK ADD  CONSTRAINT [FK_ProductVariant_Size] FOREIGN KEY([sizeID])
REFERENCES [dbo].[Size] ([id])
GO
ALTER TABLE [dbo].[ProductVariant] CHECK CONSTRAINT [FK_ProductVariant_Size]
GO
USE [master]
GO
ALTER DATABASE [DBShoesShop] SET  READ_WRITE 
GO

USE [master]
GO
/****** Object:  Database [OnlineShop]    Script Date: 9/13/2024 4:35:08 PM ******/
CREATE DATABASE [OnlineShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OnlineShop', FILENAME = N'C:\Users\hungnd\OnlineShop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OnlineShop_log', FILENAME = N'C:\Users\hungnd\OnlineShop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [OnlineShop] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnlineShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnlineShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OnlineShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OnlineShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OnlineShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OnlineShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [OnlineShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OnlineShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OnlineShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OnlineShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OnlineShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OnlineShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OnlineShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OnlineShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OnlineShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OnlineShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OnlineShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OnlineShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OnlineShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OnlineShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OnlineShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OnlineShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OnlineShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OnlineShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OnlineShop] SET  MULTI_USER 
GO
ALTER DATABASE [OnlineShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OnlineShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OnlineShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OnlineShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OnlineShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OnlineShop] SET QUERY_STORE = OFF
GO
USE [OnlineShop]
GO
/****** Object:  Table [dbo].[About]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[About](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Content] [ntext] NULL,
 CONSTRAINT [PK_About] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartDetail]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CartID] [int] NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_CartDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[ParentID] [int] NULL,
	[DisplayOrder] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Config]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config](
	[ID] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Type] [varchar](50) NULL,
	[Value] [nvarchar](250) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Config] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Content] [ntext] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Content]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Description] [nvarchar](500) NULL,
	[Image] [nvarchar](250) NULL,
	[CategoryID] [int] NULL,
	[Detail] [ntext] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
	[ViewCount] [int] NULL,
	[Tags] [nvarchar](500) NULL,
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContentTag]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContentTag](
	[ContentID] [int] NOT NULL,
	[TagID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ContentTag] PRIMARY KEY CLUSTERED 
(
	[ContentID] ASC,
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[Status] [bit] NULL,
	[Content] [nvarchar](50) NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Footer]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Footer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Content] [ntext] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Footer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](50) NULL,
	[Link] [nvarchar](250) NULL,
	[DisplayOrder] [int] NULL,
	[Target] [nvarchar](50) NULL,
	[Status] [bit] NULL,
	[MenuTypeID] [int] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuType]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_MenuType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CustomerID] [int] NULL,
	[Status] [int] NULL,
	[PaymentMethod] [nvarchar](100) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[Quantity] [int] NULL,
	[Price] [decimal](18, 0) NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](20) NULL,
	[Name] [nvarchar](250) NULL,
	[Description] [nvarchar](500) NULL,
	[Image] [nvarchar](250) NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[PromotionPrice] [decimal](18, 0) NULL,
	[Quantity] [int] NULL,
	[ProductCategoryID] [int] NOT NULL,
	[Detail] [ntext] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
	[ViewCount] [int] NULL,
	[Slug] [nvarchar](250) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[ParentID] [int] NULL,
	[DisplayOrder] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[Status] [bit] NOT NULL,
	[Slug] [nvarchar](250) NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slide]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slide](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Image] [nvarchar](250) NULL,
	[Status] [bit] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Slide] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tag]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[ID] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 9/13/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](20) NOT NULL,
	[Password] [varchar](32) NOT NULL,
	[LastName] [varchar](50) NULL,
	[FirstName] [varchar](50) NULL,
	[Phone] [varchar](10) NULL,
	[Email] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[UpdatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
	[Image] [nvarchar](250) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID], [Name], [ParentID], [DisplayOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Status]) VALUES (2, N'g', 1, 5, CAST(N'2024-08-09T09:22:15.847' AS DateTime), NULL, CAST(N'2024-08-09T09:24:38.963' AS DateTime), NULL, 1)
INSERT [dbo].[Category] ([ID], [Name], [ParentID], [DisplayOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Status]) VALUES (3, N'j40', 10, 10, CAST(N'2024-08-09T11:19:16.797' AS DateTime), NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Contact] ON 

INSERT [dbo].[Contact] ([ID], [Content], [Status]) VALUES (1, N'<p>abc<p>', 1)
SET IDENTITY_INSERT [dbo].[Contact] OFF
SET IDENTITY_INSERT [dbo].[Content] ON 

INSERT [dbo].[Content] ([ID], [Name], [Description], [Image], [CategoryID], [Detail], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [ViewCount], [Tags]) VALUES (1, N'g', N'g', N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1723084929/takmvbheoojyk6uvswc7.jpg', 2, N'g', NULL, CAST(N'2024-08-08T09:42:10.287' AS DateTime), NULL, CAST(N'2024-08-09T14:03:23.190' AS DateTime), 1, NULL, N'1')
INSERT [dbo].[Content] ([ID], [Name], [Description], [Image], [CategoryID], [Detail], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [ViewCount], [Tags]) VALUES (1002, N'hihi', N'g', N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1723175117/janxclz5lb0b7cog6iyd.jpg', 3, N'g', NULL, CAST(N'2024-08-09T10:45:17.610' AS DateTime), NULL, CAST(N'2024-08-09T14:03:39.033' AS DateTime), 1, NULL, N'1')
SET IDENTITY_INSERT [dbo].[Content] OFF
SET IDENTITY_INSERT [dbo].[Footer] ON 

INSERT [dbo].[Footer] ([ID], [Content], [Status]) VALUES (1, N'   <div class="wrap">
       <div class="section group">
           <div class="col_1_of_4 span_1_of_4">
               <h4>Information</h4>
               <ul>
                   <li><a href="about.html">About Us</a></li>
                   <li><a href="contact.html">Customer Service</a></li>
                   <li><a href="#">Advanced Search</a></li>
                   <li><a href="delivery.html">Orders and Returns</a></li>
                   <li><a href="contact.html">Contact Us</a></li>
               </ul>
           </div>
           <div class="col_1_of_4 span_1_of_4">
               <h4>Why buy from us</h4>
               <ul>
                   <li><a href="about.html">About Us</a></li>
                   <li><a href="contact.html">Customer Service</a></li>
                   <li><a href="#">Privacy Policy</a></li>
                   <li><a href="contact.html">Site Map</a></li>
                   <li><a href="#">Search Terms</a></li>
               </ul>
           </div>
           <div class="col_1_of_4 span_1_of_4">
               <h4>My account</h4>
               <ul>
                   <li><a href="contact.html">Sign In</a></li>
                   <li><a href="index.html">View Cart</a></li>
                   <li><a href="#">My Wishlist</a></li>
                   <li><a href="#">Track My Order</a></li>
                   <li><a href="contact.html">Help</a></li>
               </ul>
           </div>
           <div class="col_1_of_4 span_1_of_4">
               <h4>Contact</h4>
               <ul>
                   <li><span>+91-123-456789</span></li>
                   <li><span>+00-123-000000</span></li>
               </ul>
               <div class="social-icons">
                   <h4>Follow Us</h4>
                   <ul>
                       <li><a href="#" target="_blank"><img src="/Assets/Client/images/facebook.png" alt="" /></a></li>
                       <li><a href="#" target="_blank"><img src="/Assets/Client/images/twitter.png" alt="" /></a></li>
                       <li><a href="#" target="_blank"><img src="/Assets/Client/images/skype.png" alt="" /> </a></li>
                       <li><a href="#" target="_blank"> <img src="/Assets/Client/images/dribbble.png" alt="" /></a></li>
                       <li><a href="#" target="_blank"> <img src="/Assets/Client/images/linkedin.png" alt="" /></a></li>
                       <div class="clear"></div>
                   </ul>
               </div>
           </div>
       </div>
   </div>
   <div class="copy_right">
       <p>&copy; 2013 home_shoppe. All rights reserved | Design by <a href="http://w3layouts.com/">W3layouts</a></p>
   </div>', 1)
SET IDENTITY_INSERT [dbo].[Footer] OFF
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([ID], [Text], [Link], [DisplayOrder], [Target], [Status], [MenuTypeID]) VALUES (1, N'Trang chủ', N'/', 1, N'_blank', 1, 2)
INSERT [dbo].[Menu] ([ID], [Text], [Link], [DisplayOrder], [Target], [Status], [MenuTypeID]) VALUES (2, N'Giới thiệu', N'/gioi-thieu', 2, N'_self', 1, 2)
INSERT [dbo].[Menu] ([ID], [Text], [Link], [DisplayOrder], [Target], [Status], [MenuTypeID]) VALUES (3, N'Sản phẩm', N'/', 3, N'_self', 1, 2)
INSERT [dbo].[Menu] ([ID], [Text], [Link], [DisplayOrder], [Target], [Status], [MenuTypeID]) VALUES (4, N'Đăng ký', N'/dang-ky', 1, NULL, 1, 1)
INSERT [dbo].[Menu] ([ID], [Text], [Link], [DisplayOrder], [Target], [Status], [MenuTypeID]) VALUES (5, N'Đăng nhập', N'/dang-nhap', 2, NULL, 1, 1)
INSERT [dbo].[Menu] ([ID], [Text], [Link], [DisplayOrder], [Target], [Status], [MenuTypeID]) VALUES (1002, N'Liên hệ', N'/lien-he', 4, N'_self', 1, 2)
SET IDENTITY_INSERT [dbo].[Menu] OFF
SET IDENTITY_INSERT [dbo].[MenuType] ON 

INSERT [dbo].[MenuType] ([ID], [Name]) VALUES (1, N'Menu top')
INSERT [dbo].[MenuType] ([ID], [Name]) VALUES (2, N'Menu chính')
SET IDENTITY_INSERT [dbo].[MenuType] OFF
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [Status], [PaymentMethod]) VALUES (3002, CAST(N'2024-09-06T14:40:55.600' AS DateTime), 1, 1, NULL)
SET IDENTITY_INSERT [dbo].[Order] OFF
SET IDENTITY_INSERT [dbo].[OrderDetail] ON 

INSERT [dbo].[OrderDetail] ([ID], [ProductID], [OrderID], [Quantity], [Price]) VALUES (1, 5018, 3002, 3, NULL)
INSERT [dbo].[OrderDetail] ([ID], [ProductID], [OrderID], [Quantity], [Price]) VALUES (2, 4023, 3002, 1, NULL)
SET IDENTITY_INSERT [dbo].[OrderDetail] OFF
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [Code], [Name], [Description], [Image], [Price], [PromotionPrice], [Quantity], [ProductCategoryID], [Detail], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [ViewCount], [Slug]) VALUES (8, N'gvsdfgsf', N'Samsung Galaxy A3', N'2', N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1722918076/rml5wj6jithtvnjjwef2.jpg', CAST(2 AS Decimal(18, 0)), NULL, 2, 1, N'2', NULL, CAST(N'2024-08-05T11:47:17.097' AS DateTime), NULL, CAST(N'2024-08-23T14:36:23.873' AS DateTime), 1, 20, N'samsung-galaxy-a3')
INSERT [dbo].[Product] ([ID], [Code], [Name], [Description], [Image], [Price], [PromotionPrice], [Quantity], [ProductCategoryID], [Detail], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [ViewCount], [Slug]) VALUES (9, N'2', N'dien tu', N'2', N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1722838152/xk3dx9nzde7gqumbp9ze.jpg', CAST(2 AS Decimal(18, 0)), NULL, 2, 1, N'2', NULL, CAST(N'2024-08-05T13:09:13.233' AS DateTime), NULL, CAST(N'2024-08-23T14:36:34.367' AS DateTime), 1, 0, NULL)
INSERT [dbo].[Product] ([ID], [Code], [Name], [Description], [Image], [Price], [PromotionPrice], [Quantity], [ProductCategoryID], [Detail], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [ViewCount], [Slug]) VALUES (2010, N'q', N'q', N'q', N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1722918076/rml5wj6jithtvnjjwef2.jpg', CAST(1 AS Decimal(18, 0)), CAST(1 AS Decimal(18, 0)), 1, 1, N'q', NULL, CAST(N'2024-08-07T14:36:13.123' AS DateTime), NULL, CAST(N'2024-08-07T14:36:22.530' AS DateTime), 1, 0, NULL)
INSERT [dbo].[Product] ([ID], [Code], [Name], [Description], [Image], [Price], [PromotionPrice], [Quantity], [ProductCategoryID], [Detail], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [ViewCount], [Slug]) VALUES (2011, N'q', N'q', N'q', N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1722918076/rml5wj6jithtvnjjwef2.jpg', CAST(1 AS Decimal(18, 0)), CAST(1 AS Decimal(18, 0)), 1, 1, N'q', NULL, CAST(N'2024-08-07T14:36:57.627' AS DateTime), NULL, CAST(N'2024-08-07T14:37:12.323' AS DateTime), 1, 0, NULL)
INSERT [dbo].[Product] ([ID], [Code], [Name], [Description], [Image], [Price], [PromotionPrice], [Quantity], [ProductCategoryID], [Detail], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [ViewCount], [Slug]) VALUES (3010, N'g', N'g', N'g', N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1722918076/rml5wj6jithtvnjjwef2.jpg', CAST(12 AS Decimal(18, 0)), CAST(12 AS Decimal(18, 0)), 12, 1, N'g', NULL, CAST(N'2024-08-09T14:25:48.537' AS DateTime), NULL, CAST(N'2024-08-09T14:31:12.927' AS DateTime), 0, 1, NULL)
INSERT [dbo].[Product] ([ID], [Code], [Name], [Description], [Image], [Price], [PromotionPrice], [Quantity], [ProductCategoryID], [Detail], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [ViewCount], [Slug]) VALUES (3011, N'g', N'g', N'g', N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1722918076/rml5wj6jithtvnjjwef2.jpg', CAST(12 AS Decimal(18, 0)), NULL, 5, 3, N'g', NULL, CAST(N'2024-08-14T15:16:34.857' AS DateTime), NULL, NULL, 1, 4, NULL)
INSERT [dbo].[Product] ([ID], [Code], [Name], [Description], [Image], [Price], [PromotionPrice], [Quantity], [ProductCategoryID], [Detail], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [ViewCount], [Slug]) VALUES (4018, N'g', N'g', N'g', N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1722838152/xk3dx9nzde7gqumbp9ze.jpg', CAST(12 AS Decimal(18, 0)), CAST(50 AS Decimal(18, 0)), 5, 1002, N'g', NULL, CAST(N'2024-08-16T14:50:07.243' AS DateTime), NULL, NULL, 1, 2, NULL)
INSERT [dbo].[Product] ([ID], [Code], [Name], [Description], [Image], [Price], [PromotionPrice], [Quantity], [ProductCategoryID], [Detail], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [ViewCount], [Slug]) VALUES (4019, N'g', N'g', N'g', N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1722838152/xk3dx9nzde7gqumbp9ze.jpg', CAST(12 AS Decimal(18, 0)), CAST(50 AS Decimal(18, 0)), 5, 1, N'g', NULL, CAST(N'2024-08-16T15:06:34.490' AS DateTime), NULL, NULL, 1, 2, NULL)
INSERT [dbo].[Product] ([ID], [Code], [Name], [Description], [Image], [Price], [PromotionPrice], [Quantity], [ProductCategoryID], [Detail], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [ViewCount], [Slug]) VALUES (4020, N'g', N'g', N'g', N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1722838152/xk3dx9nzde7gqumbp9ze.jpg', CAST(1 AS Decimal(18, 0)), CAST(1 AS Decimal(18, 0)), 1, 1, N'g', NULL, CAST(N'2024-08-16T15:08:06.847' AS DateTime), NULL, NULL, 1, 9, NULL)
INSERT [dbo].[Product] ([ID], [Code], [Name], [Description], [Image], [Price], [PromotionPrice], [Quantity], [ProductCategoryID], [Detail], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [ViewCount], [Slug]) VALUES (4023, N'g', N'g', N'g', N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1722838152/xk3dx9nzde7gqumbp9ze.jpg', CAST(0 AS Decimal(18, 0)), NULL, 0, 1, NULL, NULL, CAST(N'2024-08-16T15:10:23.860' AS DateTime), NULL, NULL, 1, 10, NULL)
INSERT [dbo].[Product] ([ID], [Code], [Name], [Description], [Image], [Price], [PromotionPrice], [Quantity], [ProductCategoryID], [Detail], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [ViewCount], [Slug]) VALUES (5018, N'g', N'dien tu', N'g', N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1722838152/xk3dx9nzde7gqumbp9ze.jpg', CAST(12 AS Decimal(18, 0)), CAST(1 AS Decimal(18, 0)), 5, 1, N'g', NULL, CAST(N'2024-08-23T16:50:41.090' AS DateTime), NULL, NULL, 1, 2, NULL)
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[ProductCategory] ON 

INSERT [dbo].[ProductCategory] ([ID], [Name], [ParentID], [DisplayOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Status], [Slug]) VALUES (1, N'Điện thoại', NULL, 1, CAST(N'2024-08-08T14:23:54.660' AS DateTime), NULL, CAST(N'2024-08-12T14:33:42.963' AS DateTime), NULL, 1, N'dien-thoai')
INSERT [dbo].[ProductCategory] ([ID], [Name], [ParentID], [DisplayOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Status], [Slug]) VALUES (3, N'Máy tính để bàn', NULL, 1, CAST(N'2024-08-09T14:25:08.783' AS DateTime), NULL, CAST(N'2024-08-12T14:33:55.917' AS DateTime), NULL, 1, N'may-tinh-de-ban')
INSERT [dbo].[ProductCategory] ([ID], [Name], [ParentID], [DisplayOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Status], [Slug]) VALUES (1002, N'Máy tính xách tay', NULL, 1, CAST(N'2024-08-12T14:34:09.287' AS DateTime), NULL, NULL, NULL, 1, N'may-tinh-xach-tay')
INSERT [dbo].[ProductCategory] ([ID], [Name], [ParentID], [DisplayOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Status], [Slug]) VALUES (1003, N'Phụ kiện', NULL, 1, CAST(N'2024-08-12T14:35:07.023' AS DateTime), NULL, NULL, NULL, 1, N'phu-kien')
INSERT [dbo].[ProductCategory] ([ID], [Name], [ParentID], [DisplayOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Status], [Slug]) VALUES (1004, N'Tablet', NULL, 1, CAST(N'2024-08-12T14:35:30.790' AS DateTime), NULL, NULL, NULL, 1, N'tablet')
INSERT [dbo].[ProductCategory] ([ID], [Name], [ParentID], [DisplayOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Status], [Slug]) VALUES (1005, N'Đồng hồ', NULL, 1, CAST(N'2024-08-12T14:35:43.560' AS DateTime), NULL, NULL, NULL, 1, N'dong-ho')
INSERT [dbo].[ProductCategory] ([ID], [Name], [ParentID], [DisplayOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Status], [Slug]) VALUES (1006, N'Smartwatch', NULL, 1, CAST(N'2024-08-12T14:37:26.060' AS DateTime), NULL, NULL, NULL, 1, N'smartwatch')
SET IDENTITY_INSERT [dbo].[ProductCategory] OFF
SET IDENTITY_INSERT [dbo].[Slide] ON 

INSERT [dbo].[Slide] ([ID], [Image], [Status], [Name]) VALUES (1, N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1722937165/qwahipcuvyyh7vzs9dup.jpg', 1, N'1')
SET IDENTITY_INSERT [dbo].[Slide] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [UserName], [Password], [LastName], [FirstName], [Phone], [Email], [Address], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [Image]) VALUES (1, N'admin', N'81dc9bdb52d04dc20036dbd8313ed055', N'Nguyen Duc                                        ', N'Hung                                              ', N'0384296041', N'nguyenduchung.02092002@gmail.com                  ', N'Ho Chi Minh City                                  ', NULL, CAST(N'2024-08-01T13:49:39.547' AS DateTime), NULL, CAST(N'2024-09-12T15:57:39.520' AS DateTime), 1, N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1722937165/qwahipcuvyyh7vzs9dup.jpg')
INSERT [dbo].[User] ([ID], [UserName], [Password], [LastName], [FirstName], [Phone], [Email], [Address], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [Image]) VALUES (3002, N'admin2              ', N'250cf8b51c773f3f8dc8b4be867a9a02', NULL, N'Giang                                             ', N'0384296041', N'2051010122hung@ou.edu.vn                          ', N'hcm                                               ', NULL, CAST(N'2024-08-06T11:53:06.470' AS DateTime), NULL, CAST(N'2024-08-07T10:09:04.470' AS DateTime), 1, NULL)
INSERT [dbo].[User] ([ID], [UserName], [Password], [LastName], [FirstName], [Phone], [Email], [Address], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [Image]) VALUES (3003, N'admin23             ', N'202cb962ac59075b964b07152d234b70', N'Nguyen Duc                                        ', N'Hung                                              ', N'0384296041', N'nguyenduchung.02092002@gmail.com                  ', N'Ho Chi Minh City                                  ', NULL, CAST(N'2024-08-06T16:14:57.843' AS DateTime), NULL, CAST(N'2024-08-07T10:09:14.933' AS DateTime), 1, NULL)
INSERT [dbo].[User] ([ID], [UserName], [Password], [LastName], [FirstName], [Phone], [Email], [Address], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [Image]) VALUES (3004, N'duchung             ', N'202cb962ac59075b964b07152d234b70', N'Duc                                               ', N'Hung                                              ', N'0384296041', N'2051010122hung@ou.edu.vn                          ', N'hcm                                               ', NULL, CAST(N'2024-08-06T16:27:53.867' AS DateTime), NULL, CAST(N'2024-08-07T14:24:00.020' AS DateTime), 1, N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1722937165/qwahipcuvyyh7vzs9dup.jpg')
INSERT [dbo].[User] ([ID], [UserName], [Password], [LastName], [FirstName], [Phone], [Email], [Address], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [Image]) VALUES (4005, N'admin123', N'123', N'Nguy?n', N'Ð?c Hung', N'0384296041', N'1010@gmail.com', N'abc', NULL, CAST(N'2024-08-20T15:08:44.300' AS DateTime), NULL, NULL, 1, NULL)
INSERT [dbo].[User] ([ID], [UserName], [Password], [LastName], [FirstName], [Phone], [Email], [Address], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [Image]) VALUES (4006, N'admin12345', N'202cb962ac59075b964b07152d234b70', N'Nguy?n', N'Ð?c Hung', N'0384296041', N'101010@gmail.com', N'abc', NULL, CAST(N'2024-08-20T15:15:49.197' AS DateTime), NULL, NULL, 1, N'https://res.cloudinary.com/dluqeqfiv/image/upload/v1724141748/x6fyybtisqdscu2ghdjp.jpg')
INSERT [dbo].[User] ([ID], [UserName], [Password], [LastName], [FirstName], [Phone], [Email], [Address], [UpdatedBy], [CreatedDate], [CreatedBy], [UpdatedDate], [Status], [Image]) VALUES (5005, N'admin123456', N'22ee7b247c8375057f79369aad77683a', N'Nguy?n', N'Ð?c Hung', N'0384296041', N'10104@gmail.com', N'abc', NULL, CAST(N'2024-08-23T15:51:23.040' AS DateTime), NULL, NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Cart] ADD  CONSTRAINT [DF_Cart_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_DisplayOrder]  DEFAULT ((0)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Config] ADD  CONSTRAINT [DF_Config_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Content] ADD  CONSTRAINT [DF_Content_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Content] ADD  CONSTRAINT [DF_Content_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Content] ADD  CONSTRAINT [DF_Content_ViewCount]  DEFAULT ((0)) FOR [ViewCount]
GO
ALTER TABLE [dbo].[Feedback] ADD  CONSTRAINT [DF_Feedback_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Feedback] ADD  CONSTRAINT [DF_Feedback_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Footer] ADD  CONSTRAINT [DF_Footer_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Menu] ADD  CONSTRAINT [DF_Menu_DisplayOrder]  DEFAULT ((0)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[Menu] ADD  CONSTRAINT [DF_Menu_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_Quantity]  DEFAULT ((1)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Price]  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Quantity]  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_ViewCount]  DEFAULT ((0)) FOR [ViewCount]
GO
ALTER TABLE [dbo].[ProductCategory] ADD  CONSTRAINT [DF_ProductCategory_DisplayOrder]  DEFAULT ((0)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[ProductCategory] ADD  CONSTRAINT [DF_ProductCategory_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ProductCategory] ADD  CONSTRAINT [DF_ProductCategory_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
USE [master]
GO
ALTER DATABASE [OnlineShop] SET  READ_WRITE 
GO

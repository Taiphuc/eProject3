USE [master]
GO
/****** Object:  Database [Project3_Db]    Script Date: 7/12/2021 2:47:22 PM ******/
CREATE DATABASE [Project3_Db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Project3_Db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVERNEW\MSSQL\DATA\Project3_Db.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Project3_Db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVERNEW\MSSQL\DATA\Project3_Db_log.ldf' , SIZE = 784KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Project3_Db] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Project3_Db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Project3_Db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Project3_Db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Project3_Db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Project3_Db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Project3_Db] SET ARITHABORT OFF 
GO
ALTER DATABASE [Project3_Db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Project3_Db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Project3_Db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Project3_Db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Project3_Db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Project3_Db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Project3_Db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Project3_Db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Project3_Db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Project3_Db] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Project3_Db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Project3_Db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Project3_Db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Project3_Db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Project3_Db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Project3_Db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Project3_Db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Project3_Db] SET RECOVERY FULL 
GO
ALTER DATABASE [Project3_Db] SET  MULTI_USER 
GO
ALTER DATABASE [Project3_Db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Project3_Db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Project3_Db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Project3_Db] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Project3_Db', N'ON'
GO
USE [Project3_Db]
GO
/****** Object:  Table [dbo].[About]    Script Date: 7/12/2021 2:47:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[About](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedOn] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedOn] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_About] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 7/12/2021 2:47:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](256) NULL,
	[ObjectName] [nvarchar](256) NULL,
	[Address] [nvarchar](256) NULL,
	[Mobile] [varchar](20) NULL,
	[Website] [varchar](256) NULL,
	[Email] [varchar](256) NULL,
	[CreatedOn] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[ModifiedOn] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__Contact__3214EC07B68E50F5] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Credential]    Script Date: 7/12/2021 2:47:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Credential](
	[UserGroupId] [varchar](50) NOT NULL,
	[RoleId] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Credential_1] PRIMARY KEY CLUSTERED 
(
	[UserGroupId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 7/12/2021 2:47:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NULL,
	[Phone] [varchar](50) NULL,
	[Email] [varchar](256) NULL,
	[Address] [nvarchar](256) NULL,
	[Content] [nvarchar](max) NULL,
	[Reply] [nvarchar](max) NULL,
	[CreatedOn] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[ModifiedOn] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__Feedback__3214EC07ABFD67FA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Footer]    Script Date: 7/12/2021 2:47:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Footer](
	[Id] [uniqueidentifier] NOT NULL,
	[CoppyRight] [nvarchar](100) NULL,
	[Address] [nvarchar](256) NULL,
	[PhoneNumber] [nvarchar](256) NULL,
	[CreatedOn] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[ModifiedOn] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__Footer__3214EC07852F6A97] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 7/12/2021 2:47:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](256) NOT NULL,
	[Link] [nvarchar](256) NULL,
	[DisplayOrder] [int] NULL,
	[ParentId] [int] NULL,
	[Target] [nvarchar](256) NOT NULL,
	[CreatedOn] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[ModifiedOn] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__Menu__3214EC079B6A10D6] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 7/12/2021 2:47:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NULL,
	[Summary] [nvarchar](max) NULL,
	[MetaTitle] [varchar](256) NULL,
	[Description] [nvarchar](max) NULL,
	[UrlImage] [nvarchar](256) NULL,
	[NewsCategoryId] [uniqueidentifier] NULL,
	[Warranty] [int] NULL,
	[MetaKeywords] [nvarchar](256) NULL,
	[MetaDescriptions] [nvarchar](256) NULL,
	[TopHot] [datetime] NULL,
	[ViewCount] [int] NULL,
	[Tag] [nvarchar](256) NULL,
	[CreatedOn] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[ModifiedOn] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__Content__3214EC0722536217] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsCategory]    Script Date: 7/12/2021 2:47:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsCategory](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NULL,
	[MetaTitle] [varchar](256) NULL,
	[ParentId] [int] NULL,
	[DisplayOrder] [int] NULL,
	[SeoTitle] [nvarchar](256) NULL,
	[MetaKeywords] [nvarchar](256) NULL,
	[CreatedOn] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[ModifiedOn] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__Category__3214EC07DEE8C6AA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 7/12/2021 2:47:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ShipName] [nvarchar](256) NOT NULL,
	[ShipPhone] [nvarchar](50) NOT NULL,
	[ShipAddress] [nvarchar](256) NOT NULL,
	[ShipEmail] [varchar](256) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[ModifiedOn] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__Order__3214EC07DF9E2F66] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 7/12/2021 2:47:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[ProductId] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NULL,
	[Price] [decimal](18, 0) NULL,
	[CreatedOn] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[ModifiedOn] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 7/12/2021 2:47:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NULL,
	[Code] [decimal](18, 0) NULL,
	[MetaTitle] [nvarchar](256) NULL,
	[Description] [nvarchar](max) NULL,
	[UrlImage] [nvarchar](256) NULL,
	[Price] [decimal](18, 0) NULL,
	[PromotionPrice] [decimal](18, 0) NULL,
	[IncludeVAT] [bit] NULL,
	[Quantity] [int] NULL,
	[ProductCategoryId] [uniqueidentifier] NULL,
	[Detail] [ntext] NULL,
	[Warranty] [int] NULL,
	[MetaKeywords] [nvarchar](256) NULL,
	[MetaDescriptions] [nvarchar](max) NULL,
	[TopHot] [bit] NULL,
	[IsNormalProduct2] [bit] NULL,
	[IsNormalProduct1] [bit] NULL,
	[IsNewProduct] [bit] NULL,
	[ViewCount] [int] NULL,
	[CreatedOn] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[ModifiedOn] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__Product__3214EC072E0A9E49] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 7/12/2021 2:47:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NULL,
	[MetaTitle] [varchar](256) NULL,
	[ParentId] [int] NULL,
	[DisplayOrder] [int] NULL,
	[SeoTitle] [nvarchar](256) NULL,
	[MetaKeywords] [nvarchar](256) NULL,
	[MetaDescriptions] [nvarchar](256) NULL,
	[CreatedOn] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[ModifiedOn] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__ProductC__3214EC077834B296] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 7/12/2021 2:47:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slide]    Script Date: 7/12/2021 2:47:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slide](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UrlImage] [nvarchar](256) NOT NULL,
	[DisplayOrder] [int] NULL,
	[Link] [nvarchar](256) NULL,
	[Description] [nvarchar](256) NULL,
	[CreatedOn] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[ModifiedOn] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__Slide__3214EC073DFECF16] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/12/2021 2:47:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[GroupId] [varchar](50) NULL,
	[AvatarUrl] [nvarchar](256) NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NULL,
	[ConfirmPassword] [varchar](50) NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[ResetPasswordCode] [nvarchar](100) NULL,
	[Email] [varchar](256) NULL,
	[Address] [nvarchar](max) NULL,
	[CreatedOn] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[ModifiedOn] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__User__3214EC07B2B93D14] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 7/12/2021 2:47:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup](
	[Id] [varchar](50) NOT NULL,
	[Name] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[About] ([Id], [Title], [Description], [Content], [IsDeleted], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (N'71725cfd-cf6d-49d9-b152-9102907d997e', N'Giới thiệu thông tin cửa hàng,  sứ mệnh cửa hàng', N'Ga là một trong những phụ kiện không thể thiếu góp phần tạo nên những đám cháy nổi bật. Chính vì điều đó Gas Xịn mong muốn đem đến những bình ga mới nhất được cập nhật thường xuyên giúp các bạn dễ dàng hơn trong việc lựa chọn và cập nhật những cách cháy nổ thịnh hành của ngành cháy nổ trong nước và thế giới.', N'<p>Bình ga l&agrave; một trong những phụ kiện kh&ocirc;ng thể thiếu g&oacute;p phần tạo n&ecirc;n phong c&aacute;ch cháy nổ của mỗi người. Ch&iacute;nh v&igrave; điều đ&oacute; Gas Vip Pro mong muốn đem đến những bình ga mới nhất được cập nhật thường xuy&ecirc;n gi&uacute;p c&aacute;c bạn dễ d&agrave;ng hơn trong việc lựa chọn v&agrave; cập nhật những phong cách cháy nổ thịnh h&agrave;nh của ngành cháy nổ trong nước v&agrave; thế giới.</p>

<p>Gas Vip Pro&nbsp;được biết đến với những sản phẩm bếp ga, bình ga hợp thời trang, nhập trực tiếp từ những c&ocirc;ng ty ga uy t&iacute;n, cao cấp m&agrave; kh&ocirc;ng qua trung gian, đảm bảo về chất lượng v&agrave; gi&aacute; th&agrave;nh &hellip; Gas Vip Pro cung cấp mọi kiểu d&aacute;ng bếp ga, bình ga, ... mới nhất: Bình to b&uacute;p b&ecirc;, bình nhỏ tro cốt, bình b&aacute;nh mỳ, bình bệt, bình vải &hellip;.Nếu l&agrave; một người y&ecirc;u thời trang, t&igrave;m kiếm những phong c&aacute;ch mới, th&igrave; bạn kh&ocirc;ng thể bỏ qua những xu hướng nổi bật của việc bốc cháy : combat gas, gas orford, ga ra ô tô, gas kim loại, phong c&aacute;ch sporty với gas đế b&aacute;nh m&igrave;&hellip;Thật dễ d&agrave;ng, chỉ với một click bạn c&oacute; thể nhanh ch&oacute;ng sở hữu ch&uacute;ng trong tủ đồ của m&igrave;nh với Gas Vip Pro.</p>

<h2><strong>Li&ecirc;n hệ với ch&uacute;ng t&ocirc;i:</strong></h2>

<p>Phone: 0947 569 488</p>

<p>Website:&nbsp;<a href="http://www.freestyleshop.com/">www.freestyleshop.com/</a></p>

<p>Facebook:&nbsp;<a href="https://www.facebook.com/freestyleshop">https://www.facebook.com/freestyleshop</a></p>

<p>Facebook &nbsp;ch&iacute;nh:&nbsp;https://www.facebook.com/freestyleshop</p>

<h2><strong>Free Style Shop cam kết:</strong></h2>

<p>- H&agrave;ng thật giống 100% ảnh chụp về kiểu d&aacute;ng, mẫu m&atilde;.</p>

<p>- Mẫu mới cập nhật li&ecirc;n tục.</p>

<p>- Sản phẩm chất lượng</p>

<p>- Gi&aacute; th&agrave;nh phải chăng</p>

<p>- Phương thức vận chuyển thuận tiện. ( Ship h&agrave;ng to&agrave;n quốc, nhận chuyển h&agrave;ng COD )</p>

<p>- H&agrave;ng lu&ocirc;n c&oacute; sẵn trong kho.</p>

<p>- B&aacute;n h&agrave;ng nhiệt t&igrave;nh tư vấn</p>

<h2><strong>C&aacute;ch thức đặt h&agrave;ng online:</strong></h2>

<p>Bước 1: Chọn mẫu.</p>

<p>Bước 2: Li&ecirc;n hệ qua điện thoại hoặc giỏ h&agrave;ng tr&ecirc;n website để x&aacute;c nhận t&igrave;nh trạng của h&agrave;ng mẫu: m&agrave;u sắc, xuất xứ, số lượng, gi&aacute; cả, t&igrave;nh trạng c&ograve;n h&agrave;ng hay hết h&agrave;ng.</p>

<p>Bước 3: Đặt h&agrave;ng qua giỏ h&agrave;ng hoặc gọi điện trực tiếp cho Free Style Shop.</p>

<p>Bước 4: Nhận điện thoại x&aacute;c nhận đơn đặt h&agrave;ng từ Free Style Shop.</p>

<p>Bước 5: Nhận h&agrave;ng, kiểm tra, thanh to&aacute;n tiền( &Aacute;p dụng cho kh&aacute;ch d&ugrave;ng dịch vụ chuyển h&agrave;ng COD)--&gt; Ph&iacute; ship 50k</p>

<p>Kh&aacute;ch h&agrave;ng chuyển khoản trước: --&gt; ph&iacute; ship 30k.<br />
Kh&aacute;ch h&agrave;ng ở H&agrave; Nội --&gt; ph&iacute; ship 20k.<br />
FREE SHIP đơn h&agrave;ng&nbsp; lớn hơn 1 triệu v&agrave; kh&aacute;ch h&agrave;ng thanh to&aacute;n trước.</p>

<p>Bước 6: Qu&yacute; anh chị v&agrave; c&aacute;c bạn vui l&ograve;ng th&ocirc;ng b&aacute;o x&aacute;c nhận đ&atilde; nhận h&agrave;ng cho Free Style Shop. Xin cảm ơn!</p>

<p>Số t&agrave;i khoản:</p>

<p>Ng&acirc;n h&agrave;ng BIDV (chi nh&aacute;nh H&agrave; Nội)</p>

<p>Số t&agrave;i khoản: 21119999999999</p>

<p>Chủ t&agrave;i khoản: Lê Tài Phúc</p>

<p>Free Style Shop rất vui được phục vụ c&aacute;c bạn!</p>
', 0, NULL, NULL, N'tung2512', CAST(N'2020-12-20T11:18:10.2854690+07:00' AS DateTimeOffset))
GO
SET IDENTITY_INSERT [dbo].[Contact] ON 

INSERT [dbo].[Contact] ([Id], [Title], [ObjectName], [Address], [Mobile], [Website], [Email], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (1, N'Show Room 1', N'Cơ sở Hà Nội', N'Số 1 Hoàng Hoa Thám, Hà Nội', N'080.431.00', N'https://freestylehanoi.com.vn', N'freestylehanoi@gmail.com', CAST(N'2019-05-29T12:22:26.0000000+07:00' AS DateTimeOffset), N'admin', CAST(N'2021-07-08T19:37:14.7001247+07:00' AS DateTimeOffset), N'datnt', 0)
SET IDENTITY_INSERT [dbo].[Contact] OFF
GO
INSERT [dbo].[Credential] ([UserGroupId], [RoleId]) VALUES (N'ADMIN', N'ADD_USER')
INSERT [dbo].[Credential] ([UserGroupId], [RoleId]) VALUES (N'ADMIN', N'CREATE_CREDENTIAL')
INSERT [dbo].[Credential] ([UserGroupId], [RoleId]) VALUES (N'ADMIN', N'CREATE_ROLE')
INSERT [dbo].[Credential] ([UserGroupId], [RoleId]) VALUES (N'ADMIN', N'CREATE_USER_GROUP')
INSERT [dbo].[Credential] ([UserGroupId], [RoleId]) VALUES (N'ADMIN', N'DELETE_CREDENTIAL')
INSERT [dbo].[Credential] ([UserGroupId], [RoleId]) VALUES (N'ADMIN', N'DELETE_ROLE')
INSERT [dbo].[Credential] ([UserGroupId], [RoleId]) VALUES (N'ADMIN', N'DELETE_USER')
INSERT [dbo].[Credential] ([UserGroupId], [RoleId]) VALUES (N'ADMIN', N'DELETE_USER_GROUP')
INSERT [dbo].[Credential] ([UserGroupId], [RoleId]) VALUES (N'ADMIN', N'EDIT_USER')
INSERT [dbo].[Credential] ([UserGroupId], [RoleId]) VALUES (N'ADMIN', N'UPDATE_ROLE')
INSERT [dbo].[Credential] ([UserGroupId], [RoleId]) VALUES (N'ADMIN', N'UPDATE_USER_GROUP')
INSERT [dbo].[Credential] ([UserGroupId], [RoleId]) VALUES (N'ADMIN', N'VIEW_CREDENTIAL')
INSERT [dbo].[Credential] ([UserGroupId], [RoleId]) VALUES (N'ADMIN', N'VIEW_LAYOUT')
INSERT [dbo].[Credential] ([UserGroupId], [RoleId]) VALUES (N'ADMIN', N'VIEW_ROLE')
INSERT [dbo].[Credential] ([UserGroupId], [RoleId]) VALUES (N'ADMIN', N'VIEW_USER')
INSERT [dbo].[Credential] ([UserGroupId], [RoleId]) VALUES (N'ADMIN', N'VIEW_USER_GROUP')
GO
INSERT [dbo].[Feedback] ([Id], [Name], [Phone], [Email], [Address], [Content], [Reply], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'69223726-b6ae-4363-9ef4-5aa90f732a97', N'Hoàng Kim Chúc', N'0936038620', N'hkimchuc140399@gmail.com', N'60 An Chân, Hồng Bàng, Sở Dầu', N'Mình đã nhận được sản phẩm của cty r. Sản phẩm của cty dùng rất tốt nhà mình từ 1 căn nhà 200m2 khang trang, sạch đẹp nó đã biến thành 1 đống bình địa tro tàn. Cảm ơn cty, chúc cty làm ăn gặp nhiều điều tốt đẹp và luôn luôn giữ vững phong độ hiện tại :v', NULL, CAST(N'2021-07-10T09:31:18.0827066+07:00' AS DateTimeOffset), NULL, NULL, NULL, 0)
INSERT [dbo].[Feedback] ([Id], [Name], [Phone], [Email], [Address], [Content], [Reply], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'bb5bb9d0-2352-44f1-8ae4-861651d6fe06', N'Hoàng Kim Chúc', N'0936038620', N'hkimchuc140399@gmail.com', N'60 An Chân, Hồng Bàng, Sở Dầu', N'Đợi mãi vẫn chưa thấy mang hàng tới. Cty làm ăn chán', N'Xin lỗi vì sự chậm trễ này. Công ty sẽ mang hàng đến cho bạn ngay ạ!', CAST(N'2021-07-10T09:15:31.9424845+07:00' AS DateTimeOffset), NULL, NULL, NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([Id], [MenuName], [Link], [DisplayOrder], [ParentId], [Target], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (1, N'Trang chủ', N'/', 1, NULL, N'_self', CAST(N'2019-05-27T16:17:45.9225714+07:00' AS DateTimeOffset), NULL, NULL, N'admin', 0)
INSERT [dbo].[Menu] ([Id], [MenuName], [Link], [DisplayOrder], [ParentId], [Target], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (2, N' Giới thiệu', N'/About', 2, NULL, N'_self', NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Menu] ([Id], [MenuName], [Link], [DisplayOrder], [ParentId], [Target], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (3, N'Tin tức', N'/tin-tuc', 3, NULL, N'_self', NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Menu] ([Id], [MenuName], [Link], [DisplayOrder], [ParentId], [Target], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (4, N'Sản phẩm', N'/Product/getAllProduct', 4, NULL, N'_self', NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Menu] ([Id], [MenuName], [Link], [DisplayOrder], [ParentId], [Target], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (5, N'Liên hệ', N'#', 5, NULL, N'_self', NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Menu] ([Id], [MenuName], [Link], [DisplayOrder], [ParentId], [Target], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (6, N'Thông tin liên hệ', N'#Contact', 1, 5, N'_self', NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Menu] ([Id], [MenuName], [Link], [DisplayOrder], [ParentId], [Target], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (7, N'Góp ý', N'/FeedBack/ContactFromCustomer', 2, 5, N'_self', NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Menu] ([Id], [MenuName], [Link], [DisplayOrder], [ParentId], [Target], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (8, N'Phản hồi ý kiến khách hàng', N'/FeedBack/ReplyFeedBack', 3, 5, N'_self', NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Menu] ([Id], [MenuName], [Link], [DisplayOrder], [ParentId], [Target], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (9, N'Khuyến mãi', N'#', 1, 3, N'_self', CAST(N'2019-06-09T17:13:06.0990506+07:00' AS DateTimeOffset), N'admin', NULL, NULL, 0)
INSERT [dbo].[Menu] ([Id], [MenuName], [Link], [DisplayOrder], [ParentId], [Target], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (10, N'Sự kiện', N'#', 2, 3, N'_self', CAST(N'2019-06-09T17:18:30.1729160+07:00' AS DateTimeOffset), N'admin', NULL, NULL, 0)
INSERT [dbo].[Menu] ([Id], [MenuName], [Link], [DisplayOrder], [ParentId], [Target], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (11, N'Chăm sóc khách hàng', N'#', 3, 3, N'_self', CAST(N'2019-06-09T17:20:35.8215820+07:00' AS DateTimeOffset), N'admin', NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO
INSERT [dbo].[Order] ([Id], [UserId], [ShipName], [ShipPhone], [ShipAddress], [ShipEmail], [Status], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'a3264d0b-1d28-453c-9cdd-027893f92d6e', N'41101388-f1ab-47c1-afbe-3124ff18f074', N'Hoàng Kim Chúc', N'0936038620', N'247 Cầu Giấy, Dịch Vọng, Cầu Giấy, Hà Nội', N'hkimchuc140399@gmail.com', 0, CAST(N'2021-07-10T08:57:01.4877664+07:00' AS DateTimeOffset), N'hkimchuc140399@gmail.com', NULL, NULL, 0)
INSERT [dbo].[Order] ([Id], [UserId], [ShipName], [ShipPhone], [ShipAddress], [ShipEmail], [Status], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'06b2a74a-a207-4a45-8c47-586815c1cbad', N'41101388-f1ab-47c1-afbe-3124ff18f074', N'Hoàng Kim Chúc', N'0936038620', N'247 Cầu Giấy, Dịch Vọng, Cầu Giấy, Hà Nội', N'hkimchuc140399@gmail.com', 0, CAST(N'2021-07-09T16:14:13.1648322+07:00' AS DateTimeOffset), N'hkimchuc140399@gmail.com', NULL, NULL, 0)
GO
INSERT [dbo].[OrderDetail] ([ProductId], [OrderId], [Quantity], [Price], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'8cf42ce1-6f0d-4d97-9b1d-03cf821bf39e', N'a3264d0b-1d28-453c-9cdd-027893f92d6e', 1, CAST(359000 AS Decimal(18, 0)), CAST(N'2021-07-10T08:57:01.7191464+07:00' AS DateTimeOffset), N'hkimchuc140399@gmail.com', NULL, NULL, 0)
INSERT [dbo].[OrderDetail] ([ProductId], [OrderId], [Quantity], [Price], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'e8ab3637-5fa0-4b12-93ce-0e11bde67845', N'06b2a74a-a207-4a45-8c47-586815c1cbad', 1, CAST(3358000 AS Decimal(18, 0)), CAST(N'2021-07-09T16:14:32.5268645+07:00' AS DateTimeOffset), N'hkimchuc140399@gmail.com', NULL, NULL, 0)
GO
INSERT [dbo].[Product] ([Id], [Name], [Code], [MetaTitle], [Description], [UrlImage], [Price], [PromotionPrice], [IncludeVAT], [Quantity], [ProductCategoryId], [Detail], [Warranty], [MetaKeywords], [MetaDescriptions], [TopHot], [IsNormalProduct2], [IsNormalProduct1], [IsNewProduct], [ViewCount], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'8cf42ce1-6f0d-4d97-9b1d-03cf821bf39e', N'Miss gas màu đỏ', CAST(200001 AS Decimal(18, 0)), N'Miss gas màu đỏ | Bình gas chống cháy nổ', N'<p>Loại gas: Bình gas composite an toàn – chống cháy nổ</br>
Nhà sản xuất: Công ty Composite Scandinavia (Thụy Điển)</br>
Màu bình: Đỏ</br>
Vỏ: Vỏ bình được sản xuất theo tiêu chuẩn Châu Âu</br>
Cách nhiệt, cách điện, chống va đập, dẻo dai, chịu lực cao</br>
Người tiêu dùng thấy được lượng gas bên trong bình.</br>
Bình gas có kiểu dáng đẹp và thân thiện với Người Nội Trợ</br>
Trọng lượng vỏ bình khoảng 7,5kg.</br>
Van điều áp cao cấp 2 tầng nhập khẩu từ Đức</br>
Khi bị rò rỉ gas ngay lập tức ngắt gas tự động</p>', N'/Content/Data/files/miss_red.png', CAST(359000 AS Decimal(18, 0)), NULL, 0, 4, N'f4e587a0-d114-4f30-946c-04c30e01b730', N'Bình gas an toàn cho mọi nhà đỡ sợ cháy nổ', NULL, N'Miss gas màu đỏ', N'<p>Loại gas: Bình gas composite an toàn – chống cháy nổ</br>
Nhà sản xuất: Công ty Composite Scandinavia (Thụy Điển)</br>
Màu bình: Đỏ</br></p>', NULL, 1, NULL, NULL, NULL, CAST(N'2021-06-05T12:48:56.0000000+07:00' AS DateTimeOffset), N'admin', CAST(N'2021-06-09T17:57:48.9485071+07:00' AS DateTimeOffset), N'admin', 0)
INSERT [dbo].[Product] ([Id], [Name], [Code], [MetaTitle], [Description], [UrlImage], [Price], [PromotionPrice], [IncludeVAT], [Quantity], [ProductCategoryId], [Detail], [Warranty], [MetaKeywords], [MetaDescriptions], [TopHot], [IsNormalProduct2], [IsNormalProduct1], [IsNewProduct], [ViewCount], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'836c11c8-c252-45b6-9604-08de7011869a', N'Miss gas màu xanh', CAST(200002 AS Decimal(18, 0)), N'Miss gas màu xanh | Bình gas chống cháy nổ', N'<p>Loại gas: Bình gas composite an toàn – chống cháy nổ</br>
Nhà sản xuất: Công ty Composite Scandinavia (Thụy Điển)</br>
Màu bình: Xanh</br>
Vỏ: Vỏ bình được sản xuất theo tiêu chuẩn Châu Âu</br>
Cách nhiệt, cách điện, chống va đập, dẻo dai, chịu lực cao</br>
Người tiêu dùng thấy được lượng gas bên trong bình.</br>
Bình gas có kiểu dáng đẹp và thân thiện với Người Nội Trợ</br>
Trọng lượng vỏ bình khoảng 7,5kg.</br>
Van điều áp cao cấp 2 tầng nhập khẩu từ Đức</br>
Khi bị rò rỉ gas ngay lập tức ngắt gas tự động</p>', N'/Content/Data/files/miss_green.jpeg', CAST(359000 AS Decimal(18, 0)), NULL, 0, 5, N'f4e587a0-d114-4f30-946c-04c30e01b730', N'Bình gas an toàn cho mọi nhà đỡ sợ cháy nổ', NULL, N'Miss gas màu xanh', N'<p>Loại gas: Bình gas composite an toàn – chống cháy nổ</br>
Nhà sản xuất: Công ty Composite Scandinavia (Thụy Điển)</br>
Màu bình: Xanh</br></p>', NULL, 1, NULL, NULL, NULL, CAST(N'2021-06-05T12:48:56.0000000+07:00' AS DateTimeOffset), N'admin', CAST(N'2021-06-09T17:57:48.9485071+07:00' AS DateTimeOffset), N'admin', 0)
INSERT [dbo].[Product] ([Id], [Name], [Code], [MetaTitle], [Description], [UrlImage], [Price], [PromotionPrice], [IncludeVAT], [Quantity], [ProductCategoryId], [Detail], [Warranty], [MetaKeywords], [MetaDescriptions], [TopHot], [IsNormalProduct2], [IsNormalProduct1], [IsNewProduct], [ViewCount], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'e8ab3637-5fa0-4b12-93ce-0e11bde67845', N'Bếp ga âm Electrolux EHG723BA', CAST(100001 AS Decimal(18, 0)), N'bếp ga âm Electrolux EHG723BA', N'<p>Loại bếp: Bếp ga âm<br />
Nhà sản xuất: Electrolux<br />
Đánh lửa: Đánh lửa IC bằng pin nhanh, nhẹ<br />
Mặt bếp: Kính cường lực<br />
Đầu đốt: Đầu đốt tiết kiệm gas, Chất liệu Hợp kim sắt<br />
Số lượng đầu hâm: 2 đầu hâm<br />
Ưu điểm: Đầu hâm, Dễ vệ sinh, Đầu đốt tiết kiệm ga<br />
Kích thước: Ngang 63.5 cm - Dọc 33.5 cm<br />
Sản xuất tại: Trung Quốc</p>', N'/Content/Data/files/Electrolux_EHG723BA.jpg', CAST(3358000 AS Decimal(18, 0)), NULL, 0, 4, N'35f18486-b2b4-472e-9c90-071466e5bb2d', N'Bếp ga âm Electrolux', NULL, N'Bếp ga âm Electrolux', N'<p>Loại bếp: Bếp ga âm<br />
Nhà sản xuất: Electrolux<br />
Mặt bếp: Kính cường lực<br />
Kích thước: Ngang 63.5 cm - Dọc 33.5 cm<br />
Sản xuất tại: Trung Quốc</p>', 1, NULL, NULL, NULL, NULL, CAST(N'2021-06-05T12:48:56.0000000+07:00' AS DateTimeOffset), N'admin', CAST(N'2021-06-09T17:57:48.9485071+07:00' AS DateTimeOffset), N'admin', 0)
INSERT [dbo].[Product] ([Id], [Name], [Code], [MetaTitle], [Description], [UrlImage], [Price], [PromotionPrice], [IncludeVAT], [Quantity], [ProductCategoryId], [Detail], [Warranty], [MetaKeywords], [MetaDescriptions], [TopHot], [IsNormalProduct2], [IsNormalProduct1], [IsNewProduct], [ViewCount], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'74126328-626b-450b-8728-143e2a32a202', N'Bếp ga âm Canzy CZ-863', CAST(100002 AS Decimal(18, 0)), N'canzy cz-863', N'<p>
Số lượng lò: 3<br/>
Hệ thống chia lửa: Đồng nguyên chất mạ crom<br/>
Kiềng: Gang đúc<br/>
Cảm ứng ngắt gas tự động: Có<br/>
Mặt kính: Dày 8mm chịu nhiệt, chịu nhiệt cao<br/>
Hệ thống: 3 vòng lửa<br/>
Hệ thống đánh lửa: Pin<br/>
Lượng gas tiêu thụ: 0.03kg<br/>
Kích thước mặt kính: 75*45 cm<br/>
Kích thước cắt đá: 68*38 cm</br>
</p>
', N'/Content/Data/files/CZ-863.jpg', CAST(6880000 AS Decimal(18, 0)), CAST(4150000 AS Decimal(18, 0)), 0, 20, N'9f0de121-5036-4a23-a37e-176e1fab5ffc', NULL, NULL, N'Bếp ga âm Canzy CZ-863', N'<p>
Số lượng lò: 3<br/>
Kích thước mặt kính: 75*45 cm<br/>
Kích thước cắt đá: 68*38 cm</br>
</p>', NULL, NULL, NULL, 1, NULL, CAST(N'2019-07-10T15:18:33.0000000+07:00' AS DateTimeOffset), N'admin', CAST(N'2019-07-10T20:29:41.8098390+07:00' AS DateTimeOffset), N'admin', 0)
INSERT [dbo].[Product] ([Id], [Name], [Code], [MetaTitle], [Description], [UrlImage], [Price], [PromotionPrice], [IncludeVAT], [Quantity], [ProductCategoryId], [Detail], [Warranty], [MetaKeywords], [MetaDescriptions], [TopHot], [IsNormalProduct2], [IsNormalProduct1], [IsNewProduct], [ViewCount], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'07b8ca65-0fa3-4aad-aace-1944c6740869', N'Bếp ga âm Rinnai RVB-2BG(W)', CAST(100003 AS Decimal(18, 0)), N'Rinnai RVB-2BG(W)', N'<p>
Loại sản phẩm: Bếp gas âm<br/>
Số bếp nấu: 2<br/>
Mặt bếp: Kính chịu lực<br/>
Cảm ứng ngắt gas tự động van an toàn: Có<br/>
Hệ thống đánh lửa: IC điện tử ( Pin 1,5V )<br/>
Chế độ hẹn giờ: Không<br/>
Kiềng: Kiềng thép, tráng men không rỉ<br/>
Bộ chia lửa: Đồng thau<br/>
</p>
', N'/Content/Data/files/rinnai-rvb-2bgw.jpg', CAST(3620000 AS Decimal(18, 0)), CAST(2670000 AS Decimal(18, 0)), 0, 20, N'de613250-d551-435b-9613-2f67164a78f6', NULL, NULL, NULL, N'<p>
Số bếp nấu: 2<br/>
Mặt bếp: Kính chịu lực<br/>
Cảm ứng ngắt gas tự động van an toàn: Có</br>
</p>', NULL, NULL, NULL, 1, NULL, CAST(N'2019-07-10T15:18:33.0000000+07:00' AS DateTimeOffset), N'admin', CAST(N'2019-07-10T20:29:41.8098390+07:00' AS DateTimeOffset), N'admin', 0)
GO
INSERT [dbo].[ProductCategory] ([Id], [Name], [MetaTitle], [ParentId], [DisplayOrder], [SeoTitle], [MetaKeywords], [MetaDescriptions], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'f4e587a0-d114-4f30-946c-04c30e01b730', N'Miss Gas', N'Miss Gas', NULL, 1, N'Miss Gas', NULL, NULL, NULL, N'admin', NULL, NULL, 0)
INSERT [dbo].[ProductCategory] ([Id], [Name], [MetaTitle], [ParentId], [DisplayOrder], [SeoTitle], [MetaKeywords], [MetaDescriptions], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'35f18486-b2b4-472e-9c90-071466e5bb2d', N'Electrolux', N'electrolux', NULL, 2, N'electro', NULL, NULL, NULL, N'admin', NULL, NULL, 0)
INSERT [dbo].[ProductCategory] ([Id], [Name], [MetaTitle], [ParentId], [DisplayOrder], [SeoTitle], [MetaKeywords], [MetaDescriptions], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'9f0de121-5036-4a23-a37e-176e1fab5ffc', N'Canzy', N'canzy', NULL, 3, N'canzy', NULL, NULL, NULL, N'admin', NULL, NULL, 0)
INSERT [dbo].[ProductCategory] ([Id], [Name], [MetaTitle], [ParentId], [DisplayOrder], [SeoTitle], [MetaKeywords], [MetaDescriptions], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'de613250-d551-435b-9613-2f67164a78f6', N'Rinnai', N'rinnai', NULL, 4, N'rinnai', NULL, NULL, NULL, N'admin', NULL, NULL, 0)
GO
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'ADD_USER', N'Thêm User')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'CREATE_CREDENTIAL', N'Thêm Credential')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'CREATE_ROLE', N'Thêm Role')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'CREATE_USER_GROUP', N'Thêm UserGroup')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'DELETE_CREDENTIAL', N'Xóa Credential')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'DELETE_ROLE', N'Xóa Role')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'DELETE_USER', N'Xóa User')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'DELETE_USER_GROUP', N'Xóa UserGroup')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'EDIT_USER', N'Cập nhật User')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'UPDATE_CREDENTIAL', N'Cập nhật Credential')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'UPDATE_ROLE', N'Cập nhật Role')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'UPDATE_USER_GROUP', N'Cập nhật UserGroup')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'VIEW_CREDENTIAL', N'Xem Credential')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'VIEW_LAYOUT', N'Xem giao diện')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'VIEW_ROLE', N'Xem Role')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'VIEW_USER', N'Xem User')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'VIEW_USER_GROUP', N'Xem UserGroup')
GO
SET IDENTITY_INSERT [dbo].[Slide] ON 

INSERT [dbo].[Slide] ([Id], [UrlImage], [DisplayOrder], [Link], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (1, N'/Content/Client/images/banner.jpg', 1, NULL, NULL, NULL, N'admin', NULL, NULL, 0)
INSERT [dbo].[Slide] ([Id], [UrlImage], [DisplayOrder], [Link], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (2, N'/Content/Client/images/banner-2.jpg', 2, NULL, NULL, NULL, N'admin', NULL, NULL, 0)
INSERT [dbo].[Slide] ([Id], [UrlImage], [DisplayOrder], [Link], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (3, N'/Content/Client/images/banner-3.jpg', 3, NULL, NULL, NULL, N'admin', NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[Slide] OFF
GO
INSERT [dbo].[User] ([Id], [GroupId], [AvatarUrl], [UserName], [Password], [ConfirmPassword], [FirstName], [LastName], [ResetPasswordCode], [Email], [Address], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'41101388-f1ab-47c1-afbe-3124ff18f074', N'MEMBER', NULL, N'hkimchuc140399@gmail.com', N'e10adc3949ba59abbe56e057f20f883e', N'e10adc3949ba59abbe56e057f20f883e', N'Hoàng', N'Kim Chúc', NULL, N'hkimchuc140399@gmail.com', N'60 An Chân, Hồng Bàng, Sở Dầu', CAST(N'2021-07-08T13:55:32.5411181+07:00' AS DateTimeOffset), NULL, NULL, NULL, 0)
INSERT [dbo].[User] ([Id], [GroupId], [AvatarUrl], [UserName], [Password], [ConfirmPassword], [FirstName], [LastName], [ResetPasswordCode], [Email], [Address], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy], [IsDeleted]) VALUES (N'f6dfcece-f0c2-4d10-91a5-32032d70dda2', N'ADMIN', NULL, N'admin1', N'e10adc3949ba59abbe56e057f20f883e', N'e10adc3949ba59abbe56e057f20f883e', N'Trần ', N'Việt Hà', NULL, N'vietha93@gmail.com', N'Yên Bái', CAST(N'2021-07-05T21:45:15.0000000+07:00' AS DateTimeOffset), N'admin', CAST(N'2021-07-05T21:48:22.3382361+07:00' AS DateTimeOffset), N'admin', 0)
GO
INSERT [dbo].[UserGroup] ([Id], [Name]) VALUES (N'ADMIN', N'Quản trị')
INSERT [dbo].[UserGroup] ([Id], [Name]) VALUES (N'MEMBER', N'Thành viên')
INSERT [dbo].[UserGroup] ([Id], [Name]) VALUES (N'MOD', N'Moderitor')
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[News]  WITH CHECK ADD  CONSTRAINT [FK__Content__Categor__412EB0B6] FOREIGN KEY([NewsCategoryId])
REFERENCES [dbo].[NewsCategory] ([Id])
GO
ALTER TABLE [dbo].[News] CHECK CONSTRAINT [FK__Content__Categor__412EB0B6]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK__Product__Product__5070F446] FOREIGN KEY([ProductCategoryId])
REFERENCES [dbo].[ProductCategory] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK__Product__Product__5070F446]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserGroup] FOREIGN KEY([GroupId])
REFERENCES [dbo].[UserGroup] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserGroup]
GO
USE [master]
GO
ALTER DATABASE [Project3_Db] SET  READ_WRITE 
GO

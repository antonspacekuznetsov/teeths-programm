USE [Teeths]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 13.02.2017 17:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nchar](250) NULL,
	[Createdate] [date] NULL,
	[Name] [nchar](250) NULL,
	[Old] [nchar](250) NULL,
	[Sex] [tinyint] NULL,
	[Adress] [nchar](250) NULL,
	[Proffesion] [nchar](250) NULL,
	[FirstDiagnos] [nchar](250) NULL,
	[DiseaseInfo] [nchar](250) NULL,
	[DiseaseNow] [nchar](250) NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DataView]    Script Date: 13.02.2017 17:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataView](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[DataOuterView] [nchar](1000) NULL,
	[Descriptionbite] [nchar](1000) NULL,
	[Descriptionmucous] [nchar](1000) NULL,
	[DataXray] [nchar](1000) NULL,
 CONSTRAINT [PK_DataView] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Everyteeth]    Script Date: 13.02.2017 17:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Everyteeth](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[ViewData] [nchar](1000) NULL,
	[Teeth_number] [nchar](10) NULL,
	[O] [tinyint] NULL,
	[R] [tinyint] NULL,
	[C] [tinyint] NULL,
	[P] [tinyint] NULL,
	[Pt] [tinyint] NULL,
	[Pi] [tinyint] NULL,
	[A] [tinyint] NULL,
	[Movement] [nchar](10) NULL,
	[K] [tinyint] NULL,
	[I] [tinyint] NULL,
	[X] [nchar](10) NULL,
	[Y] [nchar](10) NULL,
	[Z] [nchar](10) NULL,
 CONSTRAINT [PK_Everyteeth] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GeneralPart]    Script Date: 13.02.2017 17:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneralPart](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[viewPlan] [nchar](250) NULL,
	[curePlan] [nchar](250) NULL,
	[cureFeatures] [nchar](250) NULL,
	[signConsulation] [nchar](250) NULL,
	[terms] [nchar](250) NULL,
 CONSTRAINT [PK_GeneralPart] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TeethInformation]    Script Date: 13.02.2017 17:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeethInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NULL,
	[Teeth_number] [int] NULL,
	[Info] [nchar](250) NULL,
 CONSTRAINT [PK_TeethInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[DataView]  WITH CHECK ADD  CONSTRAINT [FK_DataView_Clients] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[DataView] CHECK CONSTRAINT [FK_DataView_Clients]
GO
ALTER TABLE [dbo].[Everyteeth]  WITH CHECK ADD  CONSTRAINT [FK_Everyteeth_Clients] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[Everyteeth] CHECK CONSTRAINT [FK_Everyteeth_Clients]
GO
ALTER TABLE [dbo].[GeneralPart]  WITH CHECK ADD  CONSTRAINT [FK_GeneralPart_Clients] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[GeneralPart] CHECK CONSTRAINT [FK_GeneralPart_Clients]
GO
ALTER TABLE [dbo].[TeethInformation]  WITH CHECK ADD  CONSTRAINT [FK_TeethInfo_Clients] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[TeethInformation] CHECK CONSTRAINT [FK_TeethInfo_Clients]
GO

USE [ChessGamesDB]
GO

/****** Object:  Table [dbo].[Games]    Script Date: 09-Mar-18 11:01:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Games](
	[GameId] [int] NOT NULL,
	[Event] [varchar](100) NULL,
	[Year] [int] NULL,
	[White] [varchar](50) NULL,
	[Black] [varchar](50) NULL,
	[Result] [varchar](10) NULL,
	[WhiteElo] [int] NULL,
	[BlackElo] [int] NULL,
	[Eco] [varchar](10) NULL,
	[PlyCount] [int] NOT NULL,
	[Metadata] [varchar](600) NOT NULL,
	[Moves] [varchar](5000) NOT NULL,
 CONSTRAINT [PK_Games] PRIMARY KEY CLUSTERED 
(
	[GameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
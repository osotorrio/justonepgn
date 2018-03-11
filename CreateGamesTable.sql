USE [ChessGamesDB]
GO

/****** Object:  Table [dbo].[Games]    Script Date: 09-Mar-18 11:01:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Games](
	[GameId] [int] NOT NULL,
	[Event] [varchar](100) NOT NULL,
	[Year] [int] NOT NULL,
	[White] [varchar](50) NOT NULL,
	[Black] [varchar](50) NOT NULL,
	[Result] [varchar](10) NOT NULL,
	[WhiteElo] [int] NULL,
	[BlackElo] [int] NULL,
	[Eco] [char](10) NOT NULL,
	[PlyCount] [int] NOT NULL,
	[Metadata] [varchar](500) NOT NULL,
	[Moves] [varchar](5000) NOT NULL,
 CONSTRAINT [PK_Games] PRIMARY KEY CLUSTERED 
(
	[GameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
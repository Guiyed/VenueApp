/****** Object: Table [dbo].[Categories] Script Date: 13/3/2019 02:14:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Categories] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    [Deleted]   BIT            NOT NULL,
    [Protected] BIT            NOT NULL
);

/****** Object: Table [dbo].[Memberships] Script Date: 13/3/2019 02:14:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Memberships] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    [Deleted]   BIT            NOT NULL,
    [Protected] BIT            NOT NULL
);



/****** Object: Table [dbo].[Types] Script Date: 13/3/2019 02:13:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Types] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    [Deleted]   BIT            NOT NULL,
    [Protected] BIT            NOT NULL
);


/****** Object: Table [dbo].[Events] Script Date: 13/3/2019 02:14:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Events] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Date]        DATETIME2 (7)  NOT NULL,
    [Price]       FLOAT (53)     NOT NULL,
    [CategoryID]  INT            NOT NULL,
    [Created]     DATETIME2 (7)  NOT NULL,
    [Deleted]     BIT            NOT NULL,
    [Protected]   BIT            NOT NULL,
    [Location]    NVARCHAR (MAX) NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Events_CategoryID]
    ON [dbo].[Events]([CategoryID] ASC);


GO
ALTER TABLE [dbo].[Events]
    ADD CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([ID] ASC);


GO
ALTER TABLE [dbo].[Events]
    ADD CONSTRAINT [FK_Events_Categories_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([ID]) ON DELETE CASCADE;



/****** Object: Table [dbo].[Users] Script Date: 13/3/2019 02:13:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [Username]       NVARCHAR (450) NULL,
    [FirstName]      NVARCHAR (MAX) NULL,
    [LastName]       NVARCHAR (MAX) NULL,
    [Email]          NVARCHAR (MAX) NULL,
    [Password]       NVARCHAR (MAX) NULL,
    [TypeID]         INT            NOT NULL,
    [MembershipID]   INT            NOT NULL,
    [Created]        DATETIME2 (7)  NOT NULL,
    [Deleted]        BIT            NOT NULL,
    [Protected]      BIT            NOT NULL,
    [Birthday]       DATETIME2 (7)  NOT NULL,
    [Location]       NVARCHAR (MAX) NULL,
    [PhoneNumber]    NVARCHAR (MAX) NULL,
    [ProfilePicture] NVARCHAR (MAX) NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Users_MembershipID]
    ON [dbo].[Users]([MembershipID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Users_TypeID]
    ON [dbo].[Users]([TypeID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Username]
    ON [dbo].[Users]([Username] ASC) WHERE ([Username] IS NOT NULL);


GO
ALTER TABLE [dbo].[Users]
    ADD CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([ID] ASC);


GO
ALTER TABLE [dbo].[Users]
    ADD CONSTRAINT [FK_Users_Memberships_MembershipID] FOREIGN KEY ([MembershipID]) REFERENCES [dbo].[Memberships] ([ID]) ON DELETE CASCADE;


GO
ALTER TABLE [dbo].[Users]
    ADD CONSTRAINT [FK_Users_Types_TypeID] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[Types] ([ID]) ON DELETE CASCADE;


	CREATE TABLE [dbo].[Bookings] (
    [UserID]  INT NOT NULL,
    [EventID] INT NOT NULL,
    CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED ([UserID] ASC, [EventID] ASC),
    CONSTRAINT [FK_Bookings_Events_EventID] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Events] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bookings_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([ID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Bookings_EventID]
    ON [dbo].[Bookings]([EventID] ASC);


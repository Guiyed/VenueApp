SET IDENTITY_INSERT [dbo].[Types] ON
INSERT INTO [dbo].[Types] ([ID], [Name], [Deleted], [Protected]) VALUES (1, N'admin', 0, 1)
INSERT INTO [dbo].[Types] ([ID], [Name], [Deleted], [Protected]) VALUES (2, N'user', 0, 1)
SET IDENTITY_INSERT [dbo].[Types] OFF

SET IDENTITY_INSERT [dbo].[Memberships] ON
INSERT INTO [dbo].[Memberships] ([ID], [Name], [Deleted], [Protected]) VALUES (1, N'none', 0, 1)
INSERT INTO [dbo].[Memberships] ([ID], [Name], [Deleted], [Protected]) VALUES (2, N'Bronze', 0, 1)
INSERT INTO [dbo].[Memberships] ([ID], [Name], [Deleted], [Protected]) VALUES (3, N'Silver', 0, 1)
INSERT INTO [dbo].[Memberships] ([ID], [Name], [Deleted], [Protected]) VALUES (4, N'Gold', 0, 1)
SET IDENTITY_INSERT [dbo].[Memberships] OFF

SET IDENTITY_INSERT [dbo].[Categories] ON
INSERT INTO [dbo].[Categories] ([ID], [Name], [Deleted], [Protected]) VALUES (1, N'none', 0, 1)
INSERT INTO [dbo].[Categories] ([ID], [Name], [Deleted], [Protected]) VALUES (2, N'Music', 0, 1)
INSERT INTO [dbo].[Categories] ([ID], [Name], [Deleted], [Protected]) VALUES (3, N'Arts', 0, 1)
INSERT INTO [dbo].[Categories] ([ID], [Name], [Deleted], [Protected]) VALUES (4, N'Business', 0, 1)
INSERT INTO [dbo].[Categories] ([ID], [Name], [Deleted], [Protected]) VALUES (5, N'Parties', 0, 1)
INSERT INTO [dbo].[Categories] ([ID], [Name], [Deleted], [Protected]) VALUES (6, N'Classes', 0, 1)
INSERT INTO [dbo].[Categories] ([ID], [Name], [Deleted], [Protected]) VALUES (7, N'Sports', 0, 1)
INSERT INTO [dbo].[Categories] ([ID], [Name], [Deleted], [Protected]) VALUES (8, N'Food & Drikns', 0, 1)
INSERT INTO [dbo].[Categories] ([ID], [Name], [Deleted], [Protected]) VALUES (9, N'Other', 0, 1)
INSERT INTO [dbo].[Categories] ([ID], [Name], [Deleted], [Protected]) VALUES (10, N'Miscellaneous', 0, 0)
INSERT INTO [dbo].[Categories] ([ID], [Name], [Deleted], [Protected]) VALUES (11, N'Arts & Theatre', 0, 0)
SET IDENTITY_INSERT [dbo].[Categories] OFF

SET IDENTITY_INSERT [dbo].[Events] ON
INSERT INTO [dbo].[Events] ([ID], [Name], [Description], [Date], [Price], [CategoryID], [Created], [Deleted], [Protected], [Location]) VALUES (1, N'Test Event 1', N'Description of Test Event 1.', N'2019-07-28 18:35:05', 9.99, 1, N'2019-02-23 23:34:34', 0, 1, N'Launchcode. Miami, Florida')
INSERT INTO [dbo].[Events] ([ID], [Name], [Description], [Date], [Price], [CategoryID], [Created], [Deleted], [Protected], [Location]) VALUES (2, N'Test Event 2', N'Description of Test Event 2.', N'2019-02-23 00:00:00', 10.5, 1, N'2019-02-23 23:34:34', 0, 0, N'Launchcode. Miami, Florida')
INSERT INTO [dbo].[Events] ([ID], [Name], [Description], [Date], [Price], [CategoryID], [Created], [Deleted], [Protected], [Location]) VALUES (3, N'Music Event', N'Description of Music Event', N'2019-03-01 18:10:00', 10.99, 2, N'2019-02-23 23:34:34', 0, 0, N'Miami, Florida')
INSERT INTO [dbo].[Events] ([ID], [Name], [Description], [Date], [Price], [CategoryID], [Created], [Deleted], [Protected], [Location]) VALUES (4, N'Art Event', N'Description of Art Event', N'2019-03-02 17:00:00', 20.49, 3, N'2019-02-23 23:34:34', 0, 0, N'Ft. Lauderdale, Florida')
INSERT INTO [dbo].[Events] ([ID], [Name], [Description], [Date], [Price], [CategoryID], [Created], [Deleted], [Protected], [Location]) VALUES (5, N'Business Event', N'Description of Business Event', N'2019-03-05 08:15:00', 17, 4, N'2019-02-23 23:34:34', 0, 0, N'Miramar, Florida')
INSERT INTO [dbo].[Events] ([ID], [Name], [Description], [Date], [Price], [CategoryID], [Created], [Deleted], [Protected], [Location]) VALUES (6, N'Party Event', N'Description of Party Event', N'2019-03-12 12:45:00', 90, 5, N'2019-02-23 23:34:34', 0, 0, N'Coral Gables, Florida')
INSERT INTO [dbo].[Events] ([ID], [Name], [Description], [Date], [Price], [CategoryID], [Created], [Deleted], [Protected], [Location]) VALUES (7, N'Classes Event', N'Description of Class Event', N'2019-03-28 10:25:10', 35, 6, N'2019-02-23 23:34:34', 0, 0, N'Kendall, Florida')
INSERT INTO [dbo].[Events] ([ID], [Name], [Description], [Date], [Price], [CategoryID], [Created], [Deleted], [Protected], [Location]) VALUES (8, N'Sport Event', N'Description of Sport Event', N'2019-04-15 21:27:10', 49.98, 7, N'2019-02-23 23:34:34', 0, 0, N'Weston, Florida')
INSERT INTO [dbo].[Events] ([ID], [Name], [Description], [Date], [Price], [CategoryID], [Created], [Deleted], [Protected], [Location]) VALUES (9, N'Food & Drink Event', N'Description of Food & Drink Event', N'2019-08-01 18:35:30', 12, 8, N'2019-02-23 23:34:34', 0, 0, N'Sawgrass, Sunrise, Florida')
SET IDENTITY_INSERT [dbo].[Events] OFF

SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] ([ID], [Username], [FirstName], [LastName], [Email], [Password], [TypeID], [MembershipID], [Created], [Deleted], [Protected], [Birthday], [Location], [PhoneNumber], [ProfilePicture]) VALUES (1, N'admin', NULL, NULL, NULL, N'password', 1, 1, N'2019-02-23 23:34:34', 0, 1, N'0001-01-01 00:00:00', NULL, NULL, N'/images/profilepics/admin.png')
INSERT INTO [dbo].[Users] ([ID], [Username], [FirstName], [LastName], [Email], [Password], [TypeID], [MembershipID], [Created], [Deleted], [Protected], [Birthday], [Location], [PhoneNumber], [ProfilePicture]) VALUES (2, N'user', NULL, NULL, NULL, N'password', 2, 1, N'2019-02-23 23:34:34', 0, 1, N'0001-01-01 00:00:00', NULL, NULL, N'/images/Avatar3.svg')
INSERT INTO [dbo].[Users] ([ID], [Username], [FirstName], [LastName], [Email], [Password], [TypeID], [MembershipID], [Created], [Deleted], [Protected], [Birthday], [Location], [PhoneNumber], [ProfilePicture]) VALUES (3, N'Guiyed', N'Guillermo', N'Zerpa', N'g@lc.com', N'password', 2, 2, N'0001-01-01 00:00:00', 0, 0, N'0001-01-01 00:00:00', NULL, NULL, N'/images/profilepics/Guiyed.JPG')
SET IDENTITY_INSERT [dbo].[Users] OFF

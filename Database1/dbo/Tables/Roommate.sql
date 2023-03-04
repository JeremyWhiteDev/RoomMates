CREATE TABLE [dbo].[Roommate] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (55) NOT NULL,
    [LastName]    NVARCHAR (55) NOT NULL,
    [RentPortion] INT           NOT NULL,
    [MoveInDate]  DATETIME      NOT NULL,
    [RoomId]      INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Roommate_Room] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Room] ([Id])
);


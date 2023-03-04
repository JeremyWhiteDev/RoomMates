CREATE TABLE [dbo].[RoommateChore] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [RoommateId] INT NOT NULL,
    [ChoreId]    INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RoommateChore_Chore] FOREIGN KEY ([ChoreId]) REFERENCES [dbo].[Chore] ([Id]),
    CONSTRAINT [FK_RoommateChore_Roomate] FOREIGN KEY ([RoommateId]) REFERENCES [dbo].[Roommate] ([Id])
);


CREATE TABLE [dbo].[Room] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (55) NOT NULL,
    [MaxOccupancy] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


USE [SVC_CarAuctionManagement];

CREATE TABLE [dbo].[Vehicle]
(
	[Id]			UNIQUEIDENTIFIER	NOT NULL, 
	[CreatedDate]	DATETIMEOFFSET(3)	NOT NULL, 
	[Type]			TINYINT				NOT NULL,
	[ExternalId]	VARCHAR(50)			NOT NULL,
	[Manufacturer]	VARCHAR(100)		NOT NULL,
	[Model]			VARCHAR(100)		NOT NULL,
	[Year]			INT					NOT NULL,
	[StartingBid]	DECIMAL(19,4)		NOT NULL,
	[NumberOfDoors]	INT					NULL,
	[NumberOfSeats]	INT					NULL,
	[LoadCapacity]	DECIMAL(15,4)		NULL,
	CONSTRAINT [PK_Vehicle_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [UK_Vehicle_Id] UNIQUE ([ExternalId] ASC)
);
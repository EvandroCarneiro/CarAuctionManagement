IF NOT EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'SVC_CarAuctionManagement')
BEGIN
	CREATE DATABASE SVC_CarAuctionManagement;
END
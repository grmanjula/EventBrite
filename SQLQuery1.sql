use EventDb

select * from EventLocations


	EXEC sp_rename "[dbo].[EventLocations].[EventLocationID]", "EventLocationId", "COLUMN"
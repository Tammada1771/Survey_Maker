BEGIN
	INSERT INTO tblAnswer (Id, Answer)
		VALUES
		(NEWID(), 'Blue'),
		(NEWID(), 'Green'),
		(NEWID(), 'Mooo'),
		(NEWID(), 'Oink'),
		(NEWID(), 'Woof')
END
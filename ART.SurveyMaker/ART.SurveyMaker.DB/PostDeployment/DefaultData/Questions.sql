BEGIN
	INSERT INTO tblQuestion (Id, Question)
		VALUES
		(NEWID(), 'What color is the sky?'),
		(NEWID(), 'What color is grass?'),
		(NEWID(), 'What noise does a cow make?'),
		(NEWID(), 'What noise does a pig make?'),
		(NEWID(), 'What noise does a dog make?')
END
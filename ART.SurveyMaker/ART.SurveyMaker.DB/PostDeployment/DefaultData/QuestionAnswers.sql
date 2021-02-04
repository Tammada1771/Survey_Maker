BEGIN 

	DECLARE @QuestionId uniqueidentifier;
	SELECT @QuestionId = Id from tblQuestion where Description = 'What color is the sky?' 

	DECLARE @AnswerId uniqueidentifier;
	SELECT @AnswerId = Id from tblAnswer where Description = 'Blue'

	INSERT INTO DBO.tbltblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, true)
	
	SELECT @QuestionId = Id from tblQuestion where Description = 'What color is grass?' 

	SELECT @AnswerId = Id from tblAnswer where Description = 'Green'

	INSERT INTO DBO.tbltblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, true)

	SELECT @QuestionId = Id from tblQuestion where Description = 'What noise does a cow make?' 

	SELECT @AnswerId = Id from tblAnswer where Description = 'Mooo'

	INSERT INTO DBO.tbltblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, true)

	SELECT @QuestionId = Id from tblQuestion where Description = 'What noise does a pig make?' 

	SELECT @AnswerId = Id from tblAnswer where Description = 'Oink'

	INSERT INTO DBO.tbltblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, true)

	SELECT @QuestionId = Id from tblQuestion where Description = 'What noise does a dog make?' 

	SELECT @AnswerId = Id from tblAnswer where Description = 'Woof'

	INSERT INTO DBO.tbltblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, true)

END
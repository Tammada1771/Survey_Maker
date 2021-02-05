BEGIN 

	DECLARE @QuestionId uniqueidentifier;
	SELECT @QuestionId = Id from tblQuestion where Question = 'What color is the sky?' 

	DECLARE @AnswerId uniqueidentifier;
	SELECT @AnswerId = Id from tblAnswer where Answer = 'Blue'

	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 1)
	
	SELECT @QuestionId = Id from tblQuestion where Question = 'What color is grass?' 

	SELECT @AnswerId = Id from tblAnswer where Answer = 'Green'

	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 1)

	SELECT @QuestionId = Id from tblQuestion where Question = 'What noise does a cow make?' 

	SELECT @AnswerId = Id from tblAnswer where Answer = 'Mooo'

	INSERT INTO DBO.tblQuestionAnswer(Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 1)

	SELECT @QuestionId = Id from tblQuestion where Question = 'What noise does a pig make?' 

	SELECT @AnswerId = Id from tblAnswer where Answer = 'Oink'

	INSERT INTO DBO.tblQuestionAnswer(Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 1)

	SELECT @QuestionId = Id from tblQuestion where Question = 'What noise does a dog make?' 

	SELECT @AnswerId = Id from tblAnswer where Answer = 'Woof'

	INSERT INTO DBO.tblQuestionAnswer(Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 1)

END
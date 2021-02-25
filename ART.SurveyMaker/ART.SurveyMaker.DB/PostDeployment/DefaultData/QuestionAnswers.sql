BEGIN 
	/* Declare variables */
	DECLARE @QuestionId uniqueidentifier;
	DECLARE @AnswerId uniqueidentifier;


	/* ////////// */
	/* Question 1 */
	SELECT @QuestionId = Id from tblQuestion where Question = 'What color is the sky?' 

	/*Right Answer*/
	SELECT @AnswerId = Id from tblAnswer where Answer = 'Blue'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 1)

	/*Wrong Answers*/
	SELECT @AnswerId = Id from tblAnswer where Answer = 'Yellow'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 0)

	SELECT @AnswerId = Id from tblAnswer where Answer = 'Purple'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 0)

	SELECT @AnswerId = Id from tblAnswer where Answer = 'White'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 0)


	/* ////////// */
	/* Question 2 */
	SELECT @QuestionId = Id from tblQuestion where Question = 'What color is grass?' 

	/*Right Answer*/
	SELECT @AnswerId = Id from tblAnswer where Answer = 'Green'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 1)

	/*Wrong Answers*/
	SELECT @AnswerId = Id from tblAnswer where Answer = 'Blue'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 0)

	SELECT @AnswerId = Id from tblAnswer where Answer = 'Purple'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 0)

	SELECT @AnswerId = Id from tblAnswer where Answer = 'Yellow'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 0)


	/* ////////// */
	/* Question 3 */
	SELECT @QuestionId = Id from tblQuestion where Question = 'What noise does a cow make?' 

	/*Right Answer*/
	SELECT @AnswerId = Id from tblAnswer where Answer = 'Mooo'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 1)

	/*Wrong Answers*/
	SELECT @AnswerId = Id from tblAnswer where Answer = 'Oink'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 0)

	SELECT @AnswerId = Id from tblAnswer where Answer = 'Chirp'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 0)

	SELECT @AnswerId = Id from tblAnswer where Answer = 'Woof'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 0)


	/* ////////// */
	/* Question 4 */
	SELECT @QuestionId = Id from tblQuestion where Question = 'What noise does a pig make?' 

	/*Right Answer*/
	SELECT @AnswerId = Id from tblAnswer where Answer = 'Oink'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 1)

	/*Wrong Answers*/
	SELECT @AnswerId = Id from tblAnswer where Answer = 'Woof'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 0)

	SELECT @AnswerId = Id from tblAnswer where Answer = 'Chirp'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 0)

	SELECT @AnswerId = Id from tblAnswer where Answer = 'Mooo'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 0)


	/* ////////// */
	/* Question 5 */
	SELECT @QuestionId = Id from tblQuestion where Question = 'What noise does a dog make?' 

	/*Right Answer*/
	SELECT @AnswerId = Id from tblAnswer where Answer = 'Woof'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 1)

	/*Wrong Answers*/
	SELECT @AnswerId = Id from tblAnswer where Answer = 'Oink'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 0)

	SELECT @AnswerId = Id from tblAnswer where Answer = 'Meow'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 0)

	SELECT @AnswerId = Id from tblAnswer where Answer = 'Chirp'
	INSERT INTO DBO.tblQuestionAnswer (Id, QuestionId, AnswerId, IsCorrect)
	VALUES
	(NEWID(), @QuestionId, @AnswerId, 0)

END
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ART.SurveyMaker.BL.Models;
using ART.SurveyMaker.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace ART.SurveyMaker.BL
{
    public static class QuestionManager
    {
        public async static Task<int> Insert(Question question, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblQuestion newrow = new tblQuestion();
                    newrow.Id = Guid.NewGuid();
                    newrow.Question = question.Text;

                    question.Id = newrow.Id;

                    dc.tblQuestions.Add(newrow);
                    int results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return results;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int SyncInsert(Question question, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblQuestion newrow = new tblQuestion();
                    newrow.Id = Guid.NewGuid();
                    newrow.Question = question.Text;

                    question.Id = newrow.Id;

                    dc.tblQuestions.Add(newrow);
                    int results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return results;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<Guid> Insert(string text,  bool rollback = false)
        {
            try
            {
                Question question = new Question {Text = text };
                await Insert(question);
                return question.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static Guid SyncInsert(string text, bool rollback = false)
        {
            try
            {
                Question question = new Question { Text = text };
                SyncInsert(question);
                return question.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<int> Delete(Guid id, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblQuestion row = dc.tblQuestions.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblQuestions.Remove(row);

                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                        return results;
                    }
                    else
                    {
                        throw new Exception("Row was not found");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int SyncDelete(Guid id, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblQuestion row = dc.tblQuestions.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblQuestions.Remove(row);

                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                        return results;
                    }
                    else
                    {
                        throw new Exception("Row was not found");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Update(Question question, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblQuestion row = dc.tblQuestions.FirstOrDefault(c => c.Id == question.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        row.Question = question.Text;

                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                        return results;
                    }
                    else
                    {
                        throw new Exception("Row was not found");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int SyncUpdate(Question question, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblQuestion row = dc.tblQuestions.FirstOrDefault(c => c.Id == question.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        row.Question = question.Text;

                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                        return results;
                    }
                    else
                    {
                        throw new Exception("Row was not found");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<Question> LoadById(Guid id)
        {
            Question question = new Question();

            await Task.Run(() =>
            {
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblQuestion tblquestion = dc.tblQuestions.FirstOrDefault(q => q.Id == id);


                   question.Answers = new List<Answer>();
                   foreach (tblQuestionAnswer qa in tblquestion.tblQuestionAnswers.ToList())
                   {
                      Answer answer = new Answer
                      {
                          Id = qa.AnswerId,
                          IsCorrect = qa.IsCorrect,
                          Text = qa.Answer.Answer
                      };
                      question.Answers.Add(answer);
                   }
                }
            });
            return question;
        }

        public async static Task<IEnumerable<Question>> Load()
        {
            try
            {
                List<Question> questions = new List<Question>();

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    dc.tblQuestions
                        .ToList()
                        .ForEach(c => questions.Add(new Question
                        {
                            Id = c.Id,
                            Text = c.Question,
                            Answers = c.Answers
                        }));
                    return questions;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<List<Question>> LoadQuestions()
        {
            try
            {
                List<Question> questions = new List<Question>();

                await Task.Run(() =>
                {
                    using (SurveyMakerEntities dc = new SurveyMakerEntities())
                    {
                        foreach (tblQuestion q in dc.tblQuestions.ToList())
                        {
                            Question question = new Question { Id = q.Id, Text = q.Question };

                            question.Answers = new List<Answer>();
                            foreach (tblQuestionAnswer qa in q.tblQuestionAnswers.ToList())
                            {
                                Answer answer = new Answer
                                {
                                    Id = qa.AnswerId,
                                    IsCorrect = qa.IsCorrect,
                                    Text = qa.Answer.Answer
                                };
                                question.Answers.Add(answer);
                            }
                            questions.Add(question);
                        }
                    }
                });
                return questions;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}

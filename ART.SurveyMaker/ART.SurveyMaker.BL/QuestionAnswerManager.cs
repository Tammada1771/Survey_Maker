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
    public static class QuestionAnswerManager
    {
        public async static Task<int> Insert(QuestionAnswer questionanswer, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblQuestionAnswer newrow = new tblQuestionAnswer();

                    newrow.Id = Guid.NewGuid();
                    newrow.AnswerId = questionanswer.AnswerId;
                    newrow.QuestionId = questionanswer.QuestionId;
                    newrow.IsCorrect = questionanswer.IsCorrect;

                    questionanswer.Id = newrow.Id;

                    dc.tblQuestionAnswers.Add(newrow);
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

        public async static Task<Guid> Insert(Guid answerId, Guid questionId, bool iscorrect, bool rollback = false)
        {
            try
            {
                QuestionAnswer questionanswer = new QuestionAnswer
                {
                    AnswerId = answerId,
                    QuestionId = questionId,
                    IsCorrect = iscorrect
                };
                await Insert(questionanswer);
                return questionanswer.Id;
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
                    tblQuestionAnswer row = dc.tblQuestionAnswers.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblQuestionAnswers.Remove(row);

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

        public async static Task<int> Update(QuestionAnswer questionanswer, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblQuestionAnswer row = dc.tblQuestionAnswers.FirstOrDefault(c => c.Id == questionanswer.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        row.AnswerId = questionanswer.AnswerId;
                        row.QuestionId = questionanswer.QuestionId;
                        row.IsCorrect = questionanswer.IsCorrect;

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

        public async static Task<QuestionAnswer> LoadById(Guid id)
        {
            try
            {
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblQuestionAnswer tblquestionanswer = dc.tblQuestionAnswers.FirstOrDefault(c => c.Id == id);
                    QuestionAnswer questionanswer = new QuestionAnswer();

                    if (tblquestionanswer != null)
                    {
                        questionanswer.Id = tblquestionanswer.Id;
                        questionanswer.AnswerId = tblquestionanswer.AnswerId;
                        questionanswer.QuestionId = tblquestionanswer.QuestionId;
                        questionanswer.IsCorrect = tblquestionanswer.IsCorrect;

                        return questionanswer;
                    }
                    else
                    {
                        throw new Exception("Could not find the row");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<IEnumerable<QuestionAnswer>> Load()
        {
            try
            {
                List<QuestionAnswer> questionanswers = new List<QuestionAnswer>();

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    dc.tblQuestionAnswers
                        .ToList()
                        .ForEach(c => questionanswers.Add(new QuestionAnswer
                        {
                            Id = c.Id,
                            AnswerId = c.AnswerId,
                            QuestionId = c.QuestionId,
                            IsCorrect = c.IsCorrect
                        }));
                    return questionanswers;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}

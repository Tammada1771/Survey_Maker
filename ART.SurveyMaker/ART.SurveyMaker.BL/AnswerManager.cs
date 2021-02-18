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
    public static class AnswerManager
    {
        public async static Task<int> Insert(Answer answer, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblAnswer newrow = new tblAnswer();
                    newrow.Id = Guid.NewGuid();
                    newrow.Answer = answer.Text;

                    answer.Id = newrow.Id;

                    dc.tblAnswers.Add(newrow);
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

        public async static Task<bool> BoolInsert(Answer answer, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblAnswer newrow = new tblAnswer();
                    newrow.Id = Guid.NewGuid();
                    newrow.Answer = answer.Text;

                    answer.Id = newrow.Id;

                    dc.tblAnswers.Add(newrow);
                    int results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                    bool r;
                    if(results > 0)
                    {
                        r = true;
                    }
                    else
                    {
                        r = false;
                    }
                    return r;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<Guid> Insert(string text, bool rollback = false)
        {
            try
            {
                Answer answer = new Answer { Text = text };
                await Insert(answer);
                return answer.Id;
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
                    tblAnswer row = dc.tblAnswers.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblAnswers.Remove(row);

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

        public async static Task<int> Update(Answer answer, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblAnswer row = dc.tblAnswers.FirstOrDefault(c => c.Id == answer.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        row.Answer = answer.Text;

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

        public async static Task<Answer> LoadById(Guid id)
        {
            try
            {
                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    tblAnswer tblanswer = dc.tblAnswers.FirstOrDefault(c => c.Id == id);
                    Answer answer = new Answer();

                    if (tblanswer != null)
                    {
                        answer.Id = tblanswer.Id;
                        answer.Text = tblanswer.Answer;

                        return answer;
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

        public async static Task<List<Answer>> Load(Guid questionid)
        {
            try
            {
                List<Answer> answers = new List<Answer>();

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    List<tblQuestionAnswer> tblquestionanswers = dc.tblQuestionAnswers.Where(qa => qa.QuestionId == questionid).ToList();

                    if(tblquestionanswers != null)
                    {

                        foreach(tblQuestionAnswer qa in tblquestionanswers.ToList())
                        {
                            Answer ans = new Answer
                            {
                                Id = qa.AnswerId,
                                Text = qa.Answer.Answer,
                                IsCorrect = qa.IsCorrect
                            };
                            answers.Add(ans);
                        };
                    }
                    else
                    {
                        throw new Exception("That Question was not found");
                    }
                    return answers;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async static Task<IEnumerable<Answer>> Load()
        {
            try
            {
                List<Answer> answers = new List<Answer>();

                using (SurveyMakerEntities dc = new SurveyMakerEntities())
                {
                    dc.tblAnswers
                        .ToList()
                        .ForEach(c => answers.Add(new Answer
                        {
                            Id = c.Id,
                            Text = c.Answer
                        }));
                    return answers;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }





    }
}

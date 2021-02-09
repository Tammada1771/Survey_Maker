using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ART.SurveyMaker.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace ART.SurveyMaker.PL.Test
{
    [TestClass]
    public class utQuestionAnswer
    {
        protected SurveyMakerEntities dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            dc = new SurveyMakerEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            transaction.Rollback();
            transaction.Dispose();
        }



        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(5, dc.tblQuestionAnswers.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            tblQuestionAnswer newrow = new tblQuestionAnswer();
            newrow.Id = Guid.NewGuid();
            newrow.AnswerId = dc.tblAnswers.FirstOrDefault().Id;
            newrow.QuestionId = dc.tblQuestions.FirstOrDefault().Id;
            newrow.IsCorrect = false;

            dc.tblQuestionAnswers.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblQuestionAnswer existingrow = dc.tblQuestionAnswers.FirstOrDefault(c => c.IsCorrect == false);

            if (existingrow != null)
            {
                existingrow.IsCorrect = true;
            }
            int results = dc.SaveChanges();

            Assert.AreEqual(results, 1);

        }

        [TestMethod]
        public void DeleteTest()
        {

            InsertTest();

            tblQuestionAnswer row = dc.tblQuestionAnswers.FirstOrDefault(c => c.IsCorrect == false);

            if (row != null)
            {
                dc.tblQuestionAnswers.Remove(row);
                dc.SaveChanges();
            }

            tblQuestionAnswer deletedrow = dc.tblQuestionAnswers.FirstOrDefault(c => c.IsCorrect == false);

            Assert.IsNull(deletedrow);
        }
    }
}

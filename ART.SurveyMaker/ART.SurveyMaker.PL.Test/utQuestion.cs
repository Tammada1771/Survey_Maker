using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ART.SurveyMaker.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;
namespace ART.SurveyMaker.PL.Test
{
    [TestClass]
    public class utQuestion
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
            Assert.AreEqual(5, dc.tblQuestions.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            tblQuestion newrow = new tblQuestion();
            newrow.Id = Guid.NewGuid();
            newrow.Question = "What is my name?";

            dc.tblQuestions.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblQuestion existingrow = dc.tblQuestions.FirstOrDefault(q => q.Question == "What is my name?");

            if (existingrow != null)
            {
                existingrow.Question = "What is this class?";
            }
            int results = dc.SaveChanges();

            Assert.AreEqual(results, 1);


        }

        [TestMethod]
        public void DeleteTest()
        {

            InsertTest();

            tblQuestion row = dc.tblQuestions.FirstOrDefault(q => q.Question == "What is my name?");

            if (row != null)
            {
                dc.tblQuestions.Remove(row);
                dc.SaveChanges();
            }

            tblQuestion deletedrow = dc.tblQuestions.FirstOrDefault(q => q.Question == "What is my name?");

            Assert.IsNull(deletedrow);
        }
    }
}

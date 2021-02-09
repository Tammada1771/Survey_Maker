using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ART.SurveyMaker.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;
 

namespace ART.SurveyMaker.PL.Test
{
    [TestClass]
    public class utAnswer
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
            Assert.AreEqual(5, dc.tblAnswers.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            tblAnswer newrow = new tblAnswer();
            newrow.Id = Guid.NewGuid();
            newrow.Answer = "Adam";

            dc.tblAnswers.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblAnswer existingrow = dc.tblAnswers.FirstOrDefault(a => a.Answer == "Adam");

            if (existingrow != null)
            {
                existingrow.Answer = "Adam Tamminga";
            }
            int results = dc.SaveChanges();

            Assert.AreEqual(results, 1);


        }

        [TestMethod]
        public void DeleteTest()
        {

            InsertTest();

            tblAnswer row = dc.tblAnswers.FirstOrDefault(a => a.Answer == "Adam");

            if (row != null)
            {
                dc.tblAnswers.Remove(row);
                dc.SaveChanges();
            }

            tblAnswer deletedrow = dc.tblAnswers.FirstOrDefault(a => a.Answer == "Adam");

            Assert.IsNull(deletedrow);
        }
    }
}

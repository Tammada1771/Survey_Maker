using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ART.SurveyMaker.BL.Models;
using ART.SurveyMaker.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ART.SurveyMaker.BL.Test
{
    [TestClass]
    public class utAnswerManager
    {
        [TestMethod]
        public void LoadTest()
        {
            //Async call to an Async method
            Task.Run(async () =>
            {
                var task = await AnswerManager.Load();
                IEnumerable<Answer> answers = task;
                Assert.AreEqual(5, answers.ToList().Count);
            });

            //Sync call to an Async method
            //var task = AnswerManager.Load();
            //task.Wait();
            //IEnumerable<Models.Answer> answerlist = task.Result;
            //Assert.AreEqual(3, answerlist.ToList().Count);

        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                int results = await AnswerManager.Insert(new Answer { Text = "Chirp", IsCorrect = true}, true);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void InsertFailedTest()
        {
            Task.Run(async () =>
            {
                int results = await AnswerManager.Insert(new Answer { Text = "Chirp" }, true);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = await AnswerManager.Load();
                IEnumerable<Answer> answers = task;
                Answer answer = answers.FirstOrDefault(a => a.Text == "Chirp");
                answer.Text = "Caw Caw";
                int results = await AnswerManager.Update(answer, true);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await AnswerManager.Load();
                IEnumerable<Answer> answers = task;
                Answer answer = answers.FirstOrDefault(a => a.Text == "Caw Caw");
                int results = await AnswerManager.Delete(answer.Id);
                Assert.IsTrue(results > 0);
            });
        }
    }
}

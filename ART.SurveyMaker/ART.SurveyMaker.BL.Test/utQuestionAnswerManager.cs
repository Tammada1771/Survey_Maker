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
    public class utQuestionAnswer
    {
        [TestMethod]
        public void LoadTest()
        {
            //Async call to an Async method
            Task.Run(async () =>
            {
                var task = await QuestionAnswerManager.Load();
                IEnumerable<QuestionAnswer> questionanswers = task;
                Assert.AreEqual(5, questionanswers.ToList().Count);
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
                int results = await QuestionAnswerManager.Insert(new QuestionAnswer { AnswerId = new Guid(), QuestionId = new Guid(), IsCorrect = true }, true);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void InsertFailedTest()
        {
            Task.Run(async () =>
            {
                int results = await QuestionAnswerManager.Insert(new QuestionAnswer { QuestionId = new Guid()}, true);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = await QuestionAnswerManager.Load();
                IEnumerable<QuestionAnswer> questionAnswers = task;
                QuestionAnswer questionAnswer = questionAnswers.LastOrDefault(qa => qa.IsCorrect == true);
                questionAnswer.IsCorrect = false;
                int results = await QuestionAnswerManager.Update(questionAnswer, true);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await QuestionAnswerManager.Load();
                IEnumerable<QuestionAnswer> questionanswers = task;
                QuestionAnswer questionanswer = questionanswers.LastOrDefault(qa => qa.IsCorrect == false);
                int results = await QuestionAnswerManager.Delete(questionanswer.Id);
                Assert.IsTrue(results > 0);
            });
        }
    }
}

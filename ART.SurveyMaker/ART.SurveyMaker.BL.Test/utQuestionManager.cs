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
    public class utQuestionManager
    {
        [TestMethod]
        public void LoadTest()
        {
            //Async call to an Async method
            Task.Run(async () =>
            {
                var task = await QuestionManager.Load();
                IEnumerable<Question> questions = task;
                Assert.AreEqual(5, questions.ToList().Count);
            });

            //Sync call to an Async method
            //var task = QuestionManager.Load();
            //task.Wait();
            //IEnumerable<Models.Question> questionlist = task.Result;
            //Assert.AreEqual(3, questionlist.ToList().Count);

        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                int results = await QuestionManager.Insert(new Question { Text = "What sound does a bird make?"}, true);
                Assert.IsTrue(results > 0);
            });
        }


        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = await QuestionManager.Load();
                IEnumerable<Question> questions = task;
                Question question = questions.FirstOrDefault(q => q.Text == "What sound does a bird make?");
                question.Text = "What sound does a crow make?";
                int results = await QuestionManager.Update(question, true);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await QuestionManager.Load();
                IEnumerable<Question> questions = task;
                Question question = questions.FirstOrDefault(q => q.Text == "What sound does a crow make?");
                int results = await QuestionManager.Delete(question.Id);
                Assert.IsTrue(results > 0);
            });
        }
    }
}

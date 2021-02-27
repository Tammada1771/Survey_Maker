using ART.SurveyMaker.BL;
using ART.SurveyMaker.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ART.SurveyMaker.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class QuestionAnswerList : Window
    {
        ucMaintainAnswer[] ucAnswers = new ucMaintainAnswer[4];
        ucAddAnswer[] ucAdds = new ucAddAnswer[4];
        List<Question> questions;
        List<Answer> answers;


        public QuestionAnswerList()
        {
            InitializeComponent();
            Reload();

            cboQuestions.DisplayMemberPath = "Text";
            cboQuestions.SelectedValuePath = "Id";

            this.Title = "Survey Maker";
        }

        private async void Reload()
        {
            questions = (List<Question>)await QuestionManager.LoadQuestions();

            cboQuestions.ItemsSource = questions;
        }


        private void DrawScreen(Question question)
        {
            int i = 0;
            int count = question.Answers.Count;
            int margin = 100;

            foreach (Answer a in question.Answers)
            {
                switch (count)
                {
                    case 4:
                        ucMaintainAnswer ucAnswer4 = new ucMaintainAnswer(a.Id);
                        ucAnswer4.imgDelete.MouseDown += (sender, e) => ImgDelete_MouseDown(sender, e, a.Id, i);
                        ucAnswer4.Margin = new Thickness(100, margin, 0, 0);
                        grdAnswer.Children.Add(ucAnswer4);
                        ucAnswers[i] = ucAnswer4;
                        i++;
                        margin += 50;
                        break;
                    case 3:
                        ucMaintainAnswer ucAnswer3 = new ucMaintainAnswer(a.Id);
                        ucAnswer3.imgDelete.MouseDown += (sender, e) => ImgDelete_MouseDown(sender, e, a.Id, i);
                        ucAnswer3.Margin = new Thickness(100, margin, 0, 0);
                        grdAnswer.Children.Add(ucAnswer3);
                        ucAnswers[i] = ucAnswer3;
                        i++;
                        margin += 50;
                        if (margin == 200)
                        {
                            ucAddAnswer ucAdd = new ucAddAnswer();


                        }
                        break;
                    case 2:
                        ucMaintainAnswer ucAnswer2 = new ucMaintainAnswer(a.Id);
                        ucAnswer2.imgDelete.MouseDown += (sender, e) => ImgDelete_MouseDown(sender, e, a.Id, i);
                        ucAnswer2.Margin = new Thickness(100, margin, 0, 0);
                        grdAnswer.Children.Add(ucAnswer2);
                        ucAnswers[i] = ucAnswer2;
                        i++;
                        margin += 50;
                        break;
                    case 1:
                        ucMaintainAnswer ucAnswer1 = new ucMaintainAnswer(a.Id);
                        ucAnswer1.imgDelete.MouseDown += (sender, e) => ImgDelete_MouseDown(sender, e, a.Id, i);
                        ucAnswer1.Margin = new Thickness(100, margin, 0, 0);
                        grdAnswer.Children.Add(ucAnswer1);
                        ucAnswers[i] = ucAnswer1;
                        i++;
                        margin += 50;
                        break;
                    case 0:
                        //add four new answer adds
                        break;
                }
            }
        }

        private void ImgDelete_MouseDown(object sender, MouseButtonEventArgs e, Guid id = new Guid(), int i = 0)
        {
            var questionId = questions[cboQuestions.SelectedIndex].Id;
            ucMaintainAnswer ucAnswer = new ucMaintainAnswer();
            Guid answerId = new Guid();
            answerId = id;
            QuestionAnswerManager.SyncDelete(questionId, answerId);
            grdAnswer.Children.Remove(ucAnswer);

            var question = QuestionManager.SyncLoadById(questionId);

            DrawScreen(question);


            //if (answers.Count == 0 || answers.Count < 4)
            //{
            //    MessageBox.Show("Please Save your Answers First");
            //}
            //else if (answers.Count == 4)
            //{ 
            //    QuestionAnswerManager.SyncDelete(questionId, answerId);
            //    grdAnswer.Children.Remove(ucAnswer);
            //}

            //answers = (List<Answer>)AnswerManager.SyncLoad(questionId);

            //DrawScreen(answers);
        }


        private void BtnQuestion_Click(object sender, RoutedEventArgs e)
        {
            new MaintainQuestionAnswer(ScreenMode.Question).ShowDialog();
            Reload();
        }

        private void BtnAnswer_Click(object sender, RoutedEventArgs e)
        {
            new MaintainQuestionAnswer(ScreenMode.Answer).ShowDialog();
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var questionId = questions[cboQuestions.SelectedIndex].Id;
            var question = QuestionManager.SyncLoadById(questionId);
            
            DrawScreen(question);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            foreach(ucMaintainAnswer ucMA in ucAnswers)
            {
                QuestionAnswer qa = new QuestionAnswer();
                qa.QuestionId = questions[cboQuestions.SelectedIndex].Id;
                qa.AnswerId = ucMA.AttributeId;
                qa.IsCorrect = (bool)ucMA.rdbtnAnswer.IsChecked;

                var row = QuestionAnswerManager.Select(qa.QuestionId, qa.AnswerId, qa.IsCorrect);

                if(row == null)
                {
                    QuestionAnswerManager.SyncInsert(qa);
                }
            }

            MessageBox.Show("Answers Saved");
        }
    }
}

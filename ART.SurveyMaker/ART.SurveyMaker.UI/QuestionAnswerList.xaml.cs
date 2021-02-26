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
        List<Question> questions;
        List<Answer> answers;
        List<Guid> answersIds = new List<Guid>();

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


        private void DrawScreen(List<Answer> answers)
        {
            answersIds.Clear();
            int i = 0;
            int count = answers.Count;

            if (count == 4)
            {
                foreach (Answer ans in answers)
                {
                    switch (i)
                    {
                        case 0:
                            ucMaintainAnswer ucAnswer = new ucMaintainAnswer(ans.Id);
                            //ucAnswer.imgDelete.MouseLeftButtonUp += ImgDelete_MouseLeftButtonUp;
                            ucAnswer.Name = "Spot0";
                            ucAnswer.imgDelete.MouseDown += (sender, e) => ImgDelete_MouseDown(sender, e, ans.Id, ucAnswer.Name);
                            answersIds.Add(ans.Id);
                            ucAnswer.Margin = new Thickness(100, 100, 0, 0);
                            grdAnswer.Children.Add(ucAnswer);
                            ucAnswers[i] = ucAnswer;
                            break;
                        case 1:
                            ucMaintainAnswer ucAnswer1 = new ucMaintainAnswer(ans.Id);
                            //ucAnswer1.imgDelete.MouseLeftButtonUp += ImgDelete_MouseLeftButtonUp;
                            ucAnswer1.Name = "Spot1";
                            ucAnswer1.imgDelete.MouseDown += (sender, e) => ImgDelete_MouseDown(sender, e, ans.Id, ucAnswer1.Name);
                            answersIds.Add(ans.Id);
                            ucAnswer1.Margin = new Thickness(100, 150, 0, 0);
                            grdAnswer.Children.Add(ucAnswer1);
                            ucAnswers[i] = ucAnswer1;
                            break;
                        case 2:
                            ucMaintainAnswer ucAnswer2 = new ucMaintainAnswer(ans.Id);
                            //ucAnswer2.imgDelete.MouseLeftButtonUp += ImgDelete_MouseLeftButtonUp;
                            ucAnswer2.Margin = new Thickness(100, 200, 0, 0);
                            ucAnswer2.Name = "Spot2";
                            ucAnswer2.imgDelete.MouseDown += (sender, e) => ImgDelete_MouseDown(sender, e, ans.Id, ucAnswer2.Name);
                            answersIds.Add(ans.Id);
                            grdAnswer.Children.Add(ucAnswer2);
                            ucAnswers[i] = ucAnswer2;
                            break;
                        case 3:
                            ucMaintainAnswer ucAnswer3 = new ucMaintainAnswer(ans.Id);
                            //ucAnswer3.imgDelete.MouseLeftButtonUp += ImgDelete_MouseLeftButtonUp;
                            ucAnswer3.Margin = new Thickness(100, 250, 0, 0);
                            ucAnswer3.Name = "Spot3";
                            ucAnswer3.imgDelete.MouseDown += (sender, e) => ImgDelete_MouseDown(sender, e, ans.Id, ucAnswer3.Name);
                            answersIds.Add(ans.Id);

                            grdAnswer.Children.Add(ucAnswer3);
                            ucAnswers[i] = ucAnswer3;
                            break;
                    }

                    i++;
                } 
            }
            else
            {
                var margin = 100;
                if(count == 0)
                {
                    count++;
                }

                for(i = count; i == 4; i++ )
                {
                    ucMaintainAnswer ucAnswer = new ucMaintainAnswer();
                    //ucAnswer.imgDelete.MouseLeftButtonUp += ImgDelete_MouseLeftButtonUp;
                    ucAnswer.imgDelete.MouseDown += (sender, e) => ImgDelete_MouseDown(sender, e);
                    ucAnswer.Margin = new Thickness(100, margin, 0, 0);
                    grdAnswer.Children.Add(ucAnswer);
                    ucAnswers[i] = ucAnswer;
                    margin += 50;
                }
                
            }
            
        }

        private void ImgDelete_MouseDown(object sender, MouseButtonEventArgs e, Guid id = new Guid(), string name = "")
        {
            
            var questionId = questions[cboQuestions.SelectedIndex].Id;
            answers = (List<Answer>)AnswerManager.SyncLoad(questionId);
            ucMaintainAnswer ucAnswer = new ucMaintainAnswer();
            Guid answerId = new Guid();
            ucAnswer.Name = name;
            answerId = id;


            if (answers.Count == 0 || answers.Count < 4)
            {
                MessageBox.Show("Please Save your Answers First");
            }
            else if (answers.Count == 4)
            { 
                QuestionAnswerManager.SyncDelete(questionId, answerId);
                grdAnswer.Children.Remove(ucAnswer);
            }

            answers = (List<Answer>)AnswerManager.SyncLoad(questionId);

            DrawScreen(answers);
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

        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var questionId = questions[cboQuestions.SelectedIndex].Id;
            answers = (List<Answer>)AnswerManager.SyncLoad(questionId);
            questions = await QuestionManager.LoadQuestions();

            //USE THIS MORE!! MORON
            ////foreach(Question q in questions)
            ////{
            ////    q.Answers
            ////}
            
            DrawScreen(answers);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            foreach(ucMaintainAnswer ucans in ucAnswers)
            {
                var questionId = questions[cboQuestions.SelectedIndex].Id;
                var answerId = ucans.AttributeId;
                bool RorW = (bool)ucans.rdbtnAnswer.IsChecked;
                

                var selectcheck = QuestionAnswerManager.Select(questionId, answerId);

                if(selectcheck == null)
                {
                    QuestionAnswer questionanswer = new QuestionAnswer();

                    questionanswer.QuestionId = questionId;
                    questionanswer.AnswerId = answerId;
                    questionanswer.IsCorrect = RorW;

                    QuestionAnswerManager.SyncInsert(questionanswer);
                } 

            }
        }
    }
}

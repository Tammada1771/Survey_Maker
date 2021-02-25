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
            int i = 0;

            foreach(Answer ans in answers)
            { 
                switch(i)
                {
                    case 0:
                        ucMaintainAnswer ucAnswer = new ucMaintainAnswer(ans.Id);
                        ucAnswer.imgDelete.MouseLeftButtonUp += ImgDelete_MouseLeftButtonUp;
                        ucAnswer.Margin = new Thickness(100, 100, 0, 0);
                        grdAnswer.Children.Add(ucAnswer);
                        ucAnswers[i] = ucAnswer;
                        break;
                    case 1:
                        ucMaintainAnswer ucAnswer1 = new ucMaintainAnswer(ans.Id);
                        ucAnswer1.imgDelete.MouseLeftButtonUp += ImgDelete_MouseLeftButtonUp;
                        ucAnswer1.Margin = new Thickness(100, 150, 0, 0);
                        grdAnswer.Children.Add(ucAnswer1);
                        ucAnswers[i] = ucAnswer1;
                        break;
                    case 2:
                        ucMaintainAnswer ucAnswer2 = new ucMaintainAnswer(ans.Id);
                        ucAnswer2.imgDelete.MouseLeftButtonUp += ImgDelete_MouseLeftButtonUp;
                        ucAnswer2.Margin = new Thickness(100, 200, 0, 0);
                        grdAnswer.Children.Add(ucAnswer2);
                        ucAnswers[i] = ucAnswer2;
                        break;
                    case 3:
                        ucMaintainAnswer ucAnswer3 = new ucMaintainAnswer(ans.Id);
                        ucAnswer3.imgDelete.MouseLeftButtonUp += ImgDelete_MouseLeftButtonUp;
                        ucAnswer3.Margin = new Thickness(100, 250, 0, 0);
                        grdAnswer.Children.Add(ucAnswer3);
                        ucAnswers[i] = ucAnswer3;
                        break;
                }

                i++;
            }
            
        }

        private void ImgDelete_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Title = "Picked the image " + DateTime.Now;
        }

        private void BtnQuestion_Click(object sender, RoutedEventArgs e)
        {
            new MaintainQuestionAnswer(ScreenMode.Question).ShowDialog();
        }

        private void BtnAnswer_Click(object sender, RoutedEventArgs e)
        {
            new MaintainQuestionAnswer(ScreenMode.Answer).ShowDialog();
        }

        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var questionId = questions[cboQuestions.SelectedIndex].Id;
            answers = (List<Answer>)AnswerManager.SyncLoad(questionId);
            
            DrawScreen(answers);
        }
    }
}

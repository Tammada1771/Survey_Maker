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
            questions = (List<Question>)await QuestionManager.Load();
            //answers = 

            //cboAttribute.ItemsSource = questions;
        }


        private void BtnQuestion_Click(object sender, RoutedEventArgs e)
        {
            new MaintainQuestionAnswer(ScreenMode.Question).ShowDialog();
        }

        private void BtnAnswer_Click(object sender, RoutedEventArgs e)
        {
            new MaintainQuestionAnswer(ScreenMode.Answer).ShowDialog();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

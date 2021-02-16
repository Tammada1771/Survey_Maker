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
using System.Windows.Shapes;

namespace ART.SurveyMaker.UI
{
    public enum ScreenMode
    {
        Question = 1,
        Answer = 2
    }
    /// <summary>
    /// Interaction logic for MaintainQuestionAnswer.xaml
    /// </summary>
    /// this will look like maintain attributes

    public partial class MaintainQuestionAnswer : Window
    {
        List<Question> questions;
        List<Answer> answers;
        ScreenMode screenMode;

        public MaintainQuestionAnswer(ScreenMode screenmode)
        {
            InitializeComponent();
            screenMode = screenmode;

            Reload();

            cboAttribute.DisplayMemberPath = "Text";
            cboAttribute.SelectedValuePath = "Id";

            lblAttribute.Content = screenMode.ToString() + "s";
            this.Title = "Maintain " + screenMode.ToString() + "s";
        }

        private async void Reload()
        {
            cboAttribute.ItemsSource = null;

            switch(screenMode)
            {
                case ScreenMode.Question:
                    questions = (List<Question>)await QuestionManager.Load();
                    cboAttribute.ItemsSource = questions;
                    break;
                case ScreenMode.Answer:
                    answers = (List<Answer>)await AnswerManager.Load();
                    cboAttribute.ItemsSource = answers;
                    break;
            }
        }

        private void CboAttribute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cboAttribute.SelectedIndex > -1)
            {
                if (screenMode == ScreenMode.Question)
                    txtDescription.Text = questions[cboAttribute.SelectedIndex].Text;
                else
                    txtDescription.Text = answers[cboAttribute.SelectedIndex].Text;
            }
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            switch(screenMode)
            {
                case ScreenMode.Question:
                    Task.Run(async () =>
                    {
                        await QuestionManager.Insert(new Question { Text = txtDescription.Text });
                    });
                    break;
                case ScreenMode.Answer:
                    Task.Run(async () =>
                    {
                        await AnswerManager.Insert(new Answer { Text = txtDescription.Text });
                    });
                    break;
            }

            txtDescription.Text = string.Empty;
            Reload();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            switch (screenMode)
            {
                case ScreenMode.Question:
                    Task.Run(async () =>
                    {
                        Question question = questions[cboAttribute.SelectedIndex];
                        question.Text = txtDescription.Text;
                        await QuestionManager.Update(question);
                    });
                    break;
                case ScreenMode.Answer:
                    Task.Run(async () =>
                    {
                        Answer answer = answers[cboAttribute.SelectedIndex];
                        answer.Text = txtDescription.Text;
                        await AnswerManager.Update(answer);
                    });
                    break;
            }

            txtDescription.Text = string.Empty;
            Reload();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (screenMode)
            {
                case ScreenMode.Question:
                    Task.Run(async () =>
                    {
                        Guid id = questions[cboAttribute.SelectedIndex].Id;
                        await QuestionManager.Delete(id);
                    });
                    break;
                case ScreenMode.Answer:
                    Task.Run(async () =>
                    {
                        await AnswerManager.Delete(answers[cboAttribute.SelectedIndex].Id);
                    });
                    break;
            }

            txtDescription.Text = string.Empty;
            Reload();
        }
    }
}

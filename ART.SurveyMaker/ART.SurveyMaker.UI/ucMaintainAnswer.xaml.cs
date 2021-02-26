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
    /// Interaction logic for ucMaintainAnswer.xaml
    /// </summary>
    public partial class ucMaintainAnswer : UserControl
    {

        public List<Answer> answers;

        public ucMaintainAnswer(Guid id = new Guid())
        {
            InitializeComponent();

            Reload();

            if(id != Guid.Empty)
            {
                cboAnswers.SelectedValue = id;
            }
            else
            {
                cboAnswers.SelectedValue = null;
            }

        }

        public ucMaintainAnswer(int id)
        {
            InitializeComponent();

            Reload();

            cboAnswers.SelectedValue = id;
        }


        private async void Reload()
        {
            cboAnswers.ItemsSource = null;

            answers = (List<Answer>)await AnswerManager.Load();
            cboAnswers.ItemsSource = answers;

            cboAnswers.DisplayMemberPath = "Text";
            cboAnswers.SelectedValuePath = "Id";
        }


        public Guid AttributeId
        {
            get { return (Guid)cboAnswers.SelectedValue; }
        }

        public string AttributeText
        {
            get { return cboAnswers.Text; }
        }


        private void cboAnswers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show("Test");
        }
    }
}

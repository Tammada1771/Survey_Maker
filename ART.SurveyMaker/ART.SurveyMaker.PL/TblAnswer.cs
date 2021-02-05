using System;
using System.Collections.Generic;

#nullable disable

namespace ART.SurveyMaker.PL
{
    public partial class tblAnswer
    {
        public tblAnswer()
        {
            tblQuestionAnswers = new HashSet<tblQuestionAnswer>();
        }

        public Guid Id { get; set; }
        public string Answer { get; set; }

        public virtual ICollection<tblQuestionAnswer> tblQuestionAnswers { get; set; }
    }
}

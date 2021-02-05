using System;
using System.Collections.Generic;

#nullable disable

namespace ART.SurveyMaker.PL
{
    public partial class tblQuestion
    {
        public tblQuestion()
        {
            tblQuestionAnswers = new HashSet<tblQuestionAnswer>();
        }

        public Guid Id { get; set; }
        public string Question { get; set; }

        public virtual ICollection<tblQuestionAnswer> tblQuestionAnswers { get; set; }
    }
}

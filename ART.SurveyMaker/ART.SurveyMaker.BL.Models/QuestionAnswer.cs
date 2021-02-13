using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ART.SurveyMaker.BL.Models
{
    public class QuestionAnswer
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }
        public bool IsCorrect { get; set; }
    }
}

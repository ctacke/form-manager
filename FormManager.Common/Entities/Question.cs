using System;
using System.Collections;
using System.Collections.Generic;

namespace OpenNETCF.FormManager
{
    public class Question
    {
        public int QuestionID { get; set; }
        public bool IsRequired { get; set; }
        public bool AcceptNotes { get; set; }
        public string DefaultValue { get; set; }
        public string QuestionText { get; set; }
        public string Units { get; set; }
        public QuestionType QuestionType { get; set; }

        public Question(string questionText, QuestionType questionType, string units = "", string defaultValue = "", bool required = true, bool acceptNotes = true)
        {
            QuestionText = questionText;
            QuestionType = questionType;

            this.IsRequired = true;
            this.AcceptNotes = true;

            // set any question type-specific defaults
            switch (questionType)
            {
                case QuestionType.Input:
                    break;
                case QuestionType.PassFail:
                    break;
                case QuestionType.YesNo:
                    break;
            }
        }
    }

}

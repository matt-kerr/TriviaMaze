// Team //noComment
//
// Matt Kerr
// Mary Floyd
// Tim Unger
//
// CSCD350
// Spring 2015

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze
{
    [Serializable]
    public class Question
    {
        private string type;
        private string category;
        private string question_string;
        private string optionA;
        private string optionB;
        private string optionC;
        private string optionD;
        private string answer;

        public Question(string type, string category, string question_string, string optionA, string optionB, string optionC, string optionD, string answer)
        {
            this.type = type;
            this.category = category;
            this.question_string = question_string;
            this.optionA = optionA;
            this.optionB = optionB;
            this.optionC = optionC;
            this.optionD = optionD;
            this.answer = answer;
        }

        public string GetQuestionType()
        {
            // Returns which type of Question this is (multiple choice, true/false, or short answer)

            return this.type;
        }

        public string GetQuestionString()
        {
            // Returns the actual question that the user will get asked
            // as opposed to the Question class itself

            return this.question_string;
        }

        public string GetOptionA()
        {
            // Returns option A for a multiple choice question
            // or the true option for a true/false question

            return this.optionA;
        }

        public string GetOptionB()
        {
            // Returns option B for a multiple choice question
            // or the false option for a true/false question

            return this.optionB;
        }

        public string GetOptionC()
        {
            // Returns option C for a multiple choice question

            return this.optionC;
        }

        public string GetOptionD()
        {
            // Returns option D for a multiple choice question

            return this.optionD;
        }

        public string GetAnswer()
        {
            // Returns the answer to this Question

            return this.answer;
        }

        public string GetCategory()
        {
            // Returns the category of this Question

            return this.category;
        }
 
    }
}

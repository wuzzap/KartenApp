using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learningProggy
{
    public class Card
    {
        static sqlte_connector sc = new sqlte_connector();

        public int ID;
        public string question;
        public string answer;
        public int correct;
        public int wrong;
        public int priority;
        public string stackName;


        public DateTime correctTime;
        public DateTime wrongTime;
        public DateTime creationTime;
        public TimeSpan duration;

        public Card()
        {
            this.ID = ID;
            this.question = question;
            this.answer = answer;
            this.correct = correct;
            this.wrong = wrong;
            this.priority = priority;
            this.stackName = stackName;
            this.correctTime = correctTime;
            this.wrongTime = wrongTime;
            this.creationTime = creationTime;
            this.duration = duration;
        }

        public Card(int ID)
        {
            this.ID = ID;
            this.question = sc.getQuestion(ID);
            this.answer = sc.getAnswer(ID);
            this.correct = sc.getCorrect(ID);
            this.wrong = sc.getWrong(ID);
            this.priority = sc.getPriority(ID);
            this.stackName = sc.getStackName(ID);
            this.correctTime = sc.get_CorrectTime(ID);
            this.wrongTime = sc.get_wrongTime(ID);
            this.creationTime = sc.get_CardCreationTime(ID);
            this.duration = sc.getDuration(ID);
        }

        public Card(int ID, string question, string answer, int correct, int wrong, int priority, string stackName, DateTime correctTime, DateTime wrongTime, DateTime creationTime, TimeSpan duration)
        {
            this.ID = ID;
            this.question = question;
            this.answer = answer;
            this.correct = correct;
            this.wrong = wrong;
            this.priority = priority;
            this.stackName = stackName;
            this.correctTime = correctTime;
            this.wrongTime = wrongTime;
            this.creationTime = creationTime;
            this.duration = duration;
        }

        public static List<Card> createCards(List<int> list)
        {
            List<Card> cards = new List<Card>();

            for(int i = 0; i < list.Count; i++)
            {
                Card card = new Card(list[i]);
                cards.Add(card);
            }
            return cards;
        }


    }
}

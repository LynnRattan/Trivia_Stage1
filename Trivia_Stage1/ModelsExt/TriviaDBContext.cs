using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivia_Stage1.Models
{
    public partial class TriviaDBContext
    {
        public List<Player> GetPlayers() //פעולה המחזירה רשימה של שחקנים מטבלת השחקנים
        {
            return this.Players.ToList();
        }

        public Player Login(string mail, string password) // פעולה המקבלת מייל וסיסמה ובודקת אם קיים שחקן בטבלת שחקנים בעל המייל והסיסמה האלה ומחזירה את השחקן אם כן
        {

            return this.Players.Where(p => p.PlayerMail == mail && p.Password == password).FirstOrDefault();
        }
        public Player GetPlayerByMail(string mail)//פעולה המקבלת מייל ובודקת אם קיים שחקן בטבלת שחקנים בעל המייל הזה ומחזירה את השחקן אם כן
        {
            return this.Players.Where(p => p.PlayerMail == mail).FirstOrDefault();
        }

        

        public Player SignUp(string mail, string password, string name)//פעולה המקבלת מייל, סיסמה ושם ובודקת אם לא קיים שחקן בעל המייל הזה בטבלת שחקנים ואם לא קיים אז היא יוצרת שחקן חדש בעל הפרטים שהוכנסו
        {

            if (GetPlayerByMail(mail) != null)
                return null;

            Player p = new Player() { PlayerMail = mail, Name = name, Password = password, LevelCode = 1, Points = 0 };
            try
            {
                this.Players.Add(p);
                SaveChanges();
                return p;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<Question> PendingQuestion()//פעולה המחזירה רשימה של שאלות שהסטטוס שלהן הוא 3
        {
            return this.Questions.Where(x => x.StatusCode == 3).ToList();
        }

        public void AddQuestion(int playerId, int subject, string text, string correctAns, string wrongAns1, string wrongAns2, string wrongAns3)
        {
            Question q = new Question() {CreatedBy = playerId, SubjectCode=subject, Text =text, CorrectAns=correctAns, WrongAns1=wrongAns1, WrongAns2=wrongAns2, WrongAns3=wrongAns3, StatusCode=3};  
            this.Questions.Add(q);
            SaveChanges();
        }
        

        public Player Profile(Player player)//פעולה המחזירה את השחקן שהיא מקבלת, כלומר השחקן המחובר
        {
            return this.Players.Where(p => p == player).FirstOrDefault();
        }

        public Question RandomQusetion() //פעולה שמחזירה שאלה מתוך רשימה של שאלות בטבלת שאלות באופן רנדומלי
        {
            List <Question> q = new List<Question>(); 
            q=this.Questions.ToList();
            Random random = new Random();   
            int place = random.Next(0,q.Count);
            return q[place];
        }





    }
}

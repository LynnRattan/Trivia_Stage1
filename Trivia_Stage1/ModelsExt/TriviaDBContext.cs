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
        public List<Player> GetPlayers()
        {
            return this.Players.ToList();
        }
         
        public Player Login(string mail, string password)
        {

            return this.Players.Where(p => p.PlayerMail == mail && p.Password == password).FirstOrDefault();
        } 
        public Player GetPlayerByMail(string mail)
        {
            return this.Players.Where(p => p.PlayerMail == mail).FirstOrDefault();
        }

        //public Player GetPlayerByName(string name)
        //{
        //    return this.Players.Where(p => p.Name == name).FirstOrDefault();
        //}

       public Player SignUp(string mail, string password, string name)
        {
           
            if(GetPlayerByMail(mail) != null)
                return null;
            
            Player p = new Player() { PlayerMail=mail, Name=name, Password=password,LevelCode=1,Points=0};
            try
            {
                this.Players.Add(p);
                SaveChanges();
                return p;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }


    }
}

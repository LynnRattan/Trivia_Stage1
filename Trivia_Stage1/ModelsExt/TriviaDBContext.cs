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

        public Player GetPlayerByName(string name)
        {
            return this.Players.Where(p => p.Name == name).FirstOrDefault();
        }

       public Player SignUp(string mail, string name, string password)
        {
            if (GetPlayerByName(name) != null)
                return null;
            if(GetPlayerByMail(mail) != null)
                return null;
            
            Player p = new Player() { PlayerMail=mail, Name=name, Password=password,LevelCode=0,Points=0};
            try
            {
                this.Players.Add(p);
                SaveChanges();
                return p;
            }
            catch
            {
                return null;
            }
            return null;
        }


    }
}

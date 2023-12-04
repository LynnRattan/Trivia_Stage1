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
         
        public Player Login(string mail, string name)
        {
          return this.Players.Where(p => p.PlayerMail == mail && p.Name == name).FirstOrDefault();
        } 
    }
}

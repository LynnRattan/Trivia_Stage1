using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace Trivia_Stage1.UI
{
    using Models;
    public class TriviaScreensImp:ITriviaScreens
    {

        //Place here any state you would like to keep during the app life time
        //For example, player login details...
        Player player;
        TriviaDBContext db = new TriviaDBContext();

        //Implememnt interface here
        public bool ShowLogin() //לין
        {

            Console.WriteLine("Enter mail");
            string mail = Console.ReadLine();
            Console.WriteLine("Enter password");
            string password = Console.ReadLine();


           
                player = db.Login(mail, password);
                //אם לא תקין
                while (player == null)
                {
                    Console.WriteLine("Cannot be found. Enter again.");
                    Console.WriteLine("Enter mail");
                    mail = Console.ReadLine();
                    Console.WriteLine("Enter password");
                    password = Console.ReadLine();
                    player = db.Login(mail, password);
                }
                return true;
            
            return false;
        }
        public bool ShowSignUp() //שירה
        {
            //Logout user if anyone is logged in!
            //A reference to the logged in user should be stored as a member variable
            //in this class! Example:
            player=null;

           

            //Loop through inputs until a user/player is created or 
            //user choose to go back to menu

            char c = ' ';
            while (c != 'B' && c != 'b' && player == null)
            {
                //Clear screen
                ClearScreenAndSetTitle("Signup");

                Console.Write("Please type your email: ");
                string email = Console.ReadLine();
                while (!IsEmailValid(email))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Bad Email Format! Please try again:");
                    Console.ResetColor();
                    email = Console.ReadLine();
                }

                Console.Write("Please type your password: ");
                string password = Console.ReadLine();
                while (!IsPasswordValid(password))
                {
                    Console.ForegroundColor= ConsoleColor.Red;  
                    Console.Write("Password must be at least 4 characters! Please try again: ");
                    Console.ResetColor();   
                    password = Console.ReadLine();
                }

                Console.Write("Please type your Name: ");
                string name = Console.ReadLine();
                while (!IsNameValid(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Name must be at least 3 characters! Please try again: ");
                    Console.ResetColor();
                    name = Console.ReadLine();
                }
               
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Connecting to Server...");
                Console.ResetColor();
                //Create instance of Business Logic and call the signup method
                // *For example:
                try
                {
                  player = db.SignUp(email, password, name);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to signup! Email may already exist in DB!");
                    Console.ResetColor();
                }


                if (player == null)
                {
                    //Provide a proper message for example:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to signup! Email may already exist in DB!");
                    Console.ResetColor();
                    Console.WriteLine("Press (B)ack to go back or any other key to signup again...");


                    //Get another input from user
                    c = Console.ReadKey(true).KeyChar;
                }
            }
            //return true if signup suceeded!
            Console.WriteLine("Sign Up suceeded!");
            return true;


        }

        public void ShowAddQuestion() //שירי
        {
            
            if (player.Points == 100)
            {
                ClearScreenAndSetTitle("Add a Question");
                Console.WriteLine("Choose a subject: 1 - Sports, 2 - Politics, 3 - History, 4 - Science, 5 - Ramon HS");
                int subject = int.Parse(Console.ReadLine());
                while (subject < 1 || subject > 5)
                {
                    Console.WriteLine("Does not exist.Please try again.");
                    Console.WriteLine("Choose a subject: 1 - Sports, 2 - Politics, 3 - History, 4 - Science, 5 - Ramon HS");
                    subject = int.Parse(Console.ReadLine());

                }
                Console.WriteLine("Add the question's text: ");
                string text = Console.ReadLine();
                if (text.ToLower() == "b")
                {
                    return;
                }
                Question question = new Question();
                question.Text = text;
               
                string correctAns;
                string wrongAns1;
                string wrongAns2;
                string wrongAns3;
                Console.WriteLine("Add the right answer");
                correctAns = Console.ReadLine();
                Console.WriteLine("add the first wrong answer");
                wrongAns1 = Console.ReadLine();
                Console.WriteLine("add the second wrong answer");
                wrongAns2 = Console.ReadLine();
                Console.WriteLine("add the third wrong answer");
                wrongAns3 = Console.ReadLine();
                

                db.AddQuestion(player.PlayerId,subject,text,correctAns,wrongAns1,wrongAns2,wrongAns3);
               
                player.Points = 0;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You do not have permission to view this page");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
            }
        }

        public void ShowPendingQuestions() //שירה
        {
            List<Question> q = new List<Question>();
            q = db.PendingQuestion();

            if (player.LevelCode == 2 || player.LevelCode == 3)
            {
                if (q.Count == 0)
                {
                    ClearScreenAndSetTitle("Pending Questions");
                    Console.WriteLine("No pending questions. Press any key to get back.");
                    Console.ReadKey(true);
                }
                

                char x = '5';
               
                for (int i = 0; i < q.Count; i++)
                {
                    ClearScreenAndSetTitle("Pending Questions");
                    Console.WriteLine($"Subject number:{q[i].SubjectCode} \nQuestion:{q[i].Text} \nCorrect Answer:{q[i].CorrectAns} \nWrong Answer 1:{q[i].WrongAns1} \nWrong Answer 2: {q[i].WrongAns2} \nWrong Answer3: {q[i].WrongAns3}");
                    Console.WriteLine("Press 1 to approve, 2 to reject, 3 to skip, 4 to exit");
                    while (x == '5')
                    {
                        x = Console.ReadKey().KeyChar;
                        if (x == '1')
                        {
                            q[i].StatusCode = 1;
                        }
                        else if (x == '2')
                        {
                            q[i].StatusCode = 2;
                        }
                        else if (x == '3')
                        {
                            q[i].StatusCode = 3;
                        }
                        else if (x == '4')
                        {
                            db.SaveChanges();
                            return;
                        }
                        else
                        {
                            x = '5';
                        }
                    }
                }
                db.SaveChanges();
                
            }
            else
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("You do not have permission to view this page");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
            }
        }
        public void ShowGame()//שירי
        {
            while(true)
            {
                ClearScreenAndSetTitle("Game");
                Question q = db.RandomQusetion();
                List<string> answers = new List<string>(){q.CorrectAns,q.WrongAns1,q.WrongAns2,q.WrongAns3};
                answers = answers.OrderBy(x => Random.Shared.Next()).ToList();
                Console.WriteLine(q.Text);
                Console.WriteLine($"1) {answers[0]}");
                Console.WriteLine($"2) {answers[1]}");
                Console.WriteLine($"3) {answers[2]}");
                Console.WriteLine($"4) {answers[3]}");
                Console.WriteLine("Choose the correct answer by pressing its number.");
                int answer = 0;
                try
                {
                    answer = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                while (answer!=1 && answer!=2 && answer!=3 && answer!=4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Not an option. Please choose again.");
                    Console.ResetColor();
                    answer = int.Parse(Console.ReadLine());
                }
                if(answers[answer-1] == q.CorrectAns)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Correct! +10 points.");
                    player.Points += 10;
                    Console.ResetColor();
                    if(player.Points >100)
                    {
                        player.Points = 100;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Wrong! The correct answer is {q.CorrectAns}");
                    player.Points -= 5;
                    if(player.Points < 0)
                    {
                        player.Points = 0;
                    }
                    Console.ResetColor();
                }
                Console.WriteLine("\n To play again press A/a else press any other key.");
                
                char c = Console.ReadKey().KeyChar; 
                if(c != 'A' && c!='a')
                {
                    db.GetPlayerByMail(player.PlayerMail).Points = player.Points;
                    return;
                }

            }
            
            
        }
        public void ShowProfile()//לין
        {
            
            Player player;
            player = db.Profile(this.player);
            if (player==null)
            {
                Console.WriteLine("Not loged in. Press any key to get back...");
                Console.ReadKey(true);
            }
            Console.WriteLine($"player mail: {player.PlayerMail} \nname: {player.Name}  \npassword: {player.Password}  \nlevel: {player.LevelCode}  \npoints: {player.Points}");
            char c = ' ';
            Console.WriteLine("Press B to change your mail, C/c to change your name, A/a to change you password, or any other key to continue");
            c = Console.ReadKey().KeyChar;
           while (c == 'B' || c == 'b')
            {

                Console.Write(" Please type your  new mail: ");
                string newMail = Console.ReadLine();
                while (!IsEmailValid(newMail))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Bad Email Format! Please try again:");
                    Console.ResetColor();
                    newMail = Console.ReadLine();
                }
                if(newMail==player.PlayerMail)
                { Console.WriteLine("Already your mail."); }
                player.PlayerMail = newMail;
                db.SaveChanges();
                Console.WriteLine("Press B to change your mail, C/c to change your name, A/a to change you password, or any other key to continue");
                c = Console.ReadKey().KeyChar;

            }

            while(c =='C' || c=='c')
            { 
                Console.Write(" Please type your new Name: ");
                string newName = Console.ReadLine();
                while (!IsNameValid(newName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Name must be at least 3 characters! Please try again: ");
                    Console.ResetColor();
                    newName = Console.ReadLine();
                }
                if (newName == player.Name) { Console.WriteLine("Already your name."); }
                player.Name=newName;
                db.SaveChanges();
                Console.WriteLine("Press B to change your mail, C/c to change your name, A/a to change you password, or any other key to continue");
                c = Console.ReadKey().KeyChar;

            }

            while(c=='A' || c=='a')
            {
                Console.Write(" Please type your new password: ");
                string newPassword = Console.ReadLine();
                while (!IsPasswordValid(newPassword))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Password must be at least 4 characters! Please try again: ");
                    Console.ResetColor();
                    newPassword = Console.ReadLine();
                }
                if (newPassword == player.Password) { Console.WriteLine("Already your password."); }
                player.Password=newPassword;
                db.SaveChanges();
                Console.WriteLine("Press B to change your mail, C/c to change your name, A/a to change you password, or any other key to continue");
                c = Console.ReadKey().KeyChar;
            }
            Console.ReadKey(true);

        }


        //Private helper methods down here...
        private void ClearScreenAndSetTitle(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{title,65}");
            Console.WriteLine();
            Console.ResetColor();   
        }

        private bool IsEmailValid(string emailAddress)
        {
            //regex is string based pattern to validate a text that follows a certain rules
            // see https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference

            var pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            var regex = new Regex(pattern);
            return regex.IsMatch(emailAddress);

        //another option is using .net System.Net.Mail library which has an EmailAddress class that stores email
        //we can use it to validate the structure of the email:
       // https://learn.microsoft.com/en-us/dotnet/api/system.net.mail.mailaddress?view=net-7.0
            /*
             * try
             * {
             *     //try to create MailAddress objcect from the email address string
             *      var email=new MailAddress(emailAddress);
             *      //if success
             *      return true;
             * }
             *      //if it throws a formatExcpetion then the string is not email format.
             * catch (Exception ex)
             * {
             * return false;
             * }
             */

        }



        private bool IsPasswordValid(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 3;
        }

        private bool IsNameValid(string name)
        {
            return !string.IsNullOrEmpty(name) && name.Length >= 3;
        }
    }
}

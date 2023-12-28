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
        Player player; //עצם מטיפוס player השחקן המחובר
        TriviaDBContext db = new TriviaDBContext(); //אובייקט מטיפוס TriviaDBContext שעושים עליו את הפעולות במחלקה זו בהמשך

        //Implememnt interface here
        public bool ShowLogin() //לין
        {
            Console.WriteLine("Enter name");
            string name = Console.ReadLine(); //קליטת שם

            Console.WriteLine("Enter mail"); 
            string mail = Console.ReadLine();   //קליטת מייל
            
            Console.WriteLine("Enter password");
            string password = Console.ReadLine();  //קליטת סיסמה


           
                player = db.Login(name,mail, password);   //השמת משתמש בעצם של המשתמש player שהפרטים שלו הם השם, מייל והסיסמה שהוכנסו
                                                          
            char c = ' ';
                while  (player == null)                                     //אם לא נמצא השחקן בעל השם,מייל והסיסמה יש לנסות שוב או לצאת
                {
                Console.WriteLine("Cannot be found. Press (B)ack to go back or any other key to log in again...");                                  
                c = Console.ReadKey().KeyChar;
                if (c.ToString().ToLower() != "b") //אם המשתמש רוצה לנסות להתחבר שוב
                {
                    Console.WriteLine("Enter name");
                    name = Console.ReadLine(); //קליטת שם מחדש
                    Console.WriteLine("Enter mail");
                    mail = Console.ReadLine(); //קליטת מייל מחדש
                    Console.WriteLine("Enter password");
                    password = Console.ReadLine(); //קליטת סיסמה מחדש
                    player = db.Login(name, mail, password);
                }
                else 
                    return false; //אם לא רוצה לנסות להתחבר שוב יציאה מהמסך
                }
                return true; //אם ההתחברות עברה מעבר למסך תפריט של שחקן מחובר
            
            
        }
        public bool ShowSignUp() //שירה
        {
            //Logout user if anyone is logged in!
            //A reference to the logged in user should be stored as a member variable
            //in this class! Example:
            player=null;    //אין שחקן מחובר

           

            //Loop through inputs until a user/player is created or 
            //user choose to go back to menu

            char c = ' ';       
            while (c.ToString().ToLower() != "b" && player == null) //כל עוד אין שחקן מחובר והמשתמש לא רוצה לצאת מהמסך
            {
                //Clear screen
                ClearScreenAndSetTitle("Signup");

                Console.Write("Please type your Name: ");
                string name = Console.ReadLine();              //קליטת שם
                while (!IsNameValid(name))                       //כל עוד השם לא תקין יש לנסות שוב
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Name must be at least 3 characters! Please try again: ");
                    Console.ResetColor();
                    name = Console.ReadLine();                 //קליטת שם חדש
                }

                Console.Write("Please type your mail: ");
                string mail = Console.ReadLine();           //קליטת מייל
                while (!IsEmailValid(mail))                 //כל עוד המייל לא תקין יש לנסות שוב
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Bad Email Format! Please try again:");    
                    Console.ResetColor();
                    mail = Console.ReadLine();                  //קליטת מייל חדש
                }

                Console.Write("Please type your password: ");
                string password = Console.ReadLine();           //קליטת סיסמה
                while (!IsPasswordValid(password))              //כל עוד הסיסמה לא תקינה יש לנסות שוב
                {
                    Console.ForegroundColor= ConsoleColor.Red;  
                    Console.Write("Password must be at least 4 characters! Please try again: ");
                    Console.ResetColor();   
                    password = Console.ReadLine();                //קליטת סיסמה מחדש
                }

                
               
                Console.ForegroundColor = ConsoleColor.DarkBlue;    
                Console.WriteLine("Connecting to Server...");
                Console.ResetColor();
                //Create instance of Business Logic and call the signup method
                // *For example:
                try
                {
                  player = db.SignUp(mail, password, name);     //יצירת משתמש חדש עם הפרטים שהוזנו ושמירה שלו בdatabase
                }
                catch (Exception ex)   //אם הפעולה של ההשמה עם SignUp נכשלה
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to signup! Email may already exist in DB!"); //הודעת שגיאה
                    Console.ResetColor();
                }


                if (player == null)   //אם הפעולה של ההרשמה SignUp נכשלה
                {
                    //Provide a proper message for example:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to signup! Email may already exist in DB!");    //הודעת שגיאה
                    Console.ResetColor();
                    Console.WriteLine("Press (B)ack to go back or any other key to signup again...");


                    //Get another input from user
                    c = Console.ReadKey().KeyChar; //קליטה של מקש מהמשתמש אם לא לחץ על יש הרשמה מחדש b 
                    if (c.ToString().ToLower() == "b") //אם כן לחץ על B אז יציאה מהמסך
                        return false; 
                }
            }
            //return true if signup suceeded!
            Console.WriteLine("Sign Up suceeded!");   //הודעה שההרשמה הצליחה
            return true; //מעבר למסך הבא


        }

        public void ShowAddQuestion() //שירי
        {
            
            if (player.Points == 100 || player.LevelCode==3)  //אם לשחקן יש 100 נקודות הוא יכול להוסיף שאלה
            {
                ClearScreenAndSetTitle("Add a Question");
                int subject; //יצירת משתנה נושא
                Console.WriteLine("Choose a subject: 1 - Sports, 2 - Politics, 3 - History, 4 - Science, 5 - Ramon HS");
                try
                {
                   subject = int.Parse(Console.ReadLine());     //קליטת נושא שאלה
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message); //הודעת שגיאה
                    Console.WriteLine("Does not exist.Please try again."); //הודעת שגיאה
                    Console.WriteLine("Choose a subject: 1 - Sports, 2 - Politics, 3 - History, 4 - Science, 5 - Ramon HS");
                    subject = int.Parse(Console.ReadLine());  //קליטת נושא מחדש
                }
               
                while (subject<1 || subject>5)  //כל עוד מספר הנושא לא תקין ולא קיים בטבלת נושאים יש לנסות שוב
                {
                    Console.WriteLine("Does not exist.Please try again.");
                    Console.WriteLine("Choose a subject: 1 - Sports, 2 - Politics, 3 - History, 4 - Science, 5 - Ramon HS");
                    subject = int.Parse(Console.ReadLine());  //קליטת נושא מחדש

                }
                Console.WriteLine("Add the question's text (if you do not want to add a question press b) ");
                string text = Console.ReadLine();   //קליטת השאלה
                if (text.ToLower() == "b")  //אם השחקן לוחץ על b או B כלומר הוא רוצה לצאת ולא להוסיף שאלה אז הפעולה תיפסק והוא יצא מהמסך
                {
                    return;
                }
                Question question = new Question(); //יצירת עצם מטיפוס Question
                question.Text = text;  //השאלה שנקלטה מהשחקן היא תכונת הטקסט בטיפוס מסוג שאלה שנוצר
               
                string correctAns;   //יצירת משתנה תשובה נכונה לשאלה
                string wrongAns1;     //יצירת משתנה תשובה שגויה לשאלה מספר 1   
                string wrongAns2;      //יצירת משתנה תשובה שגויה לשאלה מספר 2
                string wrongAns3;       //יצירת משתנה תשובה שגויה לשאלה מספר 3
                Console.WriteLine("Add the right answer");
                correctAns = Console.ReadLine();    //קליטת תשובה נכונה
                Console.WriteLine("add the first wrong answer");
                wrongAns1 = Console.ReadLine();     //קליטת תשובה שגויה לשאלה מספר 1
                Console.WriteLine("add the second wrong answer");
                wrongAns2 = Console.ReadLine();    // קליטת תשובה שגויה לשאלה מספר 2
                Console.WriteLine("add the third wrong answer");
                wrongAns3 = Console.ReadLine();     // קליטת תשובה שגויה לשאלה מספר 3


                db.AddQuestion(player.PlayerId,subject,text,correctAns,wrongAns1,wrongAns2,wrongAns3);  //קריאה לפעולה שיוצרת שאלה חדשה עם הפרטים שהוזנו שהסטטוס שלה הוא 3, בהמתנה
               
                player.Points = 0;  //איפוס הנקודות של השחקן
            }
            else  //אם לשחקן יש פחות מ100 נקודות והוא לא מנהל אז הוא לא יכול להוסיף שאלה
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You do not have permission to view this page");  //הודעה שאין גישה למסך זה
                Console.ResetColor();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true); //יציאה מהמסך למסך תפריט אחרי קליטת תו מהשחקן
            }
        }

        public void ShowPendingQuestions() //שירה
        {
            List<Question> q = new List<Question>();  //יצירת רשימה של אובייקטים מטיפוס שאלה
            q = db.PendingQuestion();     //מילוי התאים ברשימה בשאלות מטבלת השאלות שהסטטוס שלהן הוא 3, בהמתנה

            if (player.LevelCode == 2 || player.LevelCode == 3)  //אם השחקן הוא מנהל או מאסטר יש לו גישה למסך
            {
                if (q.Count == 0) //אם אין שאלות בהמתנה
                {
                    ClearScreenAndSetTitle("Pending Questions");
                    Console.WriteLine("No pending questions. Press any key to get back.");
                    char c = Console.ReadKey(true).KeyChar; //יציאה מהמסך
                    
                }
                

                char x = '5'; 
               
                for (int i = 0; i < q.Count; i++) //ריצה על רשימת השאלות בהמתנה
                {
                    ClearScreenAndSetTitle("Pending Questions");
                    Console.WriteLine($"Subject number:{q[i].SubjectCode} \nQuestion:{q[i].Text} \nCorrect Answer:{q[i].CorrectAns} \nWrong Answer 1:{q[i].WrongAns1} \nWrong Answer 2: {q[i].WrongAns2} \nWrong Answer3: {q[i].WrongAns3}"); //הדפסת פרטי השאלה
                    Console.WriteLine("Press 1 to approve, 2 to reject, 3 to skip, 4 to exit");
                    while (x == '5')
                    {
                        x = Console.ReadKey().KeyChar; //קליטת הדבר שהמנהל רוצה לעשות
                        if (x == '1')                   
                        {
                            q[i].StatusCode = 1;  //אישור השאלה
                        }
                        else if (x == '2')
                        {
                            q[i].StatusCode = 2;  //דחיית/פסילת השאלה
                        }
                        else if (x == '3')
                        {
                            q[i].StatusCode = 3;  //מעבר לשאלה הבאה, לא משנה את סטטוס השאלה הקודמת
                        }
                        else if (x == '4')
                        {
                            db.SaveChanges();      //יציאה מהמסך מבלי לשנות משהו נוסף
                            return;
                        }
                        else
                        {
                            x = '5';
                        }
                    }
                }
                db.SaveChanges();  //שמירת השינויים שנעשו במסך בdatabse
                
            }
            else //אם השחקן שמנסה להיכנס למסך הוא לא מנהל או מאסטר
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("You do not have permission to view this page");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true); //יציאה מהמסך לאחר קליטת תו
            }
        }
        public void ShowGame()//שירי
        {
            while(true) //יכנס תמיד ללולאה כל עוד השחקן רוצה להמשיך לשחק
            {
                ClearScreenAndSetTitle("Game");
                Question q = db.RandomQusetion(); //יצירת אובייקט מטיפוס שאלה והכנסה אליו שאלה רנדומלית מרשימת השאלות שאושרו
                List<string> answers = new List<string>(){q.CorrectAns,q.WrongAns1,q.WrongAns2,q.WrongAns3}; //יצירת רשימה של האופציות לתשובות של השאלה
                answers = answers.OrderBy(x => Random.Shared.Next()).ToList(); //שינוי הסדר של התשובות ברשימה
                Console.WriteLine(q.Text); //הדפסת השאלה
                Console.WriteLine($"1) {answers[0]}");  //הדפסה של האופציות לתשובה
                Console.WriteLine($"2) {answers[1]}");
                Console.WriteLine($"3) {answers[2]}");
                Console.WriteLine($"4) {answers[3]}");
                Console.WriteLine("Choose the correct answer by pressing its number.");
                int answer = 0; //הגדרת משתנה שהוא התשובה שהשחקן בוחר
                try
                {
                    answer = int.Parse(Console.ReadLine()); //קליטת הבחירה של התשובה מהשחקן
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                while (answer!=1 && answer!=2 && answer!=3 && answer!=4)  //אם ההזנה של התשובה לא תקינה, הודעת שגיאה
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Not an option. Please choose again.");
                    Console.ResetColor();
                    answer = int.Parse(Console.ReadLine());  //קליטה מחדש של התשובה של השחקן
                }
                if(answers[answer-1] == q.CorrectAns)  //אם השחקן בחר בתשובה הנכונה
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Correct! +10 points.");
                    player.Points += 10;  //הוספת 10 נקודות לשחקן
                    Console.ResetColor();
                    if(player.Points >100) //אם הנקודות של השחקן לאחר התוספת קטנה מ100 אז היא תהיה שווה ל100
                    {
                        player.Points = 100;
                    }
                }
                else //אם השחקן טעה
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Wrong! The correct answer is {q.CorrectAns}");
                    player.Points -= 5; //הורדת 5 נקודות מהנקודות של השחקן
                    if(player.Points < 0) //אם לאחר ההורדה מספר הנקודות של השחקן שלילי אז מספר הנקודות שלו יהיה שווה ל0
                    {
                        player.Points = 0;
                    }
                    Console.ResetColor();
                }
                Console.WriteLine("\n To play again press A/a else press any other key.");
                
                char c = Console.ReadKey().KeyChar; //קליטת מקש מהשחקן
                if(c != 'A' && c!='a') //אם השחקן לא לחץ על a או A  כלומר הוא לא רוצה להמשיך לשחק, שמירת השינויים ויציאה ממסך המשחק
                {
                    db.GetPlayerById(player.PlayerId).Points = player.Points; //שינוי כמות הנקודות של השחקן לאחר המשחק
                    db.SaveChanges(); //שמירת השינויים בdatabase
                    return; //עצירת פעולה כלומר חזרה למסך תפריט של שחקן מחובר
                }

            }
            
            
        }
        public void ShowProfile()//לין
        {
            
            
            player = db.Profile(this.player);   //השמה באובייקט השחקן (שנוצר בהתחלה) את השחקן שמחובר 
            if (player==null) //אם אין משתמש מחובר הודעה בהתאם
            {
                Console.WriteLine("Not loged in. Press any key to get back...");
                Console.ReadKey(true);
                return; //הפסקת הפעולה, כלומר יציאה מהמסך לאחר קליטת כל תו מהמשתמש
            }
           
                Console.WriteLine($"player mail: {player.PlayerMail} \nname: {player.Name}  \npassword: {player.Password}  \nlevel: {player.LevelCode}  \npoints: {player.Points}"); //הדפסת פרטי השחקן המחובר
            
           
            char c = ' ';
            Console.WriteLine("Press B/b to change your mail, C/c to change your name, A/a to change you password, or any other key to get back.");
            c = Console.ReadKey().KeyChar; //קליטה של תו מהשחקן
           while (c == 'B' || c == 'b') //אם השחקן רוצה לשנות את המייל שלו
            {

                Console.Write(" Please type your  new mail: ");
                string newMail = Console.ReadLine(); //קליטת מייל חדש מהשחקן
                while (!IsEmailValid(newMail)) //כל עוד המייל החדש לא תקין
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Bad Email Format! Please try again:"); 
                    Console.ResetColor();
                    newMail = Console.ReadLine(); //קליטת מייל חדש מהשחקן פעם נוספת
                }
                if(newMail==player.PlayerMail) //אם המייל החדש של השחקן זהה למייל שכבר יש לו, הדפסת הודעה בהתאם
                { Console.WriteLine("Already your mail."); }
                player.PlayerMail = newMail; //החלפת מייל השחקן למייל החדש שהכניס
                db.SaveChanges();  //שמירת שינויים בdatabase
                Console.WriteLine("Press B/b to change your mail, C/c to change your name, A/a to change you password, or any other key to get back.");
                c = Console.ReadKey().KeyChar; //קליטת תו מהשחקן

            }

            while(c =='C' || c=='c')  //אם השחקן רוצה לשנות את השם שלו
            { 
                Console.Write(" Please type your new Name: ");
                string newName = Console.ReadLine(); //קליטת שם חדש מהשחקן
                while (!IsNameValid(newName)) //כל עוד השם החדש לא תקין
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Name must be at least 3 characters! Please try again: ");
                    Console.ResetColor();
                    newName = Console.ReadLine();  //קליטת שם חדש מהשחקן פעם נוספת
                }
                if (newName == player.Name) { Console.WriteLine("Already your name."); } //אם השם החדש שהכניס השחקן זהה לשם שכבר יש לו, הודעה בהתאם
                player.Name=newName; //החלפת שם השחקן בשם החדש שהכניס
                db.SaveChanges(); //שמירת השינויים בdatabase
                Console.WriteLine("Press B/b to change your mail, C/c to change your name, A/a to change you password, or any other key to get back.");
                c = Console.ReadKey().KeyChar; //קליטת מקש מהשחקן

            }

            while(c=='A' || c=='a') //אם השחקן רוצה לשנות את הסיסמה שלו
            {
                Console.Write(" Please type your new password: ");
                string newPassword = Console.ReadLine();  //קליטת סיסמה חדשה מהשחקן
                while (!IsPasswordValid(newPassword)) //כל עוד הסיסמה החדשה לא תקינה
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Password must be at least 4 characters! Please try again: ");
                    Console.ResetColor();
                    newPassword = Console.ReadLine(); //קליטת סיסמה חדשה מהשחקן פעם נוספת
                }
                if (newPassword == player.Password) { Console.WriteLine("Already your password."); } //אם הסיסמה החדשה שהכניס השחקן זהה לזאת שכבר יש לו, הודעה בהתאם
                player.Password=newPassword; //החלפת הסיסמה של השחקן בסיסמה החדשה שהכניס
                db.SaveChanges(); //שמירת השינויים בdatabase
                Console.WriteLine("Press B/b to change your mail, C/c to change your name, A/a to change you password, or any other key to get back.");
                c = Console.ReadKey().KeyChar; //קליטת תו מהמשתנה
            }
            Console.ReadKey(true); //יציאה מהמסך לאחר קליטת תו אם השחקן החליט שהוא רוצה לצאת
            

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

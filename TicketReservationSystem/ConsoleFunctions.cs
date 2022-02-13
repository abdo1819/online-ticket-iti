using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservationSystem
{
    internal static class ConsoleFunctions
    {
        internal static void Signning_Out()
        {
            Console.WriteLine("Signning out...");
            Thread.Sleep(1500);
            Console.Clear();
        }
        internal static void Signning_In(User login)
        {
            Console.WriteLine("Signning In...");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine($"Hello, {login?.Name}");
            Thread.Sleep(1000);
            Console.Clear();
        }
        internal static void Get_Stations(out TrainStation Station_1, out TrainStation Station_2)
        {
            TrainStation temp1 = default, temp2 = default;

            string MyStation_2, MyStation_1;

            foreach (var station in DataBase.trainStations.Distinct())
            {
                Console.WriteLine(station.Address);
            }
            Console.WriteLine("\nPlease enter the start station from the above for your journey: ");
            do
            {
                MyStation_1 = Console.ReadLine();
                foreach(var station in DataBase.trainStations)
                {
                    if (station.Address == MyStation_1)
                        temp1 = station;
                }
            } while (MyStation_1 == null);
            Console.WriteLine("\nPlease enter the end station for your journey: ");
            do
            {
                MyStation_2 = Console.ReadLine();
                foreach (var station in DataBase.trainStations)
                {
                    if (station.Address == MyStation_2)
                        temp2 = station;
                }
            } while (MyStation_2 == null);

            Station_1 = temp1;
            Station_2 = temp2;

        }
        internal static string CheckPassword()
        {
            string EnteredVal = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work  
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    EnteredVal += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && EnteredVal.Length > 0)
                    {
                        EnteredVal = EnteredVal.Substring(0, (EnteredVal.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        if (string.IsNullOrWhiteSpace(EnteredVal))
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Empty value not allowed.");
                            CheckPassword();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("");
                            break;
                        }
                    }
                }
            } while (true);
            return EnteredVal;
        }
        internal static MobilWallet Create_Mobil_Wallet()
        {
            int Phone;
            string? Password;

            do
            {
                Console.Clear();
                Console.WriteLine("You don't have a mobil wallet");
                Console.WriteLine("Please add your phone number: ");
            } while (!int.TryParse(Console.ReadLine(), out Phone));

            do
            {
                Console.WriteLine("Please add your CVV number: ");
                Password = Console.ReadLine();

            } while (Password == null);

            MobilWallet mobile_wallet = new MobilWallet(Phone, Password);
            return mobile_wallet;
        }
        internal static bool HasMobilWallet(Passenger equalPasseneger)
        {
            bool found = false;
            foreach (var method in equalPasseneger.Payment_Methods)
            {
                if (method.GetType().Name == "MobilWallet")
                {
                    found = true;
                }
            }
            return found;
        }
        internal static Paypal Create_Paypal_Account()
        {
            string? Email, Password;
            do
            {
                Console.Clear();
                Console.WriteLine("You don't have a paypal account");
                Console.WriteLine("Please add your paypal account: ");
                Email = Console.ReadLine();
            } while (Email == null);

            do
            {
                Console.WriteLine("Please add your CVV number: ");
                Password = Console.ReadLine();

            } while (Password == null);

            Paypal paypal = new Paypal(Email, Password);
            return paypal;
        }
        internal static bool HasPaypal(Passenger equalPasseneger)
        {
            bool found = false;
            foreach (var method in equalPasseneger.Payment_Methods)
            {
                if (method.GetType().Name == "Paypal")
                {
                    found = true;
                }
            }
            return found;
        }
        internal static CreditCard Create_Credit_card()
        {
            int CreditCardNum, CVV;
            do
            {
                Console.Clear();
                Console.WriteLine("You don't have a credit card");
                Console.WriteLine("Please add your credit card number: ");
            } while (!int.TryParse(Console.ReadLine(), out CreditCardNum));

            do
            {
                Console.WriteLine("Please add your CVV number: ");
            } while (!int.TryParse(Console.ReadLine(), out CVV));

            CreditCard creditcard = new CreditCard(CreditCardNum, CVV);
            return creditcard;
        }
        internal static bool HasCredit(Passenger equalPasseneger)
        {
            bool found = false;
            foreach (var method in equalPasseneger.Payment_Methods)
            {
                if (method.GetType().Name == "CreditCard")
                {
                    found = true;
                }
            }
            return found;
        }
        internal static string Choose_Payment_Method()
        {
            string? Choice;
            Console.WriteLine($"\nPlease choose your payment method: ");
            do
            {
                Console.WriteLine("[Credit]  [Paypal]  [Mobile Wallet]\n");
                Choice = Console.ReadLine()?.ToLower();

            } while (Choice != "credit" && Choice != "paypal" && Choice != "vodafone cash" || Choice == null);
            return Choice;
        }
        internal static string Register_Or_Login()
        {
            string Choice;

            do
            {
                Console.Clear();
                Console.WriteLine("Please Choose: \n");
                Console.WriteLine("[Register]  [Login]\n");
                Choice = Console.ReadLine()?.ToLower();

            } while (Choice != "register" && Choice != "login" || Choice == null);
            return Choice;
        }
        internal static bool Sign_In(out User? login)
        {
            string? Name;
            string? Pass;

            do
            {
                Console.Clear();
                Console.WriteLine("\nPlease Enter your Name (Username): ");
                Name = Console.ReadLine();
            } while (Name == null);

            do
            {
                Console.WriteLine("\nPlease Enter your Password: ");
                Pass = ConsoleFunctions.CheckPassword();

            } while (Pass == null);


            foreach (var user in DataBase.Users)
            {
                if (user.Name == Name && user.Password == Pass)
                {
                    login = user;
                    return true;
                }
            }
            login = null;
            return false;
        }
        internal static User Register_New_User()
        {
            string? Name;
            string? Email;
            string? Pass;
            string? Address;

            int ID;
            int Phone;

            do
            {
                Console.Clear();
                Console.WriteLine("Please Enter your Name (Username): ");
                Name = Console.ReadLine();
            } while (Name == null);

            do
            {
                Console.WriteLine("\nPlease Enter your National ID: ");
            } while (!int.TryParse(Console.ReadLine(), out ID));

            do
            {
                Console.WriteLine("\nPlease Enter your Phone Number: ");
            } while (!int.TryParse(Console.ReadLine(), out Phone));

            do
            {
                Console.WriteLine("\nPlease Enter your Email: ");
                Email = Console.ReadLine();

            } while (Email == null);

            do
            {
                Console.WriteLine("\nPlease Enter your Password: ");
                Pass = CheckPassword();

            } while (Pass == null);

            do
            {
                Console.WriteLine("\nPlease Enter your Address: ");
                Address = Console.ReadLine();

            } while (Address == null);


            User passenger = new Passenger(ID, Name, Phone, Email, Pass, Address);
            return passenger;
        }
        internal static bool Deleting_Train(Admin admin)
        {
            int id;
            do
            {
                Console.WriteLine("\nPlease Enter the ID of the train to delete: ");
            } while (!int.TryParse(Console.ReadLine(), out id));

            Console.Clear();
            Console.WriteLine("\nProcessing...");
            Thread.Sleep(1000);
            Console.Clear();

            foreach (var trn in DataBase.trains)
            {
                if (trn.ID == id)
                {
                    return admin.RemoveTrain(trn);
                }
            }
            return false;
        }
        internal static bool Adding_Train(Admin admin)
        {
            int id, hr;
            do
            {
                Console.WriteLine("\nPlease Enter the ID of the train to add: ");
            } while (!int.TryParse(Console.ReadLine(), out id));
            do
            {
                Console.WriteLine("\nPlease Enter the hour of departure: ");
            } while (!int.TryParse(Console.ReadLine(), out hr));
            
            Console.Clear();
            Console.WriteLine("\nProcessing...");
            Thread.Sleep(1000);
            Console.Clear();
            List<TrainStation> stations = new()
            {
                new TrainStation("Cairo", 30.0, 31.2),
                new TrainStation("Giza", 29.98, 31.2),
                new TrainStation("El-Fayoum", 29.3, 30.8),
                new TrainStation("El-Minya", 28.08, 30.75)
            };

            TimeSpan t = TimeSpan.FromHours(hr);
            return admin.AddTrain(id, 100, 100, 60, stations, t);
        }
        internal static bool Deleting_User(Admin admin)
        {
            int id;
            do
            {
                Console.WriteLine("\nPlease Enter the user ID to delete: ");
            } while (!int.TryParse(Console.ReadLine(), out id));

            Console.Clear();
            Console.WriteLine("\nProcessing...");
            Thread.Sleep(1000);
            Console.Clear();

            foreach (var u in DataBase.Users)
            {
                if (u.NationalID == id)
                {
                    return admin.RemoveUser(u);
                }
            }
            return false;
        }
        internal static int Take_Action()
        {
            int action;
            Console.Clear();
            Console.WriteLine("you, as an admin, is previliged to do the following:\n");
            Console.WriteLine("Delete User (1)");
            Console.WriteLine("Add Train (2)");
            Console.WriteLine("Remove Train (3)");
            Console.WriteLine("Sign Out (0)");
            do
            {
                Console.WriteLine("\nPlease choose the action: ");
            } while (!int.TryParse(Console.ReadLine(), out action));
            
            Console.Clear();
            return action;
        }

    }
}

global using System.Device.Location;
using static TicketReservationSystem.ConsoleFunctions;


namespace TicketReservationSystem
{
    class Program
    {
        public static void Main()
        {
           
            while (true)
            {
                // creating the admin user
                var User = new Admin(120, "admin", 23454, "admin@D.com", "admin", "nothing");
                DataBase.Users.Add(User);
                
                while(true)
                {
                    string Choice = Register_Or_Login();
                    if (Choice == "register")
                    {
                        User passenger = Register_New_User();
                        DataBase.Users.Add(passenger);
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        User? login;

                        if (!Sign_In(out login))
                        {
                            Console.WriteLine("WRONG USERNAME OR PASSWORD");
                            Thread.Sleep(1500);
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            Signning_In(login);

                            if (login?.Name == "admin" && login?.Password == "admin")
                            {
                                while (true)
                                {
                                    Thread.Sleep(1000);
                                    int action = Take_Action();
                                    var admin = (Admin)login;

                                    if(action == 0)
                                    {
                                        Signning_Out();
                                        break;
                                    }

                                    switch (action)
                                    {
                                        case 1:

                                            if (Deleting_User(admin))
                                                Console.WriteLine("\nDeleted\n");
                                            else
                                                Console.Write("\nNO USER WITH THIS ID\n");

                                            break;

                                        case 2:

                                            if (Adding_Train(admin))
                                                Console.WriteLine("\nAdded\n");
                                            else
                                                Console.Write("\nTHIS TRAIN ALREADY EXISTS\n");

                                            break;

                                        case 3:

                                            if (Deleting_Train(admin))
                                                Console.WriteLine("\nDeleted\n");
                                            else
                                                Console.Write("\nNO TRAIN WITH THIS ID\n");
                                            
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                string? Station_1, Station_2;
                                bool available = false;
                                int trip, trips = 0;

                                Get_Stations(out Station_1, out Station_2);

                                foreach (Train? train in Train.GetAvailableTrains(Station_1, Station_2))
                                {
                                    trips++;
                                    Console.WriteLine(train);
                                    available = true;
                                }

                                if(available)
                                {
                                    for(int i = 1; i <= trips; i++)
                                        Console.WriteLine(i);

                                    do
                                    {
                                        Console.WriteLine("\nplease pick the number of your suitable journey:");
                                    } while (!int.TryParse(Console.ReadLine(), out trip));
                                    

                                    Choice = Choose_Payment_Method();
                                    Passenger equalPasseneger = (Passenger)login;

                                    if (Choice == "credit")
                                    {
                                        bool found = HasCredit(equalPasseneger);

                                        if (!found)
                                        {
                                            CreditCard creditcard = Create_Credit_card();
                                            equalPasseneger.Payment_Methods.Add(creditcard);
                                        }
                                    }
                                    else if (Choice == "paypal")
                                    {
                                        bool found = HasPaypal(equalPasseneger);

                                        if (!found)
                                        {
                                            Paypal paypal_account = Create_Paypal_Account();
                                            equalPasseneger.Payment_Methods.Add(paypal_account);
                                        }
                                    }
                                    else
                                    {
                                        bool found = HasMobilWallet(equalPasseneger);

                                        if (!found)
                                        {
                                            MobilWallet mobil_wallet = Create_Mobil_Wallet();
                                            equalPasseneger.Payment_Methods.Add(mobil_wallet);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\nProcessing...");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                    Console.WriteLine("\nunfortunately, there is no available journeys");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                }
                            }
                        }
                    }
                }
            }            
        }
    }
}
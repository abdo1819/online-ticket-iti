global using System.Device.Location;
using static TicketReservationSystem.ConsoleFunctions;


namespace TicketReservationSystem
{
    class Program
    {
        public static void Main()
        {
            
            populateDB();

            // creating the admin user
            var User = new Admin(120, "admin", 23454, "admin@D.com", "admin", "nothing");
            DataBase.Users.Add(User);
   
            while (true)
            {
                while (true)
                {
                    #region Login
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
                    #endregion
                    #region admin
                            if (login?.Name == "admin" && login?.Password == "admin")
                            {
                                while (true)
                                {
                                    Thread.Sleep(1000);
                                    int action = Take_Action();
                                    var admin = (Admin)login;

                                    if (action == 0)
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
                            #endregion
                    #region passenger
                            else
                            {
                            #region select_station_tier
                                TrainStation? Station_1, Station_2;
                                bool available = false;
                                int trip, trips = 0;

                                Console.Clear();
                                Get_Stations(out Station_1, out Station_2);
                                Console.Clear();
                                Console.WriteLine("Available Journeys for your search: \n");
                                var availTrains = Train.GetAvailableTrains(Station_1, Station_2);
                                foreach (Train? train in availTrains)
                                {
                                    Console.WriteLine($"####### [ {++trips} ] ######");
                                    Console.WriteLine(train);
                                    train.GetPrice(Station_1, Station_2, out decimal First, out decimal Second);
                                    Console.WriteLine($"First tier price: {(int)First} EGP");
                                    Console.WriteLine($"Second tier price: {(int)Second} EGP");
                                    available = true;
                                }
                                #endregion
                                #region payment
                                if (available)
                                {
                                    do
                                    {
                                        Console.WriteLine("\nplease pick the number of your suitable journey:");
                                    } while (!int.TryParse(Console.ReadLine(), out trip) && trip <= availTrains.Count && trip > 0);

                                    var chosenTrip = availTrains[trip - 1]; // Chosen train for passenger
                                    var availFirstSeats = new List<Seat>();
                                    var availSecondSeats = new List<Seat>();
                                    int choice = 0;
                                    chosenTrip.AvailableSeats(out availFirstSeats, out availSecondSeats);
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine($"The available number of first class seats is {availFirstSeats.Count}");
                                        Console.WriteLine($"The available number of second class seats is {availSecondSeats.Count}");
                                        Console.WriteLine("\nplease pick the seat tier (1 for First class), (2 for Second Class):");
                                    } while (!int.TryParse(Console.ReadLine(), out choice) && choice != 1 && choice != 2);
                                 
                                    IPaymentMethod Chosen_Method = null;
                                    Choice = Choose_Payment_Method();
                                    Passenger equalPasseneger = (Passenger)login;

                                    if (Choice == "credit")
                                    {
                                        bool found = HasCredit(equalPasseneger);

                                        foreach (var method in equalPasseneger.Payment_Methods)
                                        {
                                            if (method.GetType().Name == "CreditCard")
                                                Chosen_Method = method;
                                        }

                                        if (!found)
                                        {
                                            CreditCard creditcard = Create_Credit_card();
                                            equalPasseneger.Payment_Methods.Add(creditcard);
                                            Chosen_Method = creditcard;
                                        }

                                    }
                                    else if (Choice == "paypal")
                                    {
                                        bool found = HasPaypal(equalPasseneger);

                                        foreach (var method in equalPasseneger.Payment_Methods)
                                        {
                                            if (method.GetType().Name == "Paypal")
                                                Chosen_Method = method;
                                        }
                                        if (!found)
                                        {
                                            Paypal paypal_account = Create_Paypal_Account();
                                            equalPasseneger.Payment_Methods.Add(paypal_account);
                                            Chosen_Method = paypal_account;
                                        }
                                    }
                                    else
                                    {
                                        bool found = HasMobilWallet(equalPasseneger);
                                        foreach (var method in equalPasseneger.Payment_Methods)
                                        {
                                            if (method.GetType().Name == "MobilWallet")
                                                Chosen_Method = method;
                                        }

                                        if (!found)
                                        {
                                            MobilWallet mobil_wallet = Create_Mobil_Wallet();
                                            equalPasseneger.Payment_Methods.Add(mobil_wallet);
                                            Chosen_Method = mobil_wallet;
                                        }
                                    }
                                    // Buying ticket
                                    Ticket passTicket = null;
                                    string confirm = null;
                                    do
                                    {
                                        Console.WriteLine($"Please confirm your purchase "+
                                            $"Press Y to confirm or N to abort"); 
                                        confirm = Console.ReadLine();
                                    }
                                    while (confirm.ToLower() != "y" && confirm.ToLower() != "n" && confirm != null);
                                    if (confirm.ToLower() == "y")
                                    {
                                        passTicket = equalPasseneger.buy(chosenTrip, choice, Station_1, Station_2, Chosen_Method);
                                        equalPasseneger.PassengerTickets.Add(passTicket);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Payment cancelled");
                                    }

                                    if (passTicket != null)
                                        Console.WriteLine(passTicket);
                                    Console.ReadLine();
                                }
                                #endregion
                                #region failed_selection
                                else
                                {
                                    Console.WriteLine("\nProcessing...");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                    Console.WriteLine("\nunfortunately, there is no available journeys");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }
                }
            }
        }
    }
}
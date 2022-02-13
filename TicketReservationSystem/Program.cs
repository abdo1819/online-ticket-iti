﻿global using System.Device.Location;
using static TicketReservationSystem.ConsoleFunctions;


namespace TicketReservationSystem
{
    class Program
    {
        public static void Main()
        {
            ////////////////// Remove This Later ////////////////////////////////
            var User2 = new Passenger(120, "fathy", 23454, "admin@D.com", "123", "nothing");
            DataBase.Users.Add(User2);
            List<TrainStation> stations = new()
            {
                new TrainStation("Cairo", 30.0, 31.2),
                new TrainStation("Giza", 29.98, 31.2),
                new TrainStation("El-Fayoum", 29.3, 30.8),
                new TrainStation("El-Minya", 28.08, 30.75)
            };
            DataBase.trains.Add(new Train(100, 20, 20, 40, stations, new TimeSpan(2, 0, 0)));
            ////////////////////////////////////////////////////////////////////////////
            
            // creating the admin user
            var User = new Admin(120, "admin", 23454, "admin@D.com", "admin", "nothing");
            DataBase.Users.Add(User);

            while (true)
            {         
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
                                TrainStation? Station_1, Station_2;
                                bool available = false;
                                int trip, trips = 0;

                                Console.Clear();
                                Get_Stations(out Station_1, out Station_2);                         
                                Console.WriteLine("Available Journeys for your search: \n");
                                var availTrains = Train.GetAvailableTrains(Station_1, Station_2);
                                foreach (Train? train in availTrains )
                                {
                                    Console.WriteLine($"####### [ {++trips} ] ######");
                                    Console.WriteLine(train);
                                    train.GetPrice(Station_1, Station_2, out decimal First, out decimal Second);
                                    Console.WriteLine($"First tier price: {(int)First} EGP");
                                    Console.WriteLine($"Second tier price: {(int)Second} EGP");
                                    available = true;
                                }

                                if(available)
                                {
                                    do
                                    {
                                        Console.WriteLine("\nplease pick the number of your suitable journey:");
                                    } while (!int.TryParse(Console.ReadLine(), out trip) && trip <= availTrains.Count && trip > 0 );

                                    var chosenTrip = availTrains[trip-1]; // Chosen train for passenger
                                    var availFirstSeats = new List<Seat>();
                                    var availSecondSeats = new List<Seat>();
                                    int choice = 0;
                                    chosenTrip.AvailableSeats(out availFirstSeats,out availSecondSeats);
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine($"The available number of first class seats is {availFirstSeats.Count}");
                                        Console.WriteLine($"The available number of second class seats is {availSecondSeats.Count}");
                                        Console.WriteLine("\nplease pick the seat tier (1 for First class), (2 for Second Class):");
                                    } while (!int.TryParse(Console.ReadLine(), out choice) && choice != 1 && choice != 2);

                                    var chosenSeat = chosenTrip.findSeat(choice);
                                    //Create object of journey

                                    var userJourney = new Journey((User2.NationalID + 100), chosenTrip.DepartureTime,chosenTrip, Station_1, Station_2, chosenSeat);


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
                                    // Buying ticket
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
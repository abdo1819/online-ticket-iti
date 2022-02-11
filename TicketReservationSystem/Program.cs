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
                
                
                string? Choice = Register_Or_Login();

                if (Choice == "register")
                {
                    User passenger = Register_New_User();
                    DataBase.Users.Add(passenger);
                    Console.Clear();
                    Console.WriteLine($"Hello, {passenger.Name}");
                }
                else
                {
                    User? login = default;

                    if (Sign_In(login))
                        Console.WriteLine($"Hello, {login?.Name}");
                    else
                        Console.WriteLine("WRONG USERNAME OR PASSWORD");

                    
                    if (login?.Name == "admin" && login?.Password == "admin")
                    {
                        while(true)
                        {
                            int action = Take_Action();
                            var admin = (Admin)login;

                            switch (action)
                            {
                                case 1:

                                    if (Deleting_User(admin))
                                        Console.WriteLine("Deleted");
                                    else
                                        Console.Write("no user with this id");

                                    break;

                                case 2:

                                    if (Adding_Train(admin))
                                        Console.WriteLine("Added");
                                    else
                                        Console.Write("This train already exists");

                                    break;

                                case 3:

                                    if (Deleting_Train(admin))
                                        Console.WriteLine("Deleted");
                                    else
                                        Console.Write("no train with this id");

                                    break;
                                
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
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
                }
            }            
        }
    }
}
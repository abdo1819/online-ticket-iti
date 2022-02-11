
global using System.Device.Location;
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

                string? Choice;
                string? Name;
                string? Email;
                string? Pass;
                string? Address;

                int ID;
                int Phone;

                do
                {
                    Console.Clear();
                    Console.WriteLine("Please Choose: \n");
                    Console.WriteLine("[Register]  [Login]\n");
                    Choice = Console.ReadLine()?.ToLower();

                } while (Choice != "register" && Choice != "login" || Choice == null);

                if (Choice == "register")
                {
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
                        Pass = ConsoleFunctions.CheckPassword();

                    } while (Pass == null);

                    do
                    {
                        Console.WriteLine("\nPlease Enter your Address: ");
                        Address = Console.ReadLine();

                    } while (Address == null);


                    User passenger = new Passenger(ID, Name, Phone, Email, Pass, Address);
                    DataBase.Users.Add(passenger);
                    Console.Clear();
                    Console.WriteLine($"Hello, {passenger.Name}");
                    Console.WriteLine($"\nPlease choose your pick-up station: ");
                    Console.ReadLine();
                }
                else
                {
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

                    if(Name == "admin" && Pass == "admin")
                    {
                        Console.Clear();
                        Console.WriteLine("You logged in as an admin");
                    }
                    else
                    {
                        User passenger;
                        bool Found = false;
                        foreach (var user in DataBase.Users)
                        {
                            if(user.Name == Name && user.Password == Pass)
                            {
                                Found = true;
                                Console.Clear();
                                Console.WriteLine($"Hello, {user.Name}");
                                Console.WriteLine($"\nPlease choose your pick-up station: ");
                                passenger = user;
                            }
                        }
                        if (!Found)
                        {
                            Console.Clear();
                            Console.WriteLine("WRONG USERNAME OR PASSWORD...");
                        }
                    }
                    Console.ReadLine();
                }
            }
        }
    }
}
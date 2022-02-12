using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservationSystem
{
    internal class User
    {
        public User(int _NationalID, string _Name, int _Phone, string _Email, string _Password, string _Address)
        {
            NationalID = _NationalID;
            Name = _Name;
            Phone = _Phone;
            Email = _Email;
            Password = _Password;
            Address = _Address;
        }

        public int NationalID { get; }
        public string Name { get; }
        public int Phone { get; set; }
        public string Email { get; set; } 
        public string Password { get; }
        public string Address {get; set;}

    }

    internal class Passenger : User
    {
        public Passenger(int _NationalID, string _Name, int _Phone, string _Email, string _Password, string _Address):base(_NationalID, _Name, _Phone, _Email, _Password, _Address)
        {
            Payment_Methods = new List<IPaymentMethod>();
        }

        public List<IPaymentMethod> Payment_Methods;

        public override bool Equals(Object obj)
        {
            User? RightUser = obj as User;
            if (RightUser == null) return false;
            if (Object.ReferenceEquals(RightUser, this)) return true;

            return this.NationalID == RightUser.NationalID;

        }

    }

    internal class Admin : User
    {
        public Admin(int _NationalID, string _Name, int _Phone, string _Email, string _Password, string _Address) : base(_NationalID, _Name, _Phone, _Email, _Password, _Address)
        {
        }

        public bool AddTrain(int _id, int no_first_class, int no_second_class, double avg_speed, List<TrainStation> _stops, TimeSpan _departureTime)
        {

            Train train = new Train(_id, no_first_class, no_second_class, avg_speed, _stops, _departureTime);
            return train.AddToService();
        }

        public bool RemoveTrain(Train train)
        {
            return train.RemoveFromService();
        }

        public bool RemoveUser(User user)
        {
            foreach (var u in DataBase.Users)
            {
                if (u.Equals(user))
                {
                    DataBase.Users.Remove(user);
                    return true;
                }
            }
            Console.WriteLine("\nThis User doesn't exists");
            return false;
        }
    }
}

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

        List<IPaymentMethod> Payment_Methods;

    }

    internal class Admin : User
    {
        public Admin(int _NationalID, string _Name, int _Phone, string _Email, string _Password, string _Address) : base(_NationalID, _Name, _Phone, _Email, _Password, _Address)
        {
        }

    }

}

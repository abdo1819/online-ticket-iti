using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TicketReservationSystem.ConsoleFunctions;

namespace TicketReservationSystem

{
    internal interface IPaymentMethod
    {
        bool ProcessPayment(decimal amount);
        bool Refund(Ticket ticket);
    }

    internal class Paypal : IPaymentMethod
    {
        public Paypal(string _Email, string _Password)
        {
            Email = _Email;
            Password = _Password;
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine("Proccessing...");
            Console.WriteLine($"Withdrawing {amount}...");
            Console.WriteLine("done");
            return true;
        }
        public bool Refund(Ticket ticket)
        {
            return true;
        }
    }

    internal class CreditCard : IPaymentMethod
    {
        public CreditCard(int _Number, int _CVV)
        {
            Number = _Number;
            CVV = _CVV;
        }
        public int Number { get; set; }
        public int CVV{ get; set; }

        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine("Proccessing...");
            Console.WriteLine($"Withdrawing {amount}...");
            Console.WriteLine("done");
            return true;
        }
        public bool Refund(Ticket ticket)
        {
            return true;
        }
    }

    internal class MobilWallet : IPaymentMethod
    {
        public MobilWallet(int _Phone, string _Password)
        {
            Phone = _Phone;
            Password = _Password;
        }

        public int Phone { get; set; }
        public String Password { get; set; }
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine("Proccessing...");
            Console.WriteLine($"Withdrawing {amount}...");
            Console.WriteLine("done");
            return true;
        }
        public bool Refund(Ticket ticket)
        {
            return true;
        }
    }
}

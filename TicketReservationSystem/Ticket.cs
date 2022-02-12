using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservationSystem
{
    internal class Ticket
    {
        public string ID { get; set; }
        public decimal Price { get; set; }
        public Passenger Owner { get; set; }
        public DateTime PurchaseDate { get; set; }

        // TODO: Remove seat from ticket, add it to journey
        public IPaymentMethod PaymentMethod { get; set; }
        public Journey TJourney { get; set; } // TODO:Change name in UML

        public Ticket(string _ID, decimal _Price, Passenger _Owner, DateTime _PurchaseDate, IPaymentMethod _Method, Journey _TJourney)
        {
            ID = _ID;
            Price = _Price;
            Owner = _Owner;
            PurchaseDate = _PurchaseDate;
            PaymentMethod = _Method;
            TJourney = _TJourney;
        }
        public override string ToString()
        {
            return $"Ticket ID:{ID}" +
                $"Ticket Price:{Price}" +
                $"Ticket Owner's National ID:{Owner.NationalID}" +
                $"Ticket Purchase Date: {PurchaseDate.ToString("yyyy/MM/dd hh:mm:ss")}" +
                $"Ticket Payment method: {PaymentMethod}" +
                $"Train: {TJourney.JTrain.ID}" +
                $"Seat: {TJourney.Seat.Number}";
        }


    }
}

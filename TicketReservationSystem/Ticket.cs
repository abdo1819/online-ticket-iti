using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservationSystem
//namespace RAILWAY_RES_SYS
{
    internal class Ticket
    {
        private string id;
        private decimal price;
        //private Passenger owner;
        private DateTime purchaseDate=DateTime.Now;
        //DateTime dateTime = DateTime.UtcNow.Date;
        private Seat seat;
        //private IPaymentMethod? paymentMethod;
        private Journey? journey;

        public string Id { get; set; }
        public decimal Price { get; set; }
        //public DateTime PurchaseDate { get; }

        public Ticket(string _id,decimal _price,Journey _journey,Seat _seat)
        {
            Id = _id;
            Price = _price;
            journey = _journey;
            seat = _seat;
        }
        public override string ToString()
        {
            return
                $" ticket#:\t {Id} \n" +
                $" journey:\t {journey.Name} \n"+
                $" price:\t\t {Price} L.E \n" +
                $" your seat:\t {seat.Number} \n"+
                $" purchaseDate:\t {purchaseDate.ToString("yyyy/MM/dd hh:mm:ss")} \n";

        }
    }
}

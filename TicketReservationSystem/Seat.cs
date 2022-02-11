using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservationSystem
{
    public abstract class Seat
    {
        int number;
        Tier tier;
        bool reservation_state;
        public Seat(int _number, Tier _tier , bool _reserved = false)
        {
            number = _number;
            tier = _tier;
            reservation_state = _reserved;
        }

        public int Number { get { return number; } }
        public Tier Tier { get { return tier; } }
        public bool Reservation_state { get { return reservation_state; } set { this.reservation_state = value; } }
        
        public override string ToString()
        {
            return $"SeatNo: {this.Number}\n" +
                $"{this.Tier}\n" +
                $"Reserved?: {reservation_state}\n";
        }

    }

    class FirstClassSeat : Seat
    {
        public FirstClassSeat(int _number) : base(_number, new(TierType.First, 10), false)
        {
        }
    }

    class SecondClassSeat : Seat
    {
        public SecondClassSeat(int _number) : base(_number, new(TierType.Second, 7), false)
        {
        }
    }
}

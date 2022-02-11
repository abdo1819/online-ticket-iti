using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservationSystem
{
    public class Tier
    {
        public TierType Type { get; set; }
        public decimal UnitPrice { get; set; }

        public Tier(TierType _type=TierType.NA, decimal _unitPrice=0)
        {
            Type = _type;
            UnitPrice = _unitPrice;
        }

        public override string ToString()
        {
            return $"Class: {this.Type}";
        }
    }
    public enum TierType:byte
    { NA=0, First=1 , Second=2}
}

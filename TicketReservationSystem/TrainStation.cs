using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservationSystem
{
    public class TrainStation
    {
        public Coordinates Coordinates { get; set; }
        public string Address { get; set; }

        public TrainStation(string address, double _lat, double _long)
        {
            Address = address;
            Coordinates = new Coordinates(_lat, _long);
        }

        public override string ToString()
        {
            return $"Staion Address: {this.Address}\n" +
                $"Lat: {this.Coordinates.Latitude}\n" +
                $"Long: {this.Coordinates.Longitude}\n";
        }

        public static double DistanceBetween(TrainStation T1, TrainStation T2)
        {
            GeoCoordinate g1 = new(T1.Coordinates.Longitude, T1.Coordinates.Latitude);
            GeoCoordinate g2 = new(T2.Coordinates.Longitude, T2.Coordinates.Latitude);
            return g1.GetDistanceTo(g2);

        }

    }
}

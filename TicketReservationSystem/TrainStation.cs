using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservationSystem
{
    public class TrainStation: IEqualityComparer<TrainStation>
    {
        public Coordinates Coordinates { get; set; }
        public string Address { get; set; }
        public TrainStation(string address, double _lat, double _long)
        {
            Address = address;
            Coordinates = new Coordinates(_lat, _long);
            // DataBase.trainStations.Add(this);
        }
        public override string ToString()
        {
            return $"Station Address: {this.Address}\n" +
                $"Lat: {this.Coordinates.Latitude}\n" +
                $"Long: {this.Coordinates.Longitude}\n";
        }
        public static double DistanceBetween(TrainStation T1, TrainStation T2)
        {
            GeoCoordinate g1 = new(T1.Coordinates.Longitude, T1.Coordinates.Latitude);
            GeoCoordinate g2 = new(T2.Coordinates.Longitude, T2.Coordinates.Latitude);
            return g1.GetDistanceTo(g2);
        }

        public override bool Equals(object obj)
        {
            if (obj is TrainStation L)
                return this.Address.ToLower() == L.Address.ToLower();
            else
                return false;
        }

        public override int GetHashCode()
        {
            return this.Address.GetHashCode();
        }

        public bool Equals(TrainStation x, TrainStation y)
        {
            return x.Address.ToLower() == y.Address.ToLower();
        }

        public int GetHashCode([DisallowNull] TrainStation obj)
        {
            return obj.Address.GetHashCode();
        }
        public static bool AddStation(TrainStation station)
        {
            if (DataBase.trainStations.Contains(station) == false)
            {
                DataBase.trainStations.Add(station);
                return true;
            }             
            else
                return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;


namespace TicketReservationSystem
{
    public class Coordinates
    {
        double latitude;
        double longitude;
        public Coordinates(double _lat = 0, double _long =0)
        {
            latitude = _lat;
            longitude = _long;
        }
        public double Latitude { 
            get { return latitude; }
            set { latitude = value; }
        }
        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
    }
}

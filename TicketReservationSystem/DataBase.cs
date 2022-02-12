using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservationSystem
{
    internal static class DataBase
    {
        
        public static List<User> Users = new List<User>();
        public static List<Journey> journeys = new List<Journey>();
        public static List<Ticket> tickets = new List<Ticket>();
        public static List<Passenger> passengers = new List<Passenger>();
        public static List<Train> trains = new List<Train>();
        public static List<TrainStation> trainStations = new List<TrainStation>();

    }
}

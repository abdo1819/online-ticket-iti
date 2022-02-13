using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservationSystem
{
    internal class Journey
    {
        // TODO: Remove station property in UML
        public int ID { get; set; }  // TODO: Change name to ID in UML, name doesn't make sense
        public TimeSpan DepartureTime { get; set; } // TODO:Change in the UML
        public Train JTrain { get; set; }  // TODO:Change name of property in UML
        public TrainStation StartStation { get; set; }
        public TrainStation EndStation { get; set; }
        public Seat Seat { get; set; } // TODO: Add this to class diagram

        public Journey(int _ID, TimeSpan _DepartureTime, Train _Train, TrainStation _Start, TrainStation _End, Seat _Seat)
        {
            ID = _ID;
            DepartureTime = _DepartureTime;
            JTrain = _Train;
            StartStation = _Start;
            EndStation = _End;
            Seat = _Seat;
        }

        public decimal getPrice(int tier)
        {
            JTrain.GetPrice(StartStation, EndStation, out decimal t1_price, out decimal t2_price);
            return tier == 1 ? t1_price : t2_price;
        }

        public TimeSpan getEstimateArrivalTime() // TODO: Make this return TimeSpan in UML
        {
            TimeSpan arrivalTime = TimeSpan.Zero;
            TimeSpan _departureTime = JTrain.GetTrainDepartureTime(StartStation);
            int dIndex = JTrain.Stops.IndexOf(StartStation);
            int aIndex = JTrain.Stops.IndexOf(EndStation);
            double _totalDistance;
            if (dIndex > -1 && aIndex > dIndex)
            {
                _totalDistance = 0;
                for (int i = dIndex; i < aIndex; i++)
                {
                    _totalDistance += TrainStation.DistanceBetween(JTrain.Stops[i], JTrain.Stops[i + 1]);
                }
                TimeSpan duration = TimeSpan.FromHours(0.001 * _totalDistance / JTrain.AverageSpeed);

                arrivalTime = _departureTime + duration;
            }

            return arrivalTime;
        }

        //public List<Seat> AvailableSeats() // TODO:Journey is specific to passenger, this method should only be in train?
        //{
        //    var seats = Train.AvailableSeats();
        //    return seats;
        //}

        //public Dictionary<Journey,Tier> getAvailableJourneys(string _departure, string _arrival)
        //{
        //    var Journeys = new Dictionary<Journey, Tier>();
        //    var availTrains = Train.GetAvailableTrains(_departure, _arrival);
        //    foreach (var train in availTrains)
        //    {

        //    }
        //}

        public bool AddJourney() // TODO: Train method is not static, but this one is static in UML, which do we follow?
        {
            if (!DataBase.journeys.Contains(this))
            {
                DataBase.journeys.Add(this);
                return true;
            }
            else
                return false;
        }

        public override string ToString()
        {
            return $"Journey ID:{ID}" +
                $"Journey Date: {DepartureTime}" +
                $"Journey Train{JTrain.ID}" +
                $"Journey Starts at: {StartStation.Address}" +
                $"Journey Ends at: {EndStation.Address}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservationSystem
//namespace RAILWAY_RES_SYS
{
    internal class Journey
    {
        //private TrainStation station;
        private string name;
        private DateTime date;
        private Train train;
        private TrainStation startStation;
        private TrainStation endStation;

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Journey(string _name,DateTime _date,Train _train,TrainStation _startStation, TrainStation _endStation)
        {
            Name = _name;
            Date = _date;
            train = _train;
            startStation = _startStation;
            endStation = _endStation;
        }
        public static bool addJourney(Journey j)
        {
            if (DataBase.journeys.Contains(j))
            {
                return false;
            }
            else
            {
                DataBase.journeys.Add(j);
                return true;
            }
        }
        public double getDistance()
        {
            return TrainStation.DistanceBetween(endStation, startStation)/1000;

        }
        public TimeSpan getEstimateArrivalTime() //todo timespan
        {
            TimeSpan t = new((int)(train.AverageSpeed / getDistance()), 0, 0);
            return t;
        }
        
    }
}

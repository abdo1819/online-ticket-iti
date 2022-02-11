using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservationSystem
{
    public class Train
    {
        int id; /// TODO: Add to Class Diagram
        List<Seat> seats;
        //TrainStation start_station; // TODO : fix Class diagram
        //TrainStation end_station;
        List<TrainStation> stops;
        double average_speed;
        TimeSpan departure_time; //TODO: Add this to UML
        TimeSpan arrival_time; //TODO: Add this to UML
        double totalDistance; //TODO: Add this to UML

        public Train(int _id, int no_first_class, int no_second_class, double avg_speed, List<TrainStation> _stops, TimeSpan _departureTime)
        {
            id = _id;
            seats = new();

            for (int i = 1; i < no_first_class + 1; i++)
            {
                seats.Add(new FirstClassSeat(i));
            }
            for (int i = 1; i < no_second_class + 1; i++)
            {
                seats.Add(new SecondClassSeat(no_first_class + i));
            }

            average_speed = avg_speed;
            stops = _stops;
            departure_time = _departureTime;
            totalDistance = 0;
            for (int i = 0; i < stops.Count - 1; i++)
            {
                totalDistance += TrainStation.DistanceBetween(stops[i], stops[i + 1]);
            }

            TimeSpan duration = TimeSpan.FromHours(0.001 * totalDistance / AverageSpeed);

            arrival_time = departure_time + duration;
        }
        public int ID { get { return id; } set { id = value; } }
        public List<Seat> Seats { get { return seats; } set { this.seats = value; } }
        public List<TrainStation> Stops { get { return this.stops; } set { this.stops = value; } }
        public double AverageSpeed { get { return average_speed; } set { average_speed = value; } }
        public TimeSpan DepartureTime { get { return departure_time; } set { departure_time = value; } }
        public TimeSpan ArrivalTime { get { return arrival_time;} }
        public double TotalDistance { get { return totalDistance; } }
        public static List<Train> GetAvailableTrains(TrainStation departure, TrainStation arrival)
        {
            List<Train> available = new List<Train>();
            foreach(var train in DataBase.trains)
            {
                for(int i=0; i<train.stops.Count; i++)
                {
                    if(train.stops[i]==departure)
                    {
                        for(int j=i+1; j<train.stops.Count;j++)
                        {
                            if(train.stops[j]==arrival)
                            {
                                available.Add(train);
                            }
                        }
                    }
                }
            }
            return available;
        }

        public bool AddToService() // TODO: change AddTrain() to AddToService()
        {
            if (!DataBase.trains.Contains(this))
            {
                DataBase.trains.Add(this);
                return true;
            }
            else
                return false;
        }
        public bool RemoveFromService()
        {
            if (DataBase.trains.Contains(this))
            {
                DataBase.trains.Remove(this);
                return true;
            }
            else
                return false;
        }

        public static List<Train> GetAllTrains()
        {
            // String.Join(", \n", trains);
            return DataBase.trains;
        }

        public List<Seat> AvailableSeats()
        {
            List<Seat> available = this.seats.Where(i => i.Reservation_state == false).ToList();
            return available;
        }

        public bool ReserveSeat(Seat seat)
        {
            if(seat!=null && seats.Contains(seat))
            {
                seat.Reservation_state = true;
                return true;
            }
            else
                return false;
        }

        public bool FreeSeat(Seat seat)
        {
            if (seat != null && seats.Contains(seat))
            {
                seat.Reservation_state = false;
                return true;
            }
            else
                return false;
        }

        //public bool ArriveToStation(TrainStation station)
        //{
        //    if(station != null && stops.Contains(station))
        //    {
        //        this.CurrentStation = station;
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //} //TODO : Remove from UML

        public bool AddStopStation(TrainStation station)
        {
            if (station != null && !stops.Contains(station))
            {
                this.stops.Add(station);
                totalDistance = 0;
                for (int i = 0; i < stops.Count - 1; i++)
                {
                    totalDistance += TrainStation.DistanceBetween(stops[i], stops[i + 1]);
                }

                TimeSpan duration = TimeSpan.FromHours(0.001 * totalDistance / AverageSpeed);

                arrival_time = departure_time + duration;
                return true;
            }
            else
                return false;
        }

        //TODO: Add AddStopStationAfter() to uml
        public bool AddStopStationAfter(TrainStation existing_station, TrainStation required_station)
        {
            if (existing_station != null && stops.Contains(existing_station))
            {

                this.stops.Insert(stops.IndexOf(existing_station)+1, required_station);
                totalDistance = 0;
                for (int i = 0; i < stops.Count - 1; i++)
                {
                    totalDistance += TrainStation.DistanceBetween(stops[i], stops[i + 1]);
                }

                TimeSpan duration = TimeSpan.FromHours(0.001 * totalDistance / AverageSpeed);

                arrival_time = departure_time + duration;
                return true;
            }
            else
                return false;
        }
        public override string ToString()
        {
            return $"TrainNo: {this.ID}\n" +
                $"Departure Station: {this.stops[0].Address}\n" +
                $"Departure Time: {this.DepartureTime}\n" +
                $"Arrival Station: {this.stops[stops.Count - 1].Address}\n" +
                $"Arrival Time: {this.ArrivalTime}\n" +
                $"Stops: {String.Join(", ", this.stops)}";
        }
    }
}

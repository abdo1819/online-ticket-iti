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
        DateTime departure_time;
        DateTime arrival_time;

        public Train(int _id, int no_first_class, int no_second_class, double avg_speed, List<TrainStation> _stops, DateTime _departureTime)
        {
            id = _id;
            seats = new();
            for(int i = 1; i < no_first_class + 1; i++)
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
            double TotalDistance = 0;

            for (int i = 0; i < stops.Count - 1; i++)
            {
                TotalDistance += TrainStation.DistanceBetween(stops[i], stops[i + 1]);
            }

            TimeSpan duration = TimeSpan.FromHours(0.001 * TotalDistance / AverageSpeed);

            arrival_time = departure_time + duration;


        }

        public int ID { get { return id; } set { id = value; } }
        public List<Seat> Seats { get { return seats; } set { this.seats = value; } }
        public List<TrainStation> Stops { get { return this.stops; } set { this.stops = value; } }
        public double AverageSpeed { get { return average_speed; } set { average_speed = value; } }
        public DateTime DepartureTime { get { return departure_time; } set { departure_time = value; } }
        public DateTime ArrivalTime { 
            get 
            {
                return arrival_time;
            } 
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

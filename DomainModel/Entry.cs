using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class Entry
    {
        [Key]
        public int Id {get; set;}
        public DateTime TimeStamp {get; set;}
        public int Boarded {get; set;}
        public int LeftBehind {get; set;}
        public int LoopId {get; set;}
        public int DriverId {get; set;}
        public int StopId {get; set;}
        public int BusId {get; set;}

        public Entry() { }
        
        public Entry(int id, DateTime timeStamp, int boarded, int leftBehind)
        {
            Id = id;
            TimeStamp = timeStamp;
            Boarded = boarded;
            LeftBehind = leftBehind;
        }

        public Entry(DateTime timeStamp, int boarded, int leftBehind, int loopId, int driverId, int stopId, int busId)
        {
            TimeStamp = timeStamp;
            Boarded = boarded;
            LeftBehind = leftBehind;
            LoopId = loopId;
            DriverId = driverId;
            StopId = stopId;
            BusId = busId;
        }

        public Entry(Entry entry)
        {
            Id = entry.Id;
            TimeStamp = entry.TimeStamp;
            Boarded = entry.Boarded;
            LeftBehind = entry.LeftBehind;
        }
    }
}


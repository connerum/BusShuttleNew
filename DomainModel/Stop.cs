using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class Stop
    {
        [Key]
        public int Id {get; set;}

        public string Name {get; set;}
        public double Latitude {get; set;}
        public double Longitude {get; set;}
        public bool IsActive { get; set; }

        public Stop(int id, string name, double lat, double lon)
        {
            Id = id;
            Name = name;
            Latitude = lat;
            Longitude = lon;
        }

        public Stop(string name, double lat, double lon)
        {
            Name = name;
            Latitude = lat;
            Longitude = lon;
        }

        public Stop(Stop stop)
        {
            Name = stop.Name;
            Latitude = stop.Latitude;
            Longitude = stop.Longitude;

        }
        
        public Stop()
        {
            IsActive = true;
        }

        public void Update(string name)
        {
            Name = name;
        }
    }
}


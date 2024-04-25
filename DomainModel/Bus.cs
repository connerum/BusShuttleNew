using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
     public class Bus
     {
          [Key]
          public string BusName {get; set;}
          public int Id { get; set;}
          public bool IsActive { get; set; }
          
          
          public Bus(string name)
          {
               BusName = name;
          }

          public Bus(int id, string name)
          {
               Id = id;
               BusName = name;
          }

          public Bus(Bus bus)
          {
               Id = bus.Id;
               BusName = bus.BusName;
          }
          
          public Bus()
          {
               IsActive = true;
          }

          public void Update(string name)
          {
               BusName = name;
          }
     }
}


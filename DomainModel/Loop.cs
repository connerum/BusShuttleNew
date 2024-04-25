namespace DomainModel;

public class Loop
{
     public string Name {get; set;}
     public int Id { get; set;}
     public bool IsActive { get; set; }
     public List<Routes> Routes { get; set;}
     
     public Loop(int id, string name)
     {
          Id = id;
          Name = name;
     }

     public Loop(string name)
     {
          Name = name;
     }

     public Loop(Loop loop)
     {
          Id = loop.Id;
          Name = loop.Name;
     }
     
     public Loop()
     {
          IsActive = true;
     }

     public void Update(string name)
     {
          Name = name;
     }
}
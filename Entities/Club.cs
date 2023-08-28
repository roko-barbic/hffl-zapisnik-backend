namespace roko_test.Entities;
public class Club
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public virtual ICollection<Tournament> Tournaments {get; set;}
    public ICollection<Player> Players { get; set; }
}
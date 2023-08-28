namespace roko_test.Entities;

public class Tournament{
    public int Id {get; set;}
    public string Name {get; set;}
    public virtual ICollection<Club> Clubs {get; set;}
    public DateTime Date {get; set;}
    public ICollection<Game> Games {get; set;}
}
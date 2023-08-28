namespace roko_test.Entities;
public class Game
{
    public int Id { get; set; }
    public Club Club_Home {get; set;}
    public Club Club_Away {get; set;}
    public Tournament Tournament {get; set;}
    public int Club_Home_Score {get; set;}
    public int Club_Away_Score {get; set;}
    public IEnumerable<Event> Events {get; set;}
}
using roko_test.Entities;

namespace roko_test.DTO;
public class TournametWithGamesDTO
{
    public int Id{get; set;}
    public string Name { get; set; }
    public DateTime Date {get; set;}
    public List<GameDtoShort> Games {get; set;}
    public TournametWithGamesDTO(int id, string name, DateTime date, List<GameDtoShort> games)
    {
        Id = id;
        Name = name;
        Date = date;
        Games = games;
    }
}
using roko_test.Entities;

namespace roko_test.DTO;
public class GameDto
{
    public int Id { get; set; }
    public ClubDtoShort Club_Home{get; set;}
    public ClubDtoShort Club_Away {get; set;}
    public int Club_Home_Score {get; set;}
    public int Club_Away_Score {get; set;}
    public IEnumerable<EventDto> Events {get; set;}
}
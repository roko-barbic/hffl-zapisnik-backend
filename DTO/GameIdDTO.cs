using roko_test.Entities;

namespace roko_test.DTO;
public class GameIdDto
{
    public int Id { get; set; }
    public ClubIdDto Club_Home{get; set;}
    public ClubIdDto Club_Away {get; set;}
}
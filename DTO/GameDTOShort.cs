using roko_test.Entities;

namespace roko_test.DTO;
public class GameDtoShort
{
    public int Id { get; set; }
    public ClubDtoShort Club_Home{get; set;}
    public ClubDtoShort Club_Away {get; set;}
    public int Club_Home_Score {get; set;}
    public int Club_Away_Score {get; set;}
     public GameDtoShort(int id, ClubDtoShort clubHome, ClubDtoShort clubAway,
                        int clubHomeScore, int clubAwayScore)
    {
        Id = id;
        Club_Home = clubHome;
        Club_Away = clubAway;
        Club_Home_Score = clubHomeScore;
        Club_Away_Score = clubAwayScore;
    }
}
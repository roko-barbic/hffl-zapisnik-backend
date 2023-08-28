namespace roko_test.DTO;
public class TournamentDTO2
{
    public int Id{get; set;}
    public string Name { get; set; }
    public DateTime Date {get; set;}
    public List<ClubDto> Clubs {get; set;}
}
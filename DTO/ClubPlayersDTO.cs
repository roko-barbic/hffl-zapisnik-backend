using roko_test.Entities;

namespace roko_test.DTO;
public class ClubPlayersDto
{
    public string Name { get; set; }
    public ICollection<PlayerStatDto> Players { get; set; }

    public ClubPlayersDto(string name, List<PlayerStatDto> players){
        this.Name = name;
        this.Players = players;
    }

}
using roko_test.Entities;

namespace roko_test.DTO;
public class ClubIdDto
{
    public int Id {get; set;}
    public string Name { get; set; }
    public List<PlayerIdDto> Players {get; set;}
    public ClubIdDto(string name, int id){
        this.Id = id;
        this.Name=name;
    }
}
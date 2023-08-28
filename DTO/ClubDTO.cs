using roko_test.Entities;

namespace roko_test.DTO;
public class ClubDto
{
    public string Name { get; set; }
    public string City { get; set; }
    public List<PlayerDto> Players {get; set;}

}
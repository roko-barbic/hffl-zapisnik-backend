using roko_test.Entities;

namespace roko_test.DTO;
public class ClubDtoShort
{
    public string Name { get; set; }
    public ClubDtoShort(string name){
        this.Name=name;
    }
}
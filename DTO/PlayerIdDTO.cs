namespace roko_test.DTO;
public class PlayerIdDto
{
    public int Id {get; set;}
    public string FirstName { get; set; }
    public string LastName { get; set; }
     public PlayerIdDto(string firstName, string lastName, int id)
    {
        this.Id = id;
        this.FirstName = firstName;
        this.LastName = lastName;
    }
}
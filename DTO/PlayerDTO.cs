namespace roko_test.DTO;
public class PlayerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
     public PlayerDto(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
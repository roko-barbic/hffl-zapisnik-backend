namespace roko_test.Entities;
public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Club Club { get; set; }
}
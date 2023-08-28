public class CreateUser
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
}

// json representation of new dummy user
//  {
//      "username": "test",
//         "firstName": "test",
//         "lastName": "test",
//         "email": "sdas@asdas.com",
//         "dateOfBirth": "2021-09-09T00:00:00",
//         "phoneNumber": "123123123",
//         "address": "asdasd",
//         "city": "asdasd",
//         "state": "asdasd",
//         "country": "asdasd"
//     }

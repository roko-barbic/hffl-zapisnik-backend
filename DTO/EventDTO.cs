using roko_test.Entities;

namespace roko_test.DTO;
public class EventDto
{
    public int Id { get; set; }
    public PlayerDto Player_One {get; set;}
    public PlayerDto Player_Two {get; set;}
    public int Type {get; set;}
    public int TeamGettingPoints {get; set;} //služi za razlikovanje venta koji je tim dobio poene ak je 1 onda je home, 2 away
}
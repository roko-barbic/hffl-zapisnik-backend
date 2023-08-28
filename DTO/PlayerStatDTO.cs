namespace roko_test.DTO;
public class PlayerStatDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int TDPass{get; set;}
    public int TDCatch{get; set;}
    public int IntPass{get; set;}
    public int IntCatch {get; set;}
    public int IntTD {get; set;}
    public int XPPass{get; set;}
    public int XPCatch{get; set;}
    public int Safety{get; set;}
     public PlayerStatDto(string firstName, string lastName, int tdPass, int tdCatch, int intPass, int intCatch, int intTd, int xpPass, int xpCatch, int safety)
    {
        FirstName = firstName;
        LastName = lastName;
        TDPass = tdPass;
        TDCatch = tdCatch;
        IntPass = intPass;
        IntCatch = intCatch;
        IntTD = intTd;
        XPPass = xpPass;
        XPCatch = xpCatch;
        Safety = safety;
    }
}
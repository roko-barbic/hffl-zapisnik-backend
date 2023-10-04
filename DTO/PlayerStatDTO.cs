namespace roko_test.DTO;
public class PlayerStatDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int TDPass{get; set;}
    public int TDCatch{get; set;}
    public int TDRun{get; set;}
    public int IntPass{get; set;}
    public int IntCatch {get; set;}
    public int IntTD {get; set;}
    public int XPPass{get; set;}
    public int XPCatch{get; set;}
    public int XPRun{get; set;}
    public int Safety{get; set;}
     public PlayerStatDto(string firstName, string lastName, int tdPass, int tdCatch, int tdRun, int intPass, int intCatch, int intTd, int xpPass, int xpCatch, int xpRun, int safety)
    {
        FirstName = firstName;
        LastName = lastName;
        TDPass = tdPass;
        TDCatch = tdCatch;
        TDRun = tdRun;
        IntPass = intPass;
        IntCatch = intCatch;
        IntTD = intTd;
        XPPass = xpPass;
        XPCatch = xpCatch;
        XPRun = xpRun;
        Safety = safety;
    }
}
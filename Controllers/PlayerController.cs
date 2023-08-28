using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using roko_test.Data;
using roko_test.Entities;
using roko_test.DTO;

namespace roko_test.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly DataContext _context;

    public PlayerController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<PlayerStatDto>> GetPlayersStats()
    {
        var players = await _context.Players.ToListAsync();
        var games = await _context.Games
                    .Include(e => e.Events)
                    .ToListAsync();
        var playersStatDto = new List<PlayerStatDto>();

        foreach (var player in players)
        {
            var TDPass = 0;
            var TDCatch = 0;
            var IntPass = 0;
            int IntCatch = 0;
            var IntTD = 0;
            var XPPass = 0;
            var XPCatch = 0;
            var Safety = 0;
            var selectedGames = games.Where(a => a.Club_Away == player.Club || a.Club_Home == player.Club);
            foreach (var game in selectedGames)
            {
                var events = game.Events.ToList();
                foreach (var eventt in events)
                {
                    //TD
                    if(eventt.Player_One == player && eventt.Type == 1){
                        TDPass++;
                    }
                    else if(eventt.Player_Two == player && eventt.Type == 1){
                        TDCatch++;
                    }
                    //obican INT
                    else if(eventt.Player_One == player && eventt.Type == 2){
                        IntPass++;
                    }
                    else if(eventt.Player_Two == player && eventt.Type == 2){
                        IntCatch++;
                    }  
                    //picksix
                    else if(eventt.Player_One == player && eventt.Type == 3){
                        IntPass++;
                    }
                    else if(eventt.Player_Two == player && eventt.Type == 3){
                        IntCatch++;
                        IntTD++;
                        TDCatch++;
                    }
                    //XP
                    else if(eventt.Player_One == player && (eventt.Type == 4 || eventt.Type == 5)){
                        XPPass++;
                    }
                    else if(eventt.Player_Two == player && (eventt.Type == 4 || eventt.Type == 5)){
                        XPCatch++;
                    }
                    //Safety
                    else if(eventt.Player_Two == player && eventt.Type == 6){
                       Safety++;
                    }
                }
            }
            var playerDto = new PlayerStatDto(player.FirstName, player.LastName, TDPass, TDCatch, IntPass, IntCatch, IntTD, XPPass, XPCatch, Safety);
            playersStatDto.Add(playerDto);
        }
        return playersStatDto
            .OrderByDescending(a => a.TDPass)
            .ThenByDescending(b => b.TDCatch)
            .ThenByDescending(c => c.IntCatch);
        
    }

    [HttpGet ("{id}")]
    public async Task<PlayerDto> Get(int id)
    {
        var player = await _context.Players
             .Where(u => u.Id == id)
             .FirstOrDefaultAsync();

        var playerDto = new PlayerDto(player.FirstName, player.LastName);
        if (player == null)
        {
            return null; // Return a 404 Not Found response if the player is not found
        }
        else
        {
            return playerDto;
        }
    }

    // [HttpPost]
    // public async Task<bool> Post([FromBody] CreateUser userFromApi)
    // {
    //     var user = new User();
    //     user.Username = userFromApi.Username;
    //     user.FirstName = userFromApi.FirstName;
    //     user.LastName = userFromApi.LastName;
    //     user.Email = userFromApi.Email;
    //     user.DateOfBirth = userFromApi.DateOfBirth.ToUniversalTime();
    //     user.PhoneNumber = userFromApi.PhoneNumber;
    //     user.Address = userFromApi.Address;
    //     user.City = userFromApi.City;
    //     user.State = userFromApi.State;
    //     user.Country = userFromApi.Country;
    //     _context.Users.Add(user);
    //     await _context.SaveChangesAsync();

    //     return true;
    // }
}

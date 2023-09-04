using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using roko_test.Data;
using roko_test.Entities;
using roko_test.DTO;
using Microsoft.AspNetCore.Mvc.Formatters;
using AutoMapper;

namespace roko_test.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public GameController(DataContext context, IMapper mapper){
        _context = context;
        _mapper = mapper;
    }
     [HttpGet]
    public async Task<IEnumerable<Game>>Get(){
        var games = await _context.Games
            .Include(t=>t.Club_Away)
            .ThenInclude(d => d.Players)
            .Include(c=>c.Club_Home)
            .ThenInclude(f => f.Players)
            .Include(g => g.Events)
            .ToListAsync();
        return games;
    }

    [HttpGet("{id}")]
    public async Task<GameDto> GetGameDto(int id)
    {
        var game = await _context.Games
            .Include(t => t.Club_Home)
            .Include(r => r.Club_Away)
            .Include(s => s.Events)
                .ThenInclude(e => e.Player_One)
            .Include(s => s.Events)
                .ThenInclude(e => e.Player_Two)
            .FirstOrDefaultAsync(c => c.Id == id);

        var gameDTO = new GameDto();
        gameDTO.Club_Home_Score = game.Club_Home_Score;
        gameDTO.Club_Away_Score = game.Club_Away_Score;
        gameDTO.Id = game.Id;

        var homeClubDTO = new ClubDtoShort(game.Club_Home.Name);
        var awayClubDTO = new ClubDtoShort(game.Club_Away.Name);
        gameDTO.Club_Home = homeClubDTO;
        gameDTO.Club_Away = awayClubDTO;

        var eventsInGame = new List<EventDto>();
        foreach (var eventt in game.Events)
        {
            var eventDTO = new EventDto();
            int isTherePlayer = 0;
            eventDTO.Id = eventt.Id;
            eventDTO.Player_One = new PlayerDto(eventt.Player_One.FirstName, eventt.Player_One.LastName);
            eventDTO.Player_Two = new PlayerDto(eventt.Player_Two.FirstName, eventt.Player_Two.LastName);
            eventDTO.Type = eventt.Type;
            eventDTO.TeamGettingPoints=0;
            foreach(var player in game.Club_Home.Players){
                if(player.Id == eventt.Player_One.Id){
                    if(eventt.Type == 1 || eventt.Type == 4 || eventt.Type == 5){
                        eventDTO.TeamGettingPoints=1;
                    }
                    else{
                        eventDTO.TeamGettingPoints=2;
                    }
                }
            }
            if( eventDTO.TeamGettingPoints==0){
                foreach(var player in game.Club_Away.Players){
                if(player.Id == eventt.Player_One.Id){
                    if(eventt.Type == 1 || eventt.Type == 4 || eventt.Type == 5){
                        eventDTO.TeamGettingPoints=2;
                    }
                    else{
                        eventDTO.TeamGettingPoints=1;
                    }
                }
            }
            }
            eventsInGame.Add(eventDTO);
        }
        gameDTO.Events = eventsInGame;
        return gameDTO;
    }

    [HttpPost("api/games/{gameId}/events")]
    public async Task<IActionResult> AddEventToGame(int gameId, [FromBody] EventPostDto eventDto)
    {
        // Check if the game with gameId exists, and handle errors if not found.
        var game = await _context.Games.Include(b => b.Events).FirstOrDefaultAsync(a => a.Id == gameId);
        // Map EventDto to Event entity

        var newEvent = new Event();
        var Player_One = await _context.Players.FirstOrDefaultAsync( b => b.Id == eventDto.Player_OneId);
        newEvent.Player_One = Player_One;
        var Player_Two = await _context.Players.FirstOrDefaultAsync( c => c.Id == eventDto.Player_OneId);
        newEvent.Player_Two = Player_Two;
        newEvent.Type = eventDto.Type;
        var eventss = game.Events.ToList();
        eventss.Add(newEvent);
        game.Events = eventss;
        
        await _context.SaveChangesAsync();
        return Ok(newEvent);
    }

}



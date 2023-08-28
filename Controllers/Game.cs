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

        var homeClubDTO = new ClubDtoShort();
        homeClubDTO.Name = game.Club_Home.Name;
        var awayClubDTO = new ClubDtoShort();
        awayClubDTO.Name = game.Club_Away.Name;
        gameDTO.Club_Home = homeClubDTO;
        gameDTO.Club_Away = awayClubDTO;

        var eventsInGame = new List<EventDto>();
        foreach (var eventt in game.Events)
        {
            var eventDTO = new EventDto();
            eventDTO.Id = eventt.Id;
            eventDTO.Player_One = new PlayerDto(eventt.Player_One.FirstName, eventt.Player_One.LastName);
            eventDTO.Player_Two = new PlayerDto(eventt.Player_Two.FirstName, eventt.Player_Two.LastName);
            eventDTO.Type = eventt.Type;
            eventsInGame.Add(eventDTO);
        }
        gameDTO.Events = eventsInGame;
        return gameDTO;
    }
}



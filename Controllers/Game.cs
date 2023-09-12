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
        var game = await _context.Games.Include(b => b.Events).Include(c => c.Club_Home.Players).Include(d => d.Club_Away.Players).FirstOrDefaultAsync(a => a.Id == gameId);
        // Map EventDto to Event entity

        var newEvent = new Event();
        var Player_One = await _context.Players.FirstOrDefaultAsync( b => b.Id == eventDto.Player_OneId);
        newEvent.Player_One = Player_One;
        var Player_Two = await _context.Players.FirstOrDefaultAsync( c => c.Id == eventDto.Player_TwoId);
        newEvent.Player_Two = Player_Two;
        newEvent.Type = eventDto.Type;
        var eventss = game.Events.ToList();
        eventss.Add(newEvent);
        game.Events = eventss;

        //dodavanje bodova ekipi
        bool isFoundPlayerOne = false;
        foreach(var player in game.Club_Home.Players){
                if(player.Id == Player_One.Id){
                    isFoundPlayerOne = true;
                    if(eventDto.Type == 1){
                        game.Club_Home_Score+=6;
                    }
                    else if(eventDto.Type == 4){
                        game.Club_Home_Score+=2;
                    }
                    else if(eventDto.Type == 5){
                       game.Club_Home_Score+=4;
                    }
                    else if(eventDto.Type ==3){
                        game.Club_Away_Score+=6;
                    }
                    else if(eventDto.Type ==6){
                        game.Club_Away_Score+=2;
                    }
                }
            }
        if(!isFoundPlayerOne){
            foreach(var player in game.Club_Away.Players){
                if(player.Id == Player_One.Id){
                    isFoundPlayerOne = true;
                    if(eventDto.Type == 1){
                        game.Club_Away_Score+=6;
                    }
                    else if(eventDto.Type == 4){
                        game.Club_Away_Score+=1;
                    }
                    else if(eventDto.Type == 5){
                       game.Club_Away_Score+=2;
                    }
                    else if(eventDto.Type ==3){
                        game.Club_Home_Score+=6;
                    }
                    else if(eventDto.Type ==6){
                        game.Club_Home_Score+=2;
                    }
                }
            }
        }
        
        await _context.SaveChangesAsync();
        return Ok(new GameDtoShort(gameId, new ClubDtoShort(game.Club_Home.Name), new ClubDtoShort(game.Club_Away.Name), game.Club_Home_Score, game.Club_Away_Score));
    }



    
    [HttpGet("/gameIdShort")]
    public async Task<GameIdDto> GetGameIds(int id)
    {
        var game = await _context.Games
            .Include(t => t.Club_Home)
            .ThenInclude( c => c.Players)
            .Include(r => r.Club_Away)
            .ThenInclude(j => j.Players)
            .FirstOrDefaultAsync(c => c.Id == id);

        var gameDTO = new GameIdDto();
        gameDTO.Id = id;

        var homeClubDTO = new ClubIdDto(game.Club_Home.Name, game.Club_Home.Id);
        var awayClubDTO = new ClubIdDto(game.Club_Away.Name, game.Club_Away.Id);

        List<PlayerIdDto> playersHome= new List<PlayerIdDto>();
        List<PlayerIdDto> playersAway = new List<PlayerIdDto>();
        foreach (var player in game.Club_Home.Players)
        {
                playersHome.Add(new PlayerIdDto(player.FirstName, player.LastName, player.Id));
        }
            foreach (var player in game.Club_Away.Players)
        {
                playersAway.Add(new PlayerIdDto(player.FirstName, player.LastName, player.Id));
        }
        homeClubDTO.Players = playersHome;
        awayClubDTO.Players = playersAway;
        gameDTO.Club_Home = homeClubDTO;
        gameDTO.Club_Away = awayClubDTO;
        return gameDTO;
    }

    [HttpDelete("/deleteEvent/{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
       
        var eventInGame = await _context.Events.Include(s => s.Player_One).FirstOrDefaultAsync(a => a.Id == id);
        

        if (eventInGame == null)
        {
            return NotFound();
        }
        var game = await _context.Games.Include(c=>c.Club_Home).ThenInclude(d => d.Players).Include(t=>t.Club_Away).ThenInclude(r => r.Players).FirstOrDefaultAsync(b => b.Events.Contains(eventInGame));

        var player_One = eventInGame.Player_One;

        bool isFoundPlayerOne = false;
        foreach(var player in game.Club_Home.Players){
                if(player.Id == player_One.Id){
                    isFoundPlayerOne = true;
                    if(eventInGame.Type == 1){
                        game.Club_Home_Score-=6;
                    }
                    else if(eventInGame.Type == 4){
                        game.Club_Home_Score-=1;
                    }
                    else if(eventInGame.Type == 5){
                       game.Club_Home_Score-=2;
                    }
                    else if(eventInGame.Type ==3){
                        game.Club_Away_Score-=6;
                    }
                    else if(eventInGame.Type ==6){
                        game.Club_Away_Score-=2;
                    }
                }
            }
          if(!isFoundPlayerOne){
            foreach(var player in game.Club_Away.Players){
                if(player.Id == player_One.Id){
                    isFoundPlayerOne = true;
                    if(eventInGame.Type == 1){
                        game.Club_Away_Score-=6;
                    }
                    else if(eventInGame.Type == 4){
                        game.Club_Away_Score-=1;
                    }
                    else if(eventInGame.Type == 5){
                       game.Club_Away_Score-=2;
                    }
                    else if(eventInGame.Type ==3){
                        game.Club_Home_Score-=6;
                    }
                    else if(eventInGame.Type ==6){
                        game.Club_Home_Score-=2;
                    }
                }
            }
        }


        _context.Events.Remove(eventInGame);
        await _context.SaveChangesAsync();

        return Ok();
    }


    
    [HttpDelete("/deleteGame/{id}")]
    public async Task<IActionResult> DeleteGame(int id)
    {
       
        var game = await _context.Games.FirstOrDefaultAsync(a => a.Id == id);
        

        if (game == null)
        {
            return NotFound();
        }
        
        _context.Games.Remove(game);
        await _context.SaveChangesAsync();

        return Ok();
    }
}



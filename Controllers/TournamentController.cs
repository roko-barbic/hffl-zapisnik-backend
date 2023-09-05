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
public class TournamentController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public TournamentController(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<TournamentDTO>> Get()
    {
        var tournaments = await _context.Tournaments
            .OrderByDescending(t => t.Date)
            .ToListAsync();

        //    var TournamentDtos = tournaments.Select(tournament => new TournamentDTO{
        //         Name = tournament.Name,
        //         Date = tournament.Date
        //    }).ToList();

        var TournamentDtos = _mapper.Map<List<TournamentDTO>>(tournaments);

        return TournamentDtos;
    }
    // [HttpGet("{id}")]
    // public async Task<Tournament> GetTournamentInfo(int id)
    // {
    //     var tournament = await _context.Tournaments
    //         .Include(t => t.Clubs)
    //         .ThenInclude(c => c.Players)
    //         .FirstOrDefaultAsync(c => c.Id == id);

    //     //var clubsOnTournament = await _context.Clubs.Where(c => c.Id == id).ToListAsync();

    //     if (tournament == null)
    //     {
    //         return null;

    //     }

    //     var clubsInTournament = tournament.Clubs.ToList();
    //     foreach (var club in clubsInTournament)
    //     {
    //         var playersInClub = club.Players.ToList();
    //     }

    //     var Tournament = new Tournament
    //     {
    //         Id = id,
    //         Name = tournament.Name,
    //         Date = tournament.Date,
    //         Clubs = clubsInTournament
    //     };

    //     return Tournament;
    // }

    // [HttpGet("{id}")]
    // public async Task<TournamentDTO2> GetTournamentInfoDTO(int id)
    // {
    //     var tournament = await _context.Tournaments
    //         .Include(t => t.Clubs)
    //         .ThenInclude(c => c.Players)
    //         .FirstOrDefaultAsync(c => c.Id == id);

    //     //var clubsOnTournament = await _context.Clubs.Where(c => c.Id == id).ToListAsync();

    //     if (tournament == null)
    //     {
    //         return null;

    //     }

    //     var clubsInTournament = tournament.Clubs.ToList();
    //     var clubsInTournamentDTO = new List<ClubDto>();
    //     foreach (var club in clubsInTournament)
    //     {
    //         var clubDTO = new ClubDto();
    //         clubDTO.City = club.City;
    //         clubDTO.Name = club.Name;
    //         var playersInClub = club.Players.ToList();
    //         var playersInClubDTO = new List<PlayerDto>();
    //         foreach (var player in playersInClub)
    //         {
    //             var PlayerDto = new PlayerDto(player.FirstName, player.LastName);
    //             playersInClubDTO.Add(PlayerDto);
    //         }
    //         clubDTO.Players = playersInClubDTO;
    //         clubsInTournamentDTO.Add(clubDTO);
            
    //     }

    //     var Tournament = new TournamentDTO2
    //     {
    //         Id = id,
    //         Name = tournament.Name,
    //         Date = tournament.Date,
    //         Clubs = clubsInTournamentDTO
    //     };

    //     return Tournament;
    // }
     [HttpGet("{id}")]
    public async Task<TournametWithGamesDTO> GetTournamentInfoDTO(int id)
    {
        var tournament = await _context.Tournaments
            .Include(t => t.Games)
            .ThenInclude(r => r.Club_Home)
            .Include(t => t.Games)
            .ThenInclude(r => r.Club_Away)
            .FirstOrDefaultAsync(c => c.Id == id);

        //var clubsOnTournament = await _context.Clubs.Where(c => c.Id == id).ToListAsync();
        List<GameDtoShort> gamesDTO = new List<GameDtoShort>();;
        foreach (var game in tournament.Games)
        {
            gamesDTO.Add(new GameDtoShort(game.Id, 
                new ClubDtoShort(game.Club_Home.Name), new ClubDtoShort(game.Club_Away.Name),
                 game.Club_Home_Score, game.Club_Away_Score));
        }
        
        return new TournametWithGamesDTO(tournament.Id, tournament.Name, tournament.Date, gamesDTO);
    }

    [HttpPost("/newGame/{tournamentId}")]
    public async Task<IActionResult> AddGameToTournament(int tournamentId, [FromBody] GamePostDto game){
        var tournament = await _context.Tournaments.Include(r => r.Games).FirstOrDefaultAsync(a => a.Id == tournamentId);
        
        var newGame = new Game();
        var club_Home = await _context.Clubs.FirstOrDefaultAsync(b=> b.Id == game.Club_HomeId);
        var Club_Away = await _context.Clubs.FirstOrDefaultAsync(c => c.Id == game.Club_AwayId);
        newGame.Club_Home = club_Home;
        newGame.Club_Away = Club_Away;
        newGame.Tournament = tournament;
        newGame.Club_Away_Score=0;
        newGame.Club_Home_Score=0;
        tournament.Games.Add(newGame);
        await _context.SaveChangesAsync();
        return Ok();
    }
}

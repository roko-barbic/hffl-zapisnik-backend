using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using roko_test.Data;
using roko_test.Entities;
using roko_test.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace roko_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClubController : ControllerBase
    {
        private readonly DataContext _context;

        public ClubController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<ClubStatDto>> Get()
        {
            var clubs = await _context.Clubs.ToListAsync();
            var games = await _context.Games.ToListAsync();
            var clubsDto = new List<ClubStatDto>();

            // var clubDtos = clubs.Select(club => new ClubStatDto
            // {
            //     Name = club.Name,

            // }).ToList();
            foreach (var club in clubs)
            {
                var clubDto = new ClubStatDto();
                clubDto.Name = club.Name;
                int win = 0;
                int draw = 0;
                int loss = 0;
                var selectedGames = games.Where(a => a.Club_Away == club || a.Club_Home == club);
                foreach(var game in selectedGames){
                    if(game.Club_Away == club){
                        if(game.Club_Away_Score > game.Club_Home_Score){
                            win++;
                        }
                        else if(game.Club_Away_Score == game.Club_Home_Score){
                            draw++;
                        }
                        else{
                            loss++;
                        }
                    }
                    else if(game.Club_Home == club){
                        if(game.Club_Away_Score < game.Club_Home_Score){
                            win++;
                        }
                        else if(game.Club_Away_Score == game.Club_Home_Score){
                            draw++;
                        }
                        else{
                            loss++;
                        }
                    }
                }
                clubDto.Win=win;
                clubDto.Draw=draw;
                clubDto.Loss=loss;
                clubDto.id = club.Id;
                clubsDto.Add(clubDto);
            }
            
            return clubsDto
                .OrderByDescending(a => a.Win)
                .ThenByDescending(b => b.Draw);
        }

        [HttpGet("{id}")]
        public async Task<ClubPlayersDto> GetPlayers(int id)
        {
            var club = await _context.Clubs
            .FirstOrDefaultAsync(c => c.Id == id);
            var players = await _context.Players.Where(a => a.Club == club).ToListAsync();
            var games = await _context.Games
                    .Where(a => a.Club_Away == club || a.Club_Home == club)
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
                foreach (var game in games)
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
                    playersStatDto
                        .OrderByDescending(a => a.TDPass)
                        .ThenByDescending(b => b.TDCatch)
                        .ThenByDescending(c => c.IntCatch);
                    var clubPlayerStat = new ClubPlayersDto(club.Name, playersStatDto);
                    return clubPlayerStat;
        
                }

    }
}

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
                clubsDto.Add(clubDto);
            }
            
            return clubsDto
                .OrderByDescending(a => a.Win)
                .ThenByDescending(b => b.Draw);
        }

        [HttpGet("{id}")]
        public async Task<Club> Get(int id)
        {
            var club = await _context.Clubs.FirstOrDefaultAsync(c => c.Id == id);

            if (club == null)
            {
                return null; // Return a 404 Not Found response if the club is not found
            }

            var clubDto = new Club
            {
                Name = club.Name,
                City = club.City
            };

            return clubDto;
        }

    }
}

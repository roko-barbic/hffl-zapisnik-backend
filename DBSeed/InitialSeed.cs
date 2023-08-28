using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using roko_test.Entities;
using roko_test.Data;

namespace roko_test.Seed;
public static class DefaultSeeds
{

    public static async Task<bool> SeedAsync(DataContext dataContext)
    {
        // // add clubs
        // string clubsJSON = File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + "clubs.json");
        // List<Club> clubsList = JsonConvert.DeserializeObject<List<Club>>(clubsJSON);
        // dataContext.Clubs.AddRange(clubsList);

        // await dataContext.SaveChangesAsync();

        // // add players
        // string playersJSON = File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + "players.json");
        // List<Player> playersList = JsonConvert.DeserializeObject<List<Player>>(playersJSON);

      
        // int i = 20;
        // foreach (var player in playersList)
        // {
        //     player.DateOfBirth = player.DateOfBirth.ToUniversalTime();
        //     dataContext.Add(player);
        //     dataContext.Entry(player).Property("ClubId").CurrentValue = ++i;
        // }

        // await dataContext.SaveChangesAsync();
        //   
        
        
        //add tornaments
        // string tournamentsJSON = File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + "tournaments.json");
        // List<Tournament> torunamentsList = JsonConvert.DeserializeObject<List<Tournament>>(tournamentsJSON);
        // dataContext.Tournaments.AddRange(torunamentsList);

        // await dataContext.SaveChangesAsync();

        //add clubs to tournaments
        // add 4 radnom clubs to every tournament
        var clubs = await dataContext.Clubs.ToListAsync();
        // var tournaments = await dataContext.Tournaments.ToListAsync();
        var rnd = new Random();
        // foreach (var tournament in tournaments)
        // {
        //     var clubsInTournament = new List<Club>();
        //     for (int i = 0; i < 4; i++)
        //     {
        //         var club = clubs[rnd.Next(clubs.Count)];
        //         clubsInTournament.Add(club);
        //     }
        //     tournament.Clubs = clubsInTournament;
        // }


        // //add events
        // var games = await dataContext.Games.ToListAsync();
        // foreach (var game in games)
        // {
        //     var eventsInGame = new List<Event>();
        //     var homeClub = game.Club_Home;
        //     var awayClub = game.Club_Away;
        //     var homePlayers = await dataContext.Players
        //         .Where(player => player.Club == homeClub).ToListAsync();
        //     var awayPlayers = await dataContext.Players
        //         .Where(player => player.Club == awayClub).ToListAsync();
        //     // ICollection<Player> playersCollection1 = homeClub.Players;
        //     // ICollection<Player> playersCollection2 = awayClub.Players;
        //     for (int i = 0; i < 5; i++)
        //     {
                
        //         var type = i+1;
        //         var eventNew = new Event();
        //         eventNew.Type=type;

        //     if (homePlayers.Count > 0 && awayPlayers.Count > 0)
        //     {
        //         int randomIndex = rnd.Next(homePlayers.Count);
        //         eventNew.Player_One = homePlayers.ElementAt(1); //to je QB
        //         if(type ==2 || type == 3){
        //             eventNew.Player_Two = awayPlayers.ElementAt(randomIndex);//int catc
        //         }
        //         else{
        //             eventNew.Player_Two = homePlayers.ElementAt(randomIndex);//dobar cathc
        //         }

        //     }
        //     eventsInGame.Add(eventNew);
        //     }
        //     //sad eventi za drugu ekipu
        //     for (int i = 0; i < 5; i++)
        //     {
                
        //         var type = i+1;
        //         var eventNew = new Event();
        //         eventNew.Type=type;

        //     if (homePlayers.Count > 0 && awayPlayers.Count > 0)
        //     {
        //         int randomIndex = rnd.Next(homePlayers.Count);
        //         eventNew.Player_One = awayPlayers.ElementAt(1); //to je QB
        //         if(type ==2 || type == 3){
        //             eventNew.Player_Two = homePlayers.ElementAt(randomIndex);//int catc
        //         }
        //         else{
        //             eventNew.Player_Two = awayPlayers.ElementAt(randomIndex);//dobar cathc
        //         }

        //     }
        //     eventsInGame.Add(eventNew);
        //     }

        //     //dodat sve evente u game
        //     game.Events = eventsInGame;
        // }

        //add score in games
        // var i = 0;
        // foreach (var game in games)
        // {
        //     game.Club_Home_Score = 36 + i;
        //     game.Club_Away_Score = 38 - i;
        //     i++;
        // }
        await dataContext.SaveChangesAsync();
        
        return true;
    }
}

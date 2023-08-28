using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace roko_test.Migrations
{
    /// <inheritdoc />
    public partial class TournamentForClubs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Tournaments_TournamentId",
                table: "Clubs");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Game_GameId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Players_Player_OneId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Players_Player_TwoId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Clubs_Club_AwayId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Clubs_Club_HomeId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Tournaments_TournamentId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_TournamentId",
                table: "Clubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "Clubs");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "Games");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Events");

            migrationBuilder.RenameIndex(
                name: "IX_Game_TournamentId",
                table: "Games",
                newName: "IX_Games_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_Game_Club_HomeId",
                table: "Games",
                newName: "IX_Games_Club_HomeId");

            migrationBuilder.RenameIndex(
                name: "IX_Game_Club_AwayId",
                table: "Games",
                newName: "IX_Games_Club_AwayId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_Player_TwoId",
                table: "Events",
                newName: "IX_Events_Player_TwoId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_Player_OneId",
                table: "Events",
                newName: "IX_Events_Player_OneId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_GameId",
                table: "Events",
                newName: "IX_Events_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ClubTournament",
                columns: table => new
                {
                    ClubsId = table.Column<int>(type: "integer", nullable: false),
                    TournamentsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubTournament", x => new { x.ClubsId, x.TournamentsId });
                    table.ForeignKey(
                        name: "FK_ClubTournament_Clubs_ClubsId",
                        column: x => x.ClubsId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubTournament_Tournaments_TournamentsId",
                        column: x => x.TournamentsId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubTournament_TournamentsId",
                table: "ClubTournament",
                column: "TournamentsId");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Events_Games_GameId",
            //     table: "Events",
            //     column: "GameId",
            //     principalTable: "Games",
            //     principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Players_Player_OneId",
                table: "Events",
                column: "Player_OneId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Players_Player_TwoId",
                table: "Events",
                column: "Player_TwoId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Clubs_Club_AwayId",
                table: "Games",
                column: "Club_AwayId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Clubs_Club_HomeId",
                table: "Games",
                column: "Club_HomeId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Tournaments_TournamentId",
                table: "Games",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Games_GameId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Players_Player_OneId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Players_Player_TwoId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Clubs_Club_AwayId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Clubs_Club_HomeId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Tournaments_TournamentId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "ClubTournament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Game");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Event");

            migrationBuilder.RenameIndex(
                name: "IX_Games_TournamentId",
                table: "Game",
                newName: "IX_Game_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_Club_HomeId",
                table: "Game",
                newName: "IX_Game_Club_HomeId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_Club_AwayId",
                table: "Game",
                newName: "IX_Game_Club_AwayId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_Player_TwoId",
                table: "Event",
                newName: "IX_Event_Player_TwoId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_Player_OneId",
                table: "Event",
                newName: "IX_Event_Player_OneId");

            // migrationBuilder.RenameIndex(
            //     name: "IX_Events_GameId",
            //     table: "Event",
            //     newName: "IX_Event_GameId");

            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "Clubs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_TournamentId",
                table: "Clubs",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_Tournaments_TournamentId",
                table: "Clubs",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Event_Game_GameId",
            //     table: "Event",
            //     column: "GameId",
            //     principalTable: "Game",
            //     principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Players_Player_OneId",
                table: "Event",
                column: "Player_OneId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Players_Player_TwoId",
                table: "Event",
                column: "Player_TwoId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Clubs_Club_AwayId",
                table: "Game",
                column: "Club_AwayId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Clubs_Club_HomeId",
                table: "Game",
                column: "Club_HomeId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Tournaments_TournamentId",
                table: "Game",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace roko_test.Migrations
{
    /// <inheritdoc />
    public partial class AddGamesAndEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Club_HomeId = table.Column<int>(type: "integer", nullable: false),
                    Club_AwayId = table.Column<int>(type: "integer", nullable: false),
                    TournamentId = table.Column<int>(type: "integer", nullable: false),
                    Club_Home_Score = table.Column<int>(type: "integer", nullable: false),
                    Club_Away_Score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_Clubs_Club_AwayId",
                        column: x => x.Club_AwayId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Game_Clubs_Club_HomeId",
                        column: x => x.Club_HomeId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Game_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Player_OneId = table.Column<int>(type: "integer", nullable: false),
                    Player_TwoId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    // GameId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    // table.ForeignKey(
                    //     name: "FK_Event_Game_GameId",
                    //     column: x => x.GameId,
                    //     principalTable: "Game",
                    //     principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Event_Players_Player_OneId",
                        column: x => x.Player_OneId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Players_Player_TwoId",
                        column: x => x.Player_TwoId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // migrationBuilder.CreateIndex(
            //     name: "IX_Event_GameId",
            //     table: "Event",
            //     column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Player_OneId",
                table: "Event",
                column: "Player_OneId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Player_TwoId",
                table: "Event",
                column: "Player_TwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_Club_AwayId",
                table: "Game",
                column: "Club_AwayId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_Club_HomeId",
                table: "Game",
                column: "Club_HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_TournamentId",
                table: "Game",
                column: "TournamentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Game");
        }
    }
}

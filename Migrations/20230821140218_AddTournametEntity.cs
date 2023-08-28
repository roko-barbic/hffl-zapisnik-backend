using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace roko_test.Migrations
{
    /// <inheritdoc />
    public partial class AddTournametEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "Clubs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Tournaments_TournamentId",
                table: "Clubs");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_TournamentId",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "Clubs");
        }
    }
}

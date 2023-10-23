using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuizzWebApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quizzes_v2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    image_path = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Difficulty = table.Column<string>(type: "text", nullable: false),
                    Author = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizzes_v2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "questions_v2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    answers = table.Column<Dictionary<char, string>>(type: "jsonb", nullable: false),
                    correct_answers = table.Column<Dictionary<char, string>>(type: "jsonb", nullable: false),
                    QuizV2Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions_v2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_questions_v2_quizzes_v2_QuizV2Id",
                        column: x => x.QuizV2Id,
                        principalTable: "quizzes_v2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_questions_v2_QuizV2Id",
                table: "questions_v2",
                column: "QuizV2Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "questions_v2");

            migrationBuilder.DropTable(
                name: "quizzes_v2");
        }
    }
}

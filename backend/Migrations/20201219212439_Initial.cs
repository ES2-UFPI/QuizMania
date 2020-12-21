using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizMania.WebAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Choices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    QuestionId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Choices_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionQuiz",
                columns: table => new
                {
                    QuestionsId = table.Column<long>(type: "INTEGER", nullable: false),
                    QuizzesId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionQuiz", x => new { x.QuestionsId, x.QuizzesId });
                    table.ForeignKey(
                        name: "FK_QuestionQuiz_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionQuiz_Quizzes_QuizzesId",
                        column: x => x.QuizzesId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizFeedbacks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuizId = table.Column<long>(type: "INTEGER", nullable: true),
                    GoldGained = table.Column<int>(type: "INTEGER", nullable: false),
                    ExperienceGained = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizFeedbacks_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuestionId = table.Column<long>(type: "INTEGER", nullable: true),
                    QuizFeedbackId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_QuizFeedbacks_QuizFeedbackId",
                        column: x => x.QuizFeedbackId,
                        principalTable: "QuizFeedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChoiceQuestionAnswer",
                columns: table => new
                {
                    AnswersId = table.Column<long>(type: "INTEGER", nullable: false),
                    QuestionAnswersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceQuestionAnswer", x => new { x.AnswersId, x.QuestionAnswersId });
                    table.ForeignKey(
                        name: "FK_ChoiceQuestionAnswer_Choices_AnswersId",
                        column: x => x.AnswersId,
                        principalTable: "Choices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChoiceQuestionAnswer_QuestionAnswers_QuestionAnswersId",
                        column: x => x.QuestionAnswersId,
                        principalTable: "QuestionAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceQuestionAnswer_QuestionAnswersId",
                table: "ChoiceQuestionAnswer",
                column: "QuestionAnswersId");

            migrationBuilder.CreateIndex(
                name: "IX_Choices_QuestionId",
                table: "Choices",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_QuestionId",
                table: "QuestionAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_QuizFeedbackId",
                table: "QuestionAnswers",
                column: "QuizFeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionQuiz_QuizzesId",
                table: "QuestionQuiz",
                column: "QuizzesId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizFeedbacks_QuizId",
                table: "QuizFeedbacks",
                column: "QuizId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChoiceQuestionAnswer");

            migrationBuilder.DropTable(
                name: "QuestionQuiz");

            migrationBuilder.DropTable(
                name: "Choices");

            migrationBuilder.DropTable(
                name: "QuestionAnswers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "QuizFeedbacks");

            migrationBuilder.DropTable(
                name: "Quizzes");
        }
    }
}

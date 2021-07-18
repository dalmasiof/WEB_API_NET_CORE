using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSchool.WebAPI.Migrations
{
    public partial class initiMySQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Matricula = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    SobreNome = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    DataNasc = table.Column<DateTime>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Registro = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    SobreNome = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlunoCurso",
                columns: table => new
                {
                    AlunoId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Nota = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoCurso", x => new { x.AlunoId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_AlunoCurso_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoCurso_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disciplina",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CargaHoraria = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    ProfessorId = table.Column<int>(nullable: false),
                    PreRequisitoId = table.Column<int>(nullable: true),
                    CursoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplina_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Disciplina_Disciplina_PreRequisitoId",
                        column: x => x.PreRequisitoId,
                        principalTable: "Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Disciplina_Professor_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlunoDisciplina",
                columns: table => new
                {
                    AlunoId = table.Column<int>(nullable: false),
                    DisciplinaId = table.Column<int>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Nota = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoDisciplina", x => new { x.AlunoId, x.DisciplinaId });
                    table.ForeignKey(
                        name: "FK_AlunoDisciplina_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoDisciplina_Disciplina_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Ativo", "DataFim", "DataInicio", "DataNasc", "Matricula", "Nome", "SobreNome", "Telefone" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(2021, 7, 18, 20, 24, 36, 149, DateTimeKind.Local).AddTicks(4966), new DateTime(2005, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Marta", "Kent", "33225555" },
                    { 2, true, null, new DateTime(2021, 7, 18, 20, 24, 36, 149, DateTimeKind.Local).AddTicks(7799), new DateTime(2005, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Paula", "Isabela", "3354288" },
                    { 3, true, null, new DateTime(2021, 7, 18, 20, 24, 36, 149, DateTimeKind.Local).AddTicks(7916), new DateTime(2005, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Laura", "Antonia", "55668899" },
                    { 4, true, null, new DateTime(2021, 7, 18, 20, 24, 36, 149, DateTimeKind.Local).AddTicks(7941), new DateTime(2005, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Luiza", "Maria", "6565659" },
                    { 5, true, null, new DateTime(2021, 7, 18, 20, 24, 36, 149, DateTimeKind.Local).AddTicks(7961), new DateTime(2005, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Lucas", "Machado", "565685415" },
                    { 6, true, null, new DateTime(2021, 7, 18, 20, 24, 36, 149, DateTimeKind.Local).AddTicks(7987), new DateTime(2005, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Pedro", "Alvares", "456454545" },
                    { 7, true, null, new DateTime(2021, 7, 18, 20, 24, 36, 149, DateTimeKind.Local).AddTicks(8009), new DateTime(2005, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Paulo", "José", "9874512" }
                });

            migrationBuilder.InsertData(
                table: "Curso",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Tecnologia da Informação" },
                    { 2, "Sistemas de Informação" },
                    { 3, "Ciência da Computação" }
                });

            migrationBuilder.InsertData(
                table: "Professor",
                columns: new[] { "Id", "Ativo", "DataFim", "DataInicio", "Nome", "Registro", "SobreNome", "Telefone" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(2021, 7, 18, 20, 24, 36, 140, DateTimeKind.Local).AddTicks(2470), "Lauro", 1, "Oliveira", null },
                    { 2, true, null, new DateTime(2021, 7, 18, 20, 24, 36, 144, DateTimeKind.Local).AddTicks(6798), "Roberto", 2, "Soares", null },
                    { 3, true, null, new DateTime(2021, 7, 18, 20, 24, 36, 144, DateTimeKind.Local).AddTicks(6921), "Ronaldo", 3, "Marconi", null },
                    { 4, true, null, new DateTime(2021, 7, 18, 20, 24, 36, 144, DateTimeKind.Local).AddTicks(6942), "Rodrigo", 4, "Carvalho", null },
                    { 5, true, null, new DateTime(2021, 7, 18, 20, 24, 36, 144, DateTimeKind.Local).AddTicks(6950), "Alexandre", 5, "Montanha", null }
                });

            migrationBuilder.InsertData(
                table: "Disciplina",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PreRequisitoId", "ProfessorId" },
                values: new object[,]
                {
                    { 1, 0, 1, "Matemática", null, 1 },
                    { 2, 0, 3, "Matemática", null, 1 },
                    { 3, 0, 3, "Física", null, 2 },
                    { 4, 0, 1, "Português", null, 3 },
                    { 5, 0, 1, "Inglês", null, 4 },
                    { 6, 0, 2, "Inglês", null, 4 },
                    { 7, 0, 3, "Inglês", null, 4 },
                    { 8, 0, 1, "Programação", null, 5 },
                    { 9, 0, 2, "Programação", null, 5 },
                    { 10, 0, 3, "Programação", null, 5 }
                });

            migrationBuilder.InsertData(
                table: "AlunoDisciplina",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[,]
                {
                    { 2, 1, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(922), null },
                    { 4, 5, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(973), null },
                    { 2, 5, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(938), null },
                    { 1, 5, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(912), null },
                    { 7, 4, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(1032), null },
                    { 6, 4, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(1010), null },
                    { 5, 4, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(979), null },
                    { 4, 4, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(968), null },
                    { 1, 4, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(840), null },
                    { 7, 3, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(1026), null },
                    { 5, 5, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(984), null },
                    { 6, 3, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(1002), null },
                    { 7, 2, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(1021), null },
                    { 6, 2, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(995), null },
                    { 3, 2, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(949), null },
                    { 2, 2, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(928), null },
                    { 1, 2, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(65), null },
                    { 7, 1, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(1016), null },
                    { 6, 1, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(990), null },
                    { 4, 1, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(962), null },
                    { 3, 1, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(944), null },
                    { 3, 3, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(955), null },
                    { 7, 5, null, new DateTime(2021, 7, 18, 20, 24, 36, 150, DateTimeKind.Local).AddTicks(1037), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunoCurso_CursoId",
                table: "AlunoCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoDisciplina_DisciplinaId",
                table: "AlunoDisciplina",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplina_CursoId",
                table: "Disciplina",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplina_PreRequisitoId",
                table: "Disciplina",
                column: "PreRequisitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplina_ProfessorId",
                table: "Disciplina",
                column: "ProfessorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoCurso");

            migrationBuilder.DropTable(
                name: "AlunoDisciplina");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Disciplina");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Professor");
        }
    }
}

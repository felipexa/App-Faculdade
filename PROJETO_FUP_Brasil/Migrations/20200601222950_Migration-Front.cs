using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PROJETO_FUP_Brasil.Migrations
{
    public partial class MigrationFront : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chefia",
                columns: table => new
                {
                    ChefiaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeChefia = table.Column<string>(maxLength: 60, nullable: false),
                    Setor = table.Column<string>(maxLength: 60, nullable: false),
                    FuncionarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chefia", x => x.ChefiaId);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id_Curso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCurso = table.Column<string>(maxLength: 60, nullable: false),
                    ValorCurso = table.Column<decimal>(nullable: false),
                    ProfessorCurso = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id_Curso);
                });

            migrationBuilder.CreateTable(
                name: "Financeiro",
                columns: table => new
                {
                    FinanceiroViewModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financeiro", x => x.FinanceiroViewModelId);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id_Matricula = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 60, nullable: false),
                    Rg = table.Column<string>(maxLength: 9, nullable: false),
                    Cpf = table.Column<string>(maxLength: 11, nullable: false),
                    Datanascimento = table.Column<DateTime>(nullable: false),
                    DataInicioCurso = table.Column<DateTime>(nullable: false),
                    DataTerminoCurso = table.Column<DateTime>(nullable: true),
                    Genero = table.Column<string>(maxLength: 9, nullable: false),
                    CursosID = table.Column<int>(nullable: false),
                    FinanceiroViewModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id_Matricula);
                    table.ForeignKey(
                        name: "FK_Aluno_Cursos_CursosID",
                        column: x => x.CursosID,
                        principalTable: "Cursos",
                        principalColumn: "Id_Curso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aluno_Financeiro_FinanceiroViewModelId",
                        column: x => x.FinanceiroViewModelId,
                        principalTable: "Financeiro",
                        principalColumn: "FinanceiroViewModelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    FuncionarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeFuncionario = table.Column<string>(maxLength: 60, nullable: false),
                    Rg = table.Column<string>(maxLength: 9, nullable: false),
                    Cpf = table.Column<string>(maxLength: 11, nullable: false),
                    Datanascimento = table.Column<DateTime>(nullable: false),
                    DataContratacao = table.Column<DateTime>(nullable: false),
                    DataDemissao = table.Column<DateTime>(nullable: true),
                    Genero = table.Column<string>(nullable: false),
                    SalarioFuncionario = table.Column<decimal>(nullable: false),
                    FinanceiroViewModelId = table.Column<int>(nullable: true),
                    Funcionario = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.FuncionarioId);
                    table.ForeignKey(
                        name: "FK_Funcionario_Financeiro_FinanceiroViewModelId",
                        column: x => x.FinanceiroViewModelId,
                        principalTable: "Financeiro",
                        principalColumn: "FinanceiroViewModelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_Chefia_Funcionario",
                        column: x => x.Funcionario,
                        principalTable: "Chefia",
                        principalColumn: "ChefiaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id_Email = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    _Email = table.Column<string>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false),
                    ChefiaId = table.Column<int>(nullable: false),
                    FuncionarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id_Email);
                    table.ForeignKey(
                        name: "FK_Email_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id_Matricula",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Email_Chefia_ChefiaId",
                        column: x => x.ChefiaId,
                        principalTable: "Chefia",
                        principalColumn: "ChefiaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Email_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "FuncionarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Telefone",
                columns: table => new
                {
                    TelefoneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    _Telefone = table.Column<string>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false),
                    FuncionarioId = table.Column<int>(nullable: false),
                    ChefiaId = table.Column<int>(nullable: false),
                    Telefone = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefone", x => x.TelefoneId);
                    table.ForeignKey(
                        name: "FK_Telefone_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id_Matricula",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Telefone_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "FuncionarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Telefone_Chefia_Telefone",
                        column: x => x.Telefone,
                        principalTable: "Chefia",
                        principalColumn: "ChefiaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_CursosID",
                table: "Aluno",
                column: "CursosID");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_FinanceiroViewModelId",
                table: "Aluno",
                column: "FinanceiroViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Email_AlunoId",
                table: "Email",
                column: "AlunoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Email_ChefiaId",
                table: "Email",
                column: "ChefiaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Email_FuncionarioId",
                table: "Email",
                column: "FuncionarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_FinanceiroViewModelId",
                table: "Funcionario",
                column: "FinanceiroViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_Funcionario",
                table: "Funcionario",
                column: "Funcionario");

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_AlunoId",
                table: "Telefone",
                column: "AlunoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_FuncionarioId",
                table: "Telefone",
                column: "FuncionarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_Telefone",
                table: "Telefone",
                column: "Telefone",
                unique: true,
                filter: "[Telefone] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "Telefone");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Financeiro");

            migrationBuilder.DropTable(
                name: "Chefia");
        }
    }
}

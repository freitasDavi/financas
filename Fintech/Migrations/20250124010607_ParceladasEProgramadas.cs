using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fintech.Migrations
{
    /// <inheritdoc />
    public partial class ParceladasEProgramadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "users",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "CodigoDespesaParcelada",
                table: "despesas",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CodigoDespesaProgramada",
                table: "despesas",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CodigoUsuario",
                table: "despesas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "despesas_parceladas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NumeroParcelas = table.Column<int>(type: "integer", nullable: false),
                    ValorParcela = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalParcela = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    CodigoUsuario = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_despesas_parceladas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_despesas_parceladas_users_CodigoUsuario",
                        column: x => x.CodigoUsuario,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "despesas_programadas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    DataInicial = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CodigoUsuario = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_despesas_programadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_despesas_programadas_users_CodigoUsuario",
                        column: x => x.CodigoUsuario,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_despesas_CodigoDespesaParcelada",
                table: "despesas",
                column: "CodigoDespesaParcelada");

            migrationBuilder.CreateIndex(
                name: "IX_despesas_CodigoDespesaProgramada",
                table: "despesas",
                column: "CodigoDespesaProgramada");

            migrationBuilder.CreateIndex(
                name: "IX_despesas_CodigoUsuario",
                table: "despesas",
                column: "CodigoUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_despesas_parceladas_CodigoUsuario",
                table: "despesas_parceladas",
                column: "CodigoUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_despesas_programadas_CodigoUsuario",
                table: "despesas_programadas",
                column: "CodigoUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_despesas_despesas_parceladas_CodigoDespesaParcelada",
                table: "despesas",
                column: "CodigoDespesaParcelada",
                principalTable: "despesas_parceladas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_despesas_despesas_programadas_CodigoDespesaProgramada",
                table: "despesas",
                column: "CodigoDespesaProgramada",
                principalTable: "despesas_programadas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_despesas_users_CodigoUsuario",
                table: "despesas",
                column: "CodigoUsuario",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_despesas_despesas_parceladas_CodigoDespesaParcelada",
                table: "despesas");

            migrationBuilder.DropForeignKey(
                name: "FK_despesas_despesas_programadas_CodigoDespesaProgramada",
                table: "despesas");

            migrationBuilder.DropForeignKey(
                name: "FK_despesas_users_CodigoUsuario",
                table: "despesas");

            migrationBuilder.DropTable(
                name: "despesas_parceladas");

            migrationBuilder.DropTable(
                name: "despesas_programadas");

            migrationBuilder.DropIndex(
                name: "IX_despesas_CodigoDespesaParcelada",
                table: "despesas");

            migrationBuilder.DropIndex(
                name: "IX_despesas_CodigoDespesaProgramada",
                table: "despesas");

            migrationBuilder.DropIndex(
                name: "IX_despesas_CodigoUsuario",
                table: "despesas");

            migrationBuilder.DropColumn(
                name: "CodigoDespesaParcelada",
                table: "despesas");

            migrationBuilder.DropColumn(
                name: "CodigoDespesaProgramada",
                table: "despesas");

            migrationBuilder.DropColumn(
                name: "CodigoUsuario",
                table: "despesas");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}

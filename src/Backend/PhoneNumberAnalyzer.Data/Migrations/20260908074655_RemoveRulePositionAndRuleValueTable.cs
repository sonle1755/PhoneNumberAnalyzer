using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneNumberAnalyzer.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRulePositionAndRuleValueTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatternRulePositions");

            migrationBuilder.DropTable(
                name: "PatternRuleValues");

            migrationBuilder.AddColumn<int>(
                name: "ReferencePosition",
                table: "PatternRules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int[]>(
                name: "TargetPositions",
                table: "PatternRules",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);

            migrationBuilder.AddColumn<string[]>(
                name: "Values",
                table: "PatternRules",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferencePosition",
                table: "PatternRules");

            migrationBuilder.DropColumn(
                name: "TargetPositions",
                table: "PatternRules");

            migrationBuilder.DropColumn(
                name: "Values",
                table: "PatternRules");

            migrationBuilder.CreateTable(
                name: "PatternRulePositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatternRuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsReference = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Position = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatternRulePositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatternRulePositions_PatternRules_PatternRuleId",
                        column: x => x.PatternRuleId,
                        principalTable: "PatternRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatternRuleValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatternRuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatternRuleValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatternRuleValues_PatternRules_PatternRuleId",
                        column: x => x.PatternRuleId,
                        principalTable: "PatternRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatternRulePositions_PatternRuleId",
                table: "PatternRulePositions",
                column: "PatternRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_PatternRuleValues_PatternRuleId",
                table: "PatternRuleValues",
                column: "PatternRuleId");
        }
    }
}

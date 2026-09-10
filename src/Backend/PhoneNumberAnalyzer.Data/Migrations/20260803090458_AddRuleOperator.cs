using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneNumberAnalyzer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRuleOperator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RuleOperator",
                table: "PatternRuleGroups",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RuleOperator",
                table: "PatternRuleGroups");
        }
    }
}

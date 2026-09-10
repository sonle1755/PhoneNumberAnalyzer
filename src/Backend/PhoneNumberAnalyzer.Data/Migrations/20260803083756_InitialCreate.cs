using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PhoneNumberAnalyzer.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    AvatarUrl = table.Column<string>(type: "text", nullable: true),
                    EmailVerified = table.Column<bool>(type: "boolean", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastLoginAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatternTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OwnerId = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatternTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatternTemplates_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAuthProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Provider = table.Column<string>(type: "text", nullable: false),
                    ProviderUserId = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuthProviders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAuthProviders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatternRuleGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    PatternTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatternRuleGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatternRuleGroups_PatternRuleGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "PatternRuleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatternRuleGroups_PatternTemplates_PatternTemplateId",
                        column: x => x.PatternTemplateId,
                        principalTable: "PatternTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatternRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    RuleType = table.Column<int>(type: "integer", nullable: false),
                    Length = table.Column<int>(type: "integer", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatternRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatternRules_PatternRuleGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "PatternRuleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    Value = table.Column<string>(type: "text", nullable: false),
                    PatternRuleId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "IX_PatternRuleGroups_ParentId",
                table: "PatternRuleGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PatternRuleGroups_PatternTemplateId",
                table: "PatternRuleGroups",
                column: "PatternTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_PatternRulePositions_PatternRuleId",
                table: "PatternRulePositions",
                column: "PatternRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_PatternRules_GroupId",
                table: "PatternRules",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PatternRuleValues_PatternRuleId",
                table: "PatternRuleValues",
                column: "PatternRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_PatternTemplates_OwnerId",
                table: "PatternTemplates",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthProviders_UserId",
                table: "UserAuthProviders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatternRulePositions");

            migrationBuilder.DropTable(
                name: "PatternRuleValues");

            migrationBuilder.DropTable(
                name: "UserAuthProviders");

            migrationBuilder.DropTable(
                name: "PatternRules");

            migrationBuilder.DropTable(
                name: "PatternRuleGroups");

            migrationBuilder.DropTable(
                name: "PatternTemplates");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

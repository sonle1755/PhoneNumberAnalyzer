using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PhoneNumberAnalyzer.Data.Migrations
{
    /// <inheritdoc />
    public partial class SwitchUserIdToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Id", table: "UserAuthProviders");

            migrationBuilder.AddColumn<Guid>(
                    name: "Id",
                    table: "UserAuthProviders",
                    type: "uuid",
                    nullable: false);

            migrationBuilder.AddPrimaryKey(name: "PK_UserAuthProviders", "UserAuthProviders", column: "Id");

            migrationBuilder.DropForeignKey(name: "FK_PatternTemplates_Users_OwnerId",
                                            table: "PatternTemplates");
            migrationBuilder.DropForeignKey(name: "FK_UserAuthProviders_Users_UserId",
                                            table: "UserAuthProviders");

            migrationBuilder.DropColumn(name: "Id", table: "Users");

            migrationBuilder.AddColumn<Guid>(
                    name: "Id",
                    table: "Users",
                    type: "uuid",
                    nullable: false);

            migrationBuilder.AddPrimaryKey(name: "PK_Users", "Users", column: "Id");

            migrationBuilder.DropColumn(name: "UserId", table: "UserAuthProviders");

            migrationBuilder.AddColumn<Guid>(name: "UserId",
                                             table: "UserAuthProviders",
                                             type: "uuid",
                                             nullable: false);

            migrationBuilder.AddForeignKey(name: "FK_UserAuthProviders_Users_UserId",
                                           table: "UserAuthProviders",
                                           column: "UserId",
                                           principalTable: "Users",
                                           principalColumn: "Id",
                                           onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropColumn(name: "OwnerId", table: "PatternTemplates");

            migrationBuilder.AddColumn<Guid>(name: "OwnerId", table: "PatternTemplates", type: "uuid", nullable: true);

            migrationBuilder.AddForeignKey(name: "FK_PatternTemplates_Users_OwnerId",
                                           table: "PatternTemplates",
                                           column: "OwnerId",
                                           principalTable: "Users",
                                           principalColumn: "Id",
                                           onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserAuthProviders",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserAuthProviders",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "PatternTemplates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FansPen.Domain.Migrations
{
    public partial class fixDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserTopic");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Fanfics");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Topics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Ratings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ApplicationUserId",
                table: "Ratings",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_ApplicationUserId",
                table: "Ratings",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_ApplicationUserId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_ApplicationUserId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Ratings");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Fanfics",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUserTopic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    TopicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserTopic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserTopic_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserTopic_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTopic_ApplicationUserId",
                table: "ApplicationUserTopic",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTopic_TopicId",
                table: "ApplicationUserTopic",
                column: "TopicId");
        }
    }
}

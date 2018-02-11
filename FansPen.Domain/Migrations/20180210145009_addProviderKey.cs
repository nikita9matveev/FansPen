using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FansPen.Domain.Migrations
{
    public partial class addProviderKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId1",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Fanfics_AspNetUsers_UserId1",
                table: "Fanfics");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_UserId1",
                table: "Likes");

            migrationBuilder.DropTable(
                name: "UserTopic");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Likes",
                newName: "ApplicationUserId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Likes",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserId1",
                table: "Likes",
                newName: "IX_Likes_ApplicationUserId1");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Fanfics",
                newName: "ApplicationUserId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Fanfics",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Fanfics_UserId1",
                table: "Fanfics",
                newName: "IX_Fanfics_ApplicationUserId1");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Comments",
                newName: "ApplicationUserId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId1",
                table: "Comments",
                newName: "IX_Comments_ApplicationUserId1");

            migrationBuilder.AddColumn<string>(
                name: "ProviderKey",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUserTopic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<int>(nullable: false),
                    ApplicationUserId1 = table.Column<string>(nullable: true),
                    TopicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserTopic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserTopic_AspNetUsers_ApplicationUserId1",
                        column: x => x.ApplicationUserId1,
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
                name: "IX_ApplicationUserTopic_ApplicationUserId1",
                table: "ApplicationUserTopic",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTopic_TopicId",
                table: "ApplicationUserTopic",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId1",
                table: "Comments",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fanfics_AspNetUsers_ApplicationUserId1",
                table: "Fanfics",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_ApplicationUserId1",
                table: "Likes",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId1",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Fanfics_AspNetUsers_ApplicationUserId1",
                table: "Fanfics");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_ApplicationUserId1",
                table: "Likes");

            migrationBuilder.DropTable(
                name: "ApplicationUserTopic");

            migrationBuilder.DropColumn(
                name: "ProviderKey",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId1",
                table: "Likes",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Likes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_ApplicationUserId1",
                table: "Likes",
                newName: "IX_Likes_UserId1");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId1",
                table: "Fanfics",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Fanfics",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Fanfics_ApplicationUserId1",
                table: "Fanfics",
                newName: "IX_Fanfics_UserId1");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId1",
                table: "Comments",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Comments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ApplicationUserId1",
                table: "Comments",
                newName: "IX_Comments_UserId1");

            migrationBuilder.CreateTable(
                name: "UserTopic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TopicId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTopic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTopic_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTopic_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTopic_TopicId",
                table: "UserTopic",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTopic_UserId1",
                table: "UserTopic",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId1",
                table: "Comments",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fanfics_AspNetUsers_UserId1",
                table: "Fanfics",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_UserId1",
                table: "Likes",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

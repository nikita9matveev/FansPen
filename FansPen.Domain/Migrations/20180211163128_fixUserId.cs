using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FansPen.Domain.Migrations
{
    public partial class fixUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserTopic_AspNetUsers_ApplicationUserId1",
                table: "ApplicationUserTopic");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_ApplicationUserId1",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_ApplicationUserId1",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserTopic_ApplicationUserId1",
                table: "ApplicationUserTopic");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "ApplicationUserTopic");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Likes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "ApplicationUserTopic",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Likes_ApplicationUserId",
                table: "Likes",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTopic_ApplicationUserId",
                table: "ApplicationUserTopic",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserTopic_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserTopic",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_ApplicationUserId",
                table: "Likes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserTopic_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserTopic");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_ApplicationUserId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_ApplicationUserId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserTopic_ApplicationUserId",
                table: "ApplicationUserTopic");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Likes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Likes",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "ApplicationUserTopic",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "ApplicationUserTopic",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_ApplicationUserId1",
                table: "Likes",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTopic_ApplicationUserId1",
                table: "ApplicationUserTopic",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserTopic_AspNetUsers_ApplicationUserId1",
                table: "ApplicationUserTopic",
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
    }
}

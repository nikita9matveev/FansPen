using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FansPen.Domain.Migrations
{
    public partial class fixedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Fanfics_FanficId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Imgs_Topics_TopicId",
                table: "Imgs");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Topics_TopicId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Fanfics_FanficId",
                table: "Topics");

            migrationBuilder.RenameColumn(
                name: "Style",
                table: "AspNetUsers",
                newName: "Sex");

            migrationBuilder.RenameColumn(
                name: "Lang",
                table: "AspNetUsers",
                newName: "SecondName");

            migrationBuilder.RenameColumn(
                name: "Interests",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.AlterColumn<int>(
                name: "FanficId",
                table: "Topics",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "Ratings",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "Imgs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FanficId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Fanfics_FanficId",
                table: "Comments",
                column: "FanficId",
                principalTable: "Fanfics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Imgs_Topics_TopicId",
                table: "Imgs",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Topics_TopicId",
                table: "Ratings",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Fanfics_FanficId",
                table: "Topics",
                column: "FanficId",
                principalTable: "Fanfics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Fanfics_FanficId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Imgs_Topics_TopicId",
                table: "Imgs");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Topics_TopicId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Fanfics_FanficId",
                table: "Topics");

            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "AspNetUsers",
                newName: "Style");

            migrationBuilder.RenameColumn(
                name: "SecondName",
                table: "AspNetUsers",
                newName: "Lang");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "Interests");

            migrationBuilder.AlterColumn<int>(
                name: "FanficId",
                table: "Topics",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "Ratings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "Imgs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FanficId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Fanfics_FanficId",
                table: "Comments",
                column: "FanficId",
                principalTable: "Fanfics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Imgs_Topics_TopicId",
                table: "Imgs",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Topics_TopicId",
                table: "Ratings",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Fanfics_FanficId",
                table: "Topics",
                column: "FanficId",
                principalTable: "Fanfics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

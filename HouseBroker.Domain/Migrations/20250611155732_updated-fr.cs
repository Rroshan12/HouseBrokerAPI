using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseBroker.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updatedfr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyListing_AspNetUsers_BrokerId1",
                table: "PropertyListing");

            migrationBuilder.DropIndex(
                name: "IX_PropertyListing_BrokerId1",
                table: "PropertyListing");

            migrationBuilder.DropColumn(
                name: "BrokerId1",
                table: "PropertyListing");

            migrationBuilder.AlterColumn<Guid>(
                name: "BrokerId",
                table: "PropertyListing",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyListing_BrokerId",
                table: "PropertyListing",
                column: "BrokerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyListing_AspNetUsers_BrokerId",
                table: "PropertyListing",
                column: "BrokerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyListing_AspNetUsers_BrokerId",
                table: "PropertyListing");

            migrationBuilder.DropIndex(
                name: "IX_PropertyListing_BrokerId",
                table: "PropertyListing");

            migrationBuilder.AlterColumn<string>(
                name: "BrokerId",
                table: "PropertyListing",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "BrokerId1",
                table: "PropertyListing",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PropertyListing_BrokerId1",
                table: "PropertyListing",
                column: "BrokerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyListing_AspNetUsers_BrokerId1",
                table: "PropertyListing",
                column: "BrokerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

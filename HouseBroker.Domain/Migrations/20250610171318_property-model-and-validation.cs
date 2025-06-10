using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseBroker.Domain.Migrations
{
    /// <inheritdoc />
    public partial class propertymodelandvalidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyListing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrokerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrokerId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyListing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyListing_AspNetUsers_BrokerId1",
                        column: x => x.BrokerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyImage_PropertyListing_PropertyListingId",
                        column: x => x.PropertyListingId,
                        principalTable: "PropertyListing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImage_PropertyListingId",
                table: "PropertyImage",
                column: "PropertyListingId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyListing_BrokerId1",
                table: "PropertyListing",
                column: "BrokerId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyImage");

            migrationBuilder.DropTable(
                name: "PropertyListing");
        }
    }
}

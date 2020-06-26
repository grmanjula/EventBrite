using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCatalogAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "Event_Items_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Event_Location_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Event_Types_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "EventLocations",
                columns: table => new
                {
                    EventLocationID = table.Column<int>(nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    Mode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLocations", x => x.EventLocationID);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EventItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    EventTypeId = table.Column<int>(nullable: false),
                    EventLocationID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EventItems_EventLocations_EventLocationID",
                        column: x => x.EventLocationID,
                        principalTable: "EventLocations",
                        principalColumn: "EventLocationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventItems_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventItems_EventLocationID",
                table: "EventItems",
                column: "EventLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_EventItems_EventTypeId",
                table: "EventItems",
                column: "EventTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventItems");

            migrationBuilder.DropTable(
                name: "EventLocations");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropSequence(
                name: "Event_Items_hilo");

            migrationBuilder.DropSequence(
                name: "Event_Location_hilo");

            migrationBuilder.DropSequence(
                name: "Event_Types_hilo");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCatalogAPI.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventItems_EventTypes_EventTypeId",
                table: "EventItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventTypes",
                table: "EventTypes");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "EventTypes");

            migrationBuilder.AddColumn<int>(
                name: "EventTypeId",
                table: "EventTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventTypes",
                table: "EventTypes",
                column: "EventTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventItems_EventTypes_EventTypeId",
                table: "EventItems",
                column: "EventTypeId",
                principalTable: "EventTypes",
                principalColumn: "EventTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventItems_EventTypes_EventTypeId",
                table: "EventItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventTypes",
                table: "EventTypes");

            migrationBuilder.DropColumn(
                name: "EventTypeId",
                table: "EventTypes");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "EventTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventTypes",
                table: "EventTypes",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EventItems_EventTypes_EventTypeId",
                table: "EventItems",
                column: "EventTypeId",
                principalTable: "EventTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

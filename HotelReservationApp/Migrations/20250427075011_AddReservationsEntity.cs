using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationApp.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: true),
                    GuestName = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "TEXT", nullable: false),
                    Deposit = table.Column<decimal>(type: "TEXT", nullable: true),
                    NumberOfAdults = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfChildren = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ReservationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}

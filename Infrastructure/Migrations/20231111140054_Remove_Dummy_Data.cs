using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Dummy_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("0e10cf5e-d56d-4086-b592-95f72f70dc34"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("2da35c41-7a77-4613-a08e-5df71b92bee6"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Content", "CreatedOn", "TimeWasted" },
                values: new object[,]
                {
                    { new Guid("0e10cf5e-d56d-4086-b592-95f72f70dc34"), "Task 2", new DateTime(2023, 11, 5, 0, 0, 0, 0, DateTimeKind.Local), new TimeSpan(0, 0, 0, 0, 0) },
                    { new Guid("2da35c41-7a77-4613-a08e-5df71b92bee6"), "Task 1", new DateTime(2023, 11, 5, 0, 0, 0, 0, DateTimeKind.Local), new TimeSpan(0, 0, 0, 0, 0) }
                });
        }
    }
}

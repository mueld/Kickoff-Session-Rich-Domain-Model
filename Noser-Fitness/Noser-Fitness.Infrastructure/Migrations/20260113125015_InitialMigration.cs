using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Noser_Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CourseType = table.Column<int>(type: "integer", nullable: false),
                    ScheduledAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaxAttendees = table.Column<int>(type: "integer", nullable: false),
                    InvitationsValidBeforeInHours = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Firstname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Lastname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attendee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendee_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    State_ModifiedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    State_Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitation_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CourseType", "CreatedAtUtc", "CreatedByMemberId", "InvitationsValidBeforeInHours", "Location", "MaxAttendees", "Name", "ScheduledAtUtc" },
                values: new object[,]
                {
                    { new Guid("b7e2c4a9-3f6d-4c8e-9a1b-2d5f7c0e4a10"), 0, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("7c9c9f51-6b2c-4f89-b5d4-1b4c8e2a1a01"), 24, "Main Gym", 20, "Morning Fitness", new DateTime(2026, 3, 10, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c2a9e5b1-7d3f-4b8c-9e6a-1f0d4c2b5e11"), 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a3f8d7c4-9e21-4c1f-b6e2-8d3c5b7a4f02"), 12, "Studio A", 1, "Trial Workout", new DateTime(2026, 3, 10, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f9a1c5e2-4b7d-4e6a-8c3f-0d1b2a9e7c12"), 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("e1b4c9a2-6d5f-4a88-9e3b-2f7c8d1a0b03"), 48, "Cycling Room", 50, "Indoor Cycling Blast", new DateTime(2026, 3, 10, 8, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Email", "Firstname", "Lastname" },
                values: new object[,]
                {
                    { new Guid("7c9c9f51-6b2c-4f89-b5d4-1b4c8e2a1a01"), "john.doe@example.com", "John", "Doe" },
                    { new Guid("a3f8d7c4-9e21-4c1f-b6e2-8d3c5b7a4f02"), "jane.smith@example.com", "Jane", "Smith" },
                    { new Guid("e1b4c9a2-6d5f-4a88-9e3b-2f7c8d1a0b03"), "alex.miller@example.com", "Alex", "Miller" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendee_CourseId",
                table: "Attendee",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_CourseId",
                table: "Invitation",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendee");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}

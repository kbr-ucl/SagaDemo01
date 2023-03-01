using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleBatch.Service.Migrations
{
    /// <inheritdoc />
    public partial class MyFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BatchStates",
                columns: table => new
                {
                    BatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiveTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveThreshold = table.Column<int>(type: "int", nullable: true),
                    Total = table.Column<int>(type: "int", nullable: true),
                    ScheduledId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnprocessedOrderIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessingOrderIds = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchStates", x => x.BatchId);
                });

            migrationBuilder.CreateTable(
                name: "JobStates",
                columns: table => new
                {
                    BatchJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiveTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStates", x => x.BatchJobId);
                    table.ForeignKey(
                        name: "FK_JobStates_BatchStates_BatchId",
                        column: x => x.BatchId,
                        principalTable: "BatchStates",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobStates_BatchId",
                table: "JobStates",
                column: "BatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobStates");

            migrationBuilder.DropTable(
                name: "BatchStates");
        }
    }
}

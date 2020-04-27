using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPiggyBank.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationCategories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<decimal>(nullable: false),
                    Currency = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CyclicOperations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ResourceId = table.Column<Guid>(nullable: false),
                    OperationCategoryId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsIncome = table.Column<bool>(nullable: false),
                    EstimatedValue = table.Column<decimal>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    NextOccurence = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CyclicOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CyclicOperations_OperationCategories_OperationCategoryId",
                        column: x => x.OperationCategoryId,
                        principalTable: "OperationCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CyclicOperations_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ResourceId = table.Column<Guid>(nullable: false),
                    OperationCategoryId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsIncome = table.Column<bool>(nullable: false),
                    Value = table.Column<decimal>(nullable: false),
                    OccuredAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_OperationCategories_OperationCategoryId",
                        column: x => x.OperationCategoryId,
                        principalTable: "OperationCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operations_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CyclicOperations_OperationCategoryId",
                table: "CyclicOperations",
                column: "OperationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CyclicOperations_ResourceId",
                table: "CyclicOperations",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationCategories_UserId",
                table: "OperationCategories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_OperationCategoryId",
                table: "Operations",
                column: "OperationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_ResourceId",
                table: "Operations",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_UserId",
                table: "Resources",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CyclicOperations");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "OperationCategories");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

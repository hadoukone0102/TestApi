using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApi.Migrations
{
    /// <inheritdoc />
    public partial class InitCoursCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cours",
                table: "Cours");

            migrationBuilder.RenameTable(
                name: "Cours",
                newName: "Courses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Cours");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cours",
                table: "Cours",
                column: "Id");
        }
    }
}

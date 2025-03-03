using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApi.Migrations
{
    /// <inheritdoc />
    public partial class CoursCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoursTeacher = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cours", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cours");
        }
    }
}

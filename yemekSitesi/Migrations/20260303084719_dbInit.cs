using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace yemekSitesi.Migrations
{
    /// <inheritdoc />
    public partial class dbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tarifler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", nullable: true),
                    Malzemeler = table.Column<string>(type: "TEXT", nullable: true),
                    Yonerge = table.Column<string>(type: "TEXT", nullable: true),
                    Foto = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifler", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarifler");
        }
    }
}

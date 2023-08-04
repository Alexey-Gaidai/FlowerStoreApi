using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerStoreApi.Migrations
{
    public partial class thirteenth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "OrderedBouquets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "OrderedBouquets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

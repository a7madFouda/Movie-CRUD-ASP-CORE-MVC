using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coreMVC.Migrations
{
    public partial class InsertGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Genre values ('Action')");
            migrationBuilder.Sql("insert into Genre values ('Comedy')");
            migrationBuilder.Sql("insert into Genre values ('Drama')");
            migrationBuilder.Sql("insert into Genre values ('Romantic')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From Genre");
        }
    }
}

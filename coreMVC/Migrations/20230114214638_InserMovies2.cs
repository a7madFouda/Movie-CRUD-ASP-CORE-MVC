using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coreMVC.Migrations
{
    public partial class InserMovies2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                                    insert into Movie
                                    select 'Dream',2024,9.9,'My Future Life Style',bulkcolumn,1
                                    from openrowset
                                    (bulk 'C:\Users\Ahmed\Desktop\1.jpg',single_blob)
                                    as img
                                ");
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From Movie");
        }
    }
}

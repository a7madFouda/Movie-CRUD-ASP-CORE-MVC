using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coreMVC.Migrations
{
    public partial class InsertMovies : Migration
    {
        byte[] img = File.ReadAllBytes(@"C:\Users\Ahmed\Desktop\index.jpg");
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql($"insert into Movie values ('Tree',2015,7.9,'Long Story Short its amazing',{img},2)");
            migrationBuilder.Sql(@"
                                    insert into Movie
                                    select 'Flower',2055,7.9,'Long Story Short its amazing Much more AnyThing',BulkColumn,3
                                    from openrowset
                                    (
                                    bulk 'C:\Users\Ahmed\Desktop\index.jpg',SINGLE_blob
                                    )AS Poster
                                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From Movie");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manager.DAL.Migrations
{
    public partial class seeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                    table: "Categories",
                    columns: new[] { "Id", "FirstName", "LastName", "UserName", "PhoneNumber" , "PasswordHash" },
                    values: new object[] { "13bc8ffd-e44e-40f1-98f0-f9c656cc4700", "Charles", "Troy","Charles" ,"01273119777", "AQAAAAEAACcQAAAAEMyS4zQCrdIHle/7e6C792W/MD5ikB+6Prqv7OQu70jPhXkRFxeBH/l55uiTG6csMQ==" }

                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "Delete From Employees"

                );

        }
    }
}

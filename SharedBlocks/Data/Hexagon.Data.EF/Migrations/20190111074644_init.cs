using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hexagon.Data.EF.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organization_Employee",
                columns: table => new
                {
                    Employee_Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Employee_Name = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    Certificate_Type = table.Column<string>(nullable: true),
                    Certificate_Code = table.Column<string>(nullable: true),
                    Contact_Phone = table.Column<string>(nullable: true),
                    Office_Telephone1 = table.Column<string>(nullable: true),
                    Office_Telephone2 = table.Column<string>(nullable: true),
                    Office_Fax = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Home_Telephone = table.Column<string>(nullable: true),
                    Home_Address = table.Column<string>(nullable: true),
                    Duty_Desc = table.Column<string>(nullable: true),
                    Organization_Id = table.Column<string>(nullable: true),
                    Room_Num = table.Column<string>(nullable: true),
                    Driver_Phone = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Secretary_Phone = table.Column<string>(nullable: true),
                    Isvalid = table.Column<int>(nullable: true),
                    ESPACE_ACCOUNT = table.Column<string>(nullable: true),
                    SERVICE_NUMBER = table.Column<string>(nullable: true),
                    Uc_Enable = table.Column<int>(nullable: true),
                    Created_Date = table.Column<DateTime>(nullable: true),
                    Modified_Date = table.Column<DateTime>(nullable: true),
                    HeadIcon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization_Employee", x => x.Employee_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Organization_Employee");
        }
    }
}

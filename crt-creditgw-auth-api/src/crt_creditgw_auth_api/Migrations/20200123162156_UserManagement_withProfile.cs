using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace crt_creditgw_auth_api.Migrations
{
    public partial class UserManagement_withProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ProfileCompleted",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    ExternalGUID = table.Column<string>(nullable: true),
                    MembershipNumber = table.Column<string>(maxLength: 8, nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    VatNumber = table.Column<string>(nullable: true),
                    UserFirstName = table.Column<string>(maxLength: 100, nullable: false),
                    UserMiddleName = table.Column<string>(maxLength: 100, nullable: true),
                    UserLastName = table.Column<string>(maxLength: 100, nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    UserDob = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropColumn(
                name: "ProfileCompleted",
                table: "AspNetUsers");
        }
    }
}

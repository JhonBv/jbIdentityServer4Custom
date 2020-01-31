using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace crt_creditgw_auth_api.Migrations
{
    public partial class UserManagement_Full_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalGUID",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "MembershipNumber",
                table: "UserProfile");

            migrationBuilder.AddColumn<string>(
                name: "userLoginId",
                table: "UserProfile",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userLoginId = table.Column<string>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userLoginId = table.Column<string>(nullable: false),
                    AddressLine1 = table.Column<string>(maxLength: 100, nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    AddressLine3 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(maxLength: 10, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessProfile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userLoginId = table.Column<string>(nullable: false),
                    CompanyNumber = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: false),
                    RegisteredOfficeAddressId = table.Column<int>(nullable: true),
                    TradingAddressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessProfile_UserAddress_RegisteredOfficeAddressId",
                        column: x => x.RegisteredOfficeAddressId,
                        principalTable: "UserAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusinessProfile_UserAddress_TradingAddressId",
                        column: x => x.TradingAddressId,
                        principalTable: "UserAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    BusinessProfileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessCategory_BusinessProfile_BusinessProfileId",
                        column: x => x.BusinessProfileId,
                        principalTable: "BusinessProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessOfficer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    DateAppointed = table.Column<DateTime>(nullable: false),
                    Resigned = table.Column<DateTime>(nullable: false),
                    Source = table.Column<string>(nullable: true),
                    BusinessProfileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessOfficer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessOfficer_BusinessProfile_BusinessProfileId",
                        column: x => x.BusinessProfileId,
                        principalTable: "BusinessProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SICCode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SicCode = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    BusinessProfileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SICCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SICCode_BusinessProfile_BusinessProfileId",
                        column: x => x.BusinessProfileId,
                        principalTable: "BusinessProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCategory_BusinessProfileId",
                table: "BusinessCategory",
                column: "BusinessProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOfficer_BusinessProfileId",
                table: "BusinessOfficer",
                column: "BusinessProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProfile_RegisteredOfficeAddressId",
                table: "BusinessProfile",
                column: "RegisteredOfficeAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProfile_TradingAddressId",
                table: "BusinessProfile",
                column: "TradingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_SICCode_BusinessProfileId",
                table: "SICCode",
                column: "BusinessProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessCategory");

            migrationBuilder.DropTable(
                name: "BusinessOfficer");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "SICCode");

            migrationBuilder.DropTable(
                name: "BusinessProfile");

            migrationBuilder.DropTable(
                name: "UserAddress");

            migrationBuilder.DropColumn(
                name: "userLoginId",
                table: "UserProfile");

            migrationBuilder.AddColumn<string>(
                name: "ExternalGUID",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MembershipNumber",
                table: "UserProfile",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true);
        }
    }
}

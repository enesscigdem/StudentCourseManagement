using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCourseManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "087e06eb-5485-43bc-913e-db8daf8a9208", "AQAAAAIAAYagAAAAEN6QDjMJyEXfVbi1tJK1MFBohbAZgf8gaWEG1zLPFxtQJBwEDNxcTl3fKijp+hbwag==", "dedcf8ac-00b4-496b-a111-58bd464c42cd", "admin@itb.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "c0b0304c-9127-4f7a-b9b7-67ce63bee0d1", "AQAAAAIAAYagAAAAEKVyQRY/6MLDZQDyvoBCdpI+Q4ICG5zIKptQFmZmOjbWwXgunSb+r77rXg2Gt94uXA==", "dda00b40-d102-40da-ab55-219de44e46de", "admin@example.com" });
        }
    }
}

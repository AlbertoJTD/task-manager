using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class AdminRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS(SELECT Id FROM AspNetRoles WHERE Id = 'f03aa6c1-f77c-4a04-8e4c-b3d6b8b43dbd')
                BEGIN
	                INSERT AspNetRoles (Id, [Name], [NormalizedName])
	                VALUES ('f03aa6c1-f77c-4a04-8e4c-b3d6b8b43dbd', 'admin', 'ADMIN')
                END");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE AspNetRoles WHERE Id = 'f03aa6c1-f77c-4a04-8e4c-b3d6b8b43dbd'");
        }
    }
}

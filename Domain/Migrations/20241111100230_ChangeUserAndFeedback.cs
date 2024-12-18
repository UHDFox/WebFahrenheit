using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserAndFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Clients_ClientId",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Feedbacks",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_ClientId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_UserId");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Clients",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "Mail",
                table: "Clients",
                newName: "Email");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:user_role", "super_admin,high_level_admin,low_level_admin,user");

            migrationBuilder.AddColumn<byte>(
                name: "Role",
                table: "Clients",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Clients_UserId",
                table: "Feedbacks",
                column: "UserId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Clients_UserId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Feedbacks",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_ClientId");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Clients",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Clients",
                newName: "Mail");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:user_role", "super_admin,high_level_admin,low_level_admin,user");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Clients_ClientId",
                table: "Feedbacks",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

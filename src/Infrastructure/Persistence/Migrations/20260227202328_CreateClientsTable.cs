using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateClientsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "clients");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "clients",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "clients",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Cnpj",
                table: "clients",
                newName: "cnpj");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "clients",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "clients",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "clients",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_Cnpj",
                table: "clients",
                newName: "IX_clients_cnpj");

            migrationBuilder.AddPrimaryKey(
                name: "client_id",
                table: "clients",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "client_id",
                table: "clients");

            migrationBuilder.RenameTable(
                name: "clients",
                newName: "Clients");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Clients",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Clients",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "cnpj",
                table: "Clients",
                newName: "Cnpj");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "Clients",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Clients",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Clients",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_clients_cnpj",
                table: "Clients",
                newName: "IX_Clients_Cnpj");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");
        }
    }
}

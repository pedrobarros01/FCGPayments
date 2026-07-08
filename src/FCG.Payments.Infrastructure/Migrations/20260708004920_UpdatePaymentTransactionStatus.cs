using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FCG.Payments.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaymentTransactionStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransaction_PaymentTransactionStatus_StatusTransacti~",
                table: "PaymentTransaction");

            migrationBuilder.DropTable(
                name: "PaymentTransactionStatus");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransaction_StatusTransactionId",
                table: "PaymentTransaction");

            migrationBuilder.DropColumn(
                name: "StatusTransactionId",
                table: "PaymentTransaction");

            migrationBuilder.AddColumn<string>(
                name: "StatusTransaction",
                table: "PaymentTransaction",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusTransaction",
                table: "PaymentTransaction");

            migrationBuilder.AddColumn<Guid>(
                name: "StatusTransactionId",
                table: "PaymentTransaction",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PaymentTransactionStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactionStatus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PaymentTransactionStatus",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Referente a status de pagamento ser aprovado", "Aprovado" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Referente a status de pagamento ser reprovado", "Reprovado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransaction_StatusTransactionId",
                table: "PaymentTransaction",
                column: "StatusTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransaction_PaymentTransactionStatus_StatusTransacti~",
                table: "PaymentTransaction",
                column: "StatusTransactionId",
                principalTable: "PaymentTransactionStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

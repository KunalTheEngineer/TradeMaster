using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradeMaster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Watchlists_StockId",
                table: "Watchlists",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Watchlists_UserId",
                table: "Watchlists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StockId",
                table: "Orders",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Holdings_StockID",
                table: "Holdings",
                column: "StockID");

            migrationBuilder.CreateIndex(
                name: "IX_Holdings_UserID",
                table: "Holdings",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Holdings_Stocks_StockID",
                table: "Holdings",
                column: "StockID",
                principalTable: "Stocks",
                principalColumn: "StockID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holdings_Users_UserID",
                table: "Holdings",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "USerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Stocks_StockId",
                table: "Orders",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "StockID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "USerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Watchlists_Stocks_StockId",
                table: "Watchlists",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "StockID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Watchlists_Users_UserId",
                table: "Watchlists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "USerID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holdings_Stocks_StockID",
                table: "Holdings");

            migrationBuilder.DropForeignKey(
                name: "FK_Holdings_Users_UserID",
                table: "Holdings");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Stocks_StockId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Watchlists_Stocks_StockId",
                table: "Watchlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Watchlists_Users_UserId",
                table: "Watchlists");

            migrationBuilder.DropIndex(
                name: "IX_Watchlists_StockId",
                table: "Watchlists");

            migrationBuilder.DropIndex(
                name: "IX_Watchlists_UserId",
                table: "Watchlists");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StockId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Holdings_StockID",
                table: "Holdings");

            migrationBuilder.DropIndex(
                name: "IX_Holdings_UserID",
                table: "Holdings");
        }
    }
}

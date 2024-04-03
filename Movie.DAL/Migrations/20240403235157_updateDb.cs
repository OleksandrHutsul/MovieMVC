using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmCategories_Categories_CategoriesId",
                table: "FilmCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmCategories_Films_FilmsId",
                table: "FilmCategories");

            migrationBuilder.AlterColumn<int>(
                name: "FilmsId",
                table: "FilmCategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "FilmId",
                table: "FilmCategories",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte>(
                name: "CategoryId",
                table: "FilmCategories",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriesId",
                table: "FilmCategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "ParentCategoryId",
                table: "Categories",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmCategories_Categories_CategoriesId",
                table: "FilmCategories",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmCategories_Films_FilmsId",
                table: "FilmCategories",
                column: "FilmsId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmCategories_Categories_CategoriesId",
                table: "FilmCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmCategories_Films_FilmsId",
                table: "FilmCategories");

            migrationBuilder.AlterColumn<int>(
                name: "FilmsId",
                table: "FilmCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FilmId",
                table: "FilmCategories",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "FilmCategories",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriesId",
                table: "FilmCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ParentCategoryId",
                table: "Categories",
                type: "int",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmCategories_Categories_CategoriesId",
                table: "FilmCategories",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FilmCategories_Films_FilmsId",
                table: "FilmCategories",
                column: "FilmsId",
                principalTable: "Films",
                principalColumn: "Id");
        }
    }
}

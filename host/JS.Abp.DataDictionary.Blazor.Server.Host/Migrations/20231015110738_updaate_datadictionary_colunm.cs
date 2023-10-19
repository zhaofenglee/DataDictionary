using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JS.Abp.DataDictionary.Blazor.Server.Host.Migrations
{
    /// <inheritdoc />
    public partial class updaatedatadictionarycolunm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpDataDictionaryItems_AbpDataDictionaries_DataDictionaryId",
                table: "AbpDataDictionaryItems");

            migrationBuilder.RenameColumn(
                name: "IsStatic",
                table: "AbpDataDictionaryItems",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsStatic",
                table: "AbpDataDictionaries",
                newName: "IsActive");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "AbpDataDictionaryItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<Guid>(
                name: "DataDictionaryId",
                table: "AbpDataDictionaryItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AbpDataDictionaryItems",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<Guid>(
                name: "DataDictionaryId1",
                table: "AbpDataDictionaryItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpDataDictionaryItems_DataDictionaryId1",
                table: "AbpDataDictionaryItems",
                column: "DataDictionaryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpDataDictionaryItems_AbpDataDictionaries_DataDictionaryId",
                table: "AbpDataDictionaryItems",
                column: "DataDictionaryId",
                principalTable: "AbpDataDictionaries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpDataDictionaryItems_AbpDataDictionaries_DataDictionaryId1",
                table: "AbpDataDictionaryItems",
                column: "DataDictionaryId1",
                principalTable: "AbpDataDictionaries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpDataDictionaryItems_AbpDataDictionaries_DataDictionaryId",
                table: "AbpDataDictionaryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpDataDictionaryItems_AbpDataDictionaries_DataDictionaryId1",
                table: "AbpDataDictionaryItems");

            migrationBuilder.DropIndex(
                name: "IX_AbpDataDictionaryItems_DataDictionaryId1",
                table: "AbpDataDictionaryItems");

            migrationBuilder.DropColumn(
                name: "DataDictionaryId1",
                table: "AbpDataDictionaryItems");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AbpDataDictionaryItems",
                newName: "IsStatic");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AbpDataDictionaries",
                newName: "IsStatic");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "AbpDataDictionaryItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DataDictionaryId",
                table: "AbpDataDictionaryItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AbpDataDictionaryItems",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpDataDictionaryItems_AbpDataDictionaries_DataDictionaryId",
                table: "AbpDataDictionaryItems",
                column: "DataDictionaryId",
                principalTable: "AbpDataDictionaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

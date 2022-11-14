﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNETCoreMVC.Migrations
{
    /// <inheritdoc />
    public partial class TableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "_content",
                table: "_tasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "_tasks",
                columns: new[] { "_id", "_content", "_created_At", "_title" },
                values: new object[] { 2, "Autogenerated content", new DateTime(2022, 11, 13, 12, 31, 4, 920, DateTimeKind.Local).AddTicks(501), "Autogenerated Title" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "_tasks",
                keyColumn: "_id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "_content",
                table: "_tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
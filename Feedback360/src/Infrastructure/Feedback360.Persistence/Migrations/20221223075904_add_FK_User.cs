using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedback360.Persistence.Migrations
{
    public partial class add_FK_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("1babd057-e980-4cb3-9cd2-7fdd9e525668"),
                column: "Date",
                value: new DateTime(2023, 10, 23, 7, 59, 3, 390, DateTimeKind.Utc).AddTicks(1019));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("3448d5a4-0f72-4dd7-bf15-c14a46b26c00"),
                column: "Date",
                value: new DateTime(2023, 9, 23, 7, 59, 3, 390, DateTimeKind.Utc).AddTicks(967));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("62787623-4c52-43fe-b0c9-b7044fb5929b"),
                column: "Date",
                value: new DateTime(2023, 4, 23, 7, 59, 3, 390, DateTimeKind.Utc).AddTicks(1003));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("adc42c09-08c1-4d2c-9f96-2d15bb1af299"),
                column: "Date",
                value: new DateTime(2023, 8, 23, 7, 59, 3, 390, DateTimeKind.Utc).AddTicks(1134));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b419a7ca-3321-4f38-be8e-4d7b6a529319"),
                column: "Date",
                value: new DateTime(2023, 4, 23, 7, 59, 3, 390, DateTimeKind.Utc).AddTicks(988));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("ee272f8b-6096-4cb6-8625-bb4bb2d89e8b"),
                column: "Date",
                value: new DateTime(2023, 6, 23, 7, 59, 3, 390, DateTimeKind.Utc).AddTicks(935));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("3dcb3ea0-80b1-4781-b5c0-4d85c41e55a6"),
                column: "OrderPlaced",
                value: new DateTime(2022, 12, 23, 7, 59, 3, 390, DateTimeKind.Utc).AddTicks(1229));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("771cca4b-066c-4ac7-b3df-4d12837fe7e0"),
                column: "OrderPlaced",
                value: new DateTime(2022, 12, 23, 7, 59, 3, 390, DateTimeKind.Utc).AddTicks(1212));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("7e94bc5b-71a5-4c8c-bc3b-71bb7976237e"),
                column: "OrderPlaced",
                value: new DateTime(2022, 12, 23, 7, 59, 3, 390, DateTimeKind.Utc).AddTicks(1168));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("86d3a045-b42d-4854-8150-d6a374948b6e"),
                column: "OrderPlaced",
                value: new DateTime(2022, 12, 23, 7, 59, 3, 390, DateTimeKind.Utc).AddTicks(1192));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("ba0eb0ef-b69b-46fd-b8e2-41b4178ae7cb"),
                column: "OrderPlaced",
                value: new DateTime(2022, 12, 23, 7, 59, 3, 390, DateTimeKind.Utc).AddTicks(1282));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("e6a2679c-79a3-4ef1-a478-6f4c91b405b6"),
                column: "OrderPlaced",
                value: new DateTime(2022, 12, 23, 7, 59, 3, 390, DateTimeKind.Utc).AddTicks(1246));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("f5a6a3a0-4227-4973-abb5-a63fbe725923"),
                column: "OrderPlaced",
                value: new DateTime(2022, 12, 23, 7, 59, 3, 390, DateTimeKind.Utc).AddTicks(1265));

            migrationBuilder.CreateIndex(
                name: "IX_Users_DeptId",
                table: "Users",
                column: "DeptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DeptId",
                table: "Users",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "DeptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DeptId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DeptId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("1babd057-e980-4cb3-9cd2-7fdd9e525668"),
                column: "Date",
                value: new DateTime(2023, 10, 23, 7, 51, 57, 865, DateTimeKind.Utc).AddTicks(1507));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("3448d5a4-0f72-4dd7-bf15-c14a46b26c00"),
                column: "Date",
                value: new DateTime(2023, 9, 23, 7, 51, 57, 865, DateTimeKind.Utc).AddTicks(1454));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("62787623-4c52-43fe-b0c9-b7044fb5929b"),
                column: "Date",
                value: new DateTime(2023, 4, 23, 7, 51, 57, 865, DateTimeKind.Utc).AddTicks(1490));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("adc42c09-08c1-4d2c-9f96-2d15bb1af299"),
                column: "Date",
                value: new DateTime(2023, 8, 23, 7, 51, 57, 865, DateTimeKind.Utc).AddTicks(1528));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b419a7ca-3321-4f38-be8e-4d7b6a529319"),
                column: "Date",
                value: new DateTime(2023, 4, 23, 7, 51, 57, 865, DateTimeKind.Utc).AddTicks(1472));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("ee272f8b-6096-4cb6-8625-bb4bb2d89e8b"),
                column: "Date",
                value: new DateTime(2023, 6, 23, 7, 51, 57, 865, DateTimeKind.Utc).AddTicks(1420));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("3dcb3ea0-80b1-4781-b5c0-4d85c41e55a6"),
                column: "OrderPlaced",
                value: new DateTime(2022, 12, 23, 7, 51, 57, 865, DateTimeKind.Utc).AddTicks(1607));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("771cca4b-066c-4ac7-b3df-4d12837fe7e0"),
                column: "OrderPlaced",
                value: new DateTime(2022, 12, 23, 7, 51, 57, 865, DateTimeKind.Utc).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("7e94bc5b-71a5-4c8c-bc3b-71bb7976237e"),
                column: "OrderPlaced",
                value: new DateTime(2022, 12, 23, 7, 51, 57, 865, DateTimeKind.Utc).AddTicks(1551));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("86d3a045-b42d-4854-8150-d6a374948b6e"),
                column: "OrderPlaced",
                value: new DateTime(2022, 12, 23, 7, 51, 57, 865, DateTimeKind.Utc).AddTicks(1574));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("ba0eb0ef-b69b-46fd-b8e2-41b4178ae7cb"),
                column: "OrderPlaced",
                value: new DateTime(2022, 12, 23, 7, 51, 57, 865, DateTimeKind.Utc).AddTicks(1749));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("e6a2679c-79a3-4ef1-a478-6f4c91b405b6"),
                column: "OrderPlaced",
                value: new DateTime(2022, 12, 23, 7, 51, 57, 865, DateTimeKind.Utc).AddTicks(1707));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("f5a6a3a0-4227-4973-abb5-a63fbe725923"),
                column: "OrderPlaced",
                value: new DateTime(2022, 12, 23, 7, 51, 57, 865, DateTimeKind.Utc).AddTicks(1731));
        }
    }
}

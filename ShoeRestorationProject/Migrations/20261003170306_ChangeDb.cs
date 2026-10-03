using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeRestorationProject.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Countries",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoe_Images_Shoes",
                table: "ShoeImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoe_Measurements_Measurement_Properties",
                table: "ShoeMeasurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoe_Measurements_Measurement_Values",
                table: "ShoeMeasurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoe_Measurements_Shoes",
                table: "ShoeMeasurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Brands",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Colors",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Conditions",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Shoe_Types",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Sizes",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Skin_Types",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sizes_Size_Metrics",
                table: "Sizes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles",
                table: "Users");

            migrationBuilder.DropTable(
                name: "MeasurementValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shoe_Sizes",
                table: "Sizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metrics",
                table: "SizeMetrics");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_SizeId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_Title",
                table: "Shoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Shoe_Ima__3214EC07501FBD4C",
                table: "ShoeImages");

            migrationBuilder.DropIndex(
                name: "IX_Countries_IsoCode",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Shoes");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Username",
                table: "Users",
                newName: "IX_Users_Name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Sizes",
                newName: "Value");

            migrationBuilder.RenameIndex(
                name: "IX_Sizes_Name",
                table: "Sizes",
                newName: "IX_Sizes_Value");

            migrationBuilder.RenameColumn(
                name: "MeasurementValueId",
                table: "ShoeMeasurements",
                newName: "MeasurementMetricId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoeMeasurements_MeasurementValueId",
                table: "ShoeMeasurements",
                newName: "IX_ShoeMeasurements_MeasurementMetricId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Countries",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "ShoeId",
                table: "Sizes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ShoeTypes",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Shoes",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "ShoeMeasurements",
                type: "decimal(18,2)",
                maxLength: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageData",
                table: "ShoeImages",
                type: "varbinary(max)",
                maxLength: 256,
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "ShoeImages",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newsequentialid())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF_ShoeImages_Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MeasurementMetrics",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldFixedLength: true,
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "IsoCode",
                table: "Countries",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(2)",
                oldFixedLength: true,
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Brands",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sizes",
                table: "Sizes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SizeMetrics",
                table: "SizeMetrics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoeImages",
                table: "ShoeImages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_ShoeId",
                table: "Sizes",
                column: "ShoeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Countries_CountryId",
                table: "Brands",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoeImages_Shoes_ShoeId",
                table: "ShoeImages",
                column: "ShoeId",
                principalTable: "Shoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoeMeasurements_MeasurementMetrics_MeasurementMetricId",
                table: "ShoeMeasurements",
                column: "MeasurementMetricId",
                principalTable: "MeasurementMetrics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoeMeasurements_MeasurementProperties_MeasurementPropertyId",
                table: "ShoeMeasurements",
                column: "MeasurementPropertyId",
                principalTable: "MeasurementProperties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoeMeasurements_Shoes_ShoeId",
                table: "ShoeMeasurements",
                column: "ShoeId",
                principalTable: "Shoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Brands_BrandId",
                table: "Shoes",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Colors_ColorId",
                table: "Shoes",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Conditions_ConditionId",
                table: "Shoes",
                column: "ConditionId",
                principalTable: "Conditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_ShoeTypes_ShoeTypeId",
                table: "Shoes",
                column: "ShoeTypeId",
                principalTable: "ShoeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_SkinTypes_SkinTypeId",
                table: "Shoes",
                column: "SkinTypeId",
                principalTable: "SkinTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sizes_Shoes_ShoeId",
                table: "Sizes",
                column: "ShoeId",
                principalTable: "Shoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sizes_SizeMetrics_SizeMetricId",
                table: "Sizes",
                column: "SizeMetricId",
                principalTable: "SizeMetrics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Countries_CountryId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoeImages_Shoes_ShoeId",
                table: "ShoeImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoeMeasurements_MeasurementMetrics_MeasurementMetricId",
                table: "ShoeMeasurements");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoeMeasurements_MeasurementProperties_MeasurementPropertyId",
                table: "ShoeMeasurements");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoeMeasurements_Shoes_ShoeId",
                table: "ShoeMeasurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Brands_BrandId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Colors_ColorId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Conditions_ConditionId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_ShoeTypes_ShoeTypeId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_SkinTypes_SkinTypeId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sizes_Shoes_ShoeId",
                table: "Sizes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sizes_SizeMetrics_SizeMetricId",
                table: "Sizes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sizes",
                table: "Sizes");

            migrationBuilder.DropIndex(
                name: "IX_Sizes_ShoeId",
                table: "Sizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SizeMetrics",
                table: "SizeMetrics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoeImages",
                table: "ShoeImages");

            migrationBuilder.DropColumn(
                name: "ShoeId",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "ShoeMeasurements");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Name",
                table: "Users",
                newName: "IX_Users_Username");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Sizes",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Sizes_Value",
                table: "Sizes",
                newName: "IX_Sizes_Name");

            migrationBuilder.RenameColumn(
                name: "MeasurementMetricId",
                table: "ShoeMeasurements",
                newName: "MeasurementValueId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoeMeasurements_MeasurementMetricId",
                table: "ShoeMeasurements",
                newName: "IX_ShoeMeasurements_MeasurementValueId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Countries",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ShoeTypes",
                type: "nvarchar(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Shoes",
                type: "nvarchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Shoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageData",
                table: "ShoeImages",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "ShoeImages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newsequentialid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:DefaultConstraintName", "DF_ShoeImages_Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MeasurementMetrics",
                type: "nvarchar(8)",
                fixedLength: true,
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "IsoCode",
                table: "Countries",
                type: "nchar(2)",
                fixedLength: true,
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Brands",
                type: "nvarchar(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shoe_Sizes",
                table: "Sizes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metrics",
                table: "SizeMetrics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Shoe_Ima__3214EC07501FBD4C",
                table: "ShoeImages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MeasurementValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasurementPropertyId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(3,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurement_Values_Measurement_Metrics",
                        column: x => x.MeasurementPropertyId,
                        principalTable: "MeasurementMetrics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_SizeId",
                table: "Shoes",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_Title",
                table: "Shoes",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_IsoCode",
                table: "Countries",
                column: "IsoCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementValues_MeasurementPropertyId",
                table: "MeasurementValues",
                column: "MeasurementPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementValues_Name",
                table: "MeasurementValues",
                column: "Value",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Countries",
                table: "Brands",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoe_Images_Shoes",
                table: "ShoeImages",
                column: "ShoeId",
                principalTable: "Shoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoe_Measurements_Measurement_Properties",
                table: "ShoeMeasurements",
                column: "MeasurementPropertyId",
                principalTable: "MeasurementProperties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoe_Measurements_Measurement_Values",
                table: "ShoeMeasurements",
                column: "MeasurementValueId",
                principalTable: "MeasurementValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoe_Measurements_Shoes",
                table: "ShoeMeasurements",
                column: "ShoeId",
                principalTable: "Shoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Brands",
                table: "Shoes",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Colors",
                table: "Shoes",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Conditions",
                table: "Shoes",
                column: "ConditionId",
                principalTable: "Conditions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Shoe_Types",
                table: "Shoes",
                column: "ShoeTypeId",
                principalTable: "ShoeTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Sizes",
                table: "Shoes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Skin_Types",
                table: "Shoes",
                column: "SkinTypeId",
                principalTable: "SkinTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sizes_Size_Metrics",
                table: "Sizes",
                column: "SizeMetricId",
                principalTable: "SizeMetrics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}

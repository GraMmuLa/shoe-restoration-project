using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoe_restoration_project.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    IsoCode = table.Column<string>(type: "nchar(2)", fixedLength: true, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(8)", fixedLength: true, maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementMetrics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementProperties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SizeMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(2)", fixedLength: true, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkinTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkinTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brands_Countries",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "MeasurementValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    MeasurementPropertyId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    SizeMetricId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoe_Sizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sizes_Size_Metrics",
                        column: x => x.SizeMetricId,
                        principalTable: "SizeMetrics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ShoeTypeId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    ConditionId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    SkinTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shoes_Brands",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shoes_Colors",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shoes_Conditions",
                        column: x => x.ConditionId,
                        principalTable: "Conditions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shoes_Shoe_Types",
                        column: x => x.ShoeTypeId,
                        principalTable: "ShoeTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shoes_Sizes",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shoes_Skin_Types",
                        column: x => x.SkinTypeId,
                        principalTable: "SkinTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShoeImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())")
                        .Annotation("Relational:DefaultConstraintName", "DF_ShoeImages_Id"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ShoeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shoe_Ima__3214EC07501FBD4C", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shoe_Images_Shoes",
                        column: x => x.ShoeId,
                        principalTable: "Shoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShoeMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoeId = table.Column<int>(type: "int", nullable: false),
                    MeasurementPropertyId = table.Column<int>(type: "int", nullable: false),
                    MeasurementValueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoeMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shoe_Measurements_Measurement_Properties",
                        column: x => x.MeasurementPropertyId,
                        principalTable: "MeasurementProperties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shoe_Measurements_Measurement_Values",
                        column: x => x.MeasurementValueId,
                        principalTable: "MeasurementValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shoe_Measurements_Shoes",
                        column: x => x.ShoeId,
                        principalTable: "Shoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_CountryId",
                table: "Brands",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Name",
                table: "Brands",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colors_Name",
                table: "Colors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conditions_Name",
                table: "Conditions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_IsoCode",
                table: "Countries",
                column: "IsoCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementMetrics_Name",
                table: "MeasurementMetrics",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementProperties_Name",
                table: "MeasurementProperties",
                column: "Name",
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

            migrationBuilder.CreateIndex(
                name: "IX_ShoeImages_Name",
                table: "ShoeImages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoeImages_ShoeId",
                table: "ShoeImages",
                column: "ShoeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoeMeasurements_MeasurementPropertyId",
                table: "ShoeMeasurements",
                column: "MeasurementPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoeMeasurements_MeasurementValueId",
                table: "ShoeMeasurements",
                column: "MeasurementValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoeMeasurements_ShoeId",
                table: "ShoeMeasurements",
                column: "ShoeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_BrandId",
                table: "Shoes",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_ColorId",
                table: "Shoes",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_ConditionId",
                table: "Shoes",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_ShoeTypeId",
                table: "Shoes",
                column: "ShoeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_SizeId",
                table: "Shoes",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_SkinTypeId",
                table: "Shoes",
                column: "SkinTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_Title",
                table: "Shoes",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoeTypes_Name",
                table: "ShoeTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SizeMetrics_Name",
                table: "SizeMetrics",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_Name",
                table: "Sizes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_SizeMetricId",
                table: "Sizes",
                column: "SizeMetricId");

            migrationBuilder.CreateIndex(
                name: "IX_SkinTypes_Name",
                table: "SkinTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoeImages");

            migrationBuilder.DropTable(
                name: "ShoeMeasurements");

            migrationBuilder.DropTable(
                name: "MeasurementProperties");

            migrationBuilder.DropTable(
                name: "MeasurementValues");

            migrationBuilder.DropTable(
                name: "Shoes");

            migrationBuilder.DropTable(
                name: "MeasurementMetrics");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Conditions");

            migrationBuilder.DropTable(
                name: "ShoeTypes");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "SkinTypes");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "SizeMetrics");
        }
    }
}

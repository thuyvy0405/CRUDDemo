using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class initDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    IDDanhMuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__nwsCatalogate", x => x.IDDanhMuc);
                });

            migrationBuilder.CreateTable(
                name: "TinTuc",
                columns: table => new
                {
                    IDTinTuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TomTat = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NoiDung = table.Column<string>(type: "ntext", nullable: true),
                    HinhAnh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LuotXem = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__wsPost", x => x.IDTinTuc);
                });

            migrationBuilder.CreateTable(
                name: "DanhMucTinTuc",
                columns: table => new
                {
                    IDDanhMuc = table.Column<int>(type: "int", nullable: false),
                    IDTinTuc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__nwsPostCatalogate", x => new { x.IDDanhMuc, x.IDTinTuc });
                    table.ForeignKey(
                        name: "FK_DanhMucTinTuc_DanhMuc_IDDanhMuc",
                        column: x => x.IDDanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "IDDanhMuc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhMucTinTuc_TinTuc_IDTinTuc",
                        column: x => x.IDTinTuc,
                        principalTable: "TinTuc",
                        principalColumn: "IDTinTuc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DanhMucTinTuc_IDTinTuc",
                table: "DanhMucTinTuc",
                column: "IDTinTuc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhMucTinTuc");

            migrationBuilder.DropTable(
                name: "DanhMuc");

            migrationBuilder.DropTable(
                name: "TinTuc");
        }
    }
}

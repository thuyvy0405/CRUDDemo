﻿// <auto-generated />
using System;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240108153621_initDatabase")]
    partial class initDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccessLayer.Entities.DanhMuc", b =>
                {
                    b.Property<int>("IddanhMuc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IDDanhMuc");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IddanhMuc"));

                    b.Property<string>("TenDanhMuc")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("TrangThai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("IddanhMuc")
                        .HasName("PK__nwsCatalogate");

                    b.ToTable("DanhMuc", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entities.DanhMucTinTuc", b =>
                {
                    b.Property<int>("IddanhMuc")
                        .HasColumnType("int")
                        .HasColumnName("IDDanhMuc")
                        .HasColumnOrder(0);

                    b.Property<int>("IdtinTuc")
                        .HasColumnType("int")
                        .HasColumnName("IDTinTuc")
                        .HasColumnOrder(1);

                    b.HasKey("IddanhMuc", "IdtinTuc")
                        .HasName("PK__nwsPostCatalogate");

                    b.HasIndex("IdtinTuc");

                    b.ToTable("DanhMucTinTuc", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entities.TinTuc", b =>
                {
                    b.Property<int>("IdtinTuc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IDTinTuc");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdtinTuc"));

                    b.Property<string>("HinhAnh")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("LuotXem")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgayUpdate")
                        .HasColumnType("datetime");

                    b.Property<string>("NoiDung")
                        .HasColumnType("ntext");

                    b.Property<string>("TieuDe")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TomTat")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.Property<string>("Url")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdtinTuc")
                        .HasName("PK__wsPost");

                    b.ToTable("TinTuc", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Entities.DanhMucTinTuc", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.DanhMuc", "DanhMuc")
                        .WithMany("DanhMucTinTucs")
                        .HasForeignKey("IddanhMuc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entities.TinTuc", "TinTuc")
                        .WithMany("DanhMucTinTucs")
                        .HasForeignKey("IdtinTuc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DanhMuc");

                    b.Navigation("TinTuc");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.DanhMuc", b =>
                {
                    b.Navigation("DanhMucTinTucs");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.TinTuc", b =>
                {
                    b.Navigation("DanhMucTinTucs");
                });
#pragma warning restore 612, 618
        }
    }
}

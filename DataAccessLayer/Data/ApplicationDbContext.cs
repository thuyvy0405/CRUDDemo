using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;
public partial class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext()
    {

    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<DanhMucTinTuc> DanhMucTinTucs { get; set; }

    public virtual DbSet<TinTuc> TinTucs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=vei-pc\\vydang;Initial Catalog=CRUDTABLE;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.IddanhMuc).HasName("PK__nwsCatalogate");

            entity.ToTable("DanhMuc");

            entity.Property(e => e.IddanhMuc).HasColumnName("IDDanhMuc");
            entity.Property(e => e.TenDanhMuc).HasMaxLength(255);
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
        });

        modelBuilder.Entity<DanhMucTinTuc>(entity =>
        {
            entity.HasKey(e => new { e.IddanhMuc, e.IdtinTuc }).HasName("PK__nwsPostCatalogate");

            entity.ToTable("DanhMucTinTuc");

            entity.Property(e => e.IddanhMuc).HasColumnName("IDDanhMuc");
            entity.Property(e => e.IdtinTuc).HasColumnName("IDTinTuc");
        });
        // Inside your DbContext
        modelBuilder.Entity<DanhMucTinTuc>()
            .HasOne(dmtt => dmtt.DanhMuc)
            .WithMany(dm => dm.DanhMucTinTucs)
            .HasForeignKey(dmtt => dmtt.IddanhMuc);
        modelBuilder.Entity<DanhMucTinTuc>()
             .HasOne(dmtt => dmtt.TinTuc)
             .WithMany(dm => dm.DanhMucTinTucs)
             .HasForeignKey(dmtt => dmtt.IdtinTuc);

        modelBuilder.Entity<TinTuc>(entity =>
        {
            entity.HasKey(e => e.IdtinTuc).HasName("PK__wsPost");

            entity.ToTable("TinTuc");

            entity.Property(e => e.IdtinTuc).HasColumnName("IDTinTuc");
            entity.Property(e => e.HinhAnh).HasMaxLength(255);
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayUpdate).HasColumnType("datetime");
            entity.Property(e => e.NoiDung).HasColumnType("ntext");
            entity.Property(e => e.TieuDe).HasMaxLength(255);
            entity.Property(e => e.TomTat).HasMaxLength(255);
            entity.Property(e => e.Url).HasMaxLength(255);
        });
        base.OnModelCreating(modelBuilder);
    }
}

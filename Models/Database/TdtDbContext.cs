using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QRGEN.Models.Database;

public partial class QRGENDbContext : DbContext
{
    public QRGENDbContext()
    {
    }

    public QRGENDbContext(DbContextOptions<QRGENDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Favouritesvgfile> Favouritesvgfiles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Warpfont> Warpfonts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=QRGEN_db;persist security info=True;MultipleActiveResultSets=True;User ID=sa;Password=Sqlpassword37;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Favouritesvgfile>(entity =>
        {
            entity.ToTable("favouritesvgfiles");

            entity.Property(e => e.PathNum).HasColumnName("pathNum");
            entity.Property(e => e.SvgFileName)
                .IsRequired()
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("svgFileName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC076EB0227F");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Warpfont>(entity =>
        {
            entity.ToTable("warpfonts");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

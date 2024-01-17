using System;
using System.Collections.Generic;
using BPKB.MVC.Tables;
using Microsoft.EntityFrameworkCore;

namespace BPKB.MVC.Contexts;

public partial class BPKBContext : DbContext
{
    public BPKBContext()
    {
    }

    public BPKBContext(DbContextOptions<BPKBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ms_storage_location> ms_storage_location { get; set; }

    public virtual DbSet<ms_user> ms_user { get; set; }

    public virtual DbSet<tr_bpkb> tr_bpkb { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=db;User Id=sa;Password=password1q!Q;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ms_storage_location>(entity =>
        {
            entity.HasKey(e => e.location_id).HasName("PK__ms_stora__771831EA7CD52EC0");

            entity.Property(e => e.location_id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.location_name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ms_user>(entity =>
        {
            entity.HasKey(e => e.user_id).HasName("PK__ms_user__B9BE370FE27B6BCE");

            entity.Property(e => e.user_id).ValueGeneratedNever();
            entity.Property(e => e.password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.user_name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tr_bpkb>(entity =>
        {
            entity.HasKey(e => e.agreement_number).HasName("PK__tr_bpkb__21912C800BCC3791");

            entity.Property(e => e.agreement_number)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.bpkb_date).HasColumnType("datetime");
            entity.Property(e => e.bpkb_date_in).HasColumnType("datetime");
            entity.Property(e => e.bpkb_no)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.branch_id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.created_by)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.created_on).HasColumnType("datetime");
            entity.Property(e => e.faktur_date).HasColumnType("datetime");
            entity.Property(e => e.faktur_no)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.last_updated_by)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.last_updated_on).HasColumnType("datetime");
            entity.Property(e => e.location_id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.police_no)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.location).WithMany(p => p.tr_bpkb)
                .HasForeignKey(d => d.location_id)
                .HasConstraintName("FK__tr_bpkb__locatio__4AB81AF0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

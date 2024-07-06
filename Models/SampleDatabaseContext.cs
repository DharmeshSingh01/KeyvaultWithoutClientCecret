using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace DotNetAPIDS.Models;

public partial class SampleDatabaseContext : DbContext
{
    public SampleDatabaseContext()
    {
    }

    public SampleDatabaseContext(DbContextOptions<SampleDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Confederation> Confederations { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        if (!optionsBuilder.IsConfigured)
        {
        }

    }
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //      => optionsBuilder.UseSqlServer("Server=.;Database=SampleDatabase;Trusted_Connection=True;TrustServerCertificate=True");
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseLazyLoadingProxies(true);
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        //      optionsBuilder.UseSqlServer("Server=sqlempdb.database.windows.net;Database=akq_tracker;User ID=userdb;Password=admin@123456;Trusted_Connection=false;");
    //    }
    //}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Confederation>(entity =>
        {
            entity.ToTable("Confederation");

            entity.Property(e => e.ConfederationId).HasColumnName("confederationId");
            entity.Property(e => e.ConfederationName)
                .HasMaxLength(50)
                .HasColumnName("confederationName");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.ToTable("Team");

            entity.Property(e => e.TeamId).HasColumnName("teamId");
            entity.Property(e => e.ConfederationId).HasColumnName("confederationId");
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .HasColumnName("countryName");

            entity.HasOne(d => d.Confederation).WithMany(p => p.Teams)
                .HasForeignKey(d => d.ConfederationId)
                .HasConstraintName("FK_Team_Confederation");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

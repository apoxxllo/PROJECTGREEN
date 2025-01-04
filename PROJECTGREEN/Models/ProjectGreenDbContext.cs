using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PROJECTGREEN.Models;

public partial class ProjectGreenDbContext : DbContext
{
    public ProjectGreenDbContext()
    {
    }

    public ProjectGreenDbContext(DbContextOptions<ProjectGreenDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Committee> Committees { get; set; }

    public virtual DbSet<CommitteeAssignment> CommitteeAssignments { get; set; }

    public virtual DbSet<EmailsForNotificationAlert> EmailsForNotificationAlerts { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Incident> Incidents { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<PositionAssignment> PositionAssignments { get; set; }

    public virtual DbSet<ProgramFeedback> ProgramFeedbacks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=JULES-IRWIN\\SQLEXPRESS;Database=ProjectGreenDB;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Committee>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommitteeName).HasColumnName("committeeName");
            entity.Property(e => e.Responsibilities).HasColumnName("responsibilities");
        });

        modelBuilder.Entity<CommitteeAssignment>(entity =>
        {
            entity.ToTable("CommitteeAssignment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommitteeId).HasColumnName("committee_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Committee).WithMany(p => p.CommitteeAssignments)
                .HasForeignKey(d => d.CommitteeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_CommitteeAssignment_Committees");

            entity.HasOne(d => d.User).WithMany(p => p.CommitteeAssignments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_CommitteeAssignment_Users");
        });

        modelBuilder.Entity<EmailsForNotificationAlert>(entity =>
        {
            entity.ToTable("EmailsForNotificationAlert");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("lastName");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EventName).HasColumnName("eventName");
            entity.Property(e => e.NoOfParticipants).HasColumnName("noOfParticipants");
            entity.Property(e => e.PhotoPath).HasColumnName("photoPath");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Incident>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.DocumentationLink).HasColumnName("documentationLink");
            entity.Property(e => e.IncidentType)
                .HasMaxLength(50)
                .HasColumnName("incidentType");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Resolution).HasColumnName("resolution");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.ToTable("Position");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PositionName).HasColumnName("positionName");
            entity.Property(e => e.Responsibilities).HasColumnName("responsibilities");
        });

        modelBuilder.Entity<PositionAssignment>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Position).WithMany(p => p.PositionAssignments)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_PositionAssignments_Position");

            entity.HasOne(d => d.User).WithMany(p => p.PositionAssignments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_PositionAssignments_Users");
        });

        modelBuilder.Entity<ProgramFeedback>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment)
                .HasMaxLength(50)
                .HasColumnName("comment");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("lastName");
            entity.Property(e => e.Stars).HasColumnName("stars");

            entity.HasOne(d => d.Event).WithMany(p => p.ProgramFeedbacks)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ProgramFeedbacks_Events");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .HasColumnName("contact");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.ProfilePicPath).HasColumnName("profilePicPath");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

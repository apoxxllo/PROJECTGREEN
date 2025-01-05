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

    public virtual DbSet<AdminNote> AdminNotes { get; set; }

    public virtual DbSet<BarangayPosition> BarangayPositions { get; set; }

    public virtual DbSet<Committee> Committees { get; set; }

    public virtual DbSet<CommitteeAssignment> CommitteeAssignments { get; set; }

    public virtual DbSet<EmailsForNotificationAlert> EmailsForNotificationAlerts { get; set; }

    public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }

    public virtual DbSet<EmployeeTimetable> EmployeeTimetables { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Incident> Incidents { get; set; }

    public virtual DbSet<IncidentFeedback> IncidentFeedbacks { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<PositionAssignment> PositionAssignments { get; set; }

    public virtual DbSet<ProgramFeedback> ProgramFeedbacks { get; set; }

    public virtual DbSet<RoleWorkSchedule> RoleWorkSchedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WasteVolumeDatum> WasteVolumeData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=JULES-IRWIN\\SQLEXPRESS;Database=ProjectGreenDB;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminNote>(entity =>
        {
            entity.ToTable("AdminNote");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Note).HasColumnName("note");
        });

        modelBuilder.Entity<BarangayPosition>(entity =>
        {
            entity.ToTable("BarangayPosition");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BrgyPosition)
                .HasMaxLength(50)
                .HasColumnName("brgyPosition");
        });

        modelBuilder.Entity<Committee>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommitteeName).HasColumnName("committeeName");
            entity.Property(e => e.Responsibilities).HasColumnName("responsibilities");
            entity.Property(e => e.RoleInGreenInitiatives).HasColumnName("roleInGreenInitiatives");
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
            entity.Property(e => e.DayOfTheWeek)
                .HasMaxLength(50)
                .HasColumnName("dayOfTheWeek");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("lastName");
            entity.Property(e => e.Zone).HasColumnName("zone");
        });

        modelBuilder.Entity<EmployeeRole>(entity =>
        {
            entity.ToTable("EmployeeRole");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmployeeRole1)
                .HasMaxLength(50)
                .HasColumnName("employeeRole");
        });

        modelBuilder.Entity<EmployeeTimetable>(entity =>
        {
            entity.HasKey(e => e.Int);

            entity.ToTable("EmployeeTimetable");

            entity.Property(e => e.Int).HasColumnName("int");
            entity.Property(e => e.FriTask).HasColumnName("friTask");
            entity.Property(e => e.MondayTask).HasColumnName("mondayTask");
            entity.Property(e => e.ThuTask).HasColumnName("thuTask");
            entity.Property(e => e.Time)
                .HasMaxLength(50)
                .HasColumnName("time");
            entity.Property(e => e.TuesTask).HasColumnName("tuesTask");
            entity.Property(e => e.WedTask).HasColumnName("wedTask");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DocuLink).HasColumnName("docuLink");
            entity.Property(e => e.EventName).HasColumnName("eventName");
            entity.Property(e => e.Location).HasColumnName("location");
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
            entity.Property(e => e.Impact).HasColumnName("impact");
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

        modelBuilder.Entity<IncidentFeedback>(entity =>
        {
            entity.ToTable("IncidentFeedback");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IncidentId).HasColumnName("incident_id");

            entity.HasOne(d => d.Incident).WithMany(p => p.IncidentFeedbacks)
                .HasForeignKey(d => d.IncidentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_IncidentFeedback_Incidents");
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
            entity.Property(e => e.SubmittedAt)
                .HasColumnType("datetime")
                .HasColumnName("submittedAt");

            entity.HasOne(d => d.Event).WithMany(p => p.ProgramFeedbacks)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ProgramFeedbacks_Events");
        });

        modelBuilder.Entity<RoleWorkSchedule>(entity =>
        {
            entity.ToTable("RoleWorkSchedule");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmployeeRoleId).HasColumnName("employeeRole_id");
            entity.Property(e => e.Remarks).HasColumnName("remarks");
            entity.Property(e => e.Schedule)
                .HasMaxLength(50)
                .HasColumnName("schedule");
            entity.Property(e => e.TaskDescription).HasColumnName("taskDescription");
            entity.Property(e => e.TimeTable)
                .HasMaxLength(50)
                .HasColumnName("timeTable");

            entity.HasOne(d => d.EmployeeRole).WithMany(p => p.RoleWorkSchedules)
                .HasForeignKey(d => d.EmployeeRoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_RoleWorkSchedule_EmployeeRole");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BarangayPositionId).HasColumnName("barangayPosition_id");
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

            entity.HasOne(d => d.BarangayPosition).WithMany(p => p.Users)
                .HasForeignKey(d => d.BarangayPositionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Users_BarangayPosition");
        });

        modelBuilder.Entity<WasteVolumeDatum>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AvarageHouseHoldRemarks).HasColumnName("avarageHouseHoldRemarks");
            entity.Property(e => e.AverageHousehouldRecyclingRate).HasColumnName("averageHousehouldRecyclingRate");
            entity.Property(e => e.BiodegradableCollected).HasColumnName("biodegradableCollected");
            entity.Property(e => e.BiodegradableRemarks).HasColumnName("biodegradableRemarks");
            entity.Property(e => e.CommunityComposting).HasColumnName("communityComposting");
            entity.Property(e => e.CompostableRemarks).HasColumnName("compostableRemarks");
            entity.Property(e => e.CompostableWaste).HasColumnName("compostableWaste");
            entity.Property(e => e.DailyWasteBio).HasColumnName("dailyWasteBio");
            entity.Property(e => e.DailyWasteNonbio).HasColumnName("dailyWasteNonbio");
            entity.Property(e => e.DailyWasteRecyclable).HasColumnName("dailyWasteRecyclable");
            entity.Property(e => e.EwasteRecycled).HasColumnName("ewasteRecycled");
            entity.Property(e => e.EwasteRemarks).HasColumnName("ewasteRemarks");
            entity.Property(e => e.HouseholdSegregation).HasColumnName("householdSegregation");
            entity.Property(e => e.MonthlyRecycled).HasColumnName("monthlyRecycled");
            entity.Property(e => e.MonthlyRecycledRemarks).HasColumnName("monthlyRecycledRemarks");
            entity.Property(e => e.MrfProcessingCapacity).HasColumnName("mrfProcessingCapacity");
            entity.Property(e => e.MrfRemarks).HasColumnName("mrfRemarks");
            entity.Property(e => e.NonbioCollected).HasColumnName("nonbioCollected");
            entity.Property(e => e.NonbioRemarks).HasColumnName("nonbioRemarks");
            entity.Property(e => e.PlasticFreeRemark).HasColumnName("plasticFreeRemark");
            entity.Property(e => e.PlasticfreeReductionImpact).HasColumnName("plasticfreeReductionImpact");
            entity.Property(e => e.RecyclablesCollected).HasColumnName("recyclablesCollected");
            entity.Property(e => e.RecyclablesRemarks).HasColumnName("recyclablesRemarks");
            entity.Property(e => e.RecyclingSuccessRate).HasColumnName("recyclingSuccessRate");
            entity.Property(e => e.TotalHouseholds).HasColumnName("totalHouseholds");
            entity.Property(e => e.TotalPopulation).HasColumnName("totalPopulation");
            entity.Property(e => e.UncollectedRemarks).HasColumnName("uncollectedRemarks");
            entity.Property(e => e.UncollectedWaste).HasColumnName("uncollectedWaste");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

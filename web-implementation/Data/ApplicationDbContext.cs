using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GrapheneTrace.Web.Models;
using System.Security.Cryptography.X509Certificates;

namespace GrapheneTrace.Web.Data;

/// <summary>
/// Database context for Identity and application data.
/// Author: SID:2412494
/// </summary>
/// <remarks>
/// Design Pattern: Extends IdentityDbContext to leverage ASP.NET Core Identity's
/// built-in authentication and authorization infrastructure.
///
/// Why IdentityDbContext:
/// - Automatically configures Identity tables (AspNetUsers, AspNetRoles, etc.)
/// - Handles relationships between Identity entities
/// - Provides optimized schema for authentication workflows
/// - Battle-tested by millions of applications
///
/// Generic Parameters:
/// - ApplicationUser: Our custom user entity with Guid key
/// - IdentityRole&lt;Guid&gt;: Role entity with Guid key (for future use)
/// - Guid: Primary key type for all Identity tables
///
/// Tables Created Automatically by Identity:
/// - AspNetUsers: User accounts (with our custom fields)
/// - AspNetRoles: Role definitions (optional for our use case)
/// - AspNetUserRoles: User-role mapping
/// - AspNetUserClaims: Custom claims per user
/// - AspNetUserLogins: External login providers (Google, etc.)
/// - AspNetUserTokens: Password reset tokens, 2FA tokens
/// - AspNetRoleClaims: Claims per role
///
/// Future Expansion:
/// Add DbSets here for application data (PressureReadings, Devices, etc.)
/// </remarks>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Configures the database schema using Fluent API.
    /// </summary>
    /// <remarks>
    /// Called by EF Core when building the model.
    /// base.OnModelCreating() must be called to apply Identity's default configuration.
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Apply Identity's default configuration (creates all Identity tables)
        base.OnModelCreating(builder);

        // Configure ApplicationUser custom properties
        builder.Entity<ApplicationUser>(entity =>
        {
            // Enforce required constraints and max lengths for data integrity
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.UserType)
                .IsRequired()
                .HasMaxLength(20);

            // DeactivatedAt is nullable, no configuration needed

            // Optional: Create index on UserType for faster role-based queries
            entity.HasIndex(e => e.UserType);

            // Optional: Create index on DeactivatedAt for filtering active users
            entity.HasIndex(e => e.DeactivatedAt);
        });

        // Future: Add configurations for other entities here
        // Example:
        // builder.Entity<PressureReading>(entity =>
        // {
        //     entity.HasOne(p => p.User)
        //         .WithMany()
        //         .HasForeignKey(p => p.UserId);
        // });

        //Configure PatientSessionData properties
        //Author: 2414111 
        builder.Entity<PatientSessionData>(entity =>
        {
            entity.HasKey(a => a.SessionId);
            entity.Property(a => a.SessionId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(a => a.DeviceId)
                .IsRequired()
                .HasMaxLength(8);

            entity.Property(a => a.UserId)
                .IsRequired();

            entity.Property(a => a.RawData)
                .IsRequired();

            entity.HasIndex(a => a.SessionId);
            entity.HasIndex(a => a.DeviceId);
            entity.HasIndex(a => a.UserId);
        });

        //Configure PatientSnapshotData properties
        //Author: 2414111 
        builder.Entity<PatientSnapshotData>(entity =>
        {
            entity.HasKey(b => b.SnapshotId);
            entity.Property(b => b.SnapshotId)
                .IsRequired()
                .ValueGeneratedOnAdd(); //automated incremental psqlkey

            entity.Property(b => b.SessionId)
                .IsRequired();

            entity.Property(b => b.DeviceId)
                .IsRequired()
                .HasMaxLength(8);

            entity.Property(b => b.UserId)
                .IsRequired();

            entity.Property(b => b.SnapshotData)
                .IsRequired();

            entity.HasIndex(b => b.SnapshotId);
            entity.HasIndex(b => b.SessionId);
            entity.HasIndex(b => b.DeviceId);
            entity.HasIndex(b => b.UserId);

            entity.HasOne<PatientSessionData>()
                .WithMany()
                .HasForeignKey(b => b.SessionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
    //connect the PatientSessionData and PatientSnapshotData models to the database
    public DbSet<PatientSessionData> PatientSessionDatas { get; set; }
    public DbSet<PatientSnapshotData> PatientSnapshotDatas { get; set; }
}

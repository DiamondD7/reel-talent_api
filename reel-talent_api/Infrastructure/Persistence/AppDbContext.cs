using Microsoft.EntityFrameworkCore;
using reel_talent_api.Domain.Models;

namespace reel_talent_api.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {
            
        }

        public DbSet<User>Users { get; set; }
        public DbSet<CastingProfile>CastingProfiles { get; set; }
        public DbSet<Talent> Talents { get; set; }
        public DbSet<TalentDetails> TalentDetails { get; set; }
        public DbSet<TalentSkill> TalentSkills { get; set; }
        public DbSet<TalentLanguage>TalentLanguages { get; set; }
        public DbSet<TalentExperience> TalentExperiences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(t => t.Email).IsUnique();
            });

            modelBuilder.Entity<CastingProfile>(entity =>
            {
                entity.HasOne(t => t.User)
                .WithOne(t => t.CastingProfile)
                .HasForeignKey<CastingProfile>(t => t.UserId);
            });


            modelBuilder.Entity<Talent>(entity =>
            {

                entity.HasKey(t => t.Id);

                entity.Property(t => t.FirstName).HasMaxLength(100);
                entity.Property(t => t.LastName).HasMaxLength(100);
                entity.Property(t => t.FullName).HasMaxLength(200);

                entity.HasOne(t => t.User)
                .WithOne(t => t.Talent)
                .HasForeignKey<Talent>(t => t.UserId);

                entity.HasOne(t => t.Details).WithOne(d => d.Talent)
                .HasForeignKey<TalentDetails>(d => d.TalentId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TalentDetails>(entity =>
            {
                entity.HasKey(d => d.TalentId);

                entity.Property(d => d.Bio).HasMaxLength(2000);
                entity.Property(d => d.Ethnicity).HasMaxLength(100);
                entity.Property(d => d.NaturalAccent).HasMaxLength(100);
                entity.Property(d => d.EyeColor).HasMaxLength(50);
                entity.Property(d => d.HairColor).HasMaxLength(50);
                entity.Property(d => d.ShoeSize).HasMaxLength(20);
                entity.Property(d => d.ClothingSize).HasMaxLength(20);

                entity.Property(d => d.Height).HasPrecision(5, 2);
                entity.Property(d => d.Weight).HasPrecision(5, 2);

                entity.HasMany(d => d.Skills)
                .WithOne()
                .HasForeignKey(s => s.TalentDetailsId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.Languages)
                .WithOne()
                .HasForeignKey(l => l.TalentDetailsId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.Experiences)
                    .WithOne()
                    .HasForeignKey(e => e.TalentDetailsId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TalentSkill>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<TalentLanguage>(entity =>
            {
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<TalentExperience>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Year).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Role).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Production).HasMaxLength(200);
                entity.Property(e => e.DirectorOrCompany).HasMaxLength(200);
                entity.Property(e => e.Url).HasMaxLength(2048);
            });
        }
    }
}

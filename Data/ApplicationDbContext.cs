using Microsoft.EntityFrameworkCore;
using PortfolioCore.Data.Entities;

namespace PortfolioCore.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
    public DbSet<SkillCategoryEntity> SkillCategories => Set<SkillCategoryEntity>();
    public DbSet<SkillItemEntity> SkillItems => Set<SkillItemEntity>();
    public DbSet<ExperienceEntity> Experiences => Set<ExperienceEntity>();
    public DbSet<EducationEntity> Educations => Set<EducationEntity>();
    public DbSet<CertificationEntity> Certifications => Set<CertificationEntity>();
    public DbSet<ReferenceEntity> References => Set<ReferenceEntity>();
    public DbSet<ContactMessageEntity> ContactMessages => Set<ContactMessageEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SkillCategoryEntity>()
            .HasMany(c => c.Skills)
            .WithOne(s => s.SkillCategory)
            .HasForeignKey(s => s.SkillCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

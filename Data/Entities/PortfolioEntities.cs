using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioCore.Data.Entities;

[Table("Projects")]
public class ProjectEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string ShortDescription { get; set; } = string.Empty;

    public string FullDescription { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Category { get; set; } = string.Empty;

    [MaxLength(100)]
    public string CategoryName { get; set; } = string.Empty;

    [MaxLength(300)]
    public string ImageUrl { get; set; } = string.Empty;

    public string TechnologiesJson { get; set; } = "[]";

    [MaxLength(300)]
    public string LiveUrl { get; set; } = string.Empty;

    [MaxLength(300)]
    public string GithubUrl { get; set; } = string.Empty;

    public bool Featured { get; set; }

    public string ArchitectureNotes { get; set; } = string.Empty;

    [MaxLength(100)]
    public string ClientName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Duration { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

[Table("SkillCategories")]
public class SkillCategoryEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string CategoryName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string IconClass { get; set; } = string.Empty;

    public List<SkillItemEntity> Skills { get; set; } = new();
}

[Table("SkillItems")]
public class SkillItemEntity
{
    [Key]
    public int Id { get; set; }

    public int SkillCategoryId { get; set; }
    
    [ForeignKey("SkillCategoryId")]
    public SkillCategoryEntity? SkillCategory { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public int ProficiencyPercentage { get; set; }

    [MaxLength(50)]
    public string LevelName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Icon { get; set; } = string.Empty;
}

[Table("Experiences")]
public class ExperienceEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Period { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Role { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Company { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Location { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string HighlightsJson { get; set; } = "[]";

    [MaxLength(50)]
    public string BadgeText { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }
}

[Table("Educations")]
public class EducationEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Degree { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Institution { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Period { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Location { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }
}

[Table("Certifications")]
public class CertificationEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Provider { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Icon { get; set; } = "fa-solid fa-certificate";
}

[Table("References")]
public class ReferenceEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Role { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Company { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Phone { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
}

[Table("ContactMessages")]
public class ContactMessageEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Subject { get; set; } = string.Empty;

    [Required]
    public string Message { get; set; } = string.Empty;

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; } = false;
}

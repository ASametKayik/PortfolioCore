using System.ComponentModel.DataAnnotations;

namespace PortfolioCore.Models;

public class ProjectModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string FullDescription { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public List<string> Technologies { get; set; } = new();
    public string LiveUrl { get; set; } = string.Empty;
    public string GithubUrl { get; set; } = string.Empty;
    public bool Featured { get; set; }
    public string ArchitectureNotes { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
}

public class SkillCategoryModel
{
    public string CategoryName { get; set; } = string.Empty;
    public string IconClass { get; set; } = string.Empty;
    public List<SkillItem> Skills { get; set; } = new();
}

public class SkillItem
{
    public string Name { get; set; } = string.Empty;
    public int ProficiencyPercentage { get; set; }
    public string LevelName { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}

public class ExperienceModel
{
    public string Period { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Highlights { get; set; } = new();
    public string BadgeText { get; set; } = string.Empty;
}

public class EducationModel
{
    public string Degree { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}

public class CertificationModel
{
    public string Name { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public string Icon { get; set; } = "fa-solid fa-certificate";
}

public class ReferenceModel
{
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class ContactFormModel
{
    [Required(ErrorMessage = "Lütfen adınızı ve soyadınızı giriniz.")]
    [StringLength(100, ErrorMessage = "Adınız en fazla 100 karakter olabilir.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lütfen e-posta adresinizi giriniz.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lütfen bir konu giriniz.")]
    [StringLength(150, ErrorMessage = "Konu en fazla 150 karakter olabilir.")]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lütfen mesajınızı yazınız.")]
    [MinLength(10, ErrorMessage = "Mesajınız en az 10 karakter olmalıdır.")]
    public string Message { get; set; } = string.Empty;
}

public class PortfolioHomeViewModel
{
    public string Name { get; set; } = "Abdul Samet Kayık";
    public string Title { get; set; } = "Software Engineering Student & .NET Developer";
    public string Bio { get; set; } = "İstanbul Üniversitesi-Cerrahpaşa Bilgisayar Programcılığı mezuniyetimin ardından DGS ile Sakarya Üniversitesi Yazılım Mühendisliği bölümüne yerleştim. 1 yıllık İngilizce Hazırlık eğitimim ile İngilizce seviyemi B2 seviyesine yükselttim. ASP.NET Core MVC, C#, Entity Framework Core, PostgreSQL, SQL Server ve modern web teknolojileri ile ölçeklenebilir yazılım çözümleri geliştiriyorum.";
    
    public string Phone { get; set; } = "(+90) 539 945 5872";
    public string Email { get; set; } = "asametkayik@gmail.com";
    public string Location { get; set; } = "İstanbul / Sakarya, Türkiye";
    public string GithubUrl { get; set; } = "https://github.com/ASametKayik";
    public string LinkedinUrl { get; set; } = "https://www.linkedin.com/in/abdulsametkayik/";
    public string EnglishLevel { get; set; } = "B2 Level (Upper-Intermediate)";

    public int CompletedProjectsCount { get; set; } = 15;
    public int YearsExperience { get; set; } = 2;
    public int HappyClientsCount { get; set; } = 10;
    public int CodeCommitsCount { get; set; } = 450;
    
    public List<ProjectModel> FeaturedProjects { get; set; } = new();
    public List<ProjectModel> AllProjects { get; set; } = new();
    public List<SkillCategoryModel> SkillCategories { get; set; } = new();
    public List<ExperienceModel> Experiences { get; set; } = new();
    public List<EducationModel> Educations { get; set; } = new();
    public List<CertificationModel> Certifications { get; set; } = new();
    public List<ReferenceModel> References { get; set; } = new();
    public ContactFormModel ContactForm { get; set; } = new();
}

public class ContactSubmissionResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}

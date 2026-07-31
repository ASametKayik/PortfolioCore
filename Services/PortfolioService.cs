using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PortfolioCore.Data;
using PortfolioCore.Data.Entities;
using PortfolioCore.Models;

namespace PortfolioCore.Services;

public class PortfolioService : IPortfolioService
{
    private readonly ApplicationDbContext _db;

    public PortfolioService(ApplicationDbContext db)
    {
        _db = db;
    }

    public PortfolioHomeViewModel GetHomeData()
    {
        try
        {
            var dbProjects = _db.Projects.AsNoTracking().ToList();
            var dbCategories = _db.SkillCategories.Include(c => c.Skills).AsNoTracking().ToList();
            var dbExperiences = _db.Experiences.AsNoTracking().OrderBy(e => e.DisplayOrder).ToList();
            var dbEducations = _db.Educations.AsNoTracking().OrderBy(e => e.DisplayOrder).ToList();
            var dbCertifications = _db.Certifications.AsNoTracking().ToList();
            var dbReferences = _db.References.AsNoTracking().ToList();

            if (dbProjects.Any())
            {
                return new PortfolioHomeViewModel
                {
                    Name = "Abdul Samet Kayık",
                    Title = "Software Engineering Student & .NET Developer",
                    Bio = "İstanbul Üniversitesi-Cerrahpaşa Bilgisayar Programcılığı mezuniyetimin ardından DGS ile Sakarya Üniversitesi Yazılım Mühendisliği bölümüne kazandım. 1 yıllık İngilizce Hazırlık eğitimim ile İngilizce seviyemi B2 seviyesine yükselttim. ASP.NET Core MVC, C#, Entity Framework Core, PostgreSQL, SQL Server ve modern web teknolojileri ile ölçeklenebilir yazılım çözümleri geliştiriyorum.",
                    Phone = "(+90) 539 945 5872",
                    Email = "asametkayik@gmail.com",
                    Location = "Sakarya / İstanbul, Türkiye",
                    GithubUrl = "https://github.com/ASametKayik",
                    LinkedinUrl = "https://www.linkedin.com/in/abdulsametkayik/",
                    CompletedProjectsCount = 15,
                    YearsExperience = 2,
                    HappyClientsCount = 10,
                    CodeCommitsCount = 450,

                    AllProjects = dbProjects.Select(MapProject).ToList(),
                    FeaturedProjects = dbProjects.Where(p => p.Featured).Select(MapProject).ToList(),
                    SkillCategories = dbCategories.Select(MapSkillCategory).ToList(),
                    Experiences = dbExperiences.Select(MapExperience).ToList(),
                    Educations = dbEducations.Select(MapEducation).ToList(),
                    Certifications = dbCertifications.Select(MapCertification).ToList(),
                    References = dbReferences.Select(MapReference).ToList()
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[PortfolioService Fallback] PostgreSQL Read Exception: {ex.Message}");
        }

        return GetFallbackData();
    }

    public ProjectModel? GetProjectById(int id)
    {
        try
        {
            var entity = _db.Projects.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (entity != null)
            {
                return MapProject(entity);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[PortfolioService Fallback] PostgreSQL Project Read Exception: {ex.Message}");
        }

        return GetFallbackData().AllProjects.FirstOrDefault(p => p.Id == id);
    }

    public ContactSubmissionResult ProcessContactMessage(ContactFormModel model)
    {
        try
        {
            var contactEntity = new ContactMessageEntity
            {
                FullName = model.FullName,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message,
                SubmittedAt = DateTime.UtcNow
            };

            _db.ContactMessages.Add(contactEntity);
            _db.SaveChanges();

            return new ContactSubmissionResult
            {
                Success = true,
                Message = $"Teşekkürler Sayın {model.FullName}, mesajınız başarıyla PostgreSQL veritabanına kaydedildi ve alındı!"
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[PortfolioService Contact Error] Could not save to DB: {ex.Message}");
            return new ContactSubmissionResult
            {
                Success = true,
                Message = $"Teşekkürler Sayın {model.FullName}, mesajınız alındı!"
            };
        }
    }

    private static ProjectModel MapProject(ProjectEntity entity)
    {
        return new ProjectModel
        {
            Id = entity.Id,
            Title = entity.Title,
            ShortDescription = entity.ShortDescription,
            FullDescription = entity.FullDescription,
            Category = entity.Category,
            CategoryName = entity.CategoryName,
            ImageUrl = entity.ImageUrl,
            Technologies = JsonSerializer.Deserialize<List<string>>(entity.TechnologiesJson ?? "[]") ?? new(),
            LiveUrl = entity.LiveUrl,
            GithubUrl = entity.GithubUrl,
            Featured = entity.Featured,
            ArchitectureNotes = entity.ArchitectureNotes,
            ClientName = entity.ClientName,
            Duration = entity.Duration
        };
    }

    private static SkillCategoryModel MapSkillCategory(SkillCategoryEntity entity)
    {
        return new SkillCategoryModel
        {
            CategoryName = entity.CategoryName,
            IconClass = entity.IconClass,
            Skills = entity.Skills.Select(s => new SkillItem
            {
                Name = s.Name,
                ProficiencyPercentage = s.ProficiencyPercentage,
                LevelName = s.LevelName,
                Icon = s.Icon
            }).ToList()
        };
    }

    private static ExperienceModel MapExperience(ExperienceEntity entity)
    {
        return new ExperienceModel
        {
            Period = entity.Period,
            Role = entity.Role,
            Company = entity.Company,
            Location = entity.Location,
            Description = entity.Description,
            Highlights = JsonSerializer.Deserialize<List<string>>(entity.HighlightsJson ?? "[]") ?? new(),
            BadgeText = entity.BadgeText
        };
    }

    private static EducationModel MapEducation(EducationEntity entity)
    {
        return new EducationModel
        {
            Degree = entity.Degree,
            Institution = entity.Institution,
            Period = entity.Period,
            Location = entity.Location
        };
    }

    private static CertificationModel MapCertification(CertificationEntity entity)
    {
        return new CertificationModel
        {
            Name = entity.Name,
            Provider = entity.Provider,
            Icon = entity.Icon
        };
    }

    private static ReferenceModel MapReference(ReferenceEntity entity)
    {
        return new ReferenceModel
        {
            Name = entity.Name,
            Role = entity.Role,
            Company = entity.Company,
            Phone = entity.Phone,
            Email = entity.Email
        };
    }

    private static PortfolioHomeViewModel GetFallbackData()
    {
        return new PortfolioHomeViewModel
        {
            Name = "Abdul Samet Kayık",
            Title = "Software Engineering Student & .NET Developer",
            Bio = "İstanbul Üniversitesi-Cerrahpaşa Bilgisayar Programcılığı mezuniyetimin ardından DGS ile Sakarya Üniversitesi Yazılım Mühendisliği bölümüne yerleştim. 1 yıllık İngilizce Hazırlık eğitimim ile İngilizce seviyemi B2 seviyesine yükselttim. ASP.NET Core MVC, C#, Entity Framework Core, PostgreSQL, SQL Server ve modern web teknolojileri ile ölçeklenebilir yazılım çözümleri geliştiriyorum.",
            Phone = "(+90) 539 945 5872",
            Email = "asametkayik@gmail.com",
            Location = "Sakarya / İstanbul, Türkiye",
            GithubUrl = "https://github.com/ASametKayik",
            LinkedinUrl = "https://www.linkedin.com/in/abdulsametkayik/",
            CompletedProjectsCount = 15,
            YearsExperience = 2,
            HappyClientsCount = 10,
            CodeCommitsCount = 450,

            AllProjects = new List<ProjectModel>
            {
                new ProjectModel
                {
                    Id = 1,
                    Title = "ASP.NET Core 10 Portfolio & PostgreSQL Architecture",
                    ShortDescription = "Entity Framework Core, PostgreSQL veritabanı, Razor Views ve glassmorphism UI ile geliştirilmiş modern portfolyo web platformu.",
                    FullDescription = "Temiz mimari standartlarına uygun, PostgreSQL veritabanı entegrasyonu, AJAX iletişim altyapısı, koyu/açık tema yönetimi ve dinamik proje filtreleme modüllerini içeren tam kapsamlı web uygulaması.",
                    Category = "dotnet",
                    CategoryName = ".NET & MVC",
                    ImageUrl = "/images/project1.jpg",
                    Technologies = new List<string> { "ASP.NET Core 10", "C#", "EF Core", "PostgreSQL", "JavaScript", "HTML5/CSS3" },
                    LiveUrl = "http://localhost:5055",
                    GithubUrl = "https://github.com/ASametKayik",
                    Featured = true,
                    ArchitectureNotes = "Clean Architecture ve Service-Repository katmanı ile modüler olarak tasarlanmıştır.",
                    ClientName = "Kişisel Portfolyo",
                    Duration = "2026"
                },
                new ProjectModel
                {
                    Id = 2,
                    Title = "AcunMedya Akademi - Dynamic Web App & CRUD Engine",
                    ShortDescription = "ASP.NET MVC, C# ve SQL Server üzerine kurulu dinamik veri tabanlı web uygulaması.",
                    FullDescription = "AcunMedya Akademi staj sürecinde geliştirilmiş, Entity Framework ile veritabanı CRUD operasyonları sunan, Razor views altyapılı web projesi.",
                    Category = "dotnet",
                    CategoryName = ".NET & Web App",
                    ImageUrl = "/images/project2.jpg",
                    Technologies = new List<string> { "ASP.NET MVC", "C#", "Entity Framework", "SQL Server", "HTML5", "CSS3" },
                    LiveUrl = "https://github.com/ASametKayik",
                    GithubUrl = "https://github.com/ASametKayik",
                    Featured = true,
                    ArchitectureNotes = "MVC tasarım deseni ve Entity Framework Code First yaklaşımı kullanılmıştır.",
                    ClientName = "AcunMedya Akademi Staj Projesi",
                    Duration = "2024 - 2025"
                },
                new ProjectModel
                {
                    Id = 3,
                    Title = "Tekno Mektep - İnteraktif Web Bileşenleri & Statik Sayfalar",
                    ShortDescription = "HTML5, CSS3 ve JavaScript ile tasarlanmış kullanıcı etkileşimli modern web sayfaları.",
                    FullDescription = "Tekno Mektep stajı kapsamında piksel kalitesinde duyarlı arayüzler ve JavaScript ile dinamik kullanıcı bileşenlerinin geliştirilmesi.",
                    Category = "frontend",
                    CategoryName = "Frontend & UI",
                    ImageUrl = "/images/project3.jpg",
                    Technologies = new List<string> { "HTML5", "CSS3", "JavaScript", "Responsive Design" },
                    LiveUrl = "https://github.com/ASametKayik",
                    GithubUrl = "https://github.com/ASametKayik",
                    Featured = true,
                    ArchitectureNotes = "Semantik HTML5 yapısı ve esnek CSS Grid/Flexbox düzenleri.",
                    ClientName = "Tekno Mektep Staj Projesi",
                    Duration = "2024"
                }
            },
            SkillCategories = new List<SkillCategoryModel>
            {
                new SkillCategoryModel
                {
                    CategoryName = ".NET & Backend Development",
                    IconClass = "fa-solid fa-code",
                    Skills = new List<SkillItem>
                    {
                        new SkillItem { Name = "ASP.NET Core MVC / ASP.NET MVC", ProficiencyPercentage = 92, LevelName = "İleri Düzey", Icon = "fa-brands fa-microsoft" },
                        new SkillItem { Name = "C# Programlama Dili", ProficiencyPercentage = 90, LevelName = "İleri Düzey", Icon = "fa-solid fa-code" },
                        new SkillItem { Name = "Entity Framework Core / EF", ProficiencyPercentage = 88, LevelName = "İleri Düzey", Icon = "fa-solid fa-database" },
                        new SkillItem { Name = "Nesne Yönelimli Programlama (OOP)", ProficiencyPercentage = 90, LevelName = "Uzman", Icon = "fa-solid fa-cubes" },
                        new SkillItem { Name = "RESTful APIs & CRUD Operasyonları", ProficiencyPercentage = 88, LevelName = "İleri Düzey", Icon = "fa-solid fa-network-wired" },
                        new SkillItem { Name = "Java & C Programlama Dili", ProficiencyPercentage = 80, LevelName = "Orta-İleri", Icon = "fa-brands fa-java" }
                    }
                },
                new SkillCategoryModel
                {
                    CategoryName = "Frontend & Web Design",
                    IconClass = "fa-solid fa-laptop-code",
                    Skills = new List<SkillItem>
                    {
                        new SkillItem { Name = "HTML5 & Semantik Web", ProficiencyPercentage = 95, LevelName = "Uzman", Icon = "fa-brands fa-html5" },
                        new SkillItem { Name = "CSS3 & Glassmorphism UI", ProficiencyPercentage = 92, LevelName = "Uzman", Icon = "fa-brands fa-css3-alt" },
                        new SkillItem { Name = "JavaScript (ES6+) Etkileşimli Bileşenler", ProficiencyPercentage = 85, LevelName = "İleri Düzey", Icon = "fa-brands fa-js" },
                        new SkillItem { Name = "Razor Views & Dynamic Web Pages", ProficiencyPercentage = 90, LevelName = "İleri Düzey", Icon = "fa-solid fa-layer-group" }
                    }
                },
                new SkillCategoryModel
                {
                    CategoryName = "Databases & Tools (Digital Skills)",
                    IconClass = "fa-solid fa-database",
                    Skills = new List<SkillItem>
                    {
                        new SkillItem { Name = "PostgreSQL & pgAdmin 4", ProficiencyPercentage = 88, LevelName = "İleri Düzey", Icon = "fa-solid fa-database" },
                        new SkillItem { Name = "SQL Server & T-SQL", ProficiencyPercentage = 88, LevelName = "İleri Düzey", Icon = "fa-solid fa-server" },
                        new SkillItem { Name = "Git & GitHub Versiyon Kontrolü", ProficiencyPercentage = 85, LevelName = "İleri Düzey", Icon = "fa-brands fa-github" },
                        new SkillItem { Name = "Docker & Konteynerizasyon", ProficiencyPercentage = 75, LevelName = "Orta Düzey", Icon = "fa-brands fa-docker" }
                    }
                }
            },
            Experiences = new List<ExperienceModel>
            {
                new ExperienceModel
                {
                    Period = "18/11/2024 – 13/06/2025",
                    Role = ".Net Developer Intern",
                    Company = "AcunMedya Akademi",
                    Location = "İstanbul, Türkiye",
                    Description = "ASP.NET MVC mimarisi ve C# dili ile dinamik web uygulamalarının geliştirilmesi.",
                    Highlights = new List<string>
                    {
                        "ASP.NET MVC ve C# kullanarak ölçeklenebilir web uygulamaları geliştirdi.",
                        "Entity Framework ve SQL Server ile güvenli CRUD operasyonlarını hayata geçirdi.",
                        "Dinamik veri odaklı sayfalar için Razor görünüm yapılarını tasarladı ve inşa etti."
                    },
                    BadgeText = "Stajyer"
                },
                new ExperienceModel
                {
                    Period = "01/07/2024 – 01/08/2024",
                    Role = "Web Development Intern",
                    Company = "Tekno Mektep",
                    Location = "İstanbul, Türkiye",
                    Description = "Ön yüz web teknolojileri ile statik ve etkileşimli sayfaların tasarlanması.",
                    Highlights = new List<string>
                    {
                        "HTML ve CSS kullanarak statik web sayfaları oluşturdu.",
                        "JavaScript kullanarak kullanıcı etkileşimli bileşenler geliştirdi."
                    },
                    BadgeText = "Stajyer"
                }
            },
            Educations = new List<EducationModel>
            {
                new EducationModel
                {
                    Degree = "Computer Programming (Bilgisayar Programcılığı)",
                    Institution = "İstanbul Üniversitesi-Cerrahpaşa",
                    Period = "02/10/2023 – 31/07/2025",
                    Location = "İstanbul, Türkiye"
                },
                new EducationModel
                {
                    Degree = "Anadolu Lisesi Mezuniyeti",
                    Institution = "Amiral Vehbi Ziya Dümer Anatolian High School",
                    Period = "2019 – 2023",
                    Location = "İstanbul, Türkiye"
                }
            },
            Certifications = new List<CertificationModel>
            {
                new CertificationModel { Name = "Sıfırdan Komple Java Geliştirici Kursu", Provider = "Udemy", Icon = "fa-brands fa-java" },
                new CertificationModel { Name = "C Programlama Dili", Provider = "BTK Akademi", Icon = "fa-solid fa-code" },
                new CertificationModel { Name = "HTML5 ile Web Geliştirme", Provider = "BTK Akademi", Icon = "fa-brands fa-html5" }
            },
            References = new List<ReferenceModel>
            {
                new ReferenceModel
                {
                    Name = "Veysel Kayık",
                    Role = "Software Developer",
                    Company = "Turkcell Global Bilgi",
                    Phone = "0543 539 62 91",
                    Email = "veyselkayik@gmail.com"
                }
            }
        };
    }
}

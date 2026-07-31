using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PortfolioCore.Data.Entities;

namespace PortfolioCore.Data;

public static class DbInitializer
{
    public static void Seed(ApplicationDbContext context)
    {
        try
        {
            context.Database.EnsureCreated();

            // Refresh seed data to ensure Sakarya University & B2 English level are persisted
            if (context.Educations.Any(e => !e.Institution.Contains("Sakarya")))
            {
                context.Projects.RemoveRange(context.Projects);
                context.SkillCategories.RemoveRange(context.SkillCategories);
                context.Experiences.RemoveRange(context.Experiences);
                context.Educations.RemoveRange(context.Educations);
                context.Certifications.RemoveRange(context.Certifications);
                context.References.RemoveRange(context.References);
                context.SaveChanges();
            }

            if (!context.Projects.Any())
            {
                var projects = new List<ProjectEntity>
                {
                    new ProjectEntity
                    {
                        Title = "ASP.NET Core 10 Portfolio & PostgreSQL Architecture",
                        ShortDescription = "Entity Framework Core, PostgreSQL veritabanı, Razor Views ve glassmorphism UI ile geliştirilmiş modern portfolyo web platformu.",
                        FullDescription = "Temiz mimari standartlarına uygun, PostgreSQL veritabanı entegrasyonu, AJAX iletişim altyapısı, koyu/açık tema yönetimi ve dinamik proje filtreleme modüllerini içeren tam kapsamlı web uygulaması.",
                        Category = "dotnet",
                        CategoryName = ".NET & MVC",
                        ImageUrl = "/images/project1.jpg",
                        TechnologiesJson = JsonSerializer.Serialize(new[] { "ASP.NET Core 10", "C#", "EF Core", "PostgreSQL", "JavaScript", "HTML5/CSS3" }),
                        LiveUrl = "http://localhost:5055",
                        GithubUrl = "https://github.com/ASametKayik",
                        Featured = true,
                        ArchitectureNotes = "Clean Architecture ve Service-Repository katmanı ile modüler olarak tasarlanmıştır.",
                        ClientName = "Kişisel Portfolyo",
                        Duration = "2026"
                    },
                    new ProjectEntity
                    {
                        Title = "AcunMedya Akademi - Dynamic Web App & CRUD Engine",
                        ShortDescription = "ASP.NET MVC, C# ve SQL Server üzerine kurulu dinamik veri tabanlı web uygulaması.",
                        FullDescription = "AcunMedya Akademi staj sürecinde geliştirilmiş, Entity Framework ile veritabanı CRUD (Create, Read, Update, Delete) operasyonları sunan, Razor views altyapılı web projesi.",
                        Category = "dotnet",
                        CategoryName = ".NET & Web App",
                        ImageUrl = "/images/project2.jpg",
                        TechnologiesJson = JsonSerializer.Serialize(new[] { "ASP.NET MVC", "C#", "Entity Framework", "SQL Server", "HTML5", "CSS3" }),
                        LiveUrl = "https://github.com/ASametKayik",
                        GithubUrl = "https://github.com/ASametKayik",
                        Featured = true,
                        ArchitectureNotes = "MVC tasarım deseni ve Entity Framework Code First yaklaşımı kullanılmıştır.",
                        ClientName = "AcunMedya Akademi Staj Projesi",
                        Duration = "2024 - 2025"
                    },
                    new ProjectEntity
                    {
                        Title = "Tekno Mektep - İnteraktif Web Bileşenleri & Statik Sayfalar",
                        ShortDescription = "HTML5, CSS3 ve JavaScript ile tasarlanmış kullanıcı etkileşimli modern web sayfaları.",
                        FullDescription = "Tekno Mektep stajı kapsamında piksel kalitesinde duyarlı (responsive) arayüzler ve JavaScript ile dinamik kullanıcı bileşenlerinin geliştirilmesi.",
                        Category = "frontend",
                        CategoryName = "Frontend & UI",
                        ImageUrl = "/images/project3.jpg",
                        TechnologiesJson = JsonSerializer.Serialize(new[] { "HTML5", "CSS3", "JavaScript", "Responsive Design" }),
                        LiveUrl = "https://github.com/ASametKayik",
                        GithubUrl = "https://github.com/ASametKayik",
                        Featured = true,
                        ArchitectureNotes = "Semantik HTML5 yapısı ve esnek CSS Grid/Flexbox düzenleri.",
                        ClientName = "Tekno Mektep Staj Projesi",
                        Duration = "2024"
                    }
                };

                context.Projects.AddRange(projects);
            }

            if (!context.SkillCategories.Any())
            {
                var skillCategories = new List<SkillCategoryEntity>
                {
                    new SkillCategoryEntity
                    {
                        CategoryName = ".NET & Backend Development",
                        IconClass = "fa-solid fa-code",
                        Skills = new List<SkillItemEntity>
                        {
                            new SkillItemEntity { Name = "ASP.NET Core MVC / ASP.NET MVC", ProficiencyPercentage = 92, LevelName = "İleri Düzey", Icon = "fa-brands fa-microsoft" },
                            new SkillItemEntity { Name = "C# Programlama Dili", ProficiencyPercentage = 90, LevelName = "İleri Düzey", Icon = "fa-solid fa-code" },
                            new SkillItemEntity { Name = "Entity Framework Core / EF", ProficiencyPercentage = 88, LevelName = "İleri Düzey", Icon = "fa-solid fa-database" },
                            new SkillItemEntity { Name = "Nesne Yönelimli Programlama (OOP)", ProficiencyPercentage = 90, LevelName = "Uzman", Icon = "fa-solid fa-cubes" },
                            new SkillItemEntity { Name = "RESTful APIs & CRUD Operasyonları", ProficiencyPercentage = 88, LevelName = "İleri Düzey", Icon = "fa-solid fa-network-wired" },
                            new SkillItemEntity { Name = "Java & C Programlama Dili", ProficiencyPercentage = 80, LevelName = "Orta-İleri", Icon = "fa-brands fa-java" }
                        }
                    },
                    new SkillCategoryEntity
                    {
                        CategoryName = "Frontend & Web Design",
                        IconClass = "fa-solid fa-laptop-code",
                        Skills = new List<SkillItemEntity>
                        {
                            new SkillItemEntity { Name = "HTML5 & Semantik Web", ProficiencyPercentage = 95, LevelName = "Uzman", Icon = "fa-brands fa-html5" },
                            new SkillItemEntity { Name = "CSS3 & Glassmorphism UI", ProficiencyPercentage = 92, LevelName = "Uzman", Icon = "fa-brands fa-css3-alt" },
                            new SkillItemEntity { Name = "JavaScript (ES6+) Etkileşimli Bileşenler", ProficiencyPercentage = 85, LevelName = "İleri Düzey", Icon = "fa-brands fa-js" },
                            new SkillItemEntity { Name = "Razor Views & Dynamic Web Pages", ProficiencyPercentage = 90, LevelName = "İleri Düzey", Icon = "fa-solid fa-layer-group" }
                        }
                    },
                    new SkillCategoryEntity
                    {
                        CategoryName = "Databases, Tools & Yabancı Dil",
                        IconClass = "fa-solid fa-database",
                        Skills = new List<SkillItemEntity>
                        {
                            new SkillItemEntity { Name = "İngilizce (B2 Level - Upper-Intermediate)", ProficiencyPercentage = 85, LevelName = "B2 Level", Icon = "fa-solid fa-language" },
                            new SkillItemEntity { Name = "PostgreSQL & pgAdmin 4", ProficiencyPercentage = 88, LevelName = "İleri Düzey", Icon = "fa-solid fa-database" },
                            new SkillItemEntity { Name = "SQL Server & T-SQL", ProficiencyPercentage = 88, LevelName = "İleri Düzey", Icon = "fa-solid fa-server" },
                            new SkillItemEntity { Name = "Git & GitHub Versiyon Kontrolü", ProficiencyPercentage = 85, LevelName = "İleri Düzey", Icon = "fa-brands fa-github" },
                            new SkillItemEntity { Name = "Docker & Konteynerizasyon", ProficiencyPercentage = 75, LevelName = "Orta Düzey", Icon = "fa-brands fa-docker" }
                        }
                    }
                };

                context.SkillCategories.AddRange(skillCategories);
            }

            if (!context.Experiences.Any())
            {
                var experiences = new List<ExperienceEntity>
                {
                    new ExperienceEntity
                    {
                        Period = "18/11/2024 – 13/06/2025",
                        Role = ".Net Developer Intern",
                        Company = "AcunMedya Akademi",
                        Location = "İstanbul, Türkiye",
                        Description = "ASP.NET MVC mimarisi ve C# dili ile dinamik web uygulamalarının geliştirilmesi.",
                        HighlightsJson = JsonSerializer.Serialize(new[]
                        {
                            "ASP.NET MVC ve C# kullanarak ölçeklenebilir web uygulamaları geliştirdi.",
                            "Entity Framework ve SQL Server ile güvenli CRUD operasyonlarını hayata geçirdi.",
                            "Dinamik veri odaklı sayfalar için Razor görünüm yapılarını tasarladı ve inşa etti."
                        }),
                        BadgeText = "Stajyer",
                        DisplayOrder = 1
                    },
                    new ExperienceEntity
                    {
                        Period = "01/07/2024 – 01/08/2024",
                        Role = "Web Development Intern",
                        Company = "Tekno Mektep",
                        Location = "İstanbul, Türkiye",
                        Description = "Ön yüz web teknolojileri ile statik ve etkileşimli sayfaların tasarlanması.",
                        HighlightsJson = JsonSerializer.Serialize(new[]
                        {
                            "HTML ve CSS kullanarak statik web sayfaları oluşturdu.",
                            "JavaScript kullanarak kullanıcı etkileşimli bileşenler geliştirdi."
                        }),
                        BadgeText = "Stajyer",
                        DisplayOrder = 2
                    }
                };

                context.Experiences.AddRange(experiences);
            }

            if (!context.Educations.Any())
            {
                var educations = new List<EducationEntity>
                {
                    new EducationEntity
                    {
                        Degree = "Yazılım Mühendisliği (Software Engineering)",
                        Institution = "Sakarya Üniversitesi",
                        Period = "2025 – Devam Ediyor",
                        Location = "Sakarya, Türkiye",
                        DisplayOrder = 1
                    },
                    new EducationEntity
                    {
                        Degree = "İngilizce Hazırlık Sınıfı (B2 Level)",
                        Institution = "Sakarya Üniversitesi",
                        Period = "2025 – 2026",
                        Location = "Sakarya, Türkiye",
                        DisplayOrder = 2
                    },
                    new EducationEntity
                    {
                        Degree = "Computer Programming (Bilgisayar Programcılığı - Ön Lisans)",
                        Institution = "İstanbul Üniversitesi-Cerrahpaşa",
                        Period = "02/10/2023 – 31/07/2025",
                        Location = "İstanbul, Türkiye",
                        DisplayOrder = 3
                    },
                    new EducationEntity
                    {
                        Degree = "Anadolu Lisesi Mezuniyeti",
                        Institution = "Amiral Vehbi Ziya Dümer Anatolian High School",
                        Period = "2019 – 2023",
                        Location = "İstanbul, Türkiye",
                        DisplayOrder = 4
                    }
                };

                context.Educations.AddRange(educations);
            }

            if (!context.Certifications.Any())
            {
                var certifications = new List<CertificationEntity>
                {
                    new CertificationEntity
                    {
                        Name = "Sıfırdan Komple Java Geliştirici Kursu",
                        Provider = "Udemy",
                        Icon = "fa-brands fa-java"
                    },
                    new CertificationEntity
                    {
                        Name = "C Programlama Dili",
                        Provider = "BTK Akademi",
                        Icon = "fa-solid fa-code"
                    },
                    new CertificationEntity
                    {
                        Name = "HTML5 ile Web Geliştirme",
                        Provider = "BTK Akademi",
                        Icon = "fa-brands fa-html5"
                    }
                };

                context.Certifications.AddRange(certifications);
            }

            if (!context.References.Any())
            {
                var references = new List<ReferenceEntity>
                {
                    new ReferenceEntity
                    {
                        Name = "Veysel Kayık",
                        Role = "Software Developer",
                        Company = "Turkcell Global Bilgi",
                        Phone = "0543 539 62 91",
                        Email = "veyselkayik@gmail.com"
                    }
                };

                context.References.AddRange(references);
            }

            context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DbInitializer Seed Warning] {ex.Message}");
        }
    }
}

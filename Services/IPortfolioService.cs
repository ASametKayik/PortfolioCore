using PortfolioCore.Models;

namespace PortfolioCore.Services;

public interface IPortfolioService
{
    PortfolioHomeViewModel GetHomeData();
    ProjectModel? GetProjectById(int id);
    ContactSubmissionResult ProcessContactMessage(ContactFormModel model);
}

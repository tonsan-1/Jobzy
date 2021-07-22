namespace Jobzy.Web.ViewModels.Employers
{
    using System.Collections.Generic;

    using Jobzy.Web.ViewModels.Profiles;

    public class EmployerViewModel : BaseProfileViewModel
    {
        public List<OpenJobsListViewModel> OpenJobs { get; set; } = new List<OpenJobsListViewModel>();
    }
}

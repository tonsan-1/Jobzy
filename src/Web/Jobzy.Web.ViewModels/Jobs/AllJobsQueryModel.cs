namespace Jobzy.Web.ViewModels.Jobs
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Common;

    public class AllJobsQueryModel
    {
        public string Category { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        public Sorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public IEnumerable<CategoriesListViewModel> Categories { get; set; }

        public IEnumerable<AllJobsListViewModel> Jobs { get; set; }
    }
}

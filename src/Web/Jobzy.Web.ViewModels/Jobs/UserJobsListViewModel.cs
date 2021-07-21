namespace Jobzy.Web.ViewModels.Jobs
{
    using System.Linq;

    using AutoMapper;
    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class UserJobsListViewModel : BaseJobViewModel, IMapFrom<Job>, IHaveCustomMappings
    {
        public int OffersCount { get; set; }

        public string TagName => this.Status == JobStatus.Open ? "Open" :
                                 this.Status == JobStatus.InContract ? "In Active Contract" :
                                 this.Status == JobStatus.Closed ? "Closed" : string.Empty;

        public string TagColor => this.Status == JobStatus.Open ? "green" :
                                  this.Status == JobStatus.InContract ? "yellow" :
                                  this.Status == JobStatus.Closed ? "red" : string.Empty;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Job, UserJobsListViewModel>()
                .ForMember(x => x.OffersCount, options => options
                .MapFrom(j => j.Offers.Count(x => !x.IsAccepted)));
        }
    }
}

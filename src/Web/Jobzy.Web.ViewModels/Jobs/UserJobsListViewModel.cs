namespace Jobzy.Web.ViewModels.Jobs
{
    using System;
    using System.Linq;

    using AutoMapper;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class UserJobsListViewModel : IMapFrom<Job>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime DatePosted { get; set; }

        public int OffersCount { get; set; }

        public bool HasContract { get; set; }

        public bool IsClosed { get; set; }

        public string TagName => !this.HasContract && !this.IsClosed ? "Open" :
                                  this.HasContract && !this.IsClosed ? "In Active Contract" :
                                 !this.HasContract && this.IsClosed ? "Closed" : string.Empty;

        public string TagColor => !this.HasContract && !this.IsClosed ? "green" :
                                   this.HasContract && !this.IsClosed ? "yellow" :
                                  !this.HasContract && this.IsClosed ? "red" : string.Empty;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Job, UserJobsListViewModel>()
                .ForMember(x => x.OffersCount, options => options
                .MapFrom(j => j.Offers.Count(x => !x.IsDeleted)));
        }
    }
}

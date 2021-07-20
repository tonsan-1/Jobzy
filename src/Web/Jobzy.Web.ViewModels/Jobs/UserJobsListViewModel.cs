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

        public string ContractStatus { get; set; }

        public bool IsClosed { get; set; }

        public string TagName => this.ContractStatus == null && !this.IsClosed ? "Open" : "In Active Contract";

        public string TagColor => this.ContractStatus == null && !this.IsClosed ? "green" : "yellow";

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Job, UserJobsListViewModel>()
                .ForMember(x => x.ContractStatus, options => options
                .MapFrom(j => j.ContractId == null ? null : j.Contract.Status.ToString()));

            configuration
                .CreateMap<Job, UserJobsListViewModel>()
                .ForMember(x => x.OffersCount, options => options
                .MapFrom(j => j.Offers.Count(x => !x.IsDeleted)));
        }
    }
}

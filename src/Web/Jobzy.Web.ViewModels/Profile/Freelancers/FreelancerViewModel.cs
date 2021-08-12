namespace Jobzy.Web.ViewModels.Profiles.Freelancers
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class FreelancerViewModel : BaseProfileViewModel, IMapFrom<Freelancer>, IHaveCustomMappings
    {
        public int OffersCount { get; set; }

        public IEnumerable<ContractsListViewModel> Contracts { get; set; } = new HashSet<ContractsListViewModel>();

        public int TotalContractsCount => this.Contracts.Count();

        public int JobsDone => this.Contracts.Count(x => x.Status == ContractStatus.Finished);

        public decimal JobSuccess
            => this.TotalContractsCount > 1 ? (this.TotalContractsCount - this.JobsDone) * 100 : 100;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Freelancer, FreelancerViewModel>()
                .ForMember(x => x.Contracts, options => options
                .MapFrom(f => f.Contracts
                .Where(x => x.Status == ContractStatus.Finished || x.Status == ContractStatus.Ongoing)));
        }
    }
}

namespace Jobzy.Web.ViewModels.Jobs
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class SingleJobViewModel : BaseJobViewModel, IMapFrom<Job>, IHaveCustomMappings
    {
        public string Description { get; set; }

        public string ContractId { get; set; }

        public string EmployerId { get; set; }

        public string EmployerName { get; set; }

        public double EmployerRating { get; set; }

        public Country EmployerLocation { get; set; }

        public List<string> OffersFreelancerIds { get; set; }

        public List<string> ContractsFreelancerIds { get; set; }

        public string EmployerLocationToString => this.EmployerLocation.GetAttribute<DisplayAttribute>().Name;

        public string DateFormatted => TimeCalculator.GetTimeAgo(this.DatePosted);

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Job, SingleJobViewModel>()

                .ForMember(x => x.OffersFreelancerIds, options => options
                .MapFrom(j => j.Offers
                .Where(o => !o.IsAccepted)
                .Select(f => f.FreelancerId)))

                .ForMember(x => x.ContractsFreelancerIds, options => options
                .MapFrom(c => c.Contracts
                .Where(x => x.Status == ContractStatus.Ongoing)
                .Select(i => i.FreelancerId)))

                .ForMember(x => x.ContractId, options => options
                .MapFrom(c => c.Contracts
                .FirstOrDefault(x => x.Status == ContractStatus.Ongoing)
                .Id));
        }
    }
}

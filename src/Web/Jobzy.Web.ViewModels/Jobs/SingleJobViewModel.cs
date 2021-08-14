namespace Jobzy.Web.ViewModels.Jobs
{
    using System;
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

        public string EmployerFirstName { get; set; }

        public string EmployerLastName { get; set; }

        public double EmployerRating { get; set; }

        public string EmployerProfileImageUrl { get; set; }

        public Country EmployerLocation { get; set; }

        public List<string> OffersFreelancerIds { get; set; }

        public List<string> ContractsFreelancerIds { get; set; }

        public string EmployerLocationToString => this.EmployerLocation.GetAttribute<DisplayAttribute>().Name;

        public string DateFormatted => TimeCalculator.GetTimeAgo(this.CreatedOn);

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
                .Id))

                .ForMember(x => x.EmployerRating, options => options
                .MapFrom(c => Math.Round(c.Employer.ReceivedReviews.Average(x => x.Rating), 2)));
        }
    }
}

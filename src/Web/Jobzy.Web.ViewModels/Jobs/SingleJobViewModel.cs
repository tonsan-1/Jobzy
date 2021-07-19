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

    public class SingleJobViewModel : IMapFrom<Job>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string JobType { get; set; }

        public decimal Budget { get; set; }

        public DateTime DatePosted { get; set; }

        public string Description { get; set; }

        public string EmployerName { get; set; }

        public double EmployerRating { get; set; }

        public Country EmployerLocation { get; set; }

        public List<string> OffersFreelancerIds { get; set; }

        public string EmployerLocationToString => this.EmployerLocation.GetAttribute<DisplayAttribute>().Name;

        public bool EmployerIsVerified { get; set; }

        public string DateFormatted => TimeCalculator.GetTimeAgo(this.DatePosted);

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Job, SingleJobViewModel>()
                .ForMember(x => x.OffersFreelancerIds, options => options
                .MapFrom(j => j.Offers.Select(x => x.FreelancerId)));
        }
    }
}

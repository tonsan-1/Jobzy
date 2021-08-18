namespace Jobzy.Web.ViewModels.Offers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class JobOfferViewModel : IMapFrom<Offer>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public int FixedPrice { get; set; }

        public int DeliveryDays { get; set; }

        public string JobTitle { get; set; }

        public string JobId { get; set; }

        public string FreelancerId { get; set; }

        public string FreelancerFirstName { get; set; }

        public string FreelancerLastName { get; set; }

        public string FreelancerEmail { get; set; }

        public double FreelancerRating { get; set; }

        public string FreelancerProfileImageUrl { get; set; }

        public Country FreelancerLocation { get; set; }

        public string FreelancerLocationToString => this.FreelancerLocation.GetAttribute<DisplayAttribute>().Name;

        public int DialogNumber { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Offer, JobOfferViewModel>()

                .ForMember(x => x.FreelancerRating, options => options
                .MapFrom(c => c.Freelancer.ReceivedReviews.Any() ?
                                Math.Round(c.Freelancer.ReceivedReviews.Average(x => x.Rating), 2) : 0.0));
        }
    }
}

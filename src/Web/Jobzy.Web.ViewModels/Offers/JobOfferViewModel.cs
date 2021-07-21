namespace Jobzy.Web.ViewModels.Offers
{
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class JobOfferViewModel : IMapFrom<Offer>
    {
        public string Id { get; set; }

        public decimal FixedPrice { get; set; }

        public int DeliveryDays { get; set; }

        public string JobTitle { get; set; }

        public string JobId { get; set; }

        public string FreelancerName { get; set; }

        public string FreelancerEmail { get; set; }

        public double FreelancerRating { get; set; }

        public string FreelancerProfileImageUrl { get; set; }

        public Country FreelancerLocation { get; set; }

        public string FreelancerLocationToString => this.FreelancerLocation.GetAttribute<DisplayAttribute>().Name;

        public int DialogNumber { get; set; }
    }
}

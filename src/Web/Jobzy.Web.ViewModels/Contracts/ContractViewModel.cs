namespace Jobzy.Web.ViewModels.Contracts
{
    using System;

    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class ContractViewModel : IMapFrom<Contract>
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public string OfferId { get; set; }

        public string OfferJobId { get; set; }

        public string OfferJobTitle { get; set; }

        public string OfferJobDescription { get; set; }

        public decimal OfferFixedPrice { get; set; }

        public int OfferDeliveryDays { get; set; }

        public string EmployerName { get; set; }

        public string FreelancerId { get; set; }

        public string FreelancerName { get; set; }

        public DateTime ContractDeadline
            => this.CreatedOn.AddDays(this.OfferDeliveryDays).ToLocalTime();
    }
}

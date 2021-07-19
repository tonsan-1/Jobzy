namespace Jobzy.Web.ViewModels.Contracts
{
    using System;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class UserContractsListViewModel : IMapFrom<Contract>
    {
        public string Id { get; set; }

        public string ShortId => this.Id.Substring(0, 8).ToUpper();

        public ContractStatus Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public string OfferJobTitle { get; set; }

        public int OfferDeliveryDays { get; set; }

        public decimal OfferFixedPrice { get; set; }

        public string StatusToString => this.Status.ToString();

        public DateTime ContractDeadline
            => this.StatusToString == "Ongoing" ?
            this.CreatedOn.AddDays(this.OfferDeliveryDays).ToLocalTime() : DateTime.MinValue;

        public int TimeLeft
            => this.ContractDeadline.Subtract(DateTime.Now).Days;

        public string TagColor => this.StatusToString == "Ongoing" && this.TimeLeft > 0 ? "green" :
                                  this.StatusToString == "Ongoing" && this.TimeLeft <= 0 ? "yellow" :
                                  this.StatusToString == "Canceled" ? "red" : "bg-secondary";
    }
}

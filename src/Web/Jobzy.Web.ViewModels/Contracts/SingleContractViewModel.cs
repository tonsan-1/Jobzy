namespace Jobzy.Web.ViewModels.Contracts
{
    using System;
    using System.Collections.Generic;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class SingleContractViewModel : IMapFrom<Contract>
    {
        public string Id { get; set; }

        public ContractStatus Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public string OfferId { get; set; }

        public string OfferJobId { get; set; }

        public string OfferJobTitle { get; set; }

        public string OfferJobDescription { get; set; }

        public int OfferFixedPrice { get; set; }

        public int OfferDeliveryDays { get; set; }

        public string EmployerName { get; set; }

        public string FreelancerId { get; set; }

        public string FreelancerName { get; set; }

        public string StatusToString => this.Status.ToString();

        public DateTime ContractDeadline
            => this.StatusToString == "Ongoing" ?
            this.CreatedOn.AddDays(this.OfferDeliveryDays).ToLocalTime() : DateTime.MinValue;

        public int TimeLeft
            => this.ContractDeadline.Subtract(DateTime.Now).Days;

        public string StatusColor => this.StatusToString == "Finished" ? "bg-secondary text-white" :
                                     this.StatusToString == "Ongoing" && this.TimeLeft <= 0 ? "bg-warning text-white" :
                                     this.StatusToString == "Canceled" ? "bg-danger text-white" : string.Empty;

        public string StatusName => this.StatusToString == "Ongoing" && this.TimeLeft > 0 ? "Ongoing" :
                                    this.StatusToString == "Ongoing" && this.TimeLeft < 0 ? "Expired" :
                                    this.StatusToString == "Canceled" ? "Canceled" : "Finished";

        public List<AttachmentListViewModel> Attachments { get; set; }
    }
}

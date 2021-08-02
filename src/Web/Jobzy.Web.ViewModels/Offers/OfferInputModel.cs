namespace Jobzy.Web.ViewModels.Offers
{
    using System.ComponentModel.DataAnnotations;

    public class OfferInputModel
    {
        [Required]
        public string JobId { get; set; }

        [Required]
        public int FixedPrice { get; set; }

        [Range(0, 365)]
        public int DeliveryDays { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}

namespace Jobzy.Web.ViewModels.Job
{
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Common;

    public class JobInputModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }

        [Range(0, 1)]
        public JobType JobType { get; set; }

        [Range(0, 5)]
        public JobCategory JobCategory { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        [Required]
        [StringLength(2048, MinimumLength = 5)]
        public string Description { get; set; }

        public string Tags { get; set; }
    }
}

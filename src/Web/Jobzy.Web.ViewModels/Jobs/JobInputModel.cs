namespace Jobzy.Web.ViewModels.Jobs
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class JobInputModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        [Required]
        [StringLength(2048, MinimumLength = 5)]
        public string Description { get; set; }

        public IEnumerable<CategoriesListViewModel> Categories { get; set; }
    }
}

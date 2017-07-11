using System.ComponentModel.DataAnnotations;

namespace jiangli.UI.Models.WebSite
{
    public class CaseReviewResultViewModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public bool right { get; set; }
    }
}
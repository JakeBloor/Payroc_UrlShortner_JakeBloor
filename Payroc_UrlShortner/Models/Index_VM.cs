using System.ComponentModel.DataAnnotations;

namespace Payroc_UrlShortner.Models
{
    public class Index_VM
    {
        [Required]
        public string URL { get; set; }
    }
}

using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroc_UrlShortner.Data
{
    public class Url
    {
        [Key]
        [MaxLength(10)]
        [StringLength(10)]
        public int Id { get; set; }
        public string URL { get; set; } = String.Empty;
        public string UrlChunk => WebEncoders.Base64UrlEncode(BitConverter.GetBytes(Id));

    }
}
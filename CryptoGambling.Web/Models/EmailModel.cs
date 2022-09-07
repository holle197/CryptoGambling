using System.ComponentModel.DataAnnotations;

namespace CryptoGambling.Web.Models
{
    public class EmailModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; } = "";
    }
}

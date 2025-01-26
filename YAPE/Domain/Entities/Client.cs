using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Client
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(50, ErrorMessage = "LastName cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "CellPhoneNumber is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "CellPhoneNumber must be 10 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "CellPhoneNumber must contain only numeric digits.")]
        public string CellPhoneNumber { get; set; }

        [Required(ErrorMessage = "DocumentType is required")]
        [StringLength(20, ErrorMessage = "DocumentType cannot exceed 20 characters.")]
        public string DocumentType { get; set; }

        [Required(ErrorMessage = "DocumentNumber is required")]
        [StringLength(10, ErrorMessage = "DocumentNumber cannot exceed 10 characters.")]
        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "ReasonOfUse is required")]
        public string ReasonOfUse { get; set; }
    }
}

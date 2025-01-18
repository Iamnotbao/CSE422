using System.ComponentModel.DataAnnotations;

namespace DeviceCategoryManagement.Models
{
    public class Device
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        [RegularExpression(@"^[A-Z0-9\-]+$", ErrorMessage = "Code must contain only uppercase letters, numbers, or hyphens.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("^(In Use|Inactive|Under Maintanance)$", ErrorMessage = "Status must be 'Active' or 'Inactive'.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Date of entry is required.")]
        public DateTime DateOfEntry { get; set; }

        public Device()
        {
        }

        public Device(int id, string name, string code, string category, string status, DateTime dateOfEntry)
        {
            Id = id;
            Name = name;
            Code = code;
            Category = category;
            Status = status;
            DateOfEntry = dateOfEntry;
        }
    }
}

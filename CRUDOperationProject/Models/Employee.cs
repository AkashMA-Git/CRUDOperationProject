using System.ComponentModel.DataAnnotations;

namespace CRUDOperationProject.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public string DateOfBirth { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
        public string Description {  get; set; }
        
        [Required(ErrorMessage = "Department is required")]

        public string Department { get; set; }

    }
    public enum Gender
    {
        Male,
        Female,
        Other
    }
}

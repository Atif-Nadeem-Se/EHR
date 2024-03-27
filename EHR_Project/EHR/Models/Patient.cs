using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHR.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("First Name")]

        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Last Name")]

        public string LastName { get; set; }
        [Required]
        [MaxLength(1)]
        public string Gender { get; set; }
        [DisplayName("Date of Birth")]

        public DateTime DateOfBirth { get; set; } = DateTime.Now.AddYears(-18);

    }
}

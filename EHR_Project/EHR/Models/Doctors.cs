using System.ComponentModel.DataAnnotations;

namespace EHR.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(50)]
        public string? Speciality { get; set; }
        public DateTime? JoiningDate { get; set; }
        [MaxLength(50)]
        public string? Qualification {  get; set; }
        [MaxLength(50)]
        public string? Address { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHR.Models
{
    public class Diagnosis
    {
        public int Id { get; set; }
        [ForeignKey("PatientId")]
        [DisplayName("Patient")]

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        [ForeignKey("DiseaseId")]
        [DisplayName("Disease")]
        public int DiseaseId { get; set; }  
        public Disease Disease { get; set; }
        [DisplayName("Visit Date")]

        public DateTime VisitDate { get; set; } = DateTime.Now;
    }
}

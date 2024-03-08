using System.ComponentModel.DataAnnotations;

namespace Assingment.Models
{
    public class Appointment
    {


        public int Id { get; set; }

        [Display(Name = "Patient ID")]
        public int PatientId { get; set; }

        [Display(Name = "Appointment Date")]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        [Display(Name = "Appointment Time")]
        [DataType(DataType.Time)]
        public TimeSpan AppointmentTime { get; set; }

        public string Description { get; set; }
    }
}

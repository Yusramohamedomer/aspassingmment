using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Assingment.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int Age { get; set; }

        // Add other properties as needed
    }
}

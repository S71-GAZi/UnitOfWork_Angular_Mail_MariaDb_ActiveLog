using System.ComponentModel.DataAnnotations;

namespace Api.Core.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required,StringLength(40)]
        public string Name { get; set; }
        [Required, StringLength(15)]

        public string Phone { get; set; }
        [Required, StringLength(40)]

        public string Email { get; set; }
        [Required, StringLength(40)]

        public string Address { get; set; }
    }
}

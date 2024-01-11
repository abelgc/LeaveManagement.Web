using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Web.Models
{
    public class LeaveTypeVM
    {
        public int Id { get; set; }

        [Display(Name = "Leave Type Name")]
        [Required]
        public required string Name { get; set; }

        [Display(Name = "Number Of Days by deafault")]
        [Required]
        [Range(1, 23, ErrorMessage = "Please Enter A Valid Number between range 1 to 23")]
        public required int DefaultDays { get; set; }
    }
}

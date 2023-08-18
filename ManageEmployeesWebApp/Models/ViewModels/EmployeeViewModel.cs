using System.ComponentModel.DataAnnotations;

namespace ManageEmployeesWebApp.Models.ViewModels {
    public class EmployeeViewModel {
        public string? Id { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "DateofBirth is required")]
        public DateTime DateofBirth { get; set; }
    }
}

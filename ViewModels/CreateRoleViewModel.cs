using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Role Name is required")]
        public string RoleName { get; set; }
    }
}
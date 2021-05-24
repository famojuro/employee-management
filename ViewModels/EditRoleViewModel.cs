using System.Collections.Generic;

namespace EmployeeManagement.ViewModels
{
    public class EditRoleViewModel : CreateRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }

        public List<string> Users { get; set; }
    }
}
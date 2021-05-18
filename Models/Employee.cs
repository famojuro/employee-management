using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    { 
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name should be less than 50 characters")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", 
            ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }
        [Required]
        public Department? Department { get; set; }
        public string PhotoPath { get; set; }
        
        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;
            var employee = (Employee) o;
            return (Id == employee.Id) && (Name == employee.Name) && (Email == employee.Email) &&
                   (Department == employee.Department);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode() ^ Email.GetHashCode() ^ Department.GetHashCode();
        }
    }
}
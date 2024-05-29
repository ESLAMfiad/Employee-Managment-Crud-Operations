using demo.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "name is required")]
        [MaxLength(50, ErrorMessage = "max length is 50 chars")]
        [MinLength(5, ErrorMessage = "min length is 5 chars")]
        public string Name { get; set; }
        [Range(22, 35, ErrorMessage = "age must be in range from 22 to 35")]
        public int? Age { get; set; }
        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$", ErrorMessage =
            "address must be like 123-street-city-country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; } //FK
        //FK Optional = on delete: restrict y3ne mynf3sh yms7 depart feha employ
        //fk required = on delete cascade 
        [InverseProperty("Employees")]
        public Department Department { get; set; }
    }
}

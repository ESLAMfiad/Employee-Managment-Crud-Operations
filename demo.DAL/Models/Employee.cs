using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Name { get; set; }
        public int? Age { get; set; }
        
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string ImageName { get; set; }
        [ForeignKey("Department")]

        public int? DepartmentId { get; set; } //FK
        //FK Optional = on delete: restrict y3ne mynf3sh yms7 depart feha employ
        //fk required = on delete cascade 
        [InverseProperty("Employees")]
        public Department Department { get; set; }

    }
}

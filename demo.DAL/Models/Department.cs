using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.DAL.Models
{
    public class Department
    {
        public int Id { get; set; } //pk
        [Required(ErrorMessage = "name required")]
        [MaxLength(50)]
        public string Name { get; set; } //net5.0 allows null unlike net6.0 is required
        [Required(ErrorMessage = "code required")]

        public string Code { get; set; }
        public DateTime DateOfCreation { get; set; }
        [InverseProperty("Department")]

        public ICollection<Employee>  Employees { get; set; } = new HashSet<Employee>(); //navigational property[many]
    }
}

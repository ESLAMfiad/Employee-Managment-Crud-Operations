using demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IQueryable<Employee> GetEmployeesByAddress(string address);  //mynf3sh IQueryable tbqa async "task" 3shan mfesh 7aga database tnf3
        IQueryable<Employee> GetEmployeesByName(string SearchValue); 
       
    }
}

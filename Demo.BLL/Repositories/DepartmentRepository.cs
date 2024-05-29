using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.Interfaces;
using demo.DAL.Models;
using demo.DAL.Context;

namespace Demo.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(MvcAppDbContext dbContext):base(dbContext) //hyinjecto mra wa7da ll generic repository as object
        {
                
        }
        
    }
}

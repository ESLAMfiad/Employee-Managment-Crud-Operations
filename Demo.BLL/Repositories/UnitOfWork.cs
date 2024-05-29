using demo.DAL.Context;
using Demo.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MvcAppDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        public UnitOfWork(MvcAppDbContext dbContext) //ask clr for object from db context "qdmt l injection w5leto hna"
        {
            EmployeeRepository = new EmployeeRepository(dbContext); //bqet b pass parameter 3ady fel classes bta3thom dlwqty
            DepartmentRepository = new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
           _dbContext.Dispose();
        }
    }
}

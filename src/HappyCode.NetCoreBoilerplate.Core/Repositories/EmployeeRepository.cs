using System.Threading;
using System.Threading.Tasks;
using HappyCode.NetCoreBoilerplate.Core.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HappyCode.NetCoreBoilerplate.Core.Dtos;
using HappyCode.NetCoreBoilerplate.Core.Extensions;

namespace HappyCode.NetCoreBoilerplate.Core.Repositories
{
    public interface IEmployeeRepository
    {
        Task<EmployeeDto> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<EmployeeDetailsDto> GetByIdWithDetailsAsync(object id, CancellationToken cancellationToken);
        Task<EmployeeDto> GetOldestAsync(CancellationToken cancellationToken);
    }

    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeesContext dbContext) : base(dbContext)
        {

        }

        public async Task<EmployeeDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var emp = await DbContext.Employees
                .SingleOrDefaultAsync(x => x.EmpNo == id);
            if (emp == null)
            {
                return null;
            }

            return emp.MapToDto();
        }

        public async Task<EmployeeDetailsDto> GetByIdWithDetailsAsync(object id, CancellationToken cancellationToken)
        {
            var emp = await DbContext.Employees
                .Include(x => x.Department)
                .SingleOrDefaultAsync(x => x.EmpNo == (int)id, cancellationToken);
            if (emp == null)
            {
                return null;
            }

            return new EmployeeDetailsDto
            {
                Id = emp.EmpNo,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                BirthDate = emp.BirthDate,
                Gender = emp.Gender,
                Department = new DepartmentDto
                {
                    Id = emp.Department.DeptNo,
                    Name = emp.Department.DeptName,
                }
            };
        }

        public async Task<EmployeeDto> GetOldestAsync(CancellationToken cancellationToken)
        {
            var emp = await DbContext.Employees
                .OrderBy(x => x.BirthDate)
                .FirstOrDefaultAsync(cancellationToken);
            if (emp == null)
            {
                return null;
            }

            return emp.MapToDto();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetCoreWebApiLearning.Data;
using DotNetCoreWebApiLearning.Entitys;

namespace DotNetCoreWebApiLearning.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataDbContext _context;

        public EmployeeRepository(DataDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            var items = await _context.Employees.Where(x => x.CompanyId == companyId).ToListAsync();
            return items;
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            if (employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }

            return await _context.Employees.Where(x => x.CompanyId == companyId && x.Id == employeeId).FirstOrDefaultAsync();
        }

        public void AddEmployee(Guid companyId, Employee employee)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            employee.CompanyId = companyId;
            _context.Employees.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            // _context.Entry(employee).State = EntityState.Modified;
        }

        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebApiLearning.Entitys;

namespace DotNetCoreWebApiLearning.Services
{
    public interface IEmployeeRepository : ISave
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId);
        void AddEmployee(Guid companyId, Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}

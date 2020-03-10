using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebApiLearning.Entitys;

namespace DotNetCoreWebApiLearning.Services
{
    public interface ICompanyRepository : ISave
    {
        Task<IEnumerable<Company>> GetCompanysAsync(IEnumerable<Guid> companyIds);
        Task<Company> GetCompanyAsync(Guid companyId);
        Task<bool> CompanyExistsAsync(Guid companyId);
        void AddCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(Company company);
    }
}

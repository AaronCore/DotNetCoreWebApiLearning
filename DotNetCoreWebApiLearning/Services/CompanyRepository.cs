using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetCoreWebApiLearning.Data;
using DotNetCoreWebApiLearning.Entitys;

namespace DotNetCoreWebApiLearning.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataDbContext _context;

        public CompanyRepository(DataDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Company>> GetCompanysAsync(IEnumerable<Guid> companyIds)
        {
            if (companyIds == null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }

            return await _context.Companies.Where(x => companyIds.Contains(x.Id)).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Company> GetCompanyAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            return await _context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
        }

        public async Task<bool> CompanyExistsAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            return await _context.Companies.AnyAsync(x => x.Id == companyId);
        }

        public void AddCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            company.Id = Guid.NewGuid();

            if (company.Employees != null)
            {
                foreach (var employee in company.Employees)
                {
                    employee.Id = Guid.NewGuid();
                }
            }

            _context.Companies.Add(company);
        }

        public void UpdateCompany(Company company)
        {
            // _context.Entry(company).State = EntityState.Modified;
        }

        public void DeleteCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            _context.Companies.Remove(company);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

    }
}

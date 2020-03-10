using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApiLearning.Services
{
    public interface ISave
    {
        Task<bool> SaveAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApiLearning.Entitys
{
    /// <summary>
    /// 公司实体
    /// </summary>
    public class Company
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 所在国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 所在行业
        /// </summary>
        public string Industry { get; set; }
        /// <summary>
        /// 公司产品
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// 公司简介
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// 注销日期
        /// </summary>
        public DateTime? BankruptTime { get; set; }

        /// <summary>
        /// 公司员工
        /// </summary>
        public ICollection<Employee> Employees { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApiLearning.Entitys
{
    /// <summary>
    /// 员工实体
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 公司Key
        /// </summary>
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; }
        /// <summary>
        /// 名
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// 姓氏
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// 所属公司
        /// </summary>
        public Company Company { get; set; }
    }
}

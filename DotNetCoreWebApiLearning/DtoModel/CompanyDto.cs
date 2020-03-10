using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApiLearning.DtoModel
{
    public class CompanyDto
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
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
    }
}

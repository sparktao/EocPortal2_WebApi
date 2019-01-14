using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class OrgEmployeeDTO
    {
    }

    public class CreateOrgEmployeeDTO
    {

        public long employee_Id;
        /// 联系人姓名
        public string employee_Name;
        /// 性别
        public string gender;
        /// 出生日期
        public DateTime birthday;
        /// 联系电话
        public string contact_Phone;
        /// 电子邮件
        public string email;
        /// 有效标志
        public bool isvalid;
}

}

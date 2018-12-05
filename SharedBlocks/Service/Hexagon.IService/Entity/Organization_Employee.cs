//=====================================================================================
// All Rights Reserved , Copyright @ Hexagon 2017
// Software Developers @ Hexagon 2017
//=====================================================================================

using Hexagon.Data.Attributes;
using System;
using System.ComponentModel;

namespace Hexagon.Entity
{
    /// <summary>
    /// 通讯录管理
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.05.06 15:45</date>
    /// </author>
    /// </summary>
    [Description("通讯录管理")]
    [PrimaryKey("EMPLOYEE_ID")]
    public class Organization_Employee : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 联系人ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("联系人ID")]
        public double Employee_Id { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("联系人姓名")]
        public string Employee_Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        [DisplayName("性别")]
        public string Gender { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("出生日期")]
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("证件类型")]
        public string Certificate_Type { get; set; }
        /// <summary>
        /// 证件代码
        /// </summary>
        /// <returns></returns>
        [DisplayName("证件代码")]
        public string Certificate_Code { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        /// <returns></returns>
        [DisplayName("联系电话")]
        public string Contact_Phone { get; set; }
        /// <summary>
        /// 办公电话1
        /// </summary>
        /// <returns></returns>
        [DisplayName("办公电话1")]
        public string Office_Telephone1 { get; set; }
        /// <summary>
        /// 办公电话2
        /// </summary>
        /// <returns></returns>
        [DisplayName("办公电话2")]
        public string Office_Telephone2 { get; set; }
        /// <summary>
        /// 办公传真
        /// </summary>
        /// <returns></returns>
        [DisplayName("办公传真")]
        public string Office_Fax { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        /// <returns></returns>
        [DisplayName("电子邮件")]
        public string Email{ get; set; }
        /// <summary>
        /// 家庭电话
        /// </summary>
        /// <returns></returns>
        [DisplayName("家庭电话")]
        public string Home_Telephone { get; set; }

        /// <summary>
        /// 家庭地址
        /// </summary>
        /// <returns></returns>
        [DisplayName("家庭地址")]
        public string Home_Address { get; set; }
        /// <summary>
        /// 职务描述
        /// </summary>
        /// <returns></returns>
        [DisplayName("职务描述")]
        public string Duty_Desc { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("单位ID")]
        public string Organization_Id { get; set; }
        /// <summary>
        /// 办公室房间号
        /// </summary>
        /// <returns></returns>
        [DisplayName("办公室房间号")]
        public string Room_Num { get; set; }
       
        /// <summary>
        /// 司机电话
        /// </summary>
        /// <returns></returns>
        [DisplayName("司机电话")]
        public string Driver_Phone { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string Note { get; set; }
        /// <summary>
        /// 秘书电话
        /// </summary>
        /// <returns></returns>
        [DisplayName("秘书电话")]
        public string Secretary_Phone { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        [DisplayName("有效标志")]
        public int? Isvalid { get; set; }
        //public int? Online { get; set; }
        
        /// <summary>
        /// UC账号
        /// </summary>
        /// <returns></returns>
        [DisplayName("UC账号")]
        public string ESPACE_ACCOUNT { get; set; }
        /// <summary>
        /// 服务号码
        /// </summary>
        /// <returns></returns>
        [DisplayName("服务号码")]
        public string SERVICE_NUMBER { get; set; }
        /// <summary>
        /// UC启用标志
        /// </summary>
        /// <returns></returns>
        [DisplayName("UC启用标志")]
        public int? Uc_Enable { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建时间")]
        public DateTime? Created_Date { get; set; }
       
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改时间")]
        public DateTime? Modified_Date { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        /// <returns></returns>
        [DisplayName("头像")]
        public string HeadIcon { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            //this.UserId = CommonHelper.GetGuid;
            this.Created_Date = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            //this.UserId = KeyValue;
            this.Modified_Date = DateTime.Now;
        }
        #endregion
    }
}
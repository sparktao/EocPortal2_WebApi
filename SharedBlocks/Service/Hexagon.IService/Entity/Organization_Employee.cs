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
    /// ͨѶ¼����
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.05.06 15:45</date>
    /// </author>
    /// </summary>
    [Description("ͨѶ¼����")]
    [PrimaryKey("EMPLOYEE_ID")]
    public class Organization_Employee : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ��ϵ��ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ϵ��ID")]
        public double Employee_Id { get; set; }
        /// <summary>
        /// ��ϵ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ϵ������")]
        public string Employee_Name { get; set; }
        /// <summary>
        /// �Ա�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Ա�")]
        public string Gender { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// ֤������
        /// </summary>
        /// <returns></returns>
        [DisplayName("֤������")]
        public string Certificate_Type { get; set; }
        /// <summary>
        /// ֤������
        /// </summary>
        /// <returns></returns>
        [DisplayName("֤������")]
        public string Certificate_Code { get; set; }
        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ϵ�绰")]
        public string Contact_Phone { get; set; }
        /// <summary>
        /// �칫�绰1
        /// </summary>
        /// <returns></returns>
        [DisplayName("�칫�绰1")]
        public string Office_Telephone1 { get; set; }
        /// <summary>
        /// �칫�绰2
        /// </summary>
        /// <returns></returns>
        [DisplayName("�칫�绰2")]
        public string Office_Telephone2 { get; set; }
        /// <summary>
        /// �칫����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�칫����")]
        public string Office_Fax { get; set; }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����ʼ�")]
        public string Email{ get; set; }
        /// <summary>
        /// ��ͥ�绰
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ͥ�绰")]
        public string Home_Telephone { get; set; }

        /// <summary>
        /// ��ͥ��ַ
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ͥ��ַ")]
        public string Home_Address { get; set; }
        /// <summary>
        /// ְ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("ְ������")]
        public string Duty_Desc { get; set; }
        /// <summary>
        /// ��λID
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λID")]
        public string Organization_Id { get; set; }
        /// <summary>
        /// �칫�ҷ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�칫�ҷ����")]
        public string Room_Num { get; set; }
       
        /// <summary>
        /// ˾���绰
        /// </summary>
        /// <returns></returns>
        [DisplayName("˾���绰")]
        public string Driver_Phone { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ע")]
        public string Note { get; set; }
        /// <summary>
        /// ����绰
        /// </summary>
        /// <returns></returns>
        [DisplayName("����绰")]
        public string Secretary_Phone { get; set; }

        /// <summary>
        /// ��Ч��־
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ч��־")]
        public int? Isvalid { get; set; }
        //public int? Online { get; set; }
        
        /// <summary>
        /// UC�˺�
        /// </summary>
        /// <returns></returns>
        [DisplayName("UC�˺�")]
        public string ESPACE_ACCOUNT { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        [DisplayName("�������")]
        public string SERVICE_NUMBER { get; set; }
        /// <summary>
        /// UC���ñ�־
        /// </summary>
        /// <returns></returns>
        [DisplayName("UC���ñ�־")]
        public int? Uc_Enable { get; set; }
        
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ʱ��")]
        public DateTime? Created_Date { get; set; }
       
        /// <summary>
        /// �޸�ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�ʱ��")]
        public DateTime? Modified_Date { get; set; }

        /// <summary>
        /// ͷ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("ͷ��")]
        public string HeadIcon { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            //this.UserId = CommonHelper.GetGuid;
            this.Created_Date = DateTime.Now;
        }
        /// <summary>
        /// �༭����
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
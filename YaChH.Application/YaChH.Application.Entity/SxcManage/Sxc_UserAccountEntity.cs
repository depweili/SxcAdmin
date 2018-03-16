using Newtonsoft.Json;
using System;
using YaChH.Application.Code;

namespace YaChH.Application.Entity.SxcManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-03-16 15:59
    /// 描 述：Sxc_UserAccount
    /// </summary>
    public class Sxc_UserAccountEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public int? ID { get; set; }
        /// <summary>
        /// AccountID
        /// </summary>
        /// <returns></returns>
        public Guid AccountID { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        /// <returns></returns>
        public decimal Balance { get; set; }
        /// <summary>
        /// LockBalance
        /// </summary>
        /// <returns></returns>
        public decimal LockBalance { get; set; }
        /// <summary>
        /// Cash
        /// </summary>
        /// <returns></returns>
        public decimal Cash { get; set; }
        /// <summary>
        /// Expense
        /// </summary>
        /// <returns></returns>
        public decimal Expense { get; set; }
        /// <summary>
        /// BankCard
        /// </summary>
        /// <returns></returns>
        public string BankCard { get; set; }
        /// <summary>
        /// IsValid
        /// </summary>
        /// <returns></returns>
        public bool? IsValid { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// IsVerified
        /// </summary>
        /// <returns></returns>
        public bool? IsVerified { get; set; }
        /// <summary>
        /// BankName
        /// </summary>
        /// <returns></returns>
        public string BankName { get; set; }
        /// <summary>
        /// BranchBankName
        /// </summary>
        /// <returns></returns>
        public string BranchBankName { get; set; }
        /// <summary>
        /// MobilePhone
        /// </summary>
        /// <returns></returns>
        public string MobilePhone { get; set; }
        /// <summary>
        /// PassWord
        /// </summary>
        /// <returns></returns>
        public string PassWord { get; set; }

        [JsonIgnore]
        public virtual Sxc_UserEntity User { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            //this.ID = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ID = int.Parse(keyValue);
                                            }
        #endregion
    }
}
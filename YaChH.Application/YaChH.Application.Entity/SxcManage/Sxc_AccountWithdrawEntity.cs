using System;
using YaChH.Application.Code;

namespace YaChH.Application.Entity.SxcManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-03-05 15:17
    /// 描 述：Sxc_AccountWithdraw
    /// </summary>
    public class Sxc_AccountWithdrawEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public int ID { get; set; }
        /// <summary>
        /// UserAccountID
        /// </summary>
        /// <returns></returns>
        public int UserAccountID { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        /// <returns></returns>
        public decimal Amount { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// State
        /// </summary>
        /// <returns></returns>
        public int State { get; set; }
        /// <summary>
        /// CompleteTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CompleteTime { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        /// <returns></returns>
        public string Memo { get; set; }
        /// <summary>
        /// AccountRecordID
        /// </summary>
        /// <returns></returns>
        public int? AccountRecordID { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// BankCard
        /// </summary>
        /// <returns></returns>
        public string BankCard { get; set; }
        /// <summary>
        /// BankName
        /// </summary>
        /// <returns></returns>
        public string BankName { get; set; }

        public string BranchBankName { get; set; }
        public string MobilePhone { get; set; }
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
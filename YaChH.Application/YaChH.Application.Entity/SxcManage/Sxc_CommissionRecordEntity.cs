using System;
using System.ComponentModel.DataAnnotations.Schema;
using YaChH.Application.Code;

namespace YaChH.Application.Entity.SxcManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-11-27 02:06
    /// 描 述：Sxc_UserPayment
    /// </summary>
    public class Sxc_CommissionRecordEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public int ID { get; set; }
        /// <summary>
        /// 佣金
        /// </summary>
        /// <returns></returns>
        [Column("COMMISSION")]
        public decimal? Commission { get; set; }
        /// <summary>
        /// UserPaymentID
        /// </summary>
        /// <returns></returns>
        [Column("USERPAYMENTID")]
        public int? UserPaymentID { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        /// <returns></returns>
        [Column("AGENTID")]
        public int? AgentID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// <returns></returns>
        [Column("STATE")]
        public int? State { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Column("CREATETIME")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 确认时间
        /// </summary>
        /// <returns></returns>
        [Column("CHECKTIME")]
        public DateTime? CheckTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("MEMO")]
        public string Memo { get; set; }
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
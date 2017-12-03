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
    public class Sxc_UserPaymentEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public int ID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        /// <returns></returns>
        [Column("USERID")]
        public int UserID { get; set; }

        public virtual Sxc_UserEntity User { get; set; }

        [NotMapped]
        public string UserName { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        /// <returns></returns>
        [Column("PAYITEMID")]
        public int? PayItemID { get; set; }
        /// <summary>
        /// PayUID
        /// </summary>
        /// <returns></returns>
        [Column("PAYUID")]
        public Guid PayUID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        /// <returns></returns>
        [Column("PAYSN")]
        public string PaySN { get; set; }
        /// <summary>
        /// 是否分配
        /// </summary>
        /// <returns></returns>
        [Column("ISDISTR")]
        public bool IsDistr { get; set; }
        /// <summary>
        /// 分配类型
        /// </summary>
        /// <returns></returns>
        [Column("DISTRTYPE")]
        public int? DistrType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// <returns></returns>
        [Column("STATE")]
        public int? State { get; set; }
        /// <summary>
        /// 缴纳金额
        /// </summary>
        /// <returns></returns>
        [Column("AMOUNT")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 分配金额
        /// </summary>
        /// <returns></returns>
        [Column("DISTRAMOUNT")]
        public decimal DistrAmount { get; set; }
        /// <summary>
        /// 佣金
        /// </summary>
        /// <returns></returns>
        [Column("COMMISSION")]
        public decimal Commission { get; set; }
        /// <summary>
        /// 最终金额
        /// </summary>
        /// <returns></returns>
        [Column("FINALAMOUNT")]
        public decimal FinalAmount { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Column("CREATETIME")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 分配时间
        /// </summary>
        /// <returns></returns>
        [Column("DISTRTIME")]
        public DateTime? DistrTime { get; set; }
        /// <summary>
        /// 结算时间
        /// </summary>
        /// <returns></returns>
        [Column("ACCOUNTTIME")]
        public DateTime? AccountTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("MEMO")]
        public string Memo { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        /// <returns></returns>
        [Column("OPERATORID")]
        public string OperatorID { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        /// <returns></returns>
        [Column("OPERATORNAME")]
        public string OperatorName { get; set; }
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
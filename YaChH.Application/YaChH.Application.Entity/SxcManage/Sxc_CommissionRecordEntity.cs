﻿using System;
using YaChH.Application.Code;

namespace YaChH.Application.Entity.SxcManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-11-29 23:12
    /// 描 述：Sxc_CommissionRecord
    /// </summary>
    public class Sxc_CommissionRecordEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public int ID { get; set; }
        /// <summary>
        /// Commission
        /// </summary>
        /// <returns></returns>
        public decimal? Commission { get; set; }
        /// <summary>
        /// UserPaymentID
        /// </summary>
        /// <returns></returns>
        public int UserPaymentID { get; set; }

        public virtual Sxc_UserPaymentEntity UserPayment { get; set; }
        /// <summary>
        /// AgentID
        /// </summary>
        /// <returns></returns>
        public int AgentID { get; set; }

        public virtual Sxc_AgentEntity Agent { get; set; }
        /// <summary>
        /// State
        /// </summary>
        /// <returns></returns>
        public int? State { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// CheckTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CheckTime { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        /// <returns></returns>
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
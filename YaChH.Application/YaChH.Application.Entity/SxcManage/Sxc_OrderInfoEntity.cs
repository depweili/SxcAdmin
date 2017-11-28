using System;
using YaChH.Application.Code;

namespace YaChH.Application.Entity.SxcManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-10-29 21:37
    /// 描 述：Sxc_OrderInfo
    /// </summary>
    public class Sxc_OrderInfoEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public int ID { get; set; }
        /// <summary>
        /// OrderUID
        /// </summary>
        /// <returns></returns>
        public Guid OrderUID { get; set; }
        /// <summary>
        /// OrderSn
        /// </summary>
        /// <returns></returns>
        public string OrderSn { get; set; }
        /// <summary>
        /// ShortContent
        /// </summary>
        /// <returns></returns>
        public string ShortContent { get; set; }
        /// <summary>
        /// OrderStatus
        /// </summary>
        /// <returns></returns>
        public int? OrderStatus { get; set; }
        /// <summary>
        /// StoreStatus
        /// </summary>
        /// <returns></returns>
        public int? StoreStatus { get; set; }
        /// <summary>
        /// PayStatus
        /// </summary>
        /// <returns></returns>
        public int? PayStatus { get; set; }
        /// <summary>
        /// Consignee
        /// </summary>
        /// <returns></returns>
        public string Consignee { get; set; }
        /// <summary>
        /// AddressRegion
        /// </summary>
        /// <returns></returns>
        public string AddressRegion { get; set; }
        /// <summary>
        /// AddressDetail
        /// </summary>
        /// <returns></returns>
        public string AddressDetail { get; set; }
        /// <summary>
        /// Zipcode
        /// </summary>
        /// <returns></returns>
        public string Zipcode { get; set; }
        /// <summary>
        /// Telephone
        /// </summary>
        /// <returns></returns>
        public string Telephone { get; set; }
        /// <summary>
        /// UserNote
        /// </summary>
        /// <returns></returns>
        public string UserNote { get; set; }
        /// <summary>
        /// TotalAmount
        /// </summary>
        /// <returns></returns>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// TotalPoints
        /// </summary>
        /// <returns></returns>
        public int? TotalPoints { get; set; }
        /// <summary>
        /// PayFee
        /// </summary>
        /// <returns></returns>
        public decimal? PayFee { get; set; }
        /// <summary>
        /// DeliverFee
        /// </summary>
        /// <returns></returns>
        public decimal? DeliverFee { get; set; }
        /// <summary>
        /// Tax
        /// </summary>
        /// <returns></returns>
        public decimal? Tax { get; set; }
        /// <summary>
        /// ToUserNote
        /// </summary>
        /// <returns></returns>
        public string ToUserNote { get; set; }
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
        /// UserIntegralID
        /// </summary>
        /// <returns></returns>
        public int UserIntegralID { get; set; }
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
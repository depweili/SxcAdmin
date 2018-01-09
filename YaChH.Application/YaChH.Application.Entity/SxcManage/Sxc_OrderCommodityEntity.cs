using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using YaChH.Application.Code;

namespace YaChH.Application.Entity.SxcManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-05-04 13:25
    /// 描 述：Sxc_OrderCommodity
    /// </summary>
    public class Sxc_OrderCommodityEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public int ID { get; set; }
        /// <summary>
        /// ShortContent
        /// </summary>
        /// <returns></returns>
        [Column("SHORTCONTENT")]
        public string ShortContent { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        /// <returns></returns>
        [Column("QUANTITY")]
        public int Quantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        /// <returns></returns>
        [Column("PRICE")]
        public decimal Price { get; set; }
        /// <summary>
        /// Points
        /// </summary>
        /// <returns></returns>
        [Column("POINTS")]
        public int Points { get; set; }
        /// <summary>
        /// TotalAmount
        /// </summary>
        /// <returns></returns>
        [Column("TOTALAMOUNT")]
        public decimal? TotalAmount { get; set; }
        /// <summary>
        /// TotalPoints
        /// </summary>
        /// <returns></returns>
        [Column("TOTALPOINTS")]
        public int TotalPoints { get; set; }
        /// <summary>
        /// IsReal
        /// </summary>
        /// <returns></returns>
        [Column("ISREAL")]
        public bool? IsReal { get; set; }
        /// <summary>
        /// CommodityAttrsStr
        /// </summary>
        /// <returns></returns>
        [Column("COMMODITYATTRSSTR")]
        public string CommodityAttrsStr { get; set; }
        /// <summary>
        /// OrderInfoID
        /// </summary>
        /// <returns></returns>
        [Column("ORDERINFOID")]
        public int OrderInfoID { get; set; }
        [JsonIgnore]
        public virtual Sxc_OrderInfoEntity OrderInfo { get; set; }
        /// <summary>
        /// CommodityID
        /// </summary>
        /// <returns></returns>
        [Column("COMMODITYID")]
        public int CommodityID { get; set; }

        [JsonIgnore]
        public virtual Sxc_CommodityEntity Commodity { get; set; }
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
using System;
using YaChH.Application.Code;

namespace YaChH.Application.Entity.SxcManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-11-16 21:11
    /// 描 述：Sxc_Commodity
    /// </summary>
    public class Sxc_CommodityEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public int ID { get; set; }
        /// <summary>
        /// CommodityUID
        /// </summary>
        /// <returns></returns>
        public Guid CommodityUID { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// Details
        /// </summary>
        /// <returns></returns>
        public string Details { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        /// <returns></returns>
        public decimal? Price { get; set; }
        /// <summary>
        /// Points
        /// </summary>
        /// <returns></returns>
        public int? Points { get; set; }
        /// <summary>
        /// Stock
        /// </summary>
        /// <returns></returns>
        public int? Stock { get; set; }
        /// <summary>
        /// Pic
        /// </summary>
        /// <returns></returns>
        public string Pic { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        /// <returns></returns>
        public string Memo { get; set; }
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
        /// CategoryID
        /// </summary>
        /// <returns></returns>
        public int? CategoryID { get; set; }
        /// <summary>
        /// IsReal
        /// </summary>
        /// <returns></returns>
        public bool? IsReal { get; set; }
        /// <summary>
        /// HasVideo
        /// </summary>
        /// <returns></returns>
        public bool? HasVideo { get; set; }
        /// <summary>
        /// ArticleID
        /// </summary>
        /// <returns></returns>
        public int? ArticleID { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            //this.ID = Guid.NewGuid().ToString();

            this.IsValid = true;
            this.CreateTime = DateTime.Now;
            this.CommodityUID = Guid.NewGuid();
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
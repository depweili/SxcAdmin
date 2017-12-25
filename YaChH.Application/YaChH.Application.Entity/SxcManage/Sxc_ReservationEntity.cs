using System;
using YaChH.Application.Code;

namespace YaChH.Application.Entity.SxcManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-12-25 09:17
    /// 描 述：Sxc_Reservation
    /// </summary>
    public class Sxc_ReservationEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public int? ID { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// MobilePhone
        /// </summary>
        /// <returns></returns>
        public string MobilePhone { get; set; }
        /// <summary>
        /// AreaInfo
        /// </summary>
        /// <returns></returns>
        public string AreaInfo { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// Purpose
        /// </summary>
        /// <returns></returns>
        public string Purpose { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        /// <returns></returns>
        public string Memo { get; set; }
        /// <summary>
        /// ReservedDate
        /// </summary>
        /// <returns></returns>
        public DateTime? ReservedDate { get; set; }
        /// <summary>
        /// State
        /// </summary>
        /// <returns></returns>
        public int? State { get; set; }
        /// <summary>
        /// ProcessDetail
        /// </summary>
        /// <returns></returns>
        public string ProcessDetail { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// UserID
        /// </summary>
        /// <returns></returns>
        public int? UserID { get; set; }
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
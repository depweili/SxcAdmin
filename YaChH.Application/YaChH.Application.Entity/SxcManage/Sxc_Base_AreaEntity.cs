using System;
using System.ComponentModel.DataAnnotations.Schema;
using YaChH.Application.Code;

namespace YaChH.Application.Entity.SxcManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-12-03 20:43
    /// 描 述：Sxc_Base_Area
    /// </summary>
    public class Sxc_Base_AreaEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public int ID { get; set; }
        /// <summary>
        /// PID
        /// </summary>
        /// <returns></returns>
        [ForeignKey("SupArea")]
        public int? PID { get; set; }
        public virtual Sxc_Base_AreaEntity SupArea { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// Level
        /// </summary>
        /// <returns></returns>
        public int? Level { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        /// <returns></returns>
        public int? Type { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }
        /// <summary>
        /// PCode
        /// </summary>
        /// <returns></returns>
        public string PCode { get; set; }
        /// <summary>
        /// Area
        /// </summary>
        /// <returns></returns>
        public string Area { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
           // this.ID = Guid.NewGuid().ToString();
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
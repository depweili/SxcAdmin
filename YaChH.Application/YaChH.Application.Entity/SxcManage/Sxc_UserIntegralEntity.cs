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
    /// 描 述：Sxc_UserIntegral
    /// </summary>
    public class Sxc_UserIntegralEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public int ID { get; set; }
        /// <summary>
        /// IntegralID
        /// </summary>
        /// <returns></returns>
        [Column("INTEGRALID")]
        public Guid IntegralID { get; set; }
        /// <summary>
        /// TotalPoints
        /// </summary>
        /// <returns></returns>
        [Column("TOTALPOINTS")]
        public int TotalPoints { get; set; }
        /// <summary>
        /// CurrentPoints
        /// </summary>
        /// <returns></returns>
        [Column("CURRENTPOINTS")]
        public int CurrentPoints { get; set; }
        /// <summary>
        /// TotalExpense
        /// </summary>
        /// <returns></returns>
        [Column("TOTALEXPENSE")]
        public int TotalExpense { get; set; }
        /// <summary>
        /// IsValid
        /// </summary>
        /// <returns></returns>
        [Column("ISVALID")]
        public bool? IsValid { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        [Column("CREATETIME")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// IntegralGradeID
        /// </summary>
        /// <returns></returns>
        [Column("INTEGRALGRADEID")]
        public int? IntegralGradeID { get; set; }


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
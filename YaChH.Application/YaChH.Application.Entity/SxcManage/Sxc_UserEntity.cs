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
    /// 描 述：Sxc_User
    /// </summary>
    public class Sxc_UserEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public int ID { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        /// <returns></returns>
        [Column("USERNAME")]
        public string UserName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        /// <returns></returns>
        [Column("PASSWORD")]
        public string Password { get; set; }
        /// <summary>
        /// AuthID
        /// </summary>
        /// <returns></returns>
        [Column("AUTHID")]
        public Guid AuthID { get; set; }
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
        /// LastActiveTime
        /// </summary>
        /// <returns></returns>
        [Column("LASTACTIVETIME")]
        public DateTime? LastActiveTime { get; set; }
        /// <summary>
        /// SystemAccount
        /// </summary>
        /// <returns></returns>
        [Column("SYSTEMACCOUNT")]
        public string SystemAccount { get; set; }


        public virtual Sxc_UserProfileEntity UserProfile { get; set; }

        public virtual Sxc_AgentEntity Agent { get; set; }

        public virtual Sxc_UserIntegralEntity UserIntegral { get; set; }

        public virtual Sxc_UserAccountEntity UserAccount { get; set; }

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
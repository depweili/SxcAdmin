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
    /// 日 期：2017-11-29 11:51
    /// 描 述：Sxc_UserProfile
    /// </summary>
    public class Sxc_UserProfileEntity : BaseEntity
    {
        private string _SystemAccount;
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public int ID { get; set; }
        /// <summary>
        /// NickName
        /// </summary>
        /// <returns></returns>
        public string NickName { get; set; }
        /// <summary>
        /// RealName
        /// </summary>
        /// <returns></returns>
        public string RealName { get; set; }
        /// <summary>
        /// Gender
        /// </summary>
        /// <returns></returns>
        public int? Gender { get; set; }
        /// <summary>
        /// Age
        /// </summary>
        /// <returns></returns>
        public int? Age { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        /// <returns></returns>
        public string Email { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// MobilePhone
        /// </summary>
        /// <returns></returns>
        public string MobilePhone { get; set; }
        /// <summary>
        /// IDCard
        /// </summary>
        /// <returns></returns>
        public string IDCard { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// IsVerified
        /// </summary>
        /// <returns></returns>
        public bool? IsVerified { get; set; }
        /// <summary>
        /// AvatarUrl
        /// </summary>
        /// <returns></returns>
        public string AvatarUrl { get; set; }
        
        [JsonIgnore]
        public virtual Sxc_UserEntity User { get; set; }

        [NotMapped]
        public virtual string SystemAccount { get; set; }

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
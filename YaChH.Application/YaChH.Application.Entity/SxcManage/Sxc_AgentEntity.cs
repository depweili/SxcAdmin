using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YaChH.Application.Code;

namespace YaChH.Application.Entity.SxcManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-11-07 11:21
    /// 描 述：Sxc_Agent
    /// </summary>
    public class Sxc_AgentEntity : BaseEntity
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
        public int? PID { get; set; }
        /// <summary>
        /// AgentID
        /// </summary>
        /// <returns></returns>
        public Guid AgentID { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        /// <returns></returns>
        public int Type { get; set; }
        /// <summary>
        /// Level
        /// </summary>
        /// <returns></returns>
        public int Level { get; set; }
        /// <summary>
        /// State
        /// </summary>
        /// <returns></returns>
        public int? State { get; set; }
        /// <summary>
        /// Commission
        /// </summary>
        /// <returns></returns>
        public decimal? Commission { get; set; }
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
        /// Area_ID
        /// </summary>
        /// <returns></returns>
        public int? Area_ID { get; set; }
        /// <summary>
        /// SupAgentBindTime
        /// </summary>
        /// <returns></returns>
        public DateTime? SupAgentBindTime { get; set; }

        [JsonIgnore]
        public virtual Sxc_AgentEntity ParentAgent { get; set; }

        [JsonIgnore]
        public virtual ICollection<Sxc_AgentEntity> ChildAgents { get; set; }

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
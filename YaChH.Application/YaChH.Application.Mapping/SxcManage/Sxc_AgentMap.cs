using YaChH.Application.Entity.SxcManage;
using System.Data.Entity.ModelConfiguration;

namespace YaChH.Application.Mapping.SxcManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-11-07 11:21
    /// 描 述：Sxc_Agent
    /// </summary>
    public class Sxc_AgentMap : EntityTypeConfiguration<Sxc_AgentEntity>
    {
        public Sxc_AgentMap()
        {
            #region 表、主键
            //表
            this.ToTable("Sxc_Agent");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系

            this.HasOptional(t => t.ParentAgent).WithMany(t => t.ChildAgents).HasForeignKey(t => t.PID);

            this.HasRequired(t => t.User).WithRequiredDependent(t => t.Agent);

            this.HasOptional(t => t.Area).WithMany().HasForeignKey(t=>t.Area_ID);

            #endregion
        }
    }
}

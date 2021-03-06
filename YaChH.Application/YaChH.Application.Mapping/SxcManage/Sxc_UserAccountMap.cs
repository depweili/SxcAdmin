﻿using YaChH.Application.Entity.SxcManage;
using System.Data.Entity.ModelConfiguration;

namespace YaChH.Application.Mapping.SxcManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-03-16 15:59
    /// 描 述：Sxc_UserAccount
    /// </summary>
    public class Sxc_UserAccountMap : EntityTypeConfiguration<Sxc_UserAccountEntity>
    {
        public Sxc_UserAccountMap()
        {
            #region 表、主键
            //表
            this.ToTable("Sxc_UserAccount");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            this.HasRequired(t => t.User).WithRequiredDependent(t => t.UserAccount);
            #endregion
        }
    }
}
